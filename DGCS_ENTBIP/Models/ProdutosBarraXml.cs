using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class ProdutosBarraXml
    {

      public string  id_xml_nfe { get; set; }
        public string chave_nfe { get; set; }
        public string nome_clifor { get; set; }
        public string nf_entrada { get; set; }
        public string serie_nf_entrada { get; set; }
        public string codigo_barra { get; set; }
        public string produto { get; set; }
        public string desc_produto { get; set; }
        public string cor_produto { get; set; }
        public string desc_cor_produto { get; set; }
        public string grupo_produto { get; set; }
        public string subgrupo_produto { get; set; }
        public string griffe { get; set; }
        public string tamanho { get; set; }
        public string grade { get; set; }
        public string unidade { get; set; }
        public string qtde { get; set; }
        public string valor_un { get; set; }
        public string valor_prod_tot { get; set; }
        public string data_saida { get; set; }
        public string destinatario { get; set; }
        public string cnpj { get; set; }
    }
}