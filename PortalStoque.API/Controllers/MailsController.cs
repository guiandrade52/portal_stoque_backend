using Newtonsoft.Json.Linq;
using PortalStoque.API.Models.Formularios;
using PortalStoque.API.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class MailsController : ApiController
    {
        static readonly IMailRepositorio mailRepositorio = new MailRepositorio();
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
            return Request.CreateResponse(HttpStatusCode.OK);
            //if (mailRepositorio.SendMail(mail))
            //    return Request.CreateResponse(HttpStatusCode.OK);
            //else
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
