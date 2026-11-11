using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class AuxEntrada
    {
        public string ids_nfs_selecionadas { get; set; }
        public string romaneio_produto { get; set; }
        public string produto { get; set; }
        public string cor_produto { get; set; }
        public string tamanho { get; set; }
        public string codigo_barra { get; set; }
        public string preco1 { get; set; }
        public string qtde_total { get; set; }
        public string qtde_bipado { get; set; }
        public string desc_produto { get; set; }
        public string fornecedor { get; set; }
        public string responsavel { get; set; }
        public string entrada_status { get; set; }
        public string qtde_vezes_bipado { get; set; }
        public string status_bipagem { get; set; }
        public string chave_nfe { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string destinatario { get; set; }
        public string cnpj { get; set; }
        public string nota_fiscal { get; set; }
        public string qtde_contada1 { get; set; }
        public string qtde_contada2 { get; set; }
        public string qtde_contada3 { get; set; }
        public string data_hora { get; set; }
    }
}