using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Cits
{
    public class CitRepositorio : ICitRepositorio
    {
        public Cit GetCit(int id)
        {
            string sql = string.Format(@"SELECT 
	                                        VIN.EXECUTIONID AS ExecutionId, 
	                                        DHINIC AS DataInicio,
	                                        DHTERM AS DataFinal
	                                        FROM AD_STOVIN VIN 
	                                        INNER JOIN BH_BPMATV ATV ON ATV.EXECUTIONID = VIN.EXECUTIONID 
	                                        AND ATV.ACTIVITIID = VIN.ACTIVITIID  
	                                        WHERE 1 = 1
	                                        AND VIN.EXECUTIONID = {0}
	                                        AND ATV.DHTERM IS NOT NULL
                                            ORDER BY VIN.EXECUTIONID DESC", id);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Cit>(sql).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar CIT " + e.Message);
            }
        }
    }
}