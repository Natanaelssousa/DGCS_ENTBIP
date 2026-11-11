using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class InventarioConfiguracao
    {

        public string codigo_inventario { get; set; }
        public string filial { get; set; }
        public string local { get; set; }
        public string nome_setor { get; set; }
        public string iniciado { get; set; }
        public string encerrado { get; set; }
        public string qtde_contada { get; set; }
        public string qtde_contagens { get; set; }
        public string ultima_qtde_contada { get; set; }
        public string numero_contagem { get; set; }


    }
}