namespace PortalStoque.API.Models.SolucaoProposta
{
    interface ISolucaoRepositorio
    {
       Solucao GetSolucaos(int executionId);
    }
}