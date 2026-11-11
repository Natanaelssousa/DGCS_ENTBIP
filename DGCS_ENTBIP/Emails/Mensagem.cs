using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DGCS_ENTBIP.Emails
{
    public class Mensagem
    {

        public StringBuilder mensagemEntradaDivergenciaFornecLote( string loja)
        {
            StringBuilder textoEmail = new StringBuilder();

            textoEmail.Append("<label>Prezado(a),</label>");
            textoEmail.AppendFormat("<br /><br />");
            textoEmail.Append("<label>Houve sobra no lote recebido na Loja: " + loja + ". Por gentileza verifique se recebeu um arquivo excel com detalhes da sobra. Obrigado! </label>");
            textoEmail.AppendFormat("<br />");
            textoEmail.Append("<label></label>");
            textoEmail.AppendFormat("<br />");
            textoEmail.Append("<label>Atenção, não responda esse e-mail.</label>");
  

            textoEmail.AppendFormat("<br />");
            textoEmail.AppendFormat("<br />");

            textoEmail.Append("<label>Enviado por: DGCS_ENTBIP  Data: " + DateTime.Now + "</label>");



            return textoEmail;
        }


        public StringBuilder mensagemEntradaDivergenciaFornec(List<AuxEntrada> DivergEntrada, string NF, string filial, string nfs)
        {

            int total_recebido = 0;
            int total_original = 0;
            StringBuilder textoEmail = new StringBuilder();

            textoEmail.Append("<label>Prezado, segue abaixo a relação dos produtos que ficaram divergentes na bipagem da loja referente as nfs "+ nfs + ".</label>");
            textoEmail.AppendFormat("<br /><br />");

            textoEmail.Append("<table style='width: 100px;  border: solid 1px #000; padding: 5px; '>");
            textoEmail.AppendFormat("<thead> <tr><th colspan='6' style='  border: solid 1px #000; padding: 10px;  background-color: #999;'>  DIVERGENCIA LOJA " + filial + " CODIGO BIPAGEM - " + NF + "    </th></tr> </thead>");
            textoEmail.AppendFormat("<tbody style='width: 100px;'>");
            foreach (AuxEntrada nota_itens in DivergEntrada)
            {
                total_recebido += Convert.ToInt32(nota_itens.qtde_bipado);
                total_original += Convert.ToInt32(nota_itens.qtde_total);

                textoEmail.AppendFormat("<tr><td style=' width: 150%; border: solid 1px #000; padding: 10px;'>Grupo:  " + nota_itens.grupo + " </td> <td style=' width: 150%; border: solid 1px #000; padding: 10px;'>Subgrupo:  " + nota_itens.subgrupo + " </td>  <td style=' width: 150%; border: solid 1px #000; padding: 10px;'>Produto:  " + nota_itens.produto + " </td> <td style='width: 150%; border: solid 1px #000; padding: 10px;'>Cod Barra:  " + nota_itens.codigo_barra + " </td><td style='width: 150%; border: solid 1px #000; padding: 10px;'>Desc Produto: " + nota_itens.desc_produto + " </td> <td style='width: 150%; border: solid 1px #000; padding: 10px;'>Bipagem:  " + nota_itens.romaneio_produto + " </td> <td style='width: 150%; border: solid 1px #000; padding: 10px;'>Qtde recebido: " + nota_itens.qtde_bipado + " </td> <td style='width: 100%; border: solid 1px #000; padding: 10px;'>Qtde original: " + nota_itens.qtde_total + "</td> </tr>");
            }

            textoEmail.Append("</ tbody >");
            textoEmail.Append("</table>");

            textoEmail.AppendFormat("<br />");
            textoEmail.AppendFormat("<br />");

            textoEmail.Append(" <table style='text - align:left'>");
            textoEmail.AppendFormat("<tr>");
            textoEmail.AppendFormat("<td>");
            textoEmail.Append(" <br />     <br />");
            textoEmail.Append("<label>Enviado por: DGCS_ENTBIP  Data: " + DateTime.Now + "</label>");



            return textoEmail;
        }

 

    }
}