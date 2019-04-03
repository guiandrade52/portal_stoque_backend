using System.Collections.Generic;

namespace PortalStoque.API.Models.Contratos
{
    interface IContratoRepositorio
    {
        IEnumerable<Contrato> GetAll(string filter);
        ContratoDetails GetDetails(int contrato);
        int DeleteContrato(int idUsuario, int contrato, int codParc);
        int SalvarContrato(int idUsuario, int codPar, int contrato, int codParAt);
        IEnumerable<CadContrato> ListaContratosPUsuario(int idUsuario);
        int DeleteAllContrato(int idUsuario);
    }
}