using System.Collections.Generic;

namespace PortalStoque.API.Models.Contratos
{
    interface IContratoRepositorio
    {
        IEnumerable<Contrato> GetAll(string filter);
    }
}