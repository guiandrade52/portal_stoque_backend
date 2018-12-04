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
    public class SWServiceInvoker
    {
        private String domain;
        private String user;
        private String pass;
        private bool debug;
        private bool silentMode;
        private string session;
        public string nuocor;
        public string nuvisita;
        public string pathcreatefile;

        public SWServiceInvoker(String domain, String user, String pass)
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

        public XmlDocument call(String serviceName, String module, String body)
        {
            String jsessionid = doLogin();

            HttpWebRequest conn = openConn(serviceName, module, jsessionid);

            XmlDocument docResp = callService(conn, body, serviceName);

            doLogout(jsessionid);

            return docResp;
        }

        private String doLogin()
        {
            HttpWebRequest conn = openConn("MobileLoginSP.login", "mge", null);

            StringBuilder bodyBuf = new StringBuilder();

            bodyBuf.Append("<NOMUSU>").Append(user).Append("</NOMUSU>");
            bodyBuf.Append("<INTERNO>").Append(pass).Append("</INTERNO>");

            XmlDocument docResp = callService(conn, bodyBuf.ToString(), "MobileLoginSP.login");

            //XmlNode jsessionNode = (XmlNode)xpath(docResp, "//jsessionid", XPathConstants.NODE);
            string jsessionNode = ((docResp.SelectNodes("serviceResponse").Item(0)).ChildNodes.Item(0)).FirstChild.InnerText;

            return jsessionNode.Trim();
        }

        private void doLogout(String jsessionid)
        {
            try
            {
                HttpWebRequest conn = openConn("MobileLoginSP.logout", "mge", jsessionid);

                callService(conn, null, "MobileLoginSP.logout");
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
                throw new Exception(msg);

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

        private XmlDocument callService(HttpWebRequest conn, String body, String serviceName)
        {
            byte[] bytes;
            Stream requestStream = null;

            try
            {
                String requestBody = buildRequestBody(body, serviceName);
                bytes = System.Text.Encoding.ASCII.GetBytes(requestBody);
                conn.Accept = "application/pdf";
                conn.ContentLength = bytes.Length;
                requestStream = conn.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                //if (!requestBody.Contains("report.bpms"))
                //{
                //    HttpWebResponse response;

                //    response = (HttpWebResponse)conn.GetResponse();
                //}


                XmlDocument doc = new XmlDocument();
                XmlNodeList nodes = null;
                HttpWebResponse response;

                response = (HttpWebResponse)conn.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();


                    if (requestBody.Contains("report.bpms"))
                    {
                        string visita = "" + Convert.ToString(this.nuvisita);
                        string path = "";

                        if (body.Contains("115"))
                            path = this.pathcreatefile + "CIT_" + this.nuocor + "" + ".pdf";
                        else
                            path = this.pathcreatefile + "RAT_" + this.nuocor + "_" + visita.Replace("/", "") + ".pdf";

                        using (Stream file = File.Create(path))
                        {
                            //Create a new node.
                            XmlElement elem = doc.CreateElement("name");
                            elem.InnerText = path;

                            doc.AppendChild(elem);

                            CopyStream(responseStream, file);
                        }
                    }
                    else
                    {
                        string responseXml = new StreamReader(responseStream).ReadToEnd();
                        doc.LoadXml(responseXml.Replace("ISO-8859-1", "utf-8"));
                    }

                }

                try
                {

                    nodes = doc.SelectNodes("serviceResponse");

                    if (!requestBody.Contains("report.bpms"))
                    {
                        if ((nodes == null || nodes.Count < 1))
                        {
                            var error = new Exception("XML de resposta não possui um elemento de resposta");
                            throw error;
                        }

                        checkResultStatus(nodes.Item(0));

                    }
                }
                catch (Exception e)
                {
                    Exception error = new Exception("Erro ao interpretar resposta do servidor: " + e);
                    throw error;
                }



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

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
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
                session = "JSESSIONID=" + sessionID;
            }

            if (serviceName == "report.bpms")
            {
                buf.Clear();
                buf.Append(Properties.Settings.Default.SankhyaURL + "/bhbpmsnkbpms/report.bpms?mgeSession=" + sessionID);
            }

            Uri u = new Uri(buf.ToString());


            var connection = (HttpWebRequest)WebRequest.Create(u);
            connection.Method = "POST";
            connection.ContentType = "text/xml";

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

    }
}