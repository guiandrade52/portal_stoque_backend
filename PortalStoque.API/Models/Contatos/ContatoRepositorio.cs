using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Contatos
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        public IEnumerable<Contato> GetAll(string filter)
        {
            string query = string.Format(@"SELECT DISTINCT TOP 100
	                                                CTT.NOMECONTATO AS Nome,
	                                                CTT.CODCONTATO AS CodContato,
	                                                CTT.EMAIL,
	                                                CTT.TELEFONE
                                                FROM TGFCTT CTT
                                                INNER JOIN TCSCON CON WITH(NOLOCK) ON CON.CODPARC = CTT.CODPARC
                                                {0}
                                                ORDER BY CTT.NOMECONTATO", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Contato>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }
        public IEnumerable<Contato> GetComContrato(string filter)
        {
            string query = string.Format(@"SELECT TOP 100
	                                                CTT.NOMECONTATO AS Nome,
	                                                CTT.CODCONTATO AS CodContato,
	                                                CTT.EMAIL,
	                                                CTT.TELEFONE
                                                FROM TGFCTT CTT
                                                INNER JOIN TCSCON CON WITH(NOLOCK) ON CON.CODPARC = CTT.CODPARC
                                                {0}
                                                ORDER BY CTT.NOMECONTATO", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Contato>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }
        public IEnumerable<Contato> GetComSerie(string filter)
        {
            string query = string.Format(@"SELECT   CTT.CODCONTATO AS CodContato
	                                                ,CTT.NOMECONTATO AS Nome
	                                                ,CTT.TELEFONE AS Telefone
	                                                ,CTT.EMAIL AS Email
	                                                ,EQP.CONTROLE
                                                FROM BH_FTLEQP EQP
	                                                INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODPARC = EQP.CODPARC    
                                               {0}", filter);
            try
            {
                using (var _Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return _Conexao.Query<Contato>(query).ToList();
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