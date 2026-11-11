using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Entrada
    {

        public string  romaneio_produto { get; set; }
        public string filial { get; set; }
        public string codigo_tab_preco { get; set; }
        public string tipo_entrada_saida { get; set; }
        public string filial_origem { get; set; }
        public string numero_nf_transferencia { get; set; }
        public string romaneio_nf_saida { get; set; }
        public string fornecedor { get; set; }
        public string cnpj { get; set; }
        public string responsavel { get; set; }
        public string emissao { get; set; }
        public string data_saida { get; set; }
        public string desc_tipo_entrada_saida { get; set; }
        public string obs { get; set; }
        public string entrada_conferida { get; set; }
        public string entrada_sem_produtos { get; set; }
        public string qtde_total { get; set; }
        public string valor_total { get; set; }
        public string fator_preco { get; set; }
        public string status_transito { get; set; }
        public string qtde_nao_conferida { get; set; }
        public string valor_nao_conferido { get; set; }
        public string entrada_por { get; set; }
        public string cgc_fornecedor { get; set; }
        public string pedido_status_loja { get; set; }
        public string data_baixa_pedido { get; set; }
        public string entrada_encerrada { get; set; }
        public string data_para_transferencia { get; set; }
        public string serie_nf_entrada { get; set; }
        public string entrada_cancelada { get; set; }
        public string lx_status_entrada { get; set; }
        public string data_entrada_conferida { get; set; }
        public string chave_nfe { get; set; }
        public string especie_serie { get; set; }
        public string utiliza_preco_origem { get; set; }


    }
}