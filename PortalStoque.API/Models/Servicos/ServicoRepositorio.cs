using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Servicos
{
    public class ServicoRepositorio : IServicoRepositorio
    {
        public IEnumerable<Servico> GetAll(string filter)
        {
            string query = string.Format(@"SELECT 
		                                    DISTINCT 
		                                    PRO.CODGRUPOPROD AS CodServico,
		                                    GRU.DESCRGRUPOPROD AS Nome
		                                    FROM TGFPRO PRO 
		                                    INNER JOIN TGFGRU GRU  WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Servico>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Serviço. " + e.Message);
            }

        }
    }
}