using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Ocorrencia
{
    public interface IOcorrenciaRepositorio
    {
        IEnumerable<Ocorrencia> GetAll(int pPagina, int pTamPag, string pWhere);
        Ocorrencia Get(int id);
        int TotalOcorrencia(string pWhere);
    }
}