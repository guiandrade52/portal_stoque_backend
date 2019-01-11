using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Anexos
{
    public class AnexoRepositorio : IAnexoRepositorio
    {
        public IEnumerable<Anexo> GetAnexo(int executionId)
        {
            string sql = string.Format(@"SELECT 
	                                        NOMEARQUIVO AS Nome,
	                                        CHAVEARQUIVO AS Chave,
	                                        CONVERT(CHAR, DHCAD, 103) AS DataCadastro,
	                                        DESCRICAO AS Descricao,
	                                        Tipo = (SELECT REVERSE(LEFT(REVERSE(NOMEARQUIVO),CHARINDEX('.', REVERSE(NOMEARQUIVO))-1)))
                                        FROM TSIANX WHERE PKREGISTRO = '{0}_0_BHBPMAtividade'", executionId);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Anexo>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Anexo " + e.Message);
            }
        }
    }
}