using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Ocorrencias
{
    public class Query
    {
        public static string GetFilter(Filter filter, Permisoes permisao, Usuario usuario)
        {
            string _where = "WHERE 1 = 1";

            switch (permisao.Perfil)
            {
                case "G":// Visualiza todas ocorrências além de ter premissões para administrar contratos
                    _where = string.Format("{0} {1}", _where, Filter(filter)); 
                    break;
                case "T":// Visualiza todas ocorrências
                    _where = string.Format("{0} {1}", _where, Filter(filter));
                    break;
                case "C":// Visualiza somente ocorrências do contrato previamente cadastrado
                    if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.NumContrato))
                        _where = string.Format("{0} AND OCO.CODPARC IN ({1}) AND OCO.NUMCONTRATO IN({2}) {3}", _where, permisao.ClienteAb, permisao.NumContrato, Filter(filter));
                    else
                        _where = "AND OCO.CODPARC IN (-1)";
                    break;
                case "CO": // Visualiza todas ocorrências abertas pelo usuário portal logado
                    _where = string.Format(@"{0} AND OCO.IDUSUPRTL = {1} {2}", _where, usuario.IdUsuario, Filter(filter));
                    break;
                default:
                    return "Error";
            }
            return _where;
        }


        private static string Filter(Filter filter)
        {
            string _where = "";
            int numero = 0;

            if (!string.IsNullOrWhiteSpace(filter.SearchMultiple))
                if (int.TryParse(filter.SearchMultiple, out numero))
                    _where += string.Format(" AND OCO.EXECUTIONID = {0} ", filter.SearchMultiple);
                else
                    _where += string.Format(" AND  PAR.NOMEPARC LIKE ('{0}%') OR OCO.CONTROLE LIKE ('{0}%') ", filter.SearchMultiple);


            if (filter.DateInit != null)
                _where += string.Format(" AND OCO.DHCHAMADA >= '{0}' ", filter.DateInit);
            //_where += string.Format(" AND OCO.DHCHAMADA >= CONVERT(VARCHAR,'{0}',103) ", DateTime.Parse(Convert.ToString(filtro.DataInit)).ToString("yyyy-MM-dd HH:mm:ss"));

            if (filter.DateFinal != null)
            {
                var data = filter.DateFinal.ToString();
                var dataformat = data.Split(' ');
                data = dataformat[0] + " 23:59:59";
                _where += string.Format(" AND OCO.DHCHAMADA <= '{0}' ", data);
                //_where += string.Format(" AND OCO.DHCHAMADA <= CONVERT(VARCHAR,'{0}',103) ", DateTime.Parse(Convert.ToString(filtro.DataFinal)).ToString("yyyy-MM-dd 23:59:59"));
            }

            if (!string.IsNullOrWhiteSpace(filter.Contrato))
                _where += string.Format(" AND OCO.NUMCONTRATO IN ({0}) ", filter.Contrato);

            if (!string.IsNullOrWhiteSpace(filter.Serie))
                _where += string.Format(" AND OCO.CONTROLE = '{0}' ", filter.Serie);

            if (!string.IsNullOrWhiteSpace(filter.Servico))
                _where += string.Format(" AND PROD.CODPROD IN ({0}) ", filter.Servico);

            if (!string.IsNullOrWhiteSpace(filter.UsuarioPortal))
                _where += string.Format(" AND OCO.IDUSUPRTL IN ({0}) ", filter.UsuarioPortal);

            if (!string.IsNullOrWhiteSpace(filter.ClienteAt))
                _where += string.Format(" AND OCO.CODPARCCON IN ({0}) ", filter.ClienteAt);

            if (!string.IsNullOrWhiteSpace(filter.ClienteAb))
                _where += string.Format(" AND OCO.CODPARC IN ({0}) ", filter.ClienteAb);

            return _where;
        }
    }
}