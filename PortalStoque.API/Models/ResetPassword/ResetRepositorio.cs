using Dapper;
using PortalStoque.API.Controllers.services;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.ResetPassword
{
    public class ResetRepositorio : IResetRepositorio
    {
        public UserReset ValidaLogin(string login)
        {
            try
            {
                if (!string.IsNullOrEmpty(login))
                    login.Trim().ToLower();
                else
                    return null;

                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    var result = conexao.Query<UserReset>(
                        @"SELECT PRTL.IDUSUPRTL AS IdUsuario
	                            ,CTT.NOMECONTATO AS Nome
	                            ,PRTL.LGNUSU AS Login
	                            ,CTT.EMAIL AS Email
	                            FROM AD_USUPRTL PRTL
	                            INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC 
	                            WHERE PRTL.LGNUSU = @login", new { login });
                    if (result.Count() > 0)
                        return result.First();
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

        public bool LogResetMail(int IdUsuario, int codigo)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    var result = conexao.Execute(@"INSERT INTO AD_CONFMAIL (IDUSUPRL, IDRESETMAIL, TIMESOLICIT) 
                                                    VALUES(@IdUsuario, @codigo, @time)", new { IdUsuario, codigo, time = DateTime.Now });
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public bool ValidaCodigo(int codigo, int idUsuario)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    var result = conexao.Query<UserReset>(@"SELECT IDUSUPRL AS IdUsuario
	                                                                FROM AD_CONFMAIL 
                                                                    WHERE IDRESETMAIL = @codigo 
	                                                                AND IDUSUPRL = @idUsuario
                                                                    AND GETDATE() < DATEADD(n , +5, TIMESOLICIT)", new { codigo, idUsuario });

                    if (result.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public bool UpdatePassword(int idUsuario, int codigo, string password)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    try
                    {
                        //CriptoHelper.HashMD5(pPassword);
                        conexao.Execute(@"UPDATE AD_USUPRTL 
                                                    SET PSWUSU = @password, ALTPSW = 'N', PASSCRIPT = 1 
                                                    WHERE IDUSUPRTL = @idUsuario ", new { password, idUsuario });

                        if (codigo > 0 && codigo != 34653670)
                            conexao.Execute(@"DELETE AD_CONFMAIL WHERE IDRESETMAIL = @codigo", new { codigo });

                        conexao.Execute(@"DELETE AD_CONFMAIL WHERE GETDATE() > DATEADD(n , +5, TIMESOLICIT)");

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.writeLog(ex.Message);
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public bool ClearCodigo(int codigo, int idUsuario)
        {
            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
            {
                try
                {
                    if (codigo > 0)
                        conexao.Execute(@"DELETE AD_CONFMAIL WHERE IDRESETMAIL = @codigo AND IDUSUPRL = @idUsuario", new { codigo, idUsuario });

                    conexao.Execute(@"DELETE AD_CONFMAIL WHERE GETDATE() > DATEADD(n , +5, TIMESOLICIT)");
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.writeLog(ex.Message);
                    throw ex;
                }
            }
        }
            }
}