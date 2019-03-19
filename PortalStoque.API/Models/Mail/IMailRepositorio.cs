using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PortalStoque.API.Models.Mail
{
    interface IMailRepositorio
    {
        bool SendMail(MailMessage mail);
    }
}