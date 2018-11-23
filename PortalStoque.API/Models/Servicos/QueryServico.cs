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
            string _where = "WHERE PRO.CODGRUPOPROD <> 0 ";

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format(@"{0} AND PRO.CODPROD IN 
                                                (
                                                  SELECT CODPROD 
                                                    FROM BH_FTLEQP EQP 
                                                    WHERE EQP.NUMCONTRATO IN ({1}) )", _where, permisao.NumContrato);
            }
            return _where;
        }
    }
}