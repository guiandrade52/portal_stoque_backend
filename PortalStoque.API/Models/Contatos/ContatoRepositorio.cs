using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Contatos
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        public IEnumerable<Contato> GetAll(string filter)
        {
            string query = string.Format(@"SELECT TOP 100
	                                            CTT.NOMECONTATO AS Nome,
	                                            CTT.CODCONTATO AS CodContato
                                            FROM TGFCTT CTT
                                            INNER JOIN TCSCON CON WITH(NOLOCK) ON CON.CODPARC = CTT.CODPARC
                                {0}", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Contato>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Contato. " + e.Message);
            }
        }
    }
}