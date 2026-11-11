using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class PedidoPendente
    {
        public string id { get; set; }
        public string pedido { get; set; }
        public string filial { get; set; }
        public string nome_clifor { get; set; }
        public string nf_entrada { get; set; }
        public string serie_nf_entrada { get; set; }
        public string chave_nfe { get; set; }
        public string ilha { get; set; }
        public string desc_ilha { get; set; }
        public string status { get; set; }

    }
}