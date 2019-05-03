using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Series
{
    public class SerieRepositorio : ISerieRepositorio
    {
        public IEnumerable<Serie> GetAll(string filter)
        {
            string query = string.Format(@"	SELECT TOP 100
                                                SERIE.CONTROLE AS Controle,
		                                        SERIE.CONTROLEFAB AS ControleFab,
		                                        PROD.DESCRPROD AS DescrProd
	                                        FROM BH_FTLEQP EQP
		                                        LEFT JOIN BH_FTLSER SERIE ON SERIE.CONTROLE = EQP.CONTROLE
		                                        INNER JOIN TGFPRO PROD ON PROD.CODPROD = EQP.CODPROD
                                                INNER JOIN TCSCON CON ON CON.NUMCONTRATO = EQP.NUMCONTRATO
												INNER JOIN TGFGRU GRU WITH(NOLOCK) ON GRU.CODGRUPOPROD = PROD.CODGRUPOPROD
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Serie>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public IEnumerable<SerieParcCon> GetAllParcCon(string serie)
        {
            string query = string.Format(@"SELECT TOP 100
                                                EQP.CONTROLE AS Serie,
		                                        PRO.DESCRPROD AS Produto,
		                                        EQP.SITUACAO,
		                                        CON.NUMCONTRATO AS Contrato,
		                                        CON.CODPARC AS CodParcCon,
		                                        PAR.NOMEPARC AS NomeParcCon,
                                                EQP.CODPARC AS CodParcAtendido,
		                                        PAREQP.RAZAOSOCIAL AS NomeParcAtendido
	                                        FROM BH_FTLEQP EQP WITH (NOLOCK)
                                            LEFT JOIN BH_FTLSER SERIE ON SERIE.CONTROLE = EQP.CONTROLE
	                                        INNER JOIN TCSCON CON  WITH (NOLOCK) ON EQP.NUMCONTRATO = CON.NUMCONTRATO
	                                        INNER JOIN TGFPAR PAR WITH (NOLOCK) ON PAR.CODPARC = CON.CODPARC
                                            INNER JOIN TGFPAR PAREQP WITH (NOLOCK) ON PAREQP.CODPARC=EQP.CODPARC
	                                        INNER JOIN TGFPRO PRO WITH(NOLOCK) ON PRO.CODPROD = EQP.CODPROD 
                                            WHERE SERIE.CONTROLEFAB LIKE '{0}%'", serie);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<SerieParcCon>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public SerieDetails GetSerieDetails(string serie)
        {
            string query = string.Format(@" SELECT 
			                                    EQP.CODPARC AS CodParc,
			                                    CON.CODPARC AS CodParcCon,
			                                    CON.NUMCONTRATO AS Contrato,
			                                    PAR.NOMEPARC AS Parceiro,
			                                    EQP.CONTROLE AS Controle,
			                                    SERIE.CONTROLEFAB AS ControleFab,			
			                                    PROD.DESCRPROD AS DescrProd,			
			                                    NAT.DESCRNAT AS DescrNat,
			                                    (SELECT ISNULL(ENDE.TIPO, '') +' '+ ENDE.NOMEEND) AS Endereco,
		                                        EQP.NUMEND AS Numero,
			                                    BAI.NOMEBAI AS Bairro,
			                                    CID.NOMECID AS Cidade,
			                                    ENDE.CODEND AS CodEndereco,
			                                    CID.CODCID AS CodCidade,
			                                    BAI.CODBAI AS CodBairro,		    			
		                                        EQP.CEP AS Cep,
		                                        EQP.COMPLEMENTO AS Complemento,
			                                    GRU.DESCRGRUPOPROD AS DescrGrupo,
			                                    PROD.CODGRUPOPROD AS CodGrupo,
			                                    PROD.CODPROD AS Produto,
                                                PROD.CODPROD AS CodProduto
		                                    FROM BH_FTLEQP EQP
			                                    LEFT JOIN BH_FTLSER SERIE ON SERIE.CONTROLE = EQP.CONTROLE
			                                    INNER JOIN TGFPRO PROD ON PROD.CODPROD = EQP.CODPROD
			                                    INNER JOIN TCSCON CON ON CON.NUMCONTRATO = EQP.NUMCONTRATO
			                                    INNER JOIN TGFNAT NAT ON NAT.CODNAT = CON.CODNAT
			                                    LEFT JOIN TSIBAI BAI ON EQP.CODBAI = BAI.CODBAI
			                                    LEFT JOIN TSIEND ENDE ON EQP.CODEND = ENDE.CODEND
		                                        LEFT JOIN TSICID CID ON EQP.CODCID = CID.CODCID
			                                    INNER JOIN TGFGRU GRU ON GRU.CODGRUPOPROD = PROD.CODGRUPOPROD
			                                    LEFT JOIN TGFPAR PAR ON EQP.CODPARC = PAR.CODPARC

			                                    WHERE 1 = 1
			                                    AND SERIE.CONTROLEFAB = '{0}'
                                                OR SERIE.CONTROLE = '{0}'", serie);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<SerieDetails>(query).FirstOrDefault();
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