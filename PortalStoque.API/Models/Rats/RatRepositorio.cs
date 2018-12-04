using Dapper;
using PortalStoque.API.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace PortalStoque.API.Models.Rats
{
    public class RatRepositorio : IRatRepositorio
    {
        public IEnumerable<Rat> GetDataRat(int id)
        {
            string sql = string.Format(@"SELECT 
	                                        EXECUTIONID AS ExecutionId, 
                                            NUMVISITA AS NumeroVisita, 
                                            DHINI AS DataInicio,
                                            DHFIN AS DataFinal,    
                                            DEFEITOCONS AS Defeito, 
                                            CAUSADEF AS Causa,
                                            SOLUCAOAPL AS Solucao 
                                        FROM AD_STOVST WHERE EXECUTIONID = {0} ORDER BY NUMVISITA DESC", id);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Rat>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar RAT " + e.Message);
            }
        }

        public string GetRATXML(int executionid, int pNumVisita)
        {
            string xml = @"<relatorio nuRfe='101'>
                                                 <parametros>
                                                      <parametro nome='PK_NO' descricao='Ocorrencia' classe='java.math.BigDecimal' instancia='' valor='{0}' pesquisa='false' requerido='false'/>
                                                      <parametro nome = 'NumeroVisita' descricao = 'NumeroVisita' classe = 'java.math.BigDecimal' instancia = '' valor = '{1}' pesquisa = 'false' requerido = 'false'/>             
                                                 </parametros>
                          </relatorio>";


            DataTable chamado = GetRAT(executionid);

            if (chamado.Rows.Count > 0)
                xml = string.Format(xml, executionid, pNumVisita);
            else
                xml = null;

            return xml;
        }

        private DataTable GetRAT(int executionid)
        {
            string StrSqlCnn = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            SqlConnection SQLConn = new SqlConnection(StrSqlCnn);

            string sql = string.Format(@"SELECT TOP 1 EXECUTIONID, NUMVISITA FROM AD_STOVST WHERE EXECUTIONID = {0} ORDER BY NUMVISITA DESC", executionid);

            // sql = sql + GetWhereClause();

            SqlCommand command = new SqlCommand(sql);
            SqlDataAdapter adptar = new SqlDataAdapter(command);

            DataTable dtChamados = new DataTable();
            adptar.SelectCommand.Connection = SQLConn;
            adptar.SelectCommand.Connection.Open();
            adptar.Fill(dtChamados);
            adptar.SelectCommand.Connection.Close();

            return dtChamados;
        }
    }
}