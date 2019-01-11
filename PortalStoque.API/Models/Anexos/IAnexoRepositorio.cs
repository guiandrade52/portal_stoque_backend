using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Anexos
{
    interface IAnexoRepositorio
    {
        IEnumerable<Anexo> GetAnexo(int executionId);
    }
}