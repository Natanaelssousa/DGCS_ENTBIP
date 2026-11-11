using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class AuxEntradaLote
    {

       public string  id_aux_entrada_lote { get; set; }
        public string ids_nfs_selecionadas { get; set; }
        public string romaneio_produto { get; set; }
        public string filial { get; set; }
        public string codigo_loja { get; set; }
        public string produto { get; set; }
        public string desc_produto { get; set; }
        public string cor_produto { get; set; }
        public string tamanho { get; set; }
        public string codigo_barra { get; set; }
        public string preco1 { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string lote { get; set; }
        public string quantidade { get; set; }
        public string xped { get; set; }
        public string chave_nfe { get; set; }
    }
}