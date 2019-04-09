using System.Data;

namespace PortalStoque.API.Models.Relatorios
{
    interface IRelatorioRepositorio
    {
        DataTable GetOcorrencia(string filtro);
    }
}