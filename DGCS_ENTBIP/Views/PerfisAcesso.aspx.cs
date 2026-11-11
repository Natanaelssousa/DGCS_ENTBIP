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
    public partial class PerfisAcesso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaPerfis();
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
        protected void GrPerfis_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_perfil = DataBinder.Eval(e.Row.DataItem, "id_perfil").ToString();
                string perfil = DataBinder.Eval(e.Row.DataItem, "perfil").ToString();
                string tela_notas_pendentes = DataBinder.Eval(e.Row.DataItem, "tela_notas_pendentes").ToString();
                string tela_cadastro_servidor = DataBinder.Eval(e.Row.DataItem, "tela_cadastro_servidor").ToString();
                string tela_cadastro_usuario = DataBinder.Eval(e.Row.DataItem, "tela_cadastro_usuario").ToString();
                string tela_parametros = DataBinder.Eval(e.Row.DataItem, "tela_parametros").ToString();
                string tela_relatorio = DataBinder.Eval(e.Row.DataItem, "tela_relatorio").ToString();
                string tela_emails = DataBinder.Eval(e.Row.DataItem, "tela_emails").ToString();
                string tela_perfis_acesso = DataBinder.Eval(e.Row.DataItem, "tela_perfis_acesso").ToString();
                string tela_relatorio_abastecimento = DataBinder.Eval(e.Row.DataItem, "tela_relatorio_abastecimento").ToString();
                string tela_relatorio_log = DataBinder.Eval(e.Row.DataItem, "tela_relatorio_log").ToString();
                string tela_pedidos_consumiveis = DataBinder.Eval(e.Row.DataItem, "tela_pedidos_consumiveis").ToString();
                string tela_inventario_ciclico = DataBinder.Eval(e.Row.DataItem, "tela_inventario_ciclico").ToString();
                string tela_parametro_ftp = DataBinder.Eval(e.Row.DataItem, "tela_parametro_ftp").ToString();
                string tela_registro_amostra = DataBinder.Eval(e.Row.DataItem, "tela_registro_amostra").ToString();

                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");



                String strComando = "EditarPerfil('" + id_perfil + "','" + perfil + "','" + tela_notas_pendentes + "','"
                    + tela_cadastro_servidor + "','" + tela_cadastro_usuario + "','" + tela_parametros + "','" + tela_relatorio + "','"
                    + tela_emails + "','" + tela_perfis_acesso + "','" + tela_relatorio_abastecimento + "','"
                    + tela_relatorio_log + "','" + tela_pedidos_consumiveis + "','" + tela_inventario_ciclico + "'" +
                    ",'" + tela_parametro_ftp + "','" + tela_registro_amostra + "');return false;";
                imgUpload.OnClientClick = strComando;

            }

        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {
            PerfilAcesso perfils = new PerfilAcesso();
            PerfilAcessoDao perfilDao = new PerfilAcessoDao();


            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {



                if (id_perfil_dgcstl.Text == "" && perfil.Text != "" && tela_cadastro_servidor.Text != "" && tela_cadastro_usuario.Text != ""
                    && tela_emails.Text != "" && tela_notas_pendentes.Text != "" && tela_parametros.Text != "" && tela_relatorio.Text != ""
                    && tela_perfis_acesso.Text != "" && tela_relatorio_abastecimento.Text != "" && tela_relatorio_logs.Text != ""
                    && tela_entrada_pedido_consumiveis.Text != "" && tela_inventario_ciclico.Text != ""
                    && tela_parametro_ftp.Text != "" && tela_registro_amostra.Text != "")
                {


                    perfils.perfil = perfil.Text;
                    perfils.tela_notas_pendentes = tela_notas_pendentes.Text;
                    perfils.tela_cadastro_servidor = tela_cadastro_servidor.Text;
                    perfils.tela_cadastro_usuario = tela_cadastro_usuario.Text;
                    perfils.tela_emails = tela_emails.Text;
                    perfils.tela_parametros = tela_parametros.Text;
                    perfils.tela_relatorio = tela_relatorio.Text;
                    perfils.tela_perfis_acesso = tela_perfis_acesso.Text;
                    perfils.tela_relatorio_abastecimento = tela_relatorio_abastecimento.Text;
                    perfils.tela_relatorio_log = tela_relatorio_logs.Text;
                    perfils.tela_pedidos_consumiveis = tela_entrada_pedido_consumiveis.Text;
                    perfils.tela_inventario_ciclico = tela_inventario_ciclico.Text;
                    perfils.tela_parametro_ftp = tela_parametro_ftp.Text;
                    perfils.tela_registro_amostra = tela_registro_amostra.Text;

                    try
                    {
                        perfilDao.Salvar(perfils);

                        perfil.Text = "";
                        tela_notas_pendentes.Text = "";
                        tela_cadastro_servidor.Text = "";
                        tela_cadastro_usuario.Text = "";
                        tela_emails.Text = "";
                        tela_parametros.Text = "";
                        tela_relatorio.Text = "";
                        tela_perfis_acesso.Text = "";
                        tela_relatorio_abastecimento.Text = "";
                        tela_relatorio_logs.Text = "";
                        tela_entrada_pedido_consumiveis.Text = "";
                        tela_parametro_ftp.Text = "";
                        tela_registro_amostra.Text = "";
                        carregaPerfis();

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {


                        perfil.Text = "";
                        tela_notas_pendentes.Text = "";
                        tela_cadastro_servidor.Text = "";
                        tela_cadastro_usuario.Text = "";
                        tela_emails.Text = "";
                        tela_parametros.Text = "";
                        tela_relatorio.Text = "";
                        tela_perfis_acesso.Text = "";
                        tela_relatorio_abastecimento.Text = "";
                        tela_relatorio_logs.Text = "";
                        tela_entrada_pedido_consumiveis.Text = "";
                        tela_inventario_ciclico.Text = "";
                        tela_parametro_ftp.Text = "";
                        tela_registro_amostra.Text = "";

                        if (erro.Message.Contains("Não é possível inserir a chave duplicada no objeto 'dbo.PERFIS_ACESSO'"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Login já existe, por gentileza tente outra opção!', 'error')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);

                        }
                    }
                }
                else if (id_perfil_dgcstl.Text != "" && perfiltl.Text != "" && tela_notas_pendentestl.Text != "" &&
                    tela_cadastro_servidortl.Text != "" && tela_cadastro_usuariotl.Text != "" && tela_emailstl.Text != ""
                    && tela_parametrostl.Text != "" && tela_perfis_acessotl.Text != "" && tela_relatoriotl.Text != ""
                   && tela_relatorio_abastecimentotl.Text != "" && tela_relatorio_logstl.Text != ""
                   && tela_entrada_pedido_consumiveistl.Text != "" && tela_inventario_ciclicotl.Text != ""
                   && tela_parametro_ftptl.Text != "" && tela_registro_amostratl.Text != "")
                {

                    perfils.id_perfil = id_perfil_dgcstl.Text;
                    perfils.perfil = perfiltl.Text;
                    perfils.tela_notas_pendentes = tela_notas_pendentestl.Text;
                    perfils.tela_cadastro_servidor = tela_cadastro_servidortl.Text;
                    perfils.tela_cadastro_usuario = tela_cadastro_usuariotl.Text;
                    perfils.tela_emails = tela_emailstl.Text;
                    perfils.tela_parametros = tela_parametrostl.Text;
                    perfils.tela_relatorio = tela_relatoriotl.Text;
                    perfils.tela_perfis_acesso = tela_perfis_acessotl.Text;
                    perfils.tela_relatorio_abastecimento = tela_relatorio_abastecimentotl.Text;
                    perfils.tela_relatorio_log = tela_relatorio_logstl.Text;
                    perfils.tela_pedidos_consumiveis = tela_entrada_pedido_consumiveistl.Text;
                    perfils.tela_inventario_ciclico = tela_inventario_ciclicotl.Text;
                    perfils.tela_parametro_ftp = tela_parametro_ftptl.Text;
                    perfils.tela_registro_amostra = tela_registro_amostratl.Text;

                    try
                    {
                        perfilDao.Atualizar(perfils)
;
                        id_perfil_dgcstl.Text = "";
                        perfiltl.Text = "";
                        tela_notas_pendentestl.Text = "";
                        tela_cadastro_servidortl.Text = "";
                        tela_cadastro_usuariotl.Text = "";
                        tela_emailstl.Text = "";
                        tela_parametrostl.Text = "";
                        tela_relatoriotl.Text = "";
                        tela_perfis_acessotl.Text = "";
                        tela_relatorio_abastecimentotl.Text = "";
                        tela_relatorio_logstl.Text = "";
                        tela_entrada_pedido_consumiveistl.Text = "";
                        tela_inventario_ciclicotl.Text = "";
                        tela_parametro_ftptl.Text = "";
                        tela_registro_amostratl.Text = "";

                        carregaPerfis();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        id_perfil_dgcstl.Text = "";
                        perfiltl.Text = "";
                        tela_notas_pendentestl.Text = "";
                        tela_cadastro_servidortl.Text = "";
                        tela_cadastro_usuariotl.Text = "";
                        tela_emailstl.Text = "";
                        tela_parametrostl.Text = "";
                        tela_relatoriotl.Text = "";
                        tela_perfis_acessotl.Text = "";
                        tela_relatorio_abastecimentotl.Text = "";
                        tela_relatorio_logstl.Text = "";
                        tela_entrada_pedido_consumiveistl.Text = "";
                        tela_inventario_ciclicotl.Text = "";
                        tela_parametro_ftptl.Text = "";
                        tela_registro_amostratl.Text = "";

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                    }
                }
                else
                {


                    id_perfil_dgcstl.Text = "";
                    perfiltl.Text = "";
                    tela_notas_pendentestl.Text = "";
                    tela_cadastro_servidortl.Text = "";
                    tela_cadastro_usuariotl.Text = "";
                    tela_emailstl.Text = "";
                    tela_parametrostl.Text = "";
                    tela_relatoriotl.Text = "";
                    tela_perfis_acessotl.Text = "";
                    tela_relatorio_abastecimentotl.Text = "";
                    tela_relatorio_logstl.Text = "";
                    tela_entrada_pedido_consumiveistl.Text = "";
                    tela_inventario_ciclicotl.Text = "";
                    tela_parametro_ftptl.Text = "";
                    tela_registro_amostratl.Text = "";


                    perfil.Text = "";
                    tela_notas_pendentes.Text = "";
                    tela_cadastro_servidor.Text = "";
                    tela_cadastro_usuario.Text = "";
                    tela_emails.Text = "";
                    tela_parametros.Text = "";
                    tela_relatorio.Text = "";
                    tela_perfis_acesso.Text = "";
                    tela_relatorio_abastecimento.Text = "";
                    tela_relatorio_logs.Text = "";
                    tela_entrada_pedido_consumiveis.Text = "";
                    tela_inventario_ciclico.Text = "";
                    tela_parametro_ftp.Text = "";
                    tela_registro_amostra.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }

                DisplayInfo();
            }
        }

        protected void carregaPerfis()
        {

            PerfilAcesso perfils = new PerfilAcesso();
            PerfilAcessoDao perfilDao = new PerfilAcessoDao();


            List<PerfilAcesso> list = perfilDao.BuscaCompletaRetornoLista();

            GrPerfis.DataSource = list;
            GrPerfis.DataBind();
        }



        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                PerfilAcesso perfils = new PerfilAcesso();
                PerfilAcessoDao perfilDao = new PerfilAcessoDao();


                string perfil = txtbusca.Text;


                List<PerfilAcesso> list = perfilDao.BuscaRetornoPalavrasChaves(perfil);

                if (list.Count > 0)
                {
                    GrPerfis.DataSource = list;
                    GrPerfis.DataBind();
                    txtbusca.Text = "";
                }
                else
                {
                    txtbusca.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhum cadastro encontrado!', 'error')", true);

                }

                DisplayInfo();
            }
        }


    }
}