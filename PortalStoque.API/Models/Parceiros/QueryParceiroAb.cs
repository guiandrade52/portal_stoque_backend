using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Parceiros
{
    public class QueryParceiroAb
    {
        public static string GetFilter(Permisoes permisao, string search)
        {
            string _where = "WHERE PAR.CODPARC <> 0 AND PAR.NOMEPARC IS NOT NULL ";
            int numero = 0;

            if (!string.IsNullOrWhiteSpace(search))
                if (int.TryParse(search, out numero))
                    _where = string.Format("{0} AND PAR.CODPARC IN ({1}) ", _where, search);
                else
                    _where = string.Format("{0} AND PAR.NOMEPARC LIKE '{1}%' ", _where, search);

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.Contratos))
                    _where = string.Format("{0} AND PAR.CODPARC IN ({1})", _where, permisao.ClienteAb);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }
    }
}