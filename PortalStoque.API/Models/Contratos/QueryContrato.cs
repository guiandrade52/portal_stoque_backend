using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Contratos
{
    public class QueryContrato
    {
        public static string GetFilter(Permisoes permisao)
        {
            string _where = @"WHERE CON.NUMCONTRATO <> 0 AND CON.ATIVO = 'S' ";

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                    _where = string.Format("{0} AND PAR.CODPARC IN ({1}) AND CON.NUMCONTRATO IN({2})", _where, permisao.ClienteAb, permisao.NumContrato);
                else
                    _where = "AND PAR.CODPARC IN (-1)";
            }
            return _where;
        }
    }
}