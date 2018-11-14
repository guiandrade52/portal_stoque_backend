using Dapper;
using PortalStoque.API.Models.Usuario;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PortalStoque.API.Models.Services
{
    public class Auth
    {
        public static bool AuthService(Login login)
        {
            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["principal"].ConnectionString))
                {
                    if (!string.IsNullOrWhiteSpace(login.UserName) && !string.IsNullOrWhiteSpace(login.Password))
                    {
                        login.UserName.ToLower();
                        var password = login.Password;//CriptoHelper.HashMD5(pPassword);

                        var usuario = conexao.Query
                            (@"SELECT LGNUSU AS Login,PSWUSU AS password	
	                            FROM AD_USUPRTL
	                            WHERE LGNUSU = @UserName 
	                            AND PSWUSU = @password", new { login.UserName, password }).ToList();
                        if (usuario.Count > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}