using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.GrupoProdutos
{
    public class QueryGrupoProduto
    {
        static public string GetFilter(Permisoes permisoes)
        {
            string _where = "WHERE 1 = 1 AND EQP.SITUACAO = 'A' ";

            if(permisoes.Perfil == "G" || permisoes.Perfil == "T")
                _where += string.Format("AND EQP.CODPARC = 1");
            else if (string.IsNullOrWhiteSpace(permisoes.Contratos))
                _where += string.Format("AND EQP.NUMCONTRATO = {0}", permisoes.Contratos);
            
            return _where;
        }

        static public string GetFilterContrato(int contrato)
        {
            string _where = "WHERE 1 = 1 AND EQP.SITUACAO = 'A' ";

            if (contrato > 0)
                _where += string.Format("AND EQP.NUMCONTRATO = {0}", contrato);
            return _where;
        }
    }
}