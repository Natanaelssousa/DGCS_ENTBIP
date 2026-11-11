using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class EntradaPedidoConsumiveis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chaveNfe.Focus();
            if (!IsPostBack)
            {

                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;




            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = ViewState["update2"];
        }

        private void DisplayInfo()
        {
            // Do what ever thing you want to do
            ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/PedidosPendentes.aspx", false);
        }



        protected void chaveNfe_TextChanged(object sender, EventArgs e)
        {


            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie chavenfe = Request.Cookies["chavenfe"];

                ComprasPedido comprasPedido1 = new ComprasPedido();
                List<ComprasPedido> list = new List<ComprasPedido>();
                ComprasPedidoDao pedidoDao = new ComprasPedidoDao();

                comprasPedido1 = pedidoDao.BuscaPorDataRetornoObjeto(chaveNfe.Text);

                if (comprasPedido1 == null && GrNotas.Rows.Count == 0)
                {
                    ComprasPedido comprasPedido = new ComprasPedido();
                    string nf = chaveNfe.Text.Substring(25, 9);
                    string serie = chaveNfe.Text.Substring(22, 3);
                    string cnpj = chaveNfe.Text.Substring(6, 14);
                 

                    if (chavenfe.Value.Trim() == chaveNfe.Text.Trim())
                    {
                        comprasPedido.chave_nfe = chaveNfe.Text;
                        comprasPedido.nf_entrada = nf;
                        comprasPedido.serie_nf_entrada = serie;
                        comprasPedido.cnpj = cnpj;
                        comprasPedido.usuario = usuarioLogin.Value;

                        list.Add(comprasPedido);


                        GrNotas.DataSource = null;
                        GrNotas.DataSource = list;

                        GrNotas.DataBind();

                        chaveNfe.Text = "";
                        chaveNfe.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'A nota bipada não é a mesma que foi selecionada!', 'warning');  ", true);
                        chaveNfe.Text = "";
                    }


                }
                else
                {

                    if (GrNotas.Rows.Count != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Só é possivel bipar uma nota por vez.', 'warning');  ", true);
                        chaveNfe.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave já possui baixa.', 'warning');  ", true);
                        chaveNfe.Text = "";
                    }

                    chaveNfe.Text = "";
                    chaveNfe.Focus();
                }

                DisplayInfo();
            }
            else
            {

                chaveNfe.Text = "";
                chaveNfe.Focus();
            }
        }

        protected void LinkButtonFinalizar_Click(object sender, EventArgs e)
        {

            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
            if (vezes_bipado == null)
            {
                HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                vezes_bipado1.Value = "1";
                Response.Cookies.Add(vezes_bipado1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                vezes_bipado.Value = "1";
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(vezes_bipado);
            }

            if (validaItensQtde == null)
            {
                HttpCookie validaItensQtde1 = new HttpCookie("validaItensQtde");
                validaItensQtde1.Value = "true";
                Response.Cookies.Add(validaItensQtde1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                validaItensQtde.Value = "true";
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(validaItensQtde);
            }

            confirmaEntradaLabel.Text = "Confirma baixa da nota ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalconfirmaEntradas').modal('show');");
            sb.Append(" });");
            sb.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);


        }

        protected void LinkButtonRefazer_Click(object sender, EventArgs e)
        {

            confirmaRefazer.Text = "Confirma refazer a bipagem da chave da nota ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalRefazer').modal('show');");
            sb.Append(" });");
            sb.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

        }

        protected void GrNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {



        }
        public string Extract(string input, int len)
        {
            if (string.IsNullOrEmpty(input) || input.Length < len)
            {
                return input;
            };

            return input.Substring(input.Length - 6);
        }

        protected void LinkButtonConfirmaRazer_Click(object sender, EventArgs e)
        {
            GrNotas.DataSource = null;

            GrNotas.DataBind();
        }

        protected void LinkButtonNegaRefazer_Click(object sender, EventArgs e)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Atualiza tela 
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalRefazer').modal('hide');$('.modal-backdrop').remove();");
            sb.Append(" });");
            sb.Append("</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Janela", sb.ToString(), false);
            chaveNfe.Text = "";
            mensagem.Visible = false;
        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {

            ComprasPedido comprasPedido = new ComprasPedido();
            ComprasPedidoDao pedidoDao = new ComprasPedidoDao();

      

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                HttpCookie banco = Request.Cookies["banco"];
                HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                HttpCookie senha_banco = Request.Cookies["senha_banco"];
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie codigo_loja = Request.Cookies["codigo_loja"];
                HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
                HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
                HttpCookie banco_loja = Request.Cookies["banco_loja"];

                PedidoPendente pedidoPendente = new PedidoPendente();
                PedidoPendenteDao notaPendenteDao = new PedidoPendenteDao(servidorCookie.Value,
              banco.Value, usuario_banco.Value, senha_banco.Value);


                for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                {

                    comprasPedido.chave_nfe = GrNotas.Rows[indice].Cells[3].Text;
                    comprasPedido.nf_entrada = GrNotas.Rows[indice].Cells[0].Text;
                    comprasPedido.serie_nf_entrada = GrNotas.Rows[indice].Cells[1].Text;
                    comprasPedido.cnpj = GrNotas.Rows[indice].Cells[2].Text;


                }

                comprasPedido.usuario = usuarioLogin.Value;
                comprasPedido.loja = codigo_loja.Value;


                try
                {



                    ProdutosBarraXmlDao xmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                    bool retorno = false;
                    try
                    {

                        ParametrosFtp parametroFTP = new ParametrosFtp();
                        ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
                      banco.Value, usuario_banco.Value, senha_banco.Value);


                        parametroFTP = fTPDao.BuscaFTPcadastrado();

                        pedidoDao.Salvartxt(comprasPedido, parametroFTP);
                        pedidoDao.Salvar(comprasPedido);
                        retorno = xmlDao.ExecutaSP_DGCS_PROCESSOS_retaguarda();

                        pedidoPendente.chave_nfe = comprasPedido.chave_nfe;
                        pedidoPendente.status = "1";

                        notaPendenteDao.Atualizar(pedidoPendente);

                        if (retorno)
                        {
                            //GrNotas.DataSource = null;

                            //GrNotas.DataBind();

                            confirmaEntSucesso.Text = "Parabéns entrada feita com sucesso!";

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$(function () {");
                            sb.Append("$('#ModalEntSucesso').modal('show');");
                            sb.Append(" });");
                            sb.Append("</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
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
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar gravar baixa.', 'error');  ", true);

                }

            }



            DisplayInfo();
        }


        protected void LinkButtonEntSucesso_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Views/PedidosPendentes.aspx", false);
        }


        }
}