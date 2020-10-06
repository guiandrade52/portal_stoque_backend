using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PortalStoque.API.Models.OcorNews
{
    public class OcorNewsRepositorio : IOcorNewsRepositorio
    {
        public string GetJsonFormattedSave(Ocor Ocor)
        {
            string json = string.Format(@"{{
                                'serviceName':'ProcessoSP.iniciaProcesso',                                
                                 'requestBody':{{
                                'processo':'ocorrencia',           
                              'entityName':'STOOCO',
                              'fields':[
                                 'CONTROLE',
                                 'BHFTLSerie.CONTROLEFAB',
                                 'TIPOAT',
                                 'CODUSU',
                                 'Usuario.NOMEUSU',
                                 'SITUACAO',
                                 'NUORIGEM',
                                 'STOORI.DESCRICAO',
                                 'DTDESEJADA',
                                 'RECORRENTE',
                                 'NUOCORRENCIAREL',
                                 'STOOCO_AD001.APRESENTACAO',
                                 'POSSUIBKP',
                                 'UTILIZABKP',
                                 'CONTROLESOL',
                                 'BHFTLSerie_AD001.CONTROLEFAB',
                                 'DTFIMGARANTIA',
                                 'DTFIMGARANTIASTO',                                
                                 'CODPARC',
                                 'Parceiro.NOMEPARC',
                                 'CODPARCCON',
                                 'Parceiro_AD001.NOMEPARC',
                                 'CODCONTATO',
                                 'Contato.NOMECONTATO',
                                 'NUMCONTRATO',
                                 'NOMECLIENTE',
                                 'EMAIL',
                                 'TELEFONE',
                                 'CEP',
                                 'CODEND',
                                 'Endereco.NOMEEND',
                                 'NUMEND',
                                 'CODBAI',
                                 'Bairro.NOMEBAI',
                                 'CODCID',
                                 'Cidade.NOMECID',
                                 'COMPLEMENTO',
                                 'CLASSIFICACAO',
                                 'TIPO',
                                 'CODGRUPO',
                                 'BPMGrupoExecucao.DESCRICAO',
                                 'CODPROD',
                                 'Produto.DESCRPROD',
                                 'NUMOTIVO',
                                 'STOMOT.DESCRICAO',
                                 'CODPRODEXC',
                                 'Produto_AD001.DESCRPROD',
                                 'IMPACTO',
                                 'CRITICIDADE',
                                 'PRIORIDADE',
                                 'DESCRICAO',
                                 'NUOCORRENCIA',
                                 'ACTIVITIID',
                                 'PROCESSINSTANCEID',
                                 'MENORVENCIMENTO',
                                 'EXECUTIONID',
                                 'APRESENTACAO',
                                 'CODUSUACP',
                                 'NUSLA',
                                 'CabecalhoSLA.DESCRICAO',
                                 'NUSLATR',
                                 'CabecalhoSLA_AD001.DESCRICAO',
                                 'DHCHAMADA',
                                 'SEQUENCIA',                                 
                                 'CODCARGAHOR',
                                 'IdentificacaoCargaHoraria.DESCRCARGAHOR',
                                 'IDUSUPRTL',
                                 'USUPRTL.NOMEUSU',
                                 'EXECUTIONIDREL',
                                 'CHAMADOTER'
                              ],
                              'records':[
                                 {{  
                                    'values':{{  
                                       '0':'{0}',
                                       '2':'C',
                                       '3':'{17}',
                                       '5':'1',
                                       '6':'4',
                                       '18':'{1}',
                                       '20':'{2}',
                                       '22':'{3}',
                                       '24':'{4}',
                                       '26':'{5}',
                                       '27':'{6}',
                                       '28':'{7}',
                                       '29':'{8}',
                                       '31':'{9}',
                                       '32':'{10}',
                                       '34':'{11}',
                                       '36':'{12}',                                 
                                       '38':'{18}',
                                       '39':'{13}',
                                       '41':'{14}',
                                       '43':'{15}',
                                       '45':'{16}',
                                       '47':'U',
                                       '48':'{22}',
                                       '50':'text',
                                        [7]
                                       '66':'{19}',
                                       '68':'{20}',
                                       '69':'{21}'
                                    }}
                                  }}
                              ]                    
                           }}
                         }}",
           Ocor.Controle,
           Ocor.CodParc, //{1}
           Ocor.CodParcCon, //{2}
           Ocor.CodContato, //{3}
           Ocor.Contrato, //{4}
           Ocor.Email, //{5}
           Ocor.Telefone, //{6}
           Ocor.Cep, //{7}
           Ocor.CodEndereco,  //{8}
           Ocor.Numero, //{9}
           Ocor.CodBairro,  //{10}
           Ocor.CodCidade, //{11}
           Ocor.Complemento, //{12}
           "1", //{13}
           Ocor.CodServico,  //{14}
           "",  //{15}
            Ocor.CodProduto,  //{16}
           "277",  //{17}
           "I",  //{18}
           Ocor.IdUsuarioPortal,//{19}
           Ocor.ProcessoRel,//{20}
           Ocor.OcorTerceiro,//{21}
           Ocor.Severidade//{22}
           );


            if (string.IsNullOrWhiteSpace(Ocor.Controle))
                json = json.Replace("[7]", "'64':'10',");
            else
                json = json.Replace("[7]", "");

            json = json.Replace("'", "\"");
            json = json.Replace("\"text\"", "\"" + "'" + Ocor.Descricao.Replace("\"", "'") + "'" + "\"");


            return json;
        }

        public string GetNuOcorrencia(string json)
        {
            int position1 = json.IndexOf('{', 1);
            int position2 = json.IndexOf('}', 0);

            json = json.Substring(position1, (position2 - position1))
                       .Replace("EXECUTIONID", "")
                       .Replace("{", "")
                       .Replace("}", "")
                       .Replace("'", "")
                       .Replace(":", "")
                       .Replace('"', ' ')
                       .Trim();
            return json;
        }

        public DataTable GetDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dataTable);
            conn.Close();
            da.Dispose();

            return dataTable;
        }

        public void Update(string query)
        {
            string connString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);

            using (SqlConnection openCon = new SqlConnection(connString))
            {
                using (SqlCommand querySaveStaff = new SqlCommand(query))
                {
                    querySaveStaff.Connection = openCon;
                    openCon.Open();
                    querySaveStaff.ExecuteNonQuery();
                }
            }
        }

        public void Insert(string query)
        {
            Update(query);
        }

        public string Message(string originalMessage)
        {
            if (originalMessage.StartsWith("A carga "))
            {
                return "A carga horária \"0\" para SLA não possui definição de turnos e horários. Verifique !";
            }

            return "Erro interno.";
        }
    }
}