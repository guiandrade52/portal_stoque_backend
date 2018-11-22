namespace PortalStoque.API.Models.Ocorrencias
{
    public class Ocorrencia
    {
        public int ExecutionId { get; set; }
        public string Situacao { get; set; }
        public string Origem { get; set; }
        public string Responsavel { get; set; }
        public string DataTr { get; set; }
        public string DataTs { get; set; }
        public string DataCr { get; set; }
        public string Serie { get; set; }
        public string UserPortal { get; set; }
        public string ClienteAt { get; set; }
        public string Contato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Classificacao { get; set; }
        public string TipoOcorrencia { get; set; }
        public string GrupoServico { get; set; }
        public string Servico { get; set; }
        public string Descricao { get; set; }
        public string Produto { get; set; }
        public int idSituacao { get; set; }
    }
}