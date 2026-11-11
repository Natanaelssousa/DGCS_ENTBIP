using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Loja_Saidas
    {

        public string romaneio_produto { get; set; }
        public string filial { get; set; }
        public string codigo_tab_preco { get; set; }
        public string tipo_entrada_saida { get; set; }
        public string filial_destino { get; set; }
        public string numero_nf_transferencia { get; set; }
        public string serie_nf { get; set; }
        public string responsavel { get; set; }
        public string indica_devolucao { get; set; }
        public string fornecedor_devolucao { get; set; }
        public string emissao { get; set; }
        public string qtde_total { get; set; }
        public string valor_total { get; set; }
        public string fator_preco { get; set; }
        public string status_transito { get; set; }
        public string saida_encerrada { get; set; }
        public string sugestao_transferencia { get; set; }
        public string lx_status_saida { get; set; }
        public string saida_cancelada { get; set; }
    }
}