using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Servidor
    {
        public string id_servidores_dgcs { get; set; }
        public string situacao { get; set; }
        public string nome_servidor { get; set; }
        public string nome_db { get; set; }
        public string nome_db_app { get; set; }
        public string usuario_banco { get; set; }
        public string senha_banco { get; set; }
        public string codigo_loja { get; set; }
        public string regiao { get; set; }
        public string estado { get; set; }
        public string ultima_edicao { get; set; }
        public string data_criacao { get; set; }
        public string parametro_ativo { get; set; }
        public string cnpj { get; set; }
        public string token { get; set; }
        public string parametro_ciclico { get; set; }
    }
}