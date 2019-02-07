using Dapper;
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
            string query = string.Format(@"SELECT TOP 100			 
		                                    EQP.CONTROLE AS Controle,
		                                    PRO.DESCRPROD AS DescrProd
	                                    FROM TGFPRO PRO
	                                    INNER JOIN TGFGRU GRU WITH(NOLOCK) ON GRU.CODGRUPOPROD = PRO.CODGRUPOPROD
	                                    INNER JOIN BH_FTLEQP EQP WITH(NOLOCK) ON EQP.CODPROD = PRO.CODPROD
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Serie>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Séries. " + e.Message);
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
			                                    ENDE.TIPO +' '+ ENDE.NOMEEND AS Endereco,
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
			                                    AND EQP.CONTROLE = '{0}'", serie);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<SerieDetails>(query).First();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar detalhe da séries. " + e.Message);
            }
        }
    }
}