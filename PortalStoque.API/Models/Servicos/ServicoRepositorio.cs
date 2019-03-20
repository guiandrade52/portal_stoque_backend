using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Servicos
{
    public class ServicoRepositorio : IServicoRepositorio
    {
        public IEnumerable<Servico> GetAll(string filter)
        {
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Servico>(filter).ToList();
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