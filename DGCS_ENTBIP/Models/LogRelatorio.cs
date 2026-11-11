using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class LogRelatorio
    {
        public string usuario { get; set; }
        public string cnpj { get; set; }
        public string periodo_inicio { get; set; }
        public string periodo_fim { get; set; }
        public string nota_fiscal { get; set; }
        public string data_consulta { get; set; }

    }
}