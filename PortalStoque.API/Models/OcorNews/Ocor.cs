using PortalStoque.API.Services;
using System.Text;

namespace PortalStoque.API.Models.OcorNews
{
    public class Ocor
    {
        public string Controle { get; set; }
        public string CodParc { get; set; }
        public string CodParcCon { get; set; }
        public string CodContato { get; set; }
        public string Contrato { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string CodEndereco { get; set; }
        public string Numero { get; set; }
        public string CodBairro { get; set; }
        public string CodCidade { get; set; }
        public string Complemento { get; set; }
        public string CodProduto { get; set; }
        public string Descricao{ get; set; }
        public int IdUsuarioPortal { get; set; }
        public string ProcessoRel { get; set; }
        public string Grupo { get; set; }
        public string CodServico { get; set; }


        private static SWServiceInvokerJson instance;
        public static SWServiceInvokerJson Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SWServiceInvokerJson(Properties.Settings.Default.SankhyaURL,
                                                        Properties.Settings.Default.SankhyaUser,
                                                        Properties.Settings.Default.SankhyaPassword);
                }
                return instance;
            }
        }

        private string DecodeIso(string text)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(text);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = iso.GetString(isoBytes);

            return msg;
        }


    }
}