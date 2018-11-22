using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Series
{
    public class SerieRepositorio : ISerieRepositorio
    {
        public IEnumerable<Serie> GetAll(string filter)
        {
            List<Serie> series;
            string query = string.Format(@"SELECT TOP 100
			                                EQP.CONTROLE AS CodSerie,
			                                SERIE.CONTROLEFAB AS CodSerieFab,			
			                                EQP.CODPARC AS CodParc,
			                                CON.CODPARC AS CodParcCon,
			                                PROD.DESCRPROD AS Nome,
			                                CON.NUMCONTRATO AS CodContrato
		                                FROM BH_FTLEQP EQP
			                                LEFT JOIN BH_FTLSER SERIE ON SERIE.CONTROLE = EQP.CONTROLE
			                                INNER JOIN TGFPRO PROD ON PROD.CODPROD = EQP.CODPROD
			                                INNER JOIN TCSCON CON ON CON.NUMCONTRATO = EQP.NUMCONTRATO
                                        {0}", filter);

            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    series = _Conexao.Query<Serie>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Contratos. " + e.Message);
            }

            return series;
        }
    }
}