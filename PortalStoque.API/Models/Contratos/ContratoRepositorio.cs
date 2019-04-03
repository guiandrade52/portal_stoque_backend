using Dapper;
using PortalStoque.API.Controllers.services;
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
            string query = string.Format(@"SELECT TOP 100
                                                    CON.NUMCONTRATO AS CodContrato,
	                                                PAR.NOMEPARC AS Nome,
                                                    PAR.RAZAOSOCIAL AS RazaoSocial,
                                                    PAR.CODPARC AS codParc
                                                    FROM TCSCON CON
                                                    INNER JOIN TGFPAR PAR  WITH(NOLOCK) ON PAR.CODPARC = CON.CODPARC
                                                    {0}", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Contrato>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public ContratoDetails GetDetails(int contrato)
        {
            string query = string.Format(@"SELECT 
	                                         CON.NUMCONTRATO AS Contrato
	                                        ,PAR.CODPARC AS CodParc
                                            ,PAR.NOMEPARC AS Nome
	                                        ,PAR.RAZAOSOCIAL AS RazaoSocial
	                                        ,NAT.DESCRNAT AS DescrNat
	                                        ,CON.CODPARC AS CodParcCon
	                                        ,ENDE.TIPO +' '+ ENDE.NOMEEND AS Endereco
	                                        ,ENDE.CODEND AS CodEndereco
	                                        ,PAR.NUMEND AS Numero
	                                        ,BAI.CODBAI	AS CodBairro
	                                        ,BAI.NOMEBAI AS Bairro
	                                        ,CID.CODCID AS CodCidade
	                                        ,CID.NOMECID AS Cidade
	                                        ,PAR.CEP
	                                        ,PAR.COMPLEMENTO
                                        FROM TGFPAR PAR
                                        INNER JOIN TCSCON CON ON PAR.CODPARC = CON.CODPARC
                                        LEFT JOIN TGFNAT NAT ON NAT.CODNAT = CON.CODNAT
                                        LEFT JOIN TSIBAI BAI ON PAR.CODBAI = BAI.CODBAI
                                        LEFT JOIN TSIEND ENDE ON PAR.CODEND = ENDE.CODEND
                                        LEFT JOIN TSICID CID ON PAR.CODCID = CID.CODCID
                                        WHERE CON.NUMCONTRATO = {0}", contrato);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<ContratoDetails>(query).First();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public int DeleteContrato(int idUsuario, int contrato, int codParc)
        {
            var sql = string.Format(@"DELETE AD_USUPRTLCON WHERE IDUSUPRTL = {0} AND NUMCONTRATO IN ({1}) AND CODPARCAT IN ({2})", idUsuario, contrato, codParc);
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public int DeleteAllContrato(int idUsuario)
        {
            var sql = string.Format(@"DELETE AD_USUPRTLCON WHERE IDUSUPRTL = {0}", idUsuario);
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public int SalvarContrato(int idUsuario, int codPar, int contrato, int codParAt)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    int exist = (int)conexao.ExecuteScalar(string.Format(@"SELECT COUNT(*) FROM AD_USUPRTLCON 
                                                        WHERE IDUSUPRTL = {0} 
                                                        AND CODPARCAT = {1} 
                                                        AND CODPARCAB = {2} 
                                                        AND NUMCONTRATO = {3}", idUsuario, codParAt, codPar, contrato));
                    if (exist > 0)
                        return exist;

                    string sql = string.Format(@"INSERT INTO AD_USUPRTLCON (IDUSUPRTL, SEQUENCIA, CODPARCAT, CODPARCAB, NUMCONTRATO)
                                            VALUES (
                                                    {0},
                                                    (SELECT ISNULL(MAX(SEQUENCIA), 0)+1  FROM AD_USUPRTLCON WHERE IDUSUPRTL = {0}),
                                                    {1}, {2}, {3})", idUsuario, codParAt, codPar, contrato);
                    conexao.Execute(sql);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public IEnumerable<CadContrato> ListaContratosPUsuario(int idUsuario)
        {
            string query = string.Format(@"SELECT IDUSUPRTL AS idUsuario,
	                                               NUMCONTRATO AS Contrato,
                                                   CODPARCAT AS CodParc,
	                                               NOMEPARC AS Nome
	                                               FROM AD_USUPRTLCON PRLCON
	                                               INNER JOIN TGFPAR PAR ON PRLCON.CODPARCAT = PAR.CODPARC
	                                               WHERE IDUSUPRTL = {0}", idUsuario);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<CadContrato>(query).ToList();
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