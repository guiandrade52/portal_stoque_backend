using System.Collections.Generic;

namespace PortalStoque.API.Models.Produtos
{
    interface IProdutoRepositorio
    {
        IEnumerable<Produto> GetAll(string filter);
    }
}