using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Contatos
{
    public class QueryContato
    {
        public static string GetFilter(Permisoes permisao, string search)
        {
            string _where = "WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(search))
            {
                _where = string.Format("{0} AND CTT.NOMECONTATO LIKE '{1}%' ", _where, search);
            }

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format("{0} AND CTT.CODPARC IN ({1})", _where, permisao.ClienteAb);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }

        public static string GetFilterSemSerie(int codParc, int contrato)
        {
            return string.Format(@"WHERE 1= 1
                                    AND CTT.CODPARC = {0}
                                    AND CON.NUMCONTRATO = {1}",codParc, contrato );
        }

        public static string getFilterComSerie(string serie)
        {
            return string.Format(@"	WHERE 1 = 1
                                    AND EQP.SITUACAO = 'A'
                                    AND EQP.CONTROLE = '{0}'", serie);
        }
    }
}