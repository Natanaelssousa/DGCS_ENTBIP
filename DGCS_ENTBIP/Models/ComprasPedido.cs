using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class ComprasPedido
    {

        public string   id { get; set; }
        public string chave_nfe { get; set; }
        public string nf_entrada { get; set; }
        public string serie_nf_entrada { get; set; }
        public string cnpj { get; set; }
        public string data_hora { get; set; }
        public string usuario { get; set; }
        public string loja { get; set; }
    }
}