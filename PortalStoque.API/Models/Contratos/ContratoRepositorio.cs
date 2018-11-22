using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Contratos
{
    public class ContratoRepositorio : IContratoRepositorio
    {
        public IEnumerable<Contrato> GetAll(string filter)
        {
            List<Contrato> contrato;
            string query = string.Format(@"SELECT
                                CON.NUMCONTRATO AS CodContrato,
	                            PAR.NOMEPARC AS Nome
                                FROM TCSCON CON
                                INNER JOIN TGFPAR PAR  WITH(NOLOCK) ON PAR.CODPARC = CON.CODPARC
                                {0}", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    contrato = _Conexao.Query<Contrato>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Contratos. " + e.Message);
            }

            return contrato;
        }
    }
}