using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DGCS_ENTBIP.Emails
{
    public class Email
    {

        public string sDestinatario { get; set; }
        public string sAssunto { get; set; }
        public string sMensagem { get; set; }
        public string sCorrespondente { get; set; }

    
    public void EnviarMailComArquivo(string sDestinatario, string sAssunto, StringBuilder sMensagem, List<AuxEntradaLote> data)
    {
        try
        {

            Email_Divergencia emails = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
            emails = emailDao.BuscaEmailEnvio();

                string sServidorMail = emails.servidor_email;
                //String sServidorMail = "smtp.office365.com";
                //String strLogin = "robovarejo@natura.net";
                //String strSenha = "#C0x!nh@23";
                string strLogin = emails.email;
                string strSenha = emails.senha;

                MailMessage oMail = new MailMessage();
            MailAddress oFrom = new MailAddress(strLogin);
            oMail.From = oFrom;

            //String sPara;
            //sDestinatario.toSplit
            String[] sPara = sDestinatario.Split(';');
            for (int i = 0; i < sPara.Length; i++)
            {
                if (sPara[i] != "")
                {
                    oMail.To.Add(sPara[i]);
                }
            }


            oMail.Body = sMensagem.ToString();
            oMail.Subject = sAssunto;
            oMail.IsBodyHtml = true;


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SobraLote");

                var projection = data.Select(p => new
                {

                    filial = p.filial,
                    codigo_loja = p.codigo_loja,
                    produto = p.produto,
                    desc_produto = p.desc_produto,
                    cor_produto = p.cor_produto,
                    tamanho = p.tamanho,
                    codigo_barra = p.codigo_barra,
                    preco1 = p.preco1,
                    grupo = p.grupo,
                    subgrupo = p.subgrupo,
                    lote = p.lote,
                    quantidade = p.quantidade,
                    xped = p.xped,
                    chave_nfe = p.chave_nfe
                }).ToList();

                // Define os cabeçalhos das colunas a partir das propriedades da classe Pessoa
                worksheet.Cells["A1"].Value = "filial";
                worksheet.Cells["B1"].Value = "codigo_loja";
                worksheet.Cells["C1"].Value = "produto";
                worksheet.Cells["D1"].Value = "desc_produto";
                worksheet.Cells["E1"].Value = "cor_produto";
                worksheet.Cells["F1"].Value = "tamanho";
                worksheet.Cells["G1"].Value = "codigo_barra";
                worksheet.Cells["H1"].Value = "preco1";
                worksheet.Cells["I1"].Value = "grupo";
                worksheet.Cells["J1"].Value = "subgrupo";
                worksheet.Cells["K1"].Value = "lote";
                worksheet.Cells["L1"].Value = "quantidade";
                worksheet.Cells["M1"].Value = "xped";
                worksheet.Cells["N1"].Value = "chave_nfe";

                // Preenche os dados a partir da lista
                worksheet.Cells["A2"].LoadFromCollection(projection, false, TableStyles.Dark10);


                    // Estilo para o cabeçalho
                    for (int col = 1; col <= 15; col++) // Colunas 
                    {
                        for (int row = 1; row <=  1; row++) // Linhas de dados
                        {
                            var cell = worksheet.Cells[row, col];
                            cell.Style.Font.Bold = true; // Texto em negrito
                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange); // Cor de fundo
                            cell.Style.Font.Color.SetColor(System.Drawing.Color.White); // Cor da letra

                           
                        }
                    }

                    // Estilo para os dados
                    for (int col = 1; col <= 15; col++) // Colunas B, C e D
                    {
                        for (int row = 2; row <= data.Count + 1; row++) // Linhas de dados
                        {
                            var cell = worksheet.Cells[row, col];
                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White); // Cor de fundo
                        }
                    }




                    // Crie um arquivo temporário em memória
                    byte[] excelData = package.GetAsByteArray();

                // Anexe o arquivo Excel ao e-mail
                var attachment = new Attachment(new MemoryStream(excelData), "SobraEntLote.xlsx");

                SmtpClient oEnvio = new SmtpClient(sServidorMail, 587);
                oEnvio.UseDefaultCredentials = false;
                oEnvio.EnableSsl = true;

                //oEnvio.Host = "outlook.office365.com";
                oEnvio.Host = emails.host_envio;
                oEnvio.Timeout = 600000;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                oEnvio.Credentials = new System.Net.NetworkCredential(strLogin, strSenha);

                oMail.Attachments.Add(attachment);

                oEnvio.Send(oMail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }

    }
    public void EnviarMail(String sDestinatario, String sAssunto, StringBuilder sMensagem, String sCorrespondente)
    {
        try
        {
            Email_Divergencia emails = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
            emails = emailDao.BuscaEmailEnvio();

            String sServidorMail = emails.servidor_email;
            //String sServidorMail = "smtp.outlook.office365.com";
            //String strLogin = "robodgcs@outlook.com";
            //String strSenha = "Robo@0804";
            String strLogin = emails.email;
            String strSenha = emails.senha;

            MailMessage oMail = new MailMessage();
            MailAddress oFrom = new MailAddress(strLogin);
            oMail.From = oFrom;

            //String sPara;
            //sDestinatario.toSplit
            String[] sPara = sDestinatario.Split(';');
            for (int i = 0; i < sPara.Length; i++)
            {
                if (sPara[i] != "")
                {
                    oMail.To.Add(sPara[i]);
                }
            }


            oMail.Body = sMensagem.ToString();
            oMail.Subject = sAssunto;
            oMail.IsBodyHtml = true;

            SmtpClient oEnvio = new SmtpClient(sServidorMail, 587);
            oEnvio.UseDefaultCredentials = false;
            oEnvio.EnableSsl = true;

            //oEnvio.Host = "outlook.office365.com";
            oEnvio.Host = emails.host_envio;
            oEnvio.Timeout = 600000;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            oEnvio.Credentials = new System.Net.NetworkCredential(strLogin, strSenha);

            oEnvio.Send(oMail);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }

    }


}
}