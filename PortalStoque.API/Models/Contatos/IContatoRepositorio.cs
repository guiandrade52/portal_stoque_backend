using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Contatos
{
    interface IContatoRepositorio
    {
        IEnumerable<Contato> GetAll(string filter);
        IEnumerable<Contato> GetComContrato(string filter);
        IEnumerable<Contato> GetComSerie(string filter);
    }
}