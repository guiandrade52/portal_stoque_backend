using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public Login Login(Login login)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    if (!string.IsNullOrWhiteSpace(login.UserName) && !string.IsNullOrWhiteSpace(login.Password))
                    {
                        //CriptoHelper.HashMD5(pPassword);
                        login.UserName.ToLower();

                        return conexao.Query<Login>
                            (@"SELECT 
	                            PRTL.IDUSUPRTL AS IdUsuario,
	                            CTT.NOMECONTATO AS Nome,
	                            PRTL.LGNUSU AS UserName
	                            FROM AD_USUPRTL PRTL
	                            INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC
                                WHERE PRTL.LGNUSU = @UserName AND PRTL.PSWUSU = @Password", new { login.UserName, login.Password }).FirstOrDefault();
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public Usuario GetUsuario(int id)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Query<Usuario>
                            (@"SELECT 
	                            PRTL.IDUSUPRTL AS IdUsuario,
	                            CTT.NOMECONTATO AS Nome,
	                            PRTL.LGNUSU AS Login,
	                            CTT.EMAIL AS Email,
                                CTT.TELEFONE AS Telefone,
                                CTT.CODCONTATO as CodContato
	                            FROM AD_USUPRTL PRTL
	                            INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC
                                WHERE PRTL.IDUSUPRTL = @id", new { id }).First();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }
        

        public Permisoes GetPermisoes(int id)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Query<Permisoes>
                        (@"SELECT 
                            PRTL.IDUSUPRTL as IdUsuario,
                            PRTL.ALTPSW AS AltPassword,
                            PRTL.PRFLUSU AS Perfil,
                            PRTL.RGSTOCOR AS RgtOcorrencia,
                            STUFF((SELECT ', ' + RTRIM(CON.CODPARCAT) AS [text()]
                            FROM AD_USUPRTLCON CON
                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
                            FOR XML PATH
                            ('')), 1, 1, '' ) AS ClienteAt,
                            STUFF((SELECT ', ' + RTRIM(CON.CODPARCAB) AS [text()]
                            FROM AD_USUPRTLCON CON
                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
                            FOR XML PATH('')), 1, 1, '' ) AS ClienteAb,
                            STUFF((SELECT ', ' + RTRIM(CON.NUMCONTRATO) AS [text()]
                            FROM AD_USUPRTLCON CON
                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
                            FOR XML PATH('')), 1, 1, '' ) AS Contratos
                            FROM AD_USUPRTL PRTL
                            WHERE PRTL.IDUSUPRTL = @id", new { id }).First();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public IEnumerable<Usuario> GetAll(string filter)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Query<Usuario>(filter).ToList();
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