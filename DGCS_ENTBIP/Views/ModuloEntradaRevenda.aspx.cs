using DGCS_ENTBIP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class ModuloEntradaRevenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie dt_acesso = Request.Cookies["dt_acesso"];
            HttpCookie token = Request.Cookies["token"];
            HttpCookie pareametro_ciclico = Request.Cookies["pareametro_ciclico"];
            HttpCookie cnpjLojaAcesso = Request.Cookies["cnpjLoja"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];



            PerfilAcesso perfilAcesso = new PerfilAcesso();

            string senhaDescriptografada = token.Value;


            string cnpjDGCS = "";
            string cnpjLoja = "";
            string loja = "";
            string senha = "";

            if (senhaDescriptografada != "")
            {
                cnpjDGCS = senhaDescriptografada.Substring(0, 14);
                cnpjLoja = senhaDescriptografada.Substring(14, 14);
                loja = senhaDescriptografada.Substring(28, 6);
                senha = senhaDescriptografada.Substring(34, 12);
            }



            SecurityToken securityToken = new SecurityToken(senha);



            if (!IsPostBack)
            {

                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                //perfilAcesso = (PerfilAcesso)Session["perfil_usuario"];
                if (perfil_usuario != null)
                {
                    string meuObjetoJson = perfil_usuario.Value;
                    perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                    // Faça o processamento necessário com o objeto
                }

                if (usuarioLogin != null)
                {
                    usuario.Text = "Usúario: " + usuarioLogin.Value;
                }

                if (perfilAcesso != null)
                {
                    tela_notas_pendentes.Text = perfilAcesso.tela_notas_pendentes;


                    if (tela_notas_pendentes.Text == "Não Permitir")
                    {
                        tela_notas_pendentes.Text = "Sem acesso";
                    }

                }

                if (securityToken.validaChaveToken() == false && cnpjDGCS != "41179975000106" && cnpjLoja != cnpjLojaAcesso.Value && loja != codigo_loja.Value)
                {


                    tela_notas_pendentes.Text = "Sem acesso";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Servidor vinculado a esse usúario está sem chave de ativação. Por favor solicitar ativação!', 'warning')", true);

                }


                if (servidorCookie == null && perfilAcesso.perfil == "Master")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Usúario esta sem servidor valido vinculado. Cadastre para conseguir usar todas as funções do sistema!', 'warning')", true);
                }

            }
        }

        protected void ButtonEntradaRevenda_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NotasPendentes.aspx", false);

        }

        protected void LinkButtonSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }

        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void acessarRelatorio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RelatorioDGCS.aspx", false);
        }
    }
}