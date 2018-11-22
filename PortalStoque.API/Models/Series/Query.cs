using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Series
{
    public class Query
    {
        public static string GetFilter(Permisoes usuario)
        {
            string _where = "WHERE 1 = 1";

            return _where;
        }
    }
}