using PortalStoque.API.Models.Usuarios;

namespace PortalStoque.API.Models.Servicos
{
    public class QueryServico
    {
        public static string GetFilter(string contrato, Permisoes permisao)
        {

            string _where = @"SELECT
                                CODPROD AS CodServico,
                                DESCRPROD AS Nome
                                FROM TGFPRO
                                WHERE CODGRUPOPROD = 20400";



            if (permisao.Perfil == "C" || permisao.Perfil == "CO")
            {
                if (!string.IsNullOrEmpty(permisao.ClienteAb) && !string.IsNullOrEmpty(permisao.Contratos))
                    _where = string.Format(@"SELECT DISTINCT  
	                                            PRO.CODPROD AS CodServico,
	                                            PRO.DESCRPROD AS Nome
                                             FROM AD_STOSRVCONT CONT
                                             INNER JOIN TGFPRO PRO ON CONT.CODPROD = PRO.CODPROD
                                            WHERE CONT.NUMCONTRATO IN({0})", contrato);
            }

            return _where;
        }

        public static string GetFilterPContrato(string contrato)
        {
            return string.Format(@"SELECT DISTINCT  
	                                    PRO.CODPROD AS CodServico,
	                                    PRO.DESCRPROD AS Nome
                                        FROM AD_STOSRVCONT CONT
                                        INNER JOIN TGFPRO PRO ON CONT.CODPROD = PRO.CODPROD
                                    WHERE CONT.NUMCONTRATO IN({0})", contrato);
        }
    }
}