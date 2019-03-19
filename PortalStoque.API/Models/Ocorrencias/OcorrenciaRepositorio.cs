using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using PortalStoque.API.Controllers.services;

namespace PortalStoque.API.Models.Ocorrencias
{
    public class OcorrenciaRepositorio : IOcorrenciaRepositorio
    {
        private List<Ocorrencia> Ocorrencias = new List<Ocorrencia>();
        public IEnumerable<Ocorrencia> GetAll(string filter, int pagina, int tamPag)
        {
            var sql = string.Format(@"
            SELECT * FROM (
                    SELECT ROW_NUMBER() OVER(ORDER BY OCO.ExecutionId DESC) AS NUMBER,
                    OCO.ExecutionId,
			        OPC.OPCAO AS Situacao,
	                (SELECT DESCRICAO FROM  AD_STOORI WITH(NOLOCK) WHERE NUORIGEM = OCO.NUORIGEM) AS Origem,
	                (SELECT NOMEUSU FROM  TSIUSU WITH(NOLOCK) WHERE CODUSU = OCO.CODUSU) AS Responsavel,
					CONVERT(CHAR, OCO.DHCHAMADA,103)+''+CONVERT(CHAR, OCO.DHCHAMADA,108) as DataCr,
					CONVERT(CHAR,  EXC.DHSLATR,103)+''+CONVERT(CHAR,  EXC.DHSLATR,108) as DataTr,
					CONVERT(CHAR, EXC.DHSLATS,103)+''+CONVERT(CHAR, EXC.DHSLATS,108) as DataTs,
	                (SELECT CONTROLEFAB FROM  BH_FTLSER WITH(NOLOCK) WHERE CONTROLE = OCO.CONTROLE) AS Serie,
					(SELECT NOMEUSU FROM AD_USUPRTL WHERE IDUSUPRTL = OCO.IDUSUPRTL) AS UserPortal,
	                PAR.NOMEPARC AS ClienteAt,
                    (SELECT NOMECONTATO FROM TGFCTT WITH(NOLOCK) WHERE CODCONTATO = OCO.CODCONTATO AND CODPARC = OCO.CODPARC) AS Contato,
                    OCO.TELEFONE AS Telefone,
                    OCO.EMAIL AS Email,      
                    ENDE.TIPO +' '+ ENDE.NOMEEND AS Lagradouro,
                    OCO.NUMEND AS Numero,
                    OCO.COMPLEMENTO AS Complemento,                
                    BAI.NOMEBAI AS Bairro,
                    OCO.CEP AS Cep,
                    TSI.UF AS Estado,
                    CID.NOMECID AS Cidade,
                    (SELECT OPCAO FROM TDDOPC WITH(NOLOCK) WHERE NUCAMPO = 9999990492 AND VALOR = OCO.CLASSIFICACAO) AS Classificacao,
                    (SELECT OPCAO FROM TDDOPC WITH(NOLOCK) WHERE NUCAMPO = 9999990505 AND VALOR = OCO.TIPO) AS TipoOcorrencia,        
			        (SELECT DESCRICAO FROM  BH_BPMGRU WITH(NOLOCK) WHERE CODGRUPO = OCO.CODGRUPO) AS GrupoServico,				
                    PROD.DESCRPROD AS Servico,
                    OCO.DESCRICAO AS Descricao,
                    PRODEXC.DESCRPROD AS Produto,
			        
					OPC.ORDEM AS idSituacao,
					OCO.CODUSU,
                    OCO.NOMECLIENTE,
                    OCO.CODPRODEXC, 
                    OCO.CODPROD,  
                    (SELECT ISNULL(TELEFONE,'') +' - '+ISNULL(EMAIL,'') AS 'TELEFONE - EMAIL' FROM TGFCTT WITH(NOLOCK)  WHERE CODCONTATO = OCO.CODCONTATO AND CODPARC = OCO.CODPARC) AS TELEF_EMAIL,
                    OCO.CODCONTATO
                    
		        FROM AD_STOOCO OCO  WITH(NOLOCK)
                    INNER JOIN TGFPAR PAR WITH(NOLOCK) ON PAR.CODPARC = OCO.CODPARC
                    LEFT JOIN TSIEND ENDE WITH(NOLOCK) ON ENDE.CODEND = OCO.CODEND
                    LEFT JOIN TSICID CID WITH(NOLOCK) ON CID.CODCID = OCO.CODCID 
		            LEFT JOIN [TSIBAI] BAI WITH(NOLOCK) ON BAI.CODBAI = OCO.CODBAI 
                    LEFT JOIN TSIUFS TSI WITH(NOLOCK) ON TSI.CODUF = CID.UF
		            INNER JOIN BH_BPMEXC EXC WITH(NOLOCK) ON EXC.EXECUTIONID = OCO.EXECUTIONID
			        LEFT JOIN TGFPRO PROD WITH(NOLOCK) ON PROD.CODPROD = OCO.CODPROD
                    LEFT JOIN TGFPRO PRODEXC WITH(NOLOCK) ON PRODEXC.CODPROD = OCO.CODPRODEXC
			        INNER JOIN TDDOPC OPC WITH(NOLOCK) ON OCO.SITUACAO = OPC.VALOR AND OPC.NUCAMPO = 9999990522
                    {0}
					) AS TBL
                WHERE NUMBER BETWEEN ((@Pagina - 1) * @TamPag + 1) AND (@Pagina * @TamPag)", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Ocorrencia>(sql, new { pagina, tamPag });
                }

            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao Recuperar dados."+ e.Message);
            }
        }
        public int GetTotalOcor(string filter)
        {
            var ret = 0;
            string sql = @"SELECT COUNT(*) FROM AD_STOOCO OCO 
                                INNER JOIN TGFPAR PAR WITH(NOLOCK) ON PAR.CODPARC = OCO.CODPARC
                                LEFT JOIN TSIEND ENDE WITH(NOLOCK) ON ENDE.CODEND = OCO.CODEND
                                LEFT JOIN TSICID CID WITH(NOLOCK) ON CID.CODCID = OCO.CODCID 
		                        LEFT JOIN [TSIBAI] BAI WITH(NOLOCK) ON BAI.CODBAI = OCO.CODBAI 
                                LEFT JOIN TSIUFS TSI WITH(NOLOCK) ON TSI.CODUF = CID.UF
		                        INNER JOIN BH_BPMEXC EXC WITH(NOLOCK) ON EXC.EXECUTIONID = OCO.EXECUTIONID
				                LEFT JOIN TGFPRO PROD WITH(NOLOCK) ON PROD.CODPROD = OCO.CODPROD
				                INNER JOIN TDDOPC OPC WITH(NOLOCK) ON OCO.SITUACAO = OPC.VALOR AND OPC.NUCAMPO = 9999990522";

            if (!string.IsNullOrEmpty(filter))
                sql = string.Format("{0} {1}", sql, filter);

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
        public Ocorrencia Get(int id)
        {
            throw new NotImplementedException();
        }

    }
}