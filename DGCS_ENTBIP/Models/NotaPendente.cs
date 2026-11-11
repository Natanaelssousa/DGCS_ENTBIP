using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class NotaPendente
    {

        public string nf_entrada {get;set;}
        public string serie_nf_entrada { get; set; }
        public string nome_clifor { get; set; }
        public string chave_nfe { get; set; }
        public string id_xml_nfe { get; set; }
        public string qtde { get; set; }
        public string responsavel { get; set; }
        public string status { get; set; }
        public string xped { get; set; }
    }
}