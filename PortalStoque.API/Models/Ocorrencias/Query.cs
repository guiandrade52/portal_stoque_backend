using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Ocorrencias
{
    public class Query
    {
        public static string GetFilter(Filter filter, Usuario user)
        {
            string where = "WHERE 1 = 1";

            switch (user.Perfil)
            {
                case "G":
                    where = string.Format("{0} {1}", where, Filter(filter));
                    break;
                case "C":
                    if (!string.IsNullOrEmpty(user.ClienteAb) && !string.IsNullOrEmpty(filter.Contrato))
                        where = string.Format("{0} AND OCO.CODPARC IN ({1}) AND OCO.NUMCONTRATO IN({2}) {3}", where, filter.ClienteAb, filter.Contrato, Filter(filter));
                    else
                        where = "AND OCO.CODPARC IN (-1)";
                    break;
                case "T":
                    where = string.Format("WHERE 1 = 1 AND OCO.CODPARC IN ({0}) AND OCO.NUMCONTRATO IN ({1}) {2}", filter.ClienteAb, filter.Contrato, Filter(filter));
                    break;
                case "CO":
                    where = string.Format(@"WHERE 1 = 1 
                                            AND OCO.CODPARC IN ({0}) 
                                            AND OCO.IDUSUPRTL = {1} 
                                            AND OCO.NUMCONTRATO IN ({2})
                                            {3}", filter.ClienteAb, filter.UsuarioPortal, filter.Contrato, Filter(filter));
                    break;
                default:
                    return "Error, query irá gerar erro.";
            }
            return where;
        }


        private static string Filter(Filter filter)
        {
            string where = "";
            int numero = 0;

            if (!string.IsNullOrWhiteSpace(filter.SearchMultiple))
                if (int.TryParse(filter.SearchMultiple, out numero))
                    where += string.Format(" AND OCO.EXECUTIONID = {0} ", filter.SearchMultiple);
                else
                    where += string.Format(" AND  PAR.NOMEPARC LIKE ('{0}%') OR OCO.CONTROLE LIKE ('{0}%') ", filter.SearchMultiple);


            if (filter.DateInit != null)
                where += string.Format(" AND OCO.DHCHAMADA >= '{0}' ", filter.DateInit);
            //where += string.Format(" AND OCO.DHCHAMADA >= CONVERT(VARCHAR,'{0}',103) ", DateTime.Parse(Convert.ToString(filtro.DataInit)).ToString("yyyy-MM-dd HH:mm:ss"));

            if (filter.DateFinal != null)
            {
                var data = filter.DateFinal.ToString();
                var dataformat = data.Split(' ');
                data = dataformat[0] + " 23:59:59";
                where += string.Format(" AND OCO.DHCHAMADA <= '{0}' ", data);
                //where += string.Format(" AND OCO.DHCHAMADA <= CONVERT(VARCHAR,'{0}',103) ", DateTime.Parse(Convert.ToString(filtro.DataFinal)).ToString("yyyy-MM-dd 23:59:59"));
            }

            if (!string.IsNullOrWhiteSpace(filter.Contrato))
                where += string.Format(" AND OCO.NUMCONTRATO IN ({0}) ", filter.Contrato);

            if (!string.IsNullOrWhiteSpace(filter.Serie))
                where += string.Format(" AND OCO.CONTROLE = '{0}' ", filter.Serie);

            if (!string.IsNullOrWhiteSpace(filter.Servico))
                where += string.Format(" AND PROD.CODPROD IN ({0}) ", filter.Servico);

            if (!string.IsNullOrWhiteSpace(filter.UsuarioPortal))
                where += string.Format(" AND OCO.IDUSUPRTL IN ({0}) ", filter.UsuarioPortal);

            if (!string.IsNullOrWhiteSpace(filter.ClienteAt))
                where += string.Format(" AND OCO.CODPARCCON IN ({0}) ", filter.ClienteAt);

            if (!string.IsNullOrWhiteSpace(filter.ClienteAb))
                where += string.Format(" AND OCO.CODPARC IN ({0}) ", filter.ClienteAb);

            return where;
        }
    }
}