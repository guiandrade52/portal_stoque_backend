using PortalStoque.API.Controllers.services;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PortalStoque.API.Models.Charts
{
    public class ChartsRepositorio : IChartsRepositorio
    {
        public int GetChartLine(Charts charts)
        {
            var ret = 0;
            if (string.IsNullOrWhiteSpace(charts.Contratos))
                return ret;

            string sql = string.Format(@"SELECT COUNT(*) FROM AD_STOOCO OCO 
                                            INNER JOIN TGFPAR PAR WITH(NOLOCK) ON PAR.CODPARC = OCO.CODPARC
                                            LEFT JOIN TSIEND ENDE WITH(NOLOCK) ON ENDE.CODEND = OCO.CODEND
                                            LEFT JOIN TSICID CID WITH(NOLOCK) ON CID.CODCID = OCO.CODCID 
	                                        LEFT JOIN [TSIBAI] BAI WITH(NOLOCK) ON BAI.CODBAI = OCO.CODBAI 
                                            LEFT JOIN TSIUFS TSI WITH(NOLOCK) ON TSI.CODUF = CID.UF
	                                        INNER JOIN BH_BPMEXC EXC WITH(NOLOCK) ON EXC.EXECUTIONID = OCO.EXECUTIONID
	                                        LEFT JOIN TGFPRO PROD WITH(NOLOCK) ON PROD.CODPROD = OCO.CODPROD
	                                        INNER JOIN TDDOPC OPC WITH(NOLOCK) ON OCO.SITUACAO = OPC.VALOR AND OPC.NUCAMPO = 9999990522
	                                        WHERE 1 = 1  
	                                        AND OCO.NUMCONTRATO IN ({3})
	                                        AND OCO.CODCONTATO = {2}
	                                        AND OCO.DHCHAMADA >= '{0}'
	                                        AND OCO.DHCHAMADA <= '{1}'", charts.DtInicio, charts.DtFinal, charts.CodContato, charts.Contratos);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    using (var comando = new SqlCommand())
                    {
                        _Conexao.Open();
                        comando.Connection = _Conexao;
                        comando.CommandText = sql;
                        var i = comando.ExecuteScalar();
                        ret = (int)i;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
            return ret;
        }
        public int GetChartRound(Charts charts)
        {
            var ret = 0;
            string sql = string.Format(@"SELECT COUNT(*) FROM AD_STOOCO OCO 
                                            INNER JOIN TGFPAR PAR WITH(NOLOCK) ON PAR.CODPARC = OCO.CODPARC
                                            LEFT JOIN TSIEND ENDE WITH(NOLOCK) ON ENDE.CODEND = OCO.CODEND
                                            LEFT JOIN TSICID CID WITH(NOLOCK) ON CID.CODCID = OCO.CODCID 
	                                        LEFT JOIN [TSIBAI] BAI WITH(NOLOCK) ON BAI.CODBAI = OCO.CODBAI 
                                            LEFT JOIN TSIUFS TSI WITH(NOLOCK) ON TSI.CODUF = CID.UF
	                                        INNER JOIN BH_BPMEXC EXC WITH(NOLOCK) ON EXC.EXECUTIONID = OCO.EXECUTIONID
	                                        LEFT JOIN TGFPRO PROD WITH(NOLOCK) ON PROD.CODPROD = OCO.CODPROD
	                                        INNER JOIN TDDOPC OPC WITH(NOLOCK) ON OCO.SITUACAO = OPC.VALOR AND OPC.NUCAMPO = 9999990522
	                                        WHERE 1 = 1  
	                                        AND OCO.NUMCONTRATO IN ({0})
	                                        AND OCO.CODCONTATO = {1}
											{2}", charts.Contratos, charts.CodContato, charts.Situacao);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    using (var comando = new SqlCommand())
                    {
                        _Conexao.Open();
                        comando.Connection = _Conexao;
                        comando.CommandText = sql;
                        ret = (int)comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
            return ret;
        }
    }
}