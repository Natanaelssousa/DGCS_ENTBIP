using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class Usuario
    {

       public string  id_usuario_dgcs { get; set; }
        public string nome_completo { get; set; }
        public string  cpf { get; set; }
        public string email { get; set; }
        public string perfil_acesso { get; set; }
        public string login_usuario { get; set; }
        public string senha { get; set; }
        public string situacao { get; set; }
        public string data_criacao { get; set; }
        public string codigo_loja { get; set; }
    }
}