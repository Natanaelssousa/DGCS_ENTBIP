using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class RegistroAmostras
    {

        public string id_registros { get; set; }

        public string cod_filial { get; set; }
        public string data_cadastro { get; set; }
        public string cpf_cliente { get; set; }
        public string produto { get; set; }
        public string qtde { get; set; }
        public string responsavel { get; set; }
        public string finalizado { get; set; }
    }
}