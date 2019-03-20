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
    }
}