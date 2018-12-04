using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Parceiros
{
    interface IParceiroRepositorio
    {
        IEnumerable<Parceiro> GetAll(string filter);
    }
}