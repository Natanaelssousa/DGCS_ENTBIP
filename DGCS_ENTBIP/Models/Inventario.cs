using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Inventario
    {

        public string codigo_inventario { get; set; }
        public string filial { get; set; }
        public string desc_inventario { get; set; }
        public string responsavel { get; set; }
        public string emissao { get; set; }
        public string arquivo_contagem { get; set; }
        public string qtde_total { get; set; }
        public string todos_produtos { get; set; }
        public string valido_e_finalizado { get; set; }
        public string obs { get; set; }
        public string data_para_transferencia { get; set; }
        public string estoque_ajustado { get; set; }
        public string data_ajuste { get; set; }
        public string responsavel_ajuste { get; set; }
        public string data_saldo { get; set; }
        public string lx_status_inventario { get; set; }
        public string data_finalizacao { get; set; }


    }
}