using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.GrupoProdutos
{
    public class GrupoProdutoRepositorio : IGrupoProduto
    {
        public IEnumerable<GrupoProduto> GetAll(int contrato)
        {
            string query = string.Format(@"SELECT 
                                                DISTINCT 
                                                PRO.CODGRUPOPROD AS CodGrupo,
                                                GRU.DESCRGRUPOPROD	AS DescGrupo
	                                            FROM TGFPRO PRO 
                                            INNER JOIN TGFGRU GRU  WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
                                            WHERE PRO.CODPROD IN (
                                                                    SELECT CODPROD 
                                                                        FROM BH_FTLEQP EQP 
						                                            WHERE EQP.NUMCONTRATO = {0}
                                                                )", contrato);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<GrupoProduto>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Grupo de Produtos. " + e.Message);
            }
        }
    }
}