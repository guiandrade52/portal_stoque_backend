namespace PortalStoque.API.Models.Usuarios
{
    public class QueryUsuario
    {
        public static string GetFilter(Permisoes permisao, string search)
        {
            string _where = "";

            

            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.Contratos))
                    _where = string.Format(@"SELECT TOP 100
	                                            PRL.IDUSUPRTL AS IdUsuario, 
	                                            PRL.NOMEUSU AS Nome
                                            FROM AD_USUPRTL PRL
                                            INNER JOIN AD_USUPRTLCON PARCON WITH(NOLOCK) ON PARCON.IDUSUPRTL = PRL.IDUSUPRTL
                                            WHERE PRL.NOMEUSU IS NOT NULL
                                            AND PARCON.NUMCONTRATO IN ({0})
                                            ", permisao.Contratos);
            }
            else
            {
                _where = string.Format(@"SELECT TOP 100
                                            PRL.IDUSUPRTL AS IdUsuario, 
                                            PRL.NOMEUSU AS Nome
                                        FROM AD_USUPRTL PRL
                                        WHERE PRL.NOMEUSU IS NOT NULL");
            }

            if (!string.IsNullOrEmpty(search))
            {
                _where = string.Format("{0} AND PRL.NOMEUSU LIKE '{1}%' ", _where, search);
            }
            return _where;
        }
    }
}