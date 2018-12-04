using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Servicos
{
    public class QueryServico
    {
        public static string GetFilter(Permisoes permisao)
        {

            string _where = @"SELECT
                                CODPROD AS CodServico,
                                DESCRPROD AS Nome
                                FROM TGFPRO
                                WHERE CODGRUPOPROD = 20400";

            

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format(@"SELECT DISTINCT  
	                                            PRO.CODPROD AS CodServico,
	                                            PRO.DESCRPROD AS Nome
                                             FROM AD_STOSRVCONT CONT
                                             INNER JOIN TGFPRO PRO ON CONT.CODPROD = PRO.CODPROD
                                            WHERE CONT.NUMCONTRATO IN({0})", permisao.NumContrato);
            }

            return _where;
        }
    }
}