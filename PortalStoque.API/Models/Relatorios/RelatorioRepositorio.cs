using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Relatorios
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        public DataTable GetOcorrencia(string filtro)
        {
            var sql = string.Format(@"SELECT OCO.EXECUTIONID AS [Número da ocorrência],
                                        OPC.OPCAO AS [Situação],
                                        OCO.DHCHAMADA AS [Data Abertura],
                                        EXC.DHSLATR AS [Tempo Resposta],
                                        EXC.DHSLATS AS [Tempo Solução],                                                
                                        (SELECT CONTROLEFAB FROM  BH_FTLSER WITH(NOLOCK) WHERE CONTROLE = OCO.CONTROLE) AS [Número de Série],
                                        PAR.NOMEPARC AS [Cliente Abertura],
                                        OCO.DESCRICAO AS [Descrição],
                                        PROD.DESCRPROD AS [Serviço],
                                        PRO2.DESCRPROD AS [Produto Execução],
                                        (SELECT NOMECONTATO FROM TGFCTT WITH(NOLOCK) WHERE CODCONTATO = OCO.CODCONTATO AND CODPARC = OCO.CODPARC) AS [Nome Contato]
                                        FROM AD_STOOCO OCO  WITH(NOLOCK)
                                        INNER JOIN TGFPAR PAR WITH(NOLOCK) ON PAR.CODPARC = OCO.CODPARC
                                        LEFT JOIN TSIEND ENDE WITH(NOLOCK) ON ENDE.CODEND = OCO.CODEND
                                        LEFT JOIN TSICID CID WITH(NOLOCK) ON CID.CODCID = OCO.CODCID 
                                        LEFT JOIN [TSIBAI] BAI WITH(NOLOCK) ON BAI.CODBAI = OCO.CODBAI 
                                        LEFT JOIN TSIUFS TSI WITH(NOLOCK) ON TSI.CODUF = CID.UF
                                        INNER JOIN BH_BPMEXC EXC WITH(NOLOCK) ON EXC.EXECUTIONID = OCO.EXECUTIONID
                                        LEFT JOIN TGFPRO PROD WITH(NOLOCK) ON PROD.CODPROD = OCO.CODPROD
                                        INNER JOIN TDDOPC OPC WITH(NOLOCK) ON OCO.SITUACAO = OPC.VALOR AND OPC.NUCAMPO = 9999990522
                                        LEFT JOIN TGFPRO AS PRO2 WITH(NOLOCK) ON PRO2.CODPROD = OCO.CODPRODEXC
                                        {0}
                                        ORDER BY OCO.EXECUTIONID DESC", filtro);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    SqlDataAdapter ocorrencias = new SqlDataAdapter(sql, _Conexao);
                    DataTable dt = new DataTable();
                    dt.TableName = "Employee";
                    ocorrencias.Fill(dt);
                    return dt;
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