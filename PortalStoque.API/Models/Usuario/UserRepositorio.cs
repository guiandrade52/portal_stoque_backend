using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuario
{
    public class UserRepositorio : IUserRepositorio
    {
        public bool ValidaUsuario(string pLogin, string pSenha)
        {
            if (pLogin == "fagner.gomes" && pSenha == "alfa@4721")
            {
                return true;
            }
            else
                return false;
        }

        public Usuario GetUsuario(string pLogin, string pSenha)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    if (!string.IsNullOrWhiteSpace(pLogin) && !string.IsNullOrWhiteSpace(pSenha))
                    {
                        var password = pSenha;//CriptoHelper.HashMD5(pPassword);
                        pLogin.ToLower();

                        var usuario = conexao.Query<Usuario>
                            (@"SELECT PRTL.IDUSUPRTL AS IdUsuario,
	                            CTT.NOMECONTATO AS Nome,
	                            PRTL.LGNUSU AS Login,
	                            PRTL.PRFLUSU AS Perfil,
	                            CTT.EMAIL AS Email,
                                CTT.TELEFONE AS Telefone,
	                            PRTL.ALTPSW AS AltPassword,
	                            PRTL.RGSTOCOR AS RgtOcorrencia,
	                            PRTL.CODPARC AS CodParc,
	                            PRTL.CODCONTATO AS CodContato,
	                            STUFF((SELECT ', ' + RTRIM(CON.CODPARCAT) AS [text()]
	                            FROM AD_USUPRTLCON CON
	                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
	                            FOR XML PATH('')), 1, 1, '' ) AS CodParcAt,
	                            STUFF((SELECT ', ' + RTRIM(CON.CODPARCAB) AS [text()]
	                            FROM AD_USUPRTLCON CON
	                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
	                            FOR XML PATH('')), 1, 1, '' ) AS CodParcAb,
	                            STUFF((SELECT ', ' + RTRIM(CON.NUMCONTRATO) AS [text()]
	                            FROM AD_USUPRTLCON CON
	                            WHERE CON.IDUSUPRTL = PRTL.IDUSUPRTL
	                            FOR XML PATH('')), 1, 1, '' ) AS NumContrato
	                            FROM AD_USUPRTL PRTL
	                            INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC
                                WHERE PRTL.LGNUSU = @pLogin AND PRTL.PSWUSU = @password", new { pLogin, password }).ToList();
                        if (usuario.Count > 0)
                            return usuario.First();
                        else
                            return null;
                    }
                    else
                        return null;
                }
            }
            catch (Exception e)
            {
                //Log.LogWrite("ValidaUsuario", e.Message);
                return null;
            }
        }
    }
}