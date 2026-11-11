using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Relatorio
    {
        public string emissao { get; set; }
        public string nf_entrada { get; set; }
        public string nome_clifor { get; set; }
        public string serie_nfe_entrada { get; set; }
        public string grupo_produto { get; set; }
        public string subgrupo_produto { get; set; }
        public string produto { get; set; }
        public string codigo_barra { get; set; }
        public string qtde_bipado { get; set; }
        public string qtde_total { get; set; }
        public string responsavel { get; set; }
        public string cnpj { get; set; }
        public string qtde_contada1 { get; set; }
        public string qtde_contada2 { get; set; }
        public string qtde_contada3 { get; set; }
        public string complementar { get; set; }
        public string divergencia { get; set; }
        public string desc_produto { get; set; }
        public string custo { get; set; }

    }
}