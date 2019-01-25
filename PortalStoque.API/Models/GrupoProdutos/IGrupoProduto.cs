using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.GrupoProdutos
{
    interface IGrupoProduto
    {
        IEnumerable<GrupoProduto> GetAll(int contrato);
    }
}