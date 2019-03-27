using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Formularios
{
    public class CadastroDeUsuario
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public string Empresa { get; set; }

        public string GetBody(CadastroDeUsuario cad)
        {

            return string.Format(@"<html>
<div bgcolor='#FFFFFF'>
    <table cellpadding='0' cellspacing='0' width='100%' align='center' border='0' bgcolor='#FFFFFF'>
        <tbody>
            <tr>
                <td>
                    <table cellpadding='0' cellspacing='0' width='90%' align='center' border='0'>
                        <tbody>
                            <tr>
                                <td>
                                    <a href='http://www.stoque.com.br/' title='www.stoque.com.br' target='_blank'>
                                        <img src='http://www.stoque.com.br/wp-content/themes/stoque/img/stoque-logo.png'
                                            alt='Stoque Soluções tecnológicas' border='0' style='display:block'>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#0397d6'>
                                        <tbody>
                                            <tr>
                                                <td width='25'>&nbsp;</td>
                                                <td align='center' height='70'>
                                                    <font color='#ffffff' size='6' face='Arial, Helvetica, sans-serif'>
                                                        <b>Solicitação Novo Usuário Portal</b><br>
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
                                                        <table border='2' width='100%' cellspacing='5' cellpadding='10'
                                                            bgcolor='#e5e5e5'>
                                                            <thead>
                                                                <tr>
                                                                    <th colspan='5' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Novo Usuário Portal</b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td> <strong>Nome:</strong> {0} </td>
                                                                    <td><strong>Telefone:</strong> {1}</td>
                                                                    <td><strong>Email:</strong> {2}</td>
                                                                    <td><strong>CNPJ/CPF:</strong> {3}</td>
                                                                    <td><strong>Empresa/Unidade:</strong> {4}</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                        Atenciosamente,<br>
                                                        Service Desk – Stoque Soluções Tecnológicas<br>
                                                        helpdesk@stoque.com.br<br />
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
                                                        <a href='https://www.facebook.com/StoqueBrasil'
                                                            style='color:#fff;text-decoration:underline;font-size:12px;text-align:center'
                                                            align='center' target='_blank'>Facebook</a>
                                                        &nbsp;<a href='https://twitter.com/StoqueBrasil'
                                                            style='color:#fff;text-decoration:underline;font-size:12px;text-align:center'
                                                            align='center' target='_blank'>Twitter</a>
                                                        &nbsp;<a
                                                            href='https://www.youtube.com/channel/UCG5LMl3FsnbCQPja3fAWv7A'
                                                            style='color:#fff;text-decoration:underline;font-size:12px;text-align:center'
                                                            align='center' target='_blank'>YouTube</a>
                                                        &nbsp;<a href='https://www.linkedin.com/company/stoque'
                                                            style='color:#fff;text-decoration:underline;font-size:12px;text-align:center'
                                                            align='center' target='_blank'>LinkedIn</a>
                                                        &nbsp;<a href='http://www.stoque.com.br/'
                                                            style='color:#fff;text-decoration:underline;font-size:12px;text-align:center'
                                                            align='center' target='_blank'>WebSite</a>
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

</html>", cad.Nome, cad.Telefone, cad.Email, cad.Cnpj, cad.Empresa);
        }
    }


}