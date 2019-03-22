using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.SolucaoProposta
{
    public class SolucaoRepositorio : ISolucaoRepositorio
    {
        public Solucao GetSolucaos(int executionId)
        {
            string query = @"SELECT  EXECUTIONID AS ExecutionId
				                    ,DHINI AS DataInicio
				                    ,DHFIN AS DataFinal
				                    ,SOLUCAOAPL AS SolucaoAplicada
				                    ,ACTIVITIID AS ActivitId
			                FROM AD_STOTAR 
			                WHERE 1 = 1
			                AND EXECUTIONID = @executionId 
			                AND ACTIVITIID = (SELECT MAX(ACTIVITIID) FROM AD_STOTAR WHERE EXECUTIONID = @executionId)
			                UNION ALL
			                SELECT EXECUTIONID
				                    ,DHINI
				                    ,DHFIN
				                    ,SOLUCAOAPL
				                    ,ACTIVITIID
			                FROM AD_STOVST 
			                WHERE 1 = 1
			                AND EXECUTIONID = @executionId 
			                AND ACTIVITIID = (SELECT MAX(ACTIVITIID) FROM AD_STOVST WHERE EXECUTIONID = @executionId)";
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    Solucao s = null;
                    IEnumerable<Solucao> solucoes = _Conexao.Query<Solucao>(query, new { executionId }).ToList();
                    if (solucoes.Count() > 0)
                    {
                        s = solucoes.Where(x => x.ActivitId == solucoes.Max(y => y.ActivitId)).First();
                        if (string.IsNullOrEmpty(s.DataInicio))
                            if (string.IsNullOrEmpty(s.DataFinal))
                                return null;
                        if (string.IsNullOrWhiteSpace(s.SolucaoAplicada))
                            return null;
                    }
                    return s;
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