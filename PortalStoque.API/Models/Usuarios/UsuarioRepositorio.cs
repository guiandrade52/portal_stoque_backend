using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;

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
                                WHERE PRTL.LGNUSU = @UserName AND PRTL.PSWUSU = @Password", new { login.UserName, login.Password }).First();
                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
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
                                CTT.TELEFONE AS Telefone
	                            FROM AD_USUPRTL PRTL
	                            INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC
                                WHERE PRTL.IDUSUPRTL = @id", new { id }).First();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Usuário. " + e.Message);
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
                            FOR XML PATH('')), 1, 1, '' ) AS NumContrato
                            FROM AD_USUPRTL PRTL
                            WHERE PRTL.IDUSUPRTL = @id", new { id }).First();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Erro ao tentar recuperar Permissões do Usuário. " + e.Message);
            }
        }
    }
}