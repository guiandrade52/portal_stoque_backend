using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.GrupoProdutos
{
    public class GrupoProdutoRepositorio : IGrupoProdutoRepositorio
    {
        public IEnumerable<GrupoProduto> GetAll(string filter)
        {
            string query = string.Format(@"SELECT 
                                                DISTINCT 
                                                PRO.CODGRUPOPROD AS CodGrupo
                                               ,GRU.DESCRGRUPOPROD	AS DescGrupo									
                                        FROM TGFPRO PRO 
                                        INNER JOIN TGFGRU GRU  WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
                                        LEFT JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
                                        {0}
                                        ORDER BY DescGrupo", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<GrupoProduto>(query).ToList();
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