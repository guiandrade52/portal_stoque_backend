namespace PortalStoque.API.Models.Series
{
    public class SerieParcCon
    {
        public string Serie { get; set; }
        public string Situacao { get; set; }
        public string Produto { get; set; }
        public int Contrato { get; set; }
        public int CodParcCon { get; set; }
        public string NomeParcCon { get; set; }
        public int CodParcAtendido { get; set; }
        public string NomeParcAtendido { get; set; }
    }
}