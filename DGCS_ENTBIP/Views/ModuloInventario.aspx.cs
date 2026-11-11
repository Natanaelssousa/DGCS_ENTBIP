using DGCS_ENTBIP.Daos;
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
    public partial class ModuloInventario : System.Web.UI.Page
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
                    tela_inventario_ciclico.Text = perfilAcesso.tela_inventario_ciclico;


                    if (pareametro_ciclico.Value == "Não Permitir")
                    {
                        tela_inventario_ciclico.Text = "Sem acesso";
                    }

                }

                if (securityToken.validaChaveToken() == false && cnpjDGCS != "41179975000106" && cnpjLoja != cnpjLojaAcesso.Value && loja != codigo_loja.Value)
                {

               
                    tela_inventario_ciclico.Text = "Sem acesso";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Servidor vinculado a esse usúario está sem chave de ativação. Por favor solicitar ativação!', 'warning')", true);

                }


                if (servidorCookie == null && perfilAcesso.perfil == "Master")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Usúario esta sem servidor valido vinculado. Cadastre para conseguir usar todas as funções do sistema!', 'warning')", true);
                }

            }
        }
        protected void LinkButtonSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }
    
 
        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

 
    


        protected void ButtonInventarioCiclico_Click(object sender, EventArgs e)
        {
            Parametro parametroCiclico = new Parametro();
            ParametroDao parametroDao = new ParametroDao();

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];

            parametroCiclico = parametroDao.BuscaParametro("Inventario Ciclico");

            InventarioDao inventarioDao = new InventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<Inventario> inventarios = new List<Inventario>();

            inventarios = inventarioDao.BuscaCompletaRetornoLista();

            int qtdeInventariosDia = 0;

            qtdeInventariosDia = Convert.ToInt16(parametroCiclico.valor);


            if (qtdeInventariosDia > inventarios.Count)
            {
                Response.Redirect("~/Views/InventarioCiclico.aspx", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Não é possivel realizar um novo inventario, limite diario atingido!', 'warning')", true);

            }


        }

        protected void ButtonRelatorio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RelatorioInventario.aspx", false);
        }
    }
}