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
                        login.UserName.ToLower();

                        return conexao.Query<Login>
                            (@"SELECT 
	                            PRTL.IDUSUPRTL AS IdUsuario,
	                            CTT.NOMECONTATO AS Nome,
	                            PRTL.LGNUSU AS UserName,
                                PRTL.ATIVO
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
                                PRTL.CODPARC AS CodParc,
	                            CTT.NOMECONTATO AS Nome,
	                            PRTL.LGNUSU AS UserName,
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
                            PRTL.PASSCRIPT AS PassCript,
	                        PRTL.CLIENTEINTERNO as ClienteInterno,
	                        PRTL.ALTPSW AS AltPassword,
	                        PRTL.PRFLUSU AS Perfil,
	                        PRTL.RGSTOCOR AS RgtOcorrencia,
	                        STUFF((SELECT ', ' + RTRIM(CON.CODPARCAT) AS [text()]
	                        FROM AD_USUPRTLCON CON
	                        WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
	                        FOR XML PATH('')), 1, 1, '' ) AS ClienteAt,
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
        public IEnumerable<ParcConConfigs> GetParcCon(int idUsuario)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    return conexao.Query<ParcConConfigs>
                            (@"SELECT IDUSUPRTL AS idUsuario,
	                               CODPARCAT AS CodParcAt,
	                               CODPARCAB AS CodParcAb,
	                               NUMCONTRATO AS Contrato
	                            FROM AD_USUPRTLCON WHERE IDUSUPRTL = @idUsuario", new { idUsuario }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                throw ex;
            }
        }

        public bool UpdateData(Usuario usuario)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {

                    var result = conexao.Execute
                         (@"UPDATE TGFCTT 
                                SET NOMECONTATO = @Nome, 
                                TELEFONE = @Telefone, 
                                EMAIL = @Email 
                            WHERE CODCONTATO = @CodContato
                            AND CODPARC = @CodParc", new { usuario.Nome, usuario.Telefone, usuario.Email, usuario.CodContato, usuario.CodParc });
                    if (result > 0)
                        return true;
                    return false;
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