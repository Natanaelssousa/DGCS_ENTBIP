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
    public partial class Menu : System.Web.UI.Page
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
                    perfil_acesso.Text = perfilAcesso.perfil;
                    tela_cadastro_servidor.Text = perfilAcesso.tela_cadastro_servidor;
                    tela_cadastro_usuario.Text = perfilAcesso.tela_cadastro_usuario;
                    tela_emails.Text = perfilAcesso.tela_emails;
                    tela_notas_pendentes.Text = perfilAcesso.tela_notas_pendentes;
                    tela_parametros.Text = perfilAcesso.tela_parametros;
                    tela_perfis_acesso.Text = perfilAcesso.tela_perfis_acesso;
                    tela_relatorio.Text = perfilAcesso.tela_relatorio;
                    tela_relatorio_abastecimento.Text = perfilAcesso.tela_relatorio_abastecimento;
                    tela_relatorio_log.Text = perfilAcesso.tela_relatorio_log;
                    tela_pedido_consumiveis.Text = perfilAcesso.tela_pedidos_consumiveis;
                    tela_inventario_ciclico.Text = perfilAcesso.tela_inventario_ciclico;
                    tela_parametro_ftp.Text = perfilAcesso.tela_parametro_ftp;
                    tela_registro_amostra.Text = perfilAcesso.tela_registro_amostra;
                    tela_escala.Text = perfilAcesso.tela_escala;


                    if (pareametro_ciclico.Value == "Não Permitir")
                    {
                        tela_inventario_ciclico.Text = "Sem acesso";
                    }

                }

                if (securityToken.validaChaveToken() == false && cnpjDGCS != "41179975000106" && cnpjLoja != cnpjLojaAcesso.Value && loja != codigo_loja.Value)
                {

                    tela_cadastro_servidor.Text = "Sem acesso";
                    tela_cadastro_usuario.Text = "Sem acesso";
                    tela_emails.Text = "Sem acesso";
                    tela_notas_pendentes.Text = "Sem acesso";
                    tela_parametros.Text = "Sem acesso";
                    tela_perfis_acesso.Text = "Sem acesso";
                    tela_relatorio.Text = "Sem acesso";
                    tela_relatorio_abastecimento.Text = "Sem acesso";
                    tela_relatorio_log.Text = "Sem acesso";
                    tela_pedido_consumiveis.Text = "Sem acesso";
                    tela_inventario_ciclico.Text = "Sem acesso";
                    tela_registro_amostra.Text = "Sem acesso";
                    tela_escala.Text = "Sem acesso";
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

        protected void acessarCadastroUsuarios_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Views/CadastroUsuario.aspx", false);

        }

        protected void acessarCadastroServidores_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Views/CadastroServidores.aspx", false);

        }

        protected void acessarParametros_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Views/Parametros.aspx", false);

        }

        protected void acessarNotasPendentes_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Views/ModuloEntradaRevenda.aspx", false);

        }

        protected void acessarRelatorio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RelatorioDGCS.aspx", false);
        }

        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void ButtonEmails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Emails.aspx", false);
        }

        protected void ButtonPerfis_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/PerfisAcesso.aspx", false);
        }

        protected void ButtonRelAbastecimento_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RelatorioAbastecimento.aspx", false);
        }

        protected void relLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RelatorioLogs.aspx", false);
        }

        protected void ButtonPedConsumiveis_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloConsumivel.aspx", false);
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


            //if (qtdeInventariosDia > inventarios.Count)
            //{
                Response.Redirect("~/Views/ModuloInventario.aspx", false);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Não é possivel realizar um novo inventario, limite diario atingido!', 'warning')", true);

            //}


        }

        protected void Buttonftp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ParametroFTP.aspx", false);
        }

        protected void ButtonRegistroAmostra_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloRegistrosAmostras.aspx", false);
        }

        protected void ButtonEscala_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloEscala.aspx", false);
        }
    }
}