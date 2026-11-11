using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class AuxInventario
    {

        public string id_aux_inventario { get; set; }
        public string romaneio_inventario { get; set; }
        public string produto { get; set; }
        public string cor_produto { get; set; }
        public string tamanho { get; set; }
        public string codigo_barra { get; set; }
        public string qtde_importado { get; set; }
        public string desc_produto { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string fornecedor { get; set; }
        public string responsavel { get; set; }
        public string qtde_vezes_importado { get; set; }
        public string data_hora { get; set; }
        public string tipo_inventario { get; set; }
        public string status_inventario { get; set; }
        public string filial { get; set; }


    }
}