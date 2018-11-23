using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Series
{
    public class QuerySerie
    {
        public static string GetFilter(Permisoes permisao)
        {
            string _where = "WHERE EQP.CONTROLE IS NOT NULL ";
            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format("{0} AND EQP.CODPARC IN({1}) AND EQP.NUMCONTRATO IN({2})", _where, permisao.ClienteAb, permisao.NumContrato);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }
    }
}