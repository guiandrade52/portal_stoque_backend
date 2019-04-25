using PortalStoque.API.Controllers.services;
using System;
using System.Net.Mail;

namespace PortalStoque.API.Models.Mail
{
    public class MailRepositorio : IMailRepositorio
    {
        public bool SendMail(MailMessage mail)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Properties.Settings.Default.SmtpHost;
                smtp.Port = Properties.Settings.Default.SmtpPorta;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SmtpFrom, Properties.Settings.Default.SmtpPassword);// Login e senha do e-mail.
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Logger.writeLog(ex.Message);
                return false;
            }
        }
    }
}