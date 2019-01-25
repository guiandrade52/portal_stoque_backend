using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Series
{
    public class SerieRepositorio : ISerieRepositorio
    {
        public IEnumerable<Serie> GetAll(string filter)
        {
            string query = string.Format(@"SELECT			 
		                                    EQP.CONTROLE AS Controle,
		                                    PRO.DESCRPROD AS DescProd
	                                    FROM TGFPRO PRO
	                                    INNER JOIN TGFGRU GRU WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
	                                    INNER JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Serie>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Séries. " + e.Message);
            }
        }
    }
}