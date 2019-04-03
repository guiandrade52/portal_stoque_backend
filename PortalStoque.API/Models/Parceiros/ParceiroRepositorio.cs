using Dapper;
using PortalStoque.API.Controllers.services;
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
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public IEnumerable<ParceiroContrato> ParceirosPorContrato(string contratos)
        {
            string query = string.Format(@"SELECT DISTINCT
	                                        PAR.CODPARC AS CodParc,
	                                        PAR.NOMEPARC AS Nome
                                        FROM TGFPAR PAR
                                        INNER JOIN TCSCON CON WITH(NOLOCK) ON PAR.CODPARC = CON.CODPARC
                                        WHERE CON.NUMCONTRATO IN ({0})", contratos);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<ParceiroContrato>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public IEnumerable<ParceiroContrato> ParceirosDoAlocado(string contratos)
        {
            string query = string.Format(@"SELECT DISTINCT
	                                        PAR.CODPARC AS CodParc,
	                                        PAR.NOMEPARC AS Nome
	                                        FROM BH_FTLEQP EQP
	                                        INNER JOIN TGFPAR PAR WITH(NOLOCK) ON EQP.CODPARC = PAR.CODPARC
	                                        WHERE NUMCONTRATO IN ({0})", contratos);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<ParceiroContrato>(query).ToList();
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