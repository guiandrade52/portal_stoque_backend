using PortalStoque.API.Models.Formularios;
using PortalStoque.API.Models.Mail;
using PortalStoque.API.Models.ResetPassword;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class MailsController : ApiController
    {
        static readonly IMailRepositorio _mailRepositorio = new MailRepositorio();
        static readonly IResetRepositorio _resetRepositorio = new ResetRepositorio();

        [HttpPost]
        [Route("api/NovoColaborador")]
        public HttpResponseMessage NovoColaborador(NovoColaboradorModel novoColaborador)
        {
            MailMessage mail = new MailMessage();

            string body = novoColaborador.GetBody(novoColaborador);

            mail.From = new MailAddress(Properties.Settings.Default.SmtpFrom);
            mail.To.Add(Properties.Settings.Default.SmtpTo);
            //mail.CC.Add(pMail.CadEmail);
            mail.Bcc.Add(Properties.Settings.Default.SmtpFrom);
            mail.Subject = string.Format("Solicitação de novo Colaborador");
            mail.Body = body;
            mail.IsBodyHtml = true;
            if (_mailRepositorio.SendMail(mail))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("api/NovoUsuario")]
        public HttpResponseMessage NovoUsuario(CadastroDeUsuario cadastro)
        {
            MailMessage mail = new MailMessage();

            string body = cadastro.GetBody(cadastro);

            mail.From = new MailAddress(Properties.Settings.Default.SmtpFrom);
            mail.To.Add(Properties.Settings.Default.SmtpTo);
            //mail.CC.Add(pMail.CadEmail);
            mail.Bcc.Add(Properties.Settings.Default.SmtpFrom);
            mail.Subject = string.Format("Solicitação de novo usuário portal");
            mail.Body = body;
            mail.IsBodyHtml = true;
            if (_mailRepositorio.SendMail(mail))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
