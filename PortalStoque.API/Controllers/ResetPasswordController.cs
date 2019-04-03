using PortalStoque.API.Models.Mail;
using PortalStoque.API.Models.ResetPassword;
using PortalStoque.API.Models.Usuarios;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class ResetPasswordController : ApiController
    {
        static readonly IResetRepositorio _resetRepositorio = new ResetRepositorio();
        static readonly IUsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        static readonly IMailRepositorio _mailRepositorio = new MailRepositorio();

        [HttpPost]
        public HttpResponseMessage ValidaCodigo(int codigo, int idUsuario)
        {
            if (_resetRepositorio.ValidaCodigo(codigo, idUsuario))
                return Request.CreateResponse(HttpStatusCode.OK, new { status = true, codigo });
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "O código expirou ou está incorreto, gentileza solicitar um novo." });
        }

        [HttpPost]
        public HttpResponseMessage ValidaLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "Login não pode ser nulo." });

            var result = _resetRepositorio.ValidaLogin(login);
            if (result != null)
            {
                Random generator = new Random();
                int r = generator.Next(1, 1000000);
                int codigo = Convert.ToInt32(r.ToString().PadLeft(6, '0'));

                if (_resetRepositorio.LogResetMail(result.IdUsuario, codigo))
                {
                    MailMessage mail = new MailMessage();
                    #region msgBody

                    string msgSaudacao;
                    if (DateTime.Now.Hour < 12)
                        msgSaudacao = "Tenha um lindo dia!";
                    else if (DateTime.Now.Hour < 17)
                        msgSaudacao = "Tenha uma ótima Tarde!";
                    else
                        msgSaudacao = "Tenha uma ótima noite!";

                    var nome = login.Substring(0, login.IndexOf("."));
                    nome = char.ToUpper(nome[0]) + nome.Substring(1);

                    var msgBody = string.Format(@"<div style='background-color:#f0f1f2;font-family:sans-serif;font-size:14px;line-height:1.3;margin:0;padding:0' bgcolor='#F0F1F2'>
    <table class='m_-1679786949994950497body' style='border:0;border-collapse:separate;border-spacing:0;width:100%;background-color:#f0f1f2' width='100%' bgcolor='#F0F1F2'>
        <tbody>
            <tr>
                <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>&nbsp;</td>
                <td class='m_-1679786949994950497container' style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top;display:block;max-width:500px;width:500px;margin:0 auto' valign='top' width='500'>
                    <div class='m_-1679786949994950497content' style='box-sizing:border-box;display:block;margin:0 auto;max-width:500px;padding-bottom:10px'>
                        <a href='http://www.stoque.com.br' title='www.stoque.com.br' target='_blank'>
                            <img src='http://www.stoque.com.br/wp-content/themes/stoque/img/stoque-logo.png' alt='NET - O mundo é dos NETS' border='0' style='display:block'>
                        </a>
                        <table class='m_-1679786949994950497main' style='border:0;border-collapse:separate;border-spacing:0;width:100%;background:#fff;border-radius:5px' width='100%'>
                            <tbody>
                                <tr>
                                    <td class='m_-1679786949994950497wrapper' style='font-family:sans-serif;font-size:14px;padding:32px;vertical-align:top;box-sizing:border-box' valign='top'>
                                        <table style='border:0;border-collapse:separate;border-spacing:0;width:100%' width='100%'>
                                            <tbody>
                                                <tr>
                                                    <td class='m_-1679786949994950497header' style='font-family:sans-serif;font-size:21px;padding:0;vertical-align:top;font-weight:normal;line-height:29px;padding-bottom:20px;text-align:center' valign='top' align='center'>
                                                        Olá {0}! Você esqueceu sua senha do Portal Stoque?
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>
                                                        <p style='color:#333;font-family:sans-serif;font-size:14px;font-weight:normal;margin:0;margin-bottom:15px;text-align:justify' align='justify'>Segue o código verificador para alteração da senha:</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>
                                                        <table class='m_-1679786949994950497btn' style='border:0;border-collapse:separate;border-spacing:0;width:100%;box-sizing:border-box' width='100%'>
                                                            <tbody>
                                                                <tr>
                                                                    <td align='left' style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top;padding-bottom:15px' valign='top'>
                                                                        <table class='m_-1679786949994950497btn-primary m_-1679786949994950497centered' style='border:0;border-collapse:separate;border-spacing:0;width:auto;margin-left:auto;margin-right:auto' width='auto'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top;border-radius:115px;text-align:center' valign='top' align='center'>
                                                                                        Código: <strong>{1}</strong>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>
                                                        <p style='color:#333;font-family:sans-serif;font-size:14px;font-weight:normal;margin:0;margin-bottom:15px;text-align:justify' align='justify'>
                                                            Sugerimos uma senha fácil de lembrar, mas que seja a mais forte possível. Tente não usar palavras que se encontram no dicionário, mas use uma combinação de letras maiúsculas e minúsculas, juntamente com números e/ou caracteres especiais.
                                                        </p>
                                                        <p style='color:#333;font-family:sans-serif;font-size:14px;font-weight:normal;margin:0;margin-bottom:15px;text-align:justify' align='justify'>Se você não solicitou a redefinição da senha, ignore este e-mail. Nenhuma alteração será realizada.</p>
                                                    </td>
                                                </tr>
                                                <tr class='m_-1679786949994950497ending' style='margin:0;margin-top:20px'>
                                                    <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>
                                                        <p style='color:#333;font-family:sans-serif;font-size:14px;font-weight:normal;margin:0;margin-bottom:15px;text-align:justify' align='justify'>
                                                            {2}<br>
                                                            <br>
                                                             Atenciosamente,<br>
                                                            Service Desk – Stoque Soluções Tecnológicas<br>
                                                            helpdesk@stoque.com.br<br/>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='m_-1679786949994950497from-ip' style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>
                                                        <p style='color:#424f59;font-family:sans-serif;font-size:13px;font-weight:normal;margin:0;margin-bottom:15px;text-align:justify;margin-top:15px' align='justify'>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>


                            </tbody>
                        </table>


                        <div class='m_-1679786949994950497footer' style='clear:both;margin-top:32px;text-align:center;width:100%' align='center' width='100%'>
                            <table style='border:0;border-collapse:separate;border-spacing:0;width:100%' width='100%'>
                                <tbody>
                                    <tr>
                                        <td class='m_-1679786949994950497content-block' style='font-family:sans-serif;font-size:12px;padding:0;vertical-align:top;padding-bottom:10px;padding-top:10px;color:#999;text-align:center' valign='top' align='center'>
                                            <span class='m_-1679786949994950497apple-link' style='color:#999;font-size:12px;text-align:center' align='center'>
                                                Enquanto estiver por aqui, confira o que está acontecendo em nossos canais sociais!
                                                <br>
                                            </span>
                                            <a href='https://www.facebook.com/StoqueBrasil' style='color:#0199ff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>Facebook</a>
                                            <a href='https://twitter.com/StoqueBrasil' style='color:#0199ff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>Twitter</a>
                                            <a href='https://www.youtube.com/channel/UCG5LMl3FsnbCQPja3fAWv7A' style='color:#0199ff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>YouTube</a>
                                            <a href='https://www.linkedin.com/company/stoque' style='color:#0199ff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>LinkedIn</a>
                                            <a href='http://www.stoque.com.br/' style='color:#0199ff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>WebSite</a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
                <td style='font-family:sans-serif;font-size:14px;padding:0;vertical-align:top' valign='top'>&nbsp;</td>
            </tr>
        </tbody>
    </table>
</div>

", nome, codigo, msgSaudacao);
                    #endregion

                    mail.From = new MailAddress(Properties.Settings.Default.SmtpFrom);
                    mail.To.Add(result.Email);
                    mail.Subject = string.Format("{0} solicitou reset de senha. ", result.Login.ToLower());
                    mail.Body = msgBody;
                    mail.IsBodyHtml = true;
                    var inicio = result.Email.Substring(0, 3);
                    var final = result.Email.Substring(result.Email.IndexOf("@"));
                    var maskmail = string.Format("{0}*****{1}", inicio, final);
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = _mailRepositorio.SendMail(mail), email = maskmail, id = result.IdUsuario });
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "Encontramos um problema para enviar o e-mail. Tente novamente mais tarde" });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "Login não cadastrado, para maiores informações entre em contato com suporte" });
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword(string password, int idUsuario, int codigo)
        {
            if (_resetRepositorio.ValidaCodigo(codigo, idUsuario))
            {
                if (_resetRepositorio.UpdatePassword(idUsuario, codigo, password))
                {
                    MailMessage mail = new MailMessage();
                    Usuario usuario = _usuarioRepositorio.GetUsuario(idUsuario);
                    #region msgBody
                    var msgBody = string.Format(@"<div bgcolor='#FFFFFF'>
    <table cellpadding='0' cellspacing='0' width='100%' align='center' border='0' bgcolor='#FFFFFF'>
        <tbody>
            <tr>
                <td>
                    <table cellpadding='0' cellspacing='0' width='600' align='center' border='0'>
                        <tbody>
                            <tr>
                                <td>
                                    <a href='http://www.stoque.com.br/' title='www.stoque.com.br' target='_blank'>
                                        <img src='http://www.stoque.com.br/wp-content/themes/stoque/img/stoque-logo.png' alt='Stoque Soluções tecnológicas' border='0' style='display:block'>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#0397d6'>
                                        <tbody>
                                            <tr>
                                                <td width='25'>&nbsp;</td>
                                                <td align='left' height='70'>
                                                    <font color='#ffffff' size='3' face='Arial, Helvetica, sans-serif'>
                                                        Prezado(a) <b>{0}</b><br>


                                                    </font>
                                                </td>
                                                <td width='25'>&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                        <tbody>
                                            <tr>
                                                <td width='25'>&nbsp;</td>
                                                <td align='left' valign='top'>
                                                    <font color='#656565' size='2' face='Arial, Helvetica, sans-serif'>
                                                        <br>
                                                        Conforme solicitado no Portal Stoque, sua senha foi redefinida.<br>
                                                        <br>
                                                        O seu login de acesso continua o mesmo:
                                                        <br><br>
                                                        Login: <strong>{1}</strong><br /><br />
                                                        Nova Senha: <strong>{2}</strong>
                                                        <br><br><br>
                                                        
                                                         Atenciosamente,<br>
                                                         Service Desk – Stoque Soluções Tecnológicas<br>
                                                         helpdesk@stoque.com.br<br/>
                                                    </font>
                                                </td>
                                                <td width='25'>&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#0397d6'>
                                        <tbody>
                                            <tr>
                                                <td width='25'>&nbsp;</td>
                                                <td align='left' height='30'>
                                                    <span style='color:#ffffff;font-family:Arial,Helvetica,sans-serif'>
                                                        Visite nossos canais&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <a href='https://www.facebook.com/StoqueBrasil' style='color:#fff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>Facebook</a>
                                                        &nbsp;<a href='https://twitter.com/StoqueBrasil' style='color:#fff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>Twitter</a>
                                                        &nbsp;<a href='https://www.youtube.com/channel/UCG5LMl3FsnbCQPja3fAWv7A' style='color:#fff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>YouTube</a>
                                                        &nbsp;<a href='https://www.linkedin.com/company/stoque' style='color:#fff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>LinkedIn</a>
                                                        &nbsp;<a href='http://www.stoque.com.br/' style='color:#fff;text-decoration:underline;font-size:12px;text-align:center' align='center' target='_blank'>WebSite</a>
                                                    </span>
                                                </td>
                                                <td width='25'>&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                        <tbody>
                                            <tr>
                                                <td width='25'>&nbsp;</td>
                                                <td align='left'>
                                                    <font color='#8e8e8e' size='2' face='Arial, Helvetica, sans-serif'>
                                                        <br>
                                                        <br><br>
                                                        © Stoque. Todos os direitos reservados.
                                                    </font>
                                                </td>
                                                <td width='25'>&nbsp;</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</div>
", usuario.Nome, usuario.UserName, password);
                    #endregion
                    mail.From = new MailAddress(Properties.Settings.Default.SmtpFrom);
                    mail.To.Add(usuario.Email);
                    mail.Subject = string.Format("{0}, sua senha foi redefinida com sucesso. ", usuario.Nome.Substring(0, usuario.Nome.IndexOf(" ")));
                    mail.Body = msgBody;
                    mail.IsBodyHtml = true;
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = true, email = _mailRepositorio.SendMail(mail) });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "Encontramos um problema para atualizar a senha, tente novamente mais tarde." });
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "O código informado está expirado ou já foi utilizado, gentileza solicitar um novo." });
        }

        [HttpPost]
        public HttpResponseMessage ClearCodigo(int codigoCleared, int idUsuario)
        {
            if (_resetRepositorio.ClearCodigo(codigoCleared, idUsuario))
                return Request.CreateResponse(HttpStatusCode.OK, new { status = true });
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = false, Message = "Obtivemos um problema para apagar o código." });
        }

    }
}
