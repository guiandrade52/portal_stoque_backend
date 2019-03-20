using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Produtos
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        public IEnumerable<Produto> GetAll(string filter)
        {
            string query = string.Format(@"SELECT	 DISTINCT
			                                         PRO.DESCRPROD AS DescProd
			                                        ,PRO.CODPROD AS CodProd
			                                        ,PRO.CODGRUPOPROD AS CodGrupo
                                                    ,GRU.DESCRGRUPOPROD AS DescrGrupo
	                                        FROM TGFPRO PRO
	                                        INNER JOIN TGFGRU GRU WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
	                                        INNER JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
	                                        {0}
                                            ORDER BY PRO.DESCRPROD", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Produto>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }
    }
}