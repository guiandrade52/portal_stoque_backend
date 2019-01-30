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
        public IEnumerable<Produto> GetAll(int contrato, int codGrupo)
        {
            string query = string.Format(@"SELECT	 DISTINCT
			                                         PRO.DESCRPROD AS DescProd
			                                        ,PRO.CODPROD AS CodProd
			                                        ,PRO.CODGRUPOPROD AS CodGrupo
			                                        ,EQP.NUMCONTRATO AS Contrato	
	                                        FROM TGFPRO PRO
	                                        INNER JOIN TGFGRU GRU WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
	                                        INNER JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
	                                        WHERE 1 = 1
	                                        AND EQP.SITUACAO = 'A'
	                                        AND EQP.NUMCONTRATO = {0}
	                                        AND GRU.CODGRUPOPROD = {1}
                                            ORDER BY PRO.DESCRPROD", contrato, codGrupo);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Produto>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Produtos. " + e.Message);
            }
        }
    }
}