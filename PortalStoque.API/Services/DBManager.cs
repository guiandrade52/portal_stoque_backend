using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PortalStoque.API.Services
{
    public static class DBManager
    {
       
        public static DataTable GetDataTable(string query)
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

        public static void Update(string query)
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

        public static void Insert(string query)
        {
            Update(query);
        }

        public static string GetFormatedQuery(string nuanx, string chave, string nomearquivo, string pkregistro)
        {
            string query = string.Format(@"INSERT INTO[sankhya].[TSIANX]
                                       ([NUATTACH]
                                       ,[NOMEINSTANCIA]
                                       ,[CHAVEARQUIVO]
                                       ,[NOMEARQUIVO]
                                       ,[DESCRICAO]
                                       ,[RESOURCEID]
                                       ,[TIPOAPRES]
                                       ,[TIPOACESSO]
                                       ,[CODUSU]
                                       ,[DHALTER]
                                       ,[PKREGISTRO]
                                       ,[CODUSUALT]
                                       ,[DHCAD]
                                       ,[LINK])
                                 VALUES
                                       ({0}
                                       ,'BHBPMAtividade'
                                       ,'{1}'
                                       ,'{2}'
                                       ,'ANEXO PORTAL'
                                       , NULL
                                       ,'GLO'
                                       ,'ALL'
                                       ,277
                                       , GETDATE()
                                       ,'{3}'
                                       ,277
                                       ,GETDATE()
                                       ,NULL)", nuanx, chave, nomearquivo, pkregistro);

            return query;
        }
    }
}