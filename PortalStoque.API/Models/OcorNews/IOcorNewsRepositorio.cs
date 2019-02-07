using System.Data;

namespace PortalStoque.API.Models.OcorNews
{
    interface IOcorNewsRepositorio
    {
        string GetJsonFormattedSave(Ocor ocor);
        string GetNuOcorrencia(string json);
        DataTable GetDataTable(string query);
        void Update(string query);
        void Insert(string query);
        string Message(string originalMessage);
    }
}