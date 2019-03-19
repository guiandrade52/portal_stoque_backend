using System.Collections.Generic;

namespace PortalStoque.API.Models.Formularios
{
    public class NovoColaboradorModel
    {
        public string Sol_Solicitante { get; set; }
        public string Sol_Setor { get; set; }
        public string Sol_Cargo { get; set; }
        public string Sol_Telefone { get; set; }
        public string Sol_Email { get; set; }
        public string Fun_Nome { get; set; }
        public string Fun_Alocado { get; set; }
        public string Fun_CPF { get; set; }
        public string Fun_Cargo { get; set; }
        public string Fun_Setor { get; set; }
        public string Log_Sistemas { get; set; }
        public string Log_PerfilCopiar { get; set; }
        public string Log_GrupoEmail { get; set; }
        public string Log_Observacao { get; set; }
        public string Hard_Install { get; set; }
        public string Hard_Observacao { get; set; }
        public string Soft_Install { get; set; }
        public string Soft_Observacao { get; set; }


        public string GetBody(NovoColaboradorModel col)
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
                                                        <b>Solicitação Novo Colaborador</b><br>
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
                                                                    <th colspan='3' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Dados do solicitante </b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <strong>Nome:</strong> {0}
                                                                    </td>
                                                                    <td><strong>Setor:</strong> {1}</td>
                                                                    <td><strong>Cargo:</strong> {2}</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <strong>E-mail:</strong>
                                                                        {3}
                                                                    </td>
                                                                    <td colspan='2'>
                                                                        <strong>Telefone ou Ramal:</strong> {4}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan='3' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Dados do Novo Funcionário </b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <strong>Nome:</strong> {5}
                                                                    </td>
                                                                    <td><strong>Setor:</strong> {6} </td>
                                                                    <td><strong>Cargo:</strong> {7}</td>
                                                                </tr>
                                                                <tr>
                                                                    <td> <strong>CPF:</strong> {8} </td>
                                                                    <td colspan='2'><strong>Funcionário será alocado
                                                                            na:</strong>
                                                                        {9}</td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan='3' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Dados para criação de login </b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Criar os seguintes usuários:</strong>
                                                                        {10}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <strong> Copiar perfil do(a) seguinte
                                                                            usuário(a): </strong>
                                                                        {11}
                                                                    </td>
                                                                    <td colspan='2'>
                                                                        <strong> Grupos de E-mail: </strong>
                                                                        {12}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Observações: </strong>
                                                                        {13}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan='3' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Instalações de Hardware </b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Equipamentos: </strong>
                                                                        {14}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Observações: </strong>
                                                                        {15}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan='3' bgcolor='#959595' color='#FFF'>
                                                                        <font color='#ffffff'
                                                                            face='Arial, Helvetica, sans-serif'>
                                                                            <b>Instalações de Softwares </b><br>
                                                                        </font>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Instalar os seguintes softwares:
                                                                        </strong>
                                                                        {16}
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan='3'>
                                                                        <strong> Observações: </strong>
                                                                        {17}
                                                                    </td>
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

</html>", col.Sol_Solicitante, col.Sol_Setor, col.Sol_Cargo, col.Sol_Email, col.Sol_Telefone, col.Fun_Nome, col.Fun_Setor, col.Fun_Cargo, col.Fun_CPF, col.Fun_Alocado,
col.Log_Sistemas, col.Log_PerfilCopiar, col.Log_GrupoEmail, col.Log_Observacao, Hard_Install, Hard_Observacao, Soft_Install, Soft_Observacao);
        }
    }


}