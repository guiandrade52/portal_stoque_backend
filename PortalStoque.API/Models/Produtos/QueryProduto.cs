using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Produtos
{
    public class QueryProduto
    {
        static public string GetFilter(int contrato, int? codGrupo)
        {
            string _where = "WHERE 1 = 1 AND EQP.SITUACAO = 'A'";

            if (contrato > 0)
                _where += string.Format("AND EQP.NUMCONTRATO = {0}", contrato);
            if (codGrupo > 0)
                _where += string.Format("AND GRU.CODGRUPOPROD = {0}", codGrupo);
            return _where;
        }
    }
}