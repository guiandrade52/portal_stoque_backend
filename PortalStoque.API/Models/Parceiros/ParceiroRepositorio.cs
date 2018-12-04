using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Parceiros
{
    public class ParceiroRepositorio : IParceiroRepositorio
    {
        public IEnumerable<Parceiro> GetAll(string filter)
        {
            string query = string.Format(@"SELECT TOP 100
	                                            PAR.CODPARC AS CodParc,
	                                            PAR.NOMEPARC AS Nome
                                            FROM TGFPAR PAR
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Parceiro>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Parceiros. " + e.Message);
            }
        }
    }
}