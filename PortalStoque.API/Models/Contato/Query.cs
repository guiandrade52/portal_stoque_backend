using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Contato
{
    public class Query
    {
        public static string GetFilter(Permisoes permisao)
        {
            string _where = "WHERE 1 = 1 ";

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format("{0} AND CTT.CODPARC IN ({0})", _where, permisao.ClienteAb);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }
    }
}