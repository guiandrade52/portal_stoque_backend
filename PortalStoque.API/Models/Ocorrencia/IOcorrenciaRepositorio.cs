using PortalStoque.API.Models.Usuario;
using System.Collections.Generic;

namespace PortalStoque.API.Models.Ocorrencia
{
    public interface IOcorrenciaRepositorio
    {
        IEnumerable<Ocorrencia> GetAll(string filter, int pagina, int tamPag);
        int GetTotalOcor(string filter);
        Ocorrencia Get(int id);
    }
}