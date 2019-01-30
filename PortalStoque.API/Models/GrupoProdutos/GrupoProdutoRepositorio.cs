using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.GrupoProdutos
{
    public class GrupoProdutoRepositorio : IGrupoProdutoRepositorio
    {
        public IEnumerable<GrupoProduto> GetAll(int contrato)
        {
            string query = string.Format(@"SELECT 
                                                DISTINCT 
                                                PRO.CODGRUPOPROD AS CodGrupo
                                               ,GRU.DESCRGRUPOPROD	AS DescGrupo									
                                        FROM TGFPRO PRO 
                                        INNER JOIN TGFGRU GRU  WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
                                        LEFT JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
                                        WHERE 1 = 1
                                        AND EQP.SITUACAO = 'A'
                                        AND EQP.NUMCONTRATO = {0}
                                        ORDER BY DescGrupo", contrato);

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