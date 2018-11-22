using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Contratos
{
    public class Query
    {
        public static string GetFilter(Permisoes usuario)
        {
            string query = @"WHERE 1 = 1
                    AND CON.NUMCONTRATO <> 0
                    AND CON.ATIVO = 'S'";

            return query;
        }
    }
}