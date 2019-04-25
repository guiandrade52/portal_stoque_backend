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
        [Route("api/SendMail")]
        public HttpResponseMessage NovoColaborador(Mail objMail)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(Properties.Settings.Default.SmtpFrom);
            mail.To.Add(Properties.Settings.Default.SmtpTo);
            //mail.Bcc.Add(Properties.Settings.Default.SmtpFrom);
            mail.Subject = objMail.Assunto;
            mail.Body = objMail.HTML;
            mail.IsBodyHtml = true;
            if (_mailRepositorio.SendMail(mail))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Encontramos um problema para enviar a mensagem, tente novamente mais tarde." });
        }
    }
}
