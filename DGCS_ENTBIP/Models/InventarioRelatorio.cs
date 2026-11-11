using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class InventarioRelatorio
    {
        public string filial { get; set; }
        public string codigo_inventario { get; set; }
        public string desc_inventario { get; set; }
        public string emissao { get; set; }
        public string produto { get; set; }
        public string desc_prod_nf { get; set; }
        public string grupo_produto { get; set; }
        public string subgrupo_produto { get; set; }
        public string qtde { get; set; }
        public string responsavel { get; set; }

    }
}