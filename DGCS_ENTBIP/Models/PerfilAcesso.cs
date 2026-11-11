using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGCS_ENTBIP.Models
{
    public class PerfilAcesso
    {

        public string id_perfil { get; set; }
        public string perfil { get; set; }
        public string tela_notas_pendentes { get; set; }
        public string tela_cadastro_servidor { get; set; }
        public string tela_cadastro_usuario { get; set; }
        public string tela_parametros { get; set; }
        public string tela_relatorio { get; set; }
        public string tela_emails { get; set; }
        public string tela_perfis_acesso { get; set; }
        public string tela_relatorio_abastecimento { get; set; }
        public string tela_relatorio_log { get; set; }
        public string tela_pedidos_consumiveis { get; set; }
        public string tela_inventario_ciclico { get; set; }
        public string tela_parametro_ftp { get; set; }
        public string tela_registro_amostra { get; set; }
        public string tela_escala { get; set; }


    }
}