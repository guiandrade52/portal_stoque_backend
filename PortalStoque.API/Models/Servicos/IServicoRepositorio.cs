using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Servicos
{
   interface IServicoRepositorio
    {
        IEnumerable<Servico> GetAll(string filter);
    }
}