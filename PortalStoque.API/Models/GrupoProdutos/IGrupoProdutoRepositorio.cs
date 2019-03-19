using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.GrupoProdutos
{
    interface IGrupoProdutoRepositorio
    {
       IEnumerable<GrupoProduto> GetAll(string filter);
    }
}