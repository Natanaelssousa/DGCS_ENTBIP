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
    public partial class PedidosPendentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];
                HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                string meuObjetoJson = perfil_usuario.Value;
                perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                if (perfilAcesso != null)
                {

                    tela_notas_pendentes.Text = perfilAcesso.tela_notas_pendentes;
                    perfil_acesso.Text = perfilAcesso.perfil;
                }
                carregaNotasPendentes();
            }
        }


        protected void GrNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              

                string id = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                string pedido = DataBinder.Eval(e.Row.DataItem, "pedido").ToString();
                string filial = DataBinder.Eval(e.Row.DataItem, "filial").ToString();
                string nome_clifor = DataBinder.Eval(e.Row.DataItem, "nome_clifor").ToString();
                string nf_entrada = DataBinder.Eval(e.Row.DataItem, "nf_entrada").ToString();
                string serie_nf_entrada = DataBinder.Eval(e.Row.DataItem, "serie_nf_entrada").ToString();
                string chave_nfe = DataBinder.Eval(e.Row.DataItem, "chave_nfe").ToString();
                string ilha = DataBinder.Eval(e.Row.DataItem, "ilha").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();

                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtnRefazer");
                Label refazer = (Label)e.Row.FindControl("refazer");
                CheckBox check = (CheckBox)e.Row.FindControl("ckIDAprova");

                if (status == "Em andamento")
                {
                    imgUpload.ImageUrl = "images/remover.png";

                }
                else
                {
                    imgUpload.Enabled = false;
                    imgUpload.ImageUrl = "images/logo_natura2.jpg";
                }

                for (int indice = 0; indice <= GrNotas.Rows.Count; indice++)
                {
                    refazer.Text = (1 + indice).ToString();
                }

                if ((tela_notas_pendentes.Text == "Total" && perfil_acesso.Text == "Master") || (tela_notas_pendentes.Text == "Total" && perfil_acesso.Text == "Gerente Loja"))
                {
                    imgUpload.Visible = true;
                    refazer.Visible = false;
                    check.Visible = true;
                }
                else if (tela_notas_pendentes.Text == "Consulta")
                {
                    imgUpload.Visible = false;
                    refazer.Visible = true;
                    check.Visible = false;
                }
                else
                {
                    imgUpload.Visible = false;
                    refazer.Visible = true;
                    check.Visible = true;
                }

            }

        }



        protected void carregaNotasPendentes()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];

            PedidoPendente pedidoPendente = new PedidoPendente();
            PedidoPendenteDao notaPendenteDao = new PedidoPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            List<PedidoPendente> list = notaPendenteDao.BuscaCompletaRetornoLista();

            GrNotas.DataSource = list;
            GrNotas.DataBind();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloConsumivel.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];

            PedidoPendente pedidoPendente = new PedidoPendente();
            PedidoPendenteDao notaPendenteDao = new PedidoPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            string palavra = txtbusca.Text;


            List<PedidoPendente> list = notaPendenteDao.BuscaRetornoPalavrasChaves(palavra);

            if (list.Count > 0)
            {
                GrNotas.DataSource = list;
                GrNotas.DataBind();
                txtbusca.Text = "";
            }
            else
            {
                txtbusca.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhum nota encontrada!', 'error');  ", true);

            }


        }

     

        protected void LinkButtonEntPadrao_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie parametro_ativo = Request.Cookies["parametro_ativo"];


            if (perfilAcesso.perfil == "Master" || tela_notas_pendentes.Text == "Total")
            {
                string chaveUsada = "";

                for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                {
                    CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");
                    if (ckIDAprova.Checked)
                    {

                        chaveUsada = GrNotas.DataKeys[indice]["chave_nfe"].ToString();

                    }
                }

                if (chaveUsada != "")
                {
                    //chaveUsada = chaveUsada.PadLeft(9, '0');
                    HttpCookie chavenfe = new HttpCookie("chavenfe");
                    chavenfe.Value = chaveUsada;
                    Response.Cookies.Add(chavenfe);
                    Response.Redirect("~/Views/EntradaPedidoConsumiveis.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario selecionar uma nota', 'error');  ", true);

                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Seu perfil de usuario não tem permissão de acesso.', 'error');  ", true);

            }



        }

        protected void imgbtnRefazer_Click(object sender, ImageClickEventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie parametro_ativo = Request.Cookies["parametro_ativo"];


            NotaPendente notas = new NotaPendente();
            NotaPendenteDao notasDao = new NotaPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            // Identifica o botão que foi clicado
            ImageButton clickedButton = (ImageButton)sender;
            // Identifica a linha que contém o botão
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;

            // Acessa o valor da célula 0
            string cell1 = row.Cells[2].Text;



            try
            {
                notas = notasDao.BuscaNotaClicada(cell1);
                auxEntradaDao.RemoverPorIdNfs(notas.id_xml_nfe);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'A nota fiscal pendente foi " +
                    "reiniciada! Se a nota pendente tenha sido iniciada com outras notas, todas serão reiniciadas!" +
                    " Notas reinciadas:" + notas.nf_entrada + "', 'warning');  ", true);

                carregaNotasPendentes();
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar apagar bipagem!', 'error');  ", true);

            }

        }

        protected void LinkButtonConfirmaRazer_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButtonBuscaNotas_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            ProdutosBarraXmlDao xmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            bool retorno = false;
            try
            {
                retorno = xmlDao.ExecutaSP_DGCS_PROCESSA_NFE_XML_LOJA(codigo_loja.Value);

                if (retorno)
                {
                    carregaNotasPendentes();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'Notas sincronizadas com sucesso!', 'success')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar sincronizar notas, " +
                                                 "entre em contato com o suporte. ', 'error');  ", true);
                }

            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar sincronizar notas, " +
                                 "entre em contato com o suporte. Erro: " + erro.ToString() + "', 'error');  ", true);

            }
        }


    }
}