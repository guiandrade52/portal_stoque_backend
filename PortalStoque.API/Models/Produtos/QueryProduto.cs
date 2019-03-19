using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Produtos
{
    public class QueryProduto
    {
        static public string GetFilter( int codGrupo, int? contrato, Permisoes permisoes )
        {
            string _where = "WHERE 1 = 1 AND EQP.SITUACAO = 'A' ";

            if(contrato > 0)
                _where += string.Format("AND EQP.NUMCONTRATO = {0}", contrato);

            else if (permisoes.Perfil == "C" || permisoes.Perfil == "CO")
                _where += string.Format("AND EQP.NUMCONTRATO IN ({0})", permisoes.Contratos);

            if (codGrupo > 0)
                _where += string.Format("AND GRU.CODGRUPOPROD = {0}", codGrupo);
            return _where;
        }
    }
}