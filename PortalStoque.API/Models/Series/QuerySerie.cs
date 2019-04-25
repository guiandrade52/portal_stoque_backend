using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Series
{
    public class QuerySerie
    {
        public static string GetFilter(Permisoes permisao, string search)
        {
            string _where = "WHERE EQP.CONTROLE IS NOT NULL AND EQP.SITUACAO = 'A'";
            if (!string.IsNullOrEmpty(search))
            {
                _where = string.Format("{0} AND SERIE.CONTROLEFAB LIKE '{1}%' OR EQP.CONTROLE LIKE'{1}%'", _where, search);
            }

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.Contratos))
                    _where = string.Format("{0} AND EQP.CODPARC IN({1}) AND EQP.NUMCONTRATO IN({2})", _where, permisao.ClienteAb, permisao.Contratos);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }

        public static string GetFilterSerProd(int contrato, int codProd, int codGrupo)
        {
            return string.Format(@"	WHERE 1 = 1
                                    AND EQP.SITUACAO = 'A'
                                    AND EQP.NUMCONTRATO = {0}
                                    AND GRU.CODGRUPOPROD = {1}
                                    AND PRO.CODPROD = {2}", contrato, codGrupo, codProd);           
        }
    }
}