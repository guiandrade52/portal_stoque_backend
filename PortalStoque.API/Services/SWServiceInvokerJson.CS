using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace PortalStoque.API.Services
{
    public class SWServiceInvokerJson
    {
        private String domain;
        private String user;
        private String pass;
        private bool debug;
        private bool silentMode;
        public String sessionid;

        public SWServiceInvokerJson(String domain, String user, String pass)
        {
            this.domain = domain;
            this.user = user;
            this.pass = pass;
        }

        public void setSilentMode(bool silentMode)
        {
            this.silentMode = silentMode;
        }

        public void setDebugMode()
        {
            this.debug = true;
        }

        public string call(String serviceName, String module, String body)
        {
            String jsessionid = doLogin();

            if (body.Contains("mgeSession"))
                body = body.Replace("mgeSession", "mgeSession=" + jsessionid);

            HttpWebRequest conn = openConn(serviceName, module, jsessionid);

            string docResp = callService(conn, body, serviceName);

            doLogout(jsessionid);

            return docResp;
        }

        private string doLogin()
        {
            HttpWebRequest conn = openConn("MobileLoginSP.login", "mge", null);

            StringBuilder bodyBuf = new StringBuilder();

            bodyBuf.Append("<NOMUSU>").Append(user).Append("</NOMUSU>");
            bodyBuf.Append("<INTERNO>").Append(pass).Append("</INTERNO>");

            XmlDocument docResp = callServiceLogin(conn, bodyBuf.ToString(), "MobileLoginSP.login");

            //XmlNode jsessionNode = (XmlNode)xpath(docResp, "//jsessionid", XPathConstants.NODE);
            string jsessionNode = ((docResp.SelectNodes("serviceResponse").Item(0)).ChildNodes.Item(0)).FirstChild.InnerText;

            return jsessionNode.Trim();
        }

        private void doLogout(String jsessionid)
        {
            try
            {
                HttpWebRequest conn = openConn("MobileLoginSP.logout", "mge", jsessionid);

                callServiceLogin(conn, null, "MobileLoginSP.logout");
            }
            catch (Exception)
            {
                //e.printStackTrace(); // pode ser ignorado
            }
        }

        private void checkResultStatus(XmlNode sr)
        {
            XmlNode statusNode = sr.Attributes.GetNamedItem("status");

            String status = statusNode.Value;

            if (!"1".Equals(status) && !silentMode)
            {
                String msg = getChildNode("statusMessage", sr).Value;
                throw new Exception("Error");
            }
        }

        private XmlNode getChildNode(String name, XmlNode parent)
        {
            XmlNodeList l = parent.ChildNodes;

            for (int i = 0; i < l.Count; i++)
            {
                XmlNode n = l.Item(i);

                if (n.Name.ToLower().Equals(name.ToLower()))
                {
                    return n;
                }
            }

            return null;
        }

        //private String decodeB64(String s)
        //{
        //    return new String(Convert.FromBase64String(s));
        //}

        //private Object xpath(XmlDocument d, String query, QName type)
        //{
        //    XPathDocument xp = new XPathDocument()

        //    XPathExpression xpe = xp.compile(query);
        //    return xpe.evaluate(d, type);
        //}

        private void printNode(XmlNode n)
        {
            // System.out.println(n.toString());

            XmlNodeList l = n.ChildNodes;

            if (l != null && l.Count > 0)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    printNode(l.Item(i));
                }
            }
        }

        private string callService(HttpWebRequest conn, String body, String serviceName)
        {
            byte[] bytes;
            Stream requestStream = null;

            try
            {
                string requestBody = "";

                #region sampleJSON
                //requestBody = "{\"serviceName\":\"DatasetSP.loadRecords\",\"requestBody\":{\"entityName\":\"Bairro\",\"parallelLoader\":true,\"fields\":[\"CODBAI\",\"NOMEBAI\",\"CODREG\",\"Regiao.NOMEREG\",\"DESCRICAOCORREIO\",\"DTALTER\"]}}";
                //requestBody = "{\"serviceName\":\"DatasetSP.loadRecords\",\"requestBody\":{\"entityName\":\"STOOCO\",\"fields\":[\"NUOCORRENCIA\",\"CONTROLE\",\"DTPREV\",\"DTPREVTR\",\"Parceiro.NOMEPARC\",\"Produto.DESCRPROD\",\"SITUACAO\",\"CODPARCCON\"],\"criteria\":{\"expression\":\"((STOOCO.CODPARCCON = 59))\",\"parameters\":[]}}}";
                #endregion

                requestBody = body;

                bytes = System.Text.Encoding.UTF8.GetBytes(requestBody);

                conn.ContentLength = bytes.Length;

                requestStream = conn.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                HttpWebResponse response;

                response = (HttpWebResponse)conn.GetResponse();

                XmlDocument doc = new XmlDocument();
                XmlNodeList nodes = null;
                string responseJson = "";
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    responseJson = new StreamReader(responseStream).ReadToEnd();

                }

                try
                {

                    if (responseJson != null)
                        return responseJson;

                    if (nodes == null || nodes.Count < 1)
                    {
                        Exception error = new Exception("XML/JSON de resposta não possui um elemento de resposta");
                        throw error;
                    }
                }
                catch (Exception e)
                {
                    Exception error = new Exception(e.Message);
                    throw error;
                }

                checkResultStatus(nodes.Item(0));

                return responseJson;
            }
            finally
            {
                if (requestStream != null)
                {
                    try
                    {
                        requestStream.Close();
                    }
                    catch (Exception )
                    {
                    }
                }
            }
        }

        private XmlDocument callServiceLogin(HttpWebRequest conn, String body, String serviceName)
        {
            byte[] bytes;
            Stream requestStream = null;
            conn.ContentType = "text/xml";
            try
            {
                String requestBody = buildRequestBody(body, serviceName);
                bytes = System.Text.Encoding.ASCII.GetBytes(requestBody);
                conn.ContentLength = bytes.Length;
                requestStream = conn.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                HttpWebResponse response;

                response = (HttpWebResponse)conn.GetResponse();

                XmlDocument doc = new XmlDocument();
                XmlNodeList nodes = null;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseXml = new StreamReader(responseStream).ReadToEnd();
                    doc.LoadXml(responseXml.Replace("ISO-8859-1", "utf-8"));
                }

                try
                {

                    nodes = doc.SelectNodes("serviceResponse");

                    if (nodes == null || nodes.Count < 1)
                    {
                        Exception error = new Exception("XML de resposta não possui um elemento de resposta");
                        throw error;
                    }
                }
                catch (Exception)
                {
                    Exception error = new Exception("Erro ao interpretar resposta do servidor");
                    throw error;
                }

                checkResultStatus(nodes.Item(0));

                return doc;
            }
            finally
            {
                if (requestStream != null)
                {
                    try
                    {
                        requestStream.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        private String buildRequestBody(String body, String serviceName)
        {
            StringBuilder buf = new StringBuilder();

            buf.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\n");
            buf.Append("<serviceRequest serviceName=\"").Append(serviceName).Append("\">\n");
            buf.Append("<requestBody>\n");
            buf.Append(body == null ? "" : body);
            buf.Append("</requestBody>\n");
            buf.Append("</serviceRequest>");

            return buf.ToString();
        }

        private HttpWebRequest openConn(String serviceName, String module, String sessionID)
        {
            StringBuilder buf = new StringBuilder();

            buf.Append(domain).Append(domain.EndsWith("/") ? "" : "/").Append(module == null ? "mge" : module).Append("/service.sbr");


            buf.Append("?serviceName=").Append(serviceName);

            if (sessionID != null)
            {
                buf.Append("&mgeSession=").Append(sessionID);
                buf.Append("&outputType=json");
            }



            buf.Append("&preventTransform=false");

            Uri u = new Uri(buf.ToString());


            var connection = (HttpWebRequest)WebRequest.Create(u);
            connection.Method = "POST";
            connection.ContentType = "application/json;charset=ISO-8859-1";

            if (sessionID != null)
            {
                connection.Headers.Add(HttpRequestHeader.Cookie, "JSESSIONID=" + sessionID);
                //Cookie cookie = new Cookie() { Name = "JSESSIONID", Value = "JSESSIONID=" + sessionID, };
                //connection.CookieContainer = new CookieContainer();
                //connection.CookieContainer.Add(u,cookie);
            }
            connection.UserAgent = "SWServiceInvoker";

            return connection;
        }

        private static void printDocument(XmlDocument doc)
        {
            //          Transformer transformer = TransformerFactory.newInstance().newTransformer();
            //          transformer.setOutputProperty(OutputKeys.INDENT, "yes");
            //          StreamResult result = new StreamResult(new StringWriter());
            //DOMSource source = new DOMSource(doc);
            //      transformer.transform(source, result);
            //String xmlString = result.getWriter().toString();
            //      System.out.println("----inicio---");
            //      System.out.println(xmlString);
            //      System.out.println("----fim-----");
        }
    }

}