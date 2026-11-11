using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class CadastroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                 ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];


                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                CarregaServidores();
                carregaUsuarios();
                CarregaPerfis();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] =  ViewState["update2"];
        }
        private void DisplayInfo()
        {
            // Do what ever thing you want to do
             ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        protected void GrUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_usuario_dgcs = DataBinder.Eval(e.Row.DataItem, "id_usuario_dgcs").ToString();
                string nome_completo = DataBinder.Eval(e.Row.DataItem, "nome_completo").ToString();
                string cpf = DataBinder.Eval(e.Row.DataItem, "cpf").ToString();
                string email = DataBinder.Eval(e.Row.DataItem, "email").ToString();
                string perfil_acesso = DataBinder.Eval(e.Row.DataItem, "perfil_acesso").ToString();
                string login_usuario = DataBinder.Eval(e.Row.DataItem, "login_usuario").ToString();
                string senha = cripSenha.DescriptografarSenhaBanco(DataBinder.Eval(e.Row.DataItem, "senha").ToString());
                string situacao = DataBinder.Eval(e.Row.DataItem, "situacao").ToString();
                string codigo_loja = DataBinder.Eval(e.Row.DataItem, "codigo_loja").ToString();
                string data_criacao = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_criacao")).ToString("dd/MM/yyyy");

                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");

                if (situacao == "1")
                {
                    situacao = "Ativo";
                }
                else if (situacao == "0")
                {
                    situacao = "Inativo";
                }

                String strComando = "EditarUsuario('" + id_usuario_dgcs + "','" + nome_completo + "','" + cpf + "','" + email + "','" + perfil_acesso + "','" + login_usuario + "','" + senha + "','" + situacao + "','" + codigo_loja + "');return false;";
                imgUpload.OnClientClick = strComando;

            }

        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                if (senha.Text != senhaConfirma.Text)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                mansagemErro1.Visible = true;
                mansagemErro1.Text = "Atenção! Senhas estão diferentes.";

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append(" $('.modal-backdrop').remove();$('#CriarUsuario').modal('show');");
                sb.Append(" });");
                sb.Append("</script>");


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
            }
            else if (id_usuario_dgcstl.Text == "" && nome_completo.Text != "" && cpf.Text != "" && situacao.Text != "" && email.Text != "" && perfil_acesso.Text != "" && login_usuario.Text != "" && senha.Text != "" && codigo_loja.Text != "")
            {

                if (situacao.Text == "Ativo")
                {
                    usuario.situacao = "1";
                }
                else if (situacao.Text == "Inativo")
                {
                    usuario.situacao = "0";
                }
                usuario.nome_completo = nome_completo.Text;
                usuario.cpf = cpf.Text;
                usuario.email = email.Text;
                usuario.perfil_acesso = perfil_acesso.Text;
                usuario.login_usuario = login_usuario.Text;
                usuario.senha = cripSenha.CriptografarSenhaBanco(senha.Text);
                usuario.codigo_loja = codigo_loja.Text;

                try
                {
                    usuarioDao.Salvar(usuario);

                    nome_completo.Text = "";
                    codigo_loja.Text = "";
                    situacao.Text = "";
                    cpf.Text = "";
                    email.Text = "";
                    perfil_acesso.Text = "";
                    login_usuario.Text = "";
                    senha.Text = "";
                    codigo_loja.Text = "";
                    senhaConfirma.Text = "";

                    carregaUsuarios();

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                }
                catch (Exception erro)
                {
                 

                    nome_completo.Text = "";
                    codigo_loja.Text = "";
                    situacao.Text = "";
                    cpf.Text = "";
                    email.Text = "";
                    perfil_acesso.Text = "";
                    login_usuario.Text = "";
                    senha.Text = "";
                    codigo_loja.Text = "";
                    senhaConfirma.Text = "";

                    if (erro.Message.Contains("Não é possível inserir a chave duplicada no objeto 'dbo.USUARIO_DGCS'"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Login já existe, por gentileza tente outra opção!', 'error')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);

                    }
               }
            }
            else
            {


                id_usuario_dgcstl.Text = "";
                nome_completotl.Text = "";
                codigo_lojatl.Text = "";
                situacaotl.Text = "";
                cpftl.Text = "";
                emailtl.Text = "";
                perfil_acessotl.Text = "";
                login_usuariotl.Text = "";
                senhatl.Text = "";
                codigo_lojatl.Text = "";
                senhaConfirma.Text = "";

                nome_completo.Text = "";
                codigo_loja.Text = "";
                situacao.Text = "";
                cpf.Text = "";
                email.Text = "";
                perfil_acesso.Text = "";
                login_usuario.Text = "";
                senha.Text = "";
                codigo_loja.Text = "";
                senhaConfirma.Text = "";

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
            }

                DisplayInfo();
            }
        }

        protected void carregaUsuarios()
        {
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (perfil_usuario != null)
            {
                string meuObjetoJson = perfil_usuario.Value;
                perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                // Faça o processamento necessário com o objeto
            }

            Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();

            List<Usuario> list = null;
            if (perfilAcesso.perfil != "Gerente Loja")
            {
                list = usuarioDao.BuscaCompletaRetornoLista();
            }
            else
            {
                list = usuarioDao.BuscaCompletaRetornoListaCodigoLoja(codigo_loja.Value);
            }

            GrUsuarios.DataSource = list;
            GrUsuarios.DataBind();
        }



        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();


            string codigo_loja = txtbusca.Text;
            string nome = txtbusca.Text;

            List<Usuario> list = usuarioDao.BuscaRetornoPalavrasChaves(codigo_loja, nome);

            if (list.Count > 0)
            {
                GrUsuarios.DataSource = list;
                GrUsuarios.DataBind();
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

        private void CarregaServidores()
        {
            HttpCookie codigo_lojaCookie = Request.Cookies["codigo_loja"];
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];

            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (perfil_usuario != null)
            {
                string meuObjetoJson = perfil_usuario.Value;
                perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                // Faça o processamento necessário com o objeto
            }

            if (perfilAcesso.perfil != "Gerente Loja")
            {
                using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

                {
                    con.Open();
                    using (var cmd = new SqlCommand("SELECT '' CODIGO_LOJA  UNION" +
                        " SELECT CODIGO_LOJA FROM SERVIDORES_DGCS WHERE SITUACAO = 1", con))

                    {

                        codigo_loja.DataTextField = "CODIGO_LOJA";
                        codigo_loja.DataValueField = "CODIGO_LOJA";

                        codigo_loja.DataSource = cmd.ExecuteReader();
                        codigo_loja.DataBind();

                        con.Close();
                        con.Open();

                        codigo_lojatl.DataTextField = "CODIGO_LOJA";
                        codigo_lojatl.DataValueField = "CODIGO_LOJA";

                        codigo_lojatl.DataSource = cmd.ExecuteReader();
                        codigo_lojatl.DataBind();

                        con.Close();
                    }


                }
            }
            else
            {
                using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

                {
                    con.Open();
                    using (var cmd = new SqlCommand("SELECT '' CODIGO_LOJA  UNION" +
                        " SELECT CODIGO_LOJA FROM SERVIDORES_DGCS WHERE SITUACAO = 1 and CODIGO_LOJA = " + codigo_lojaCookie.Value + " ", con))

                    {

                        codigo_loja.DataTextField = "CODIGO_LOJA";
                        codigo_loja.DataValueField = "CODIGO_LOJA";

                        codigo_loja.DataSource = cmd.ExecuteReader();
                        codigo_loja.DataBind();

                        con.Close();
                        con.Open();

                        codigo_lojatl.DataTextField = "CODIGO_LOJA";
                        codigo_lojatl.DataValueField = "CODIGO_LOJA";

                        codigo_lojatl.DataSource = cmd.ExecuteReader();
                        codigo_lojatl.DataBind();

                        con.Close();
                    }


                }
            }
      


        }
        private void CarregaPerfis()
        {
            HttpCookie codigo_lojaCookie = Request.Cookies["codigo_loja"];
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];

            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (perfil_usuario != null)
            {
                string meuObjetoJson = perfil_usuario.Value;
                perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                // Faça o processamento necessário com o objeto
            }

            if (perfilAcesso.perfil != "Gerente Loja")
            {
                using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

                {
                    con.Open();
                    using (var cmd = new SqlCommand("SELECT '' PERFIL  UNION" +
                        " SELECT PERFIL FROM PERFIS_ACESSO", con))

                    {

                        perfil_acesso.DataTextField = "PERFIL";
                        perfil_acesso.DataValueField = "PERFIL";

                        perfil_acesso.DataSource = cmd.ExecuteReader();
                        perfil_acesso.DataBind();

                        con.Close();
                        con.Open();

                        perfil_acessotl.DataTextField = "PERFIL";
                        perfil_acessotl.DataValueField = "PERFIL";

                        perfil_acessotl.DataSource = cmd.ExecuteReader();
                        perfil_acessotl.DataBind();

                        con.Close();
                    }


                }
            }
            else
            {
                using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

                {
                    con.Open();
                    using (var cmd = new SqlCommand("SELECT '' PERFIL  UNION" +
                        " SELECT PERFIL FROM PERFIS_ACESSO WHERE PERFIL IN ('Operador','Gerente Loja') ", con))

                    {

                        perfil_acesso.DataTextField = "PERFIL";
                        perfil_acesso.DataValueField = "PERFIL";

                        perfil_acesso.DataSource = cmd.ExecuteReader();
                        perfil_acesso.DataBind();

                        con.Close();
                        con.Open();

                        perfil_acessotl.DataTextField = "PERFIL";
                        perfil_acessotl.DataValueField = "PERFIL";

                        perfil_acessotl.DataSource = cmd.ExecuteReader();
                        perfil_acessotl.DataBind();

                        con.Close();
                    }


                }
            }


        }

        protected void LinkButtonEditar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                 if (id_usuario_dgcstl.Text != "" && nome_completotl.Text != "" && cpftl.Text != "" && situacaotl.Text != "" && emailtl.Text != "" && perfil_acessotl.Text != "" && login_usuariotl.Text != "" && senhatl.Text != "" && codigo_lojatl.Text != "")
                {
                    if (situacaotl.Text == "Ativo")
                    {
                        usuario.situacao = "1";
                    }
                    else if (situacaotl.Text == "Inativo")
                    {
                        usuario.situacao = "0";
                    }
                    usuario.id_usuario_dgcs = id_usuario_dgcstl.Text;
                    usuario.nome_completo = nome_completotl.Text;
                    usuario.cpf = cpftl.Text;
                    usuario.email = emailtl.Text;
                    usuario.perfil_acesso = perfil_acessotl.Text;
                    usuario.login_usuario = login_usuariotl.Text;
                    usuario.senha = cripSenha.CriptografarSenhaBanco(senhatl.Text);
                    usuario.codigo_loja = codigo_lojatl.Text;

                    try
                    {
                        usuarioDao.Atualizar(usuario);

                        id_usuario_dgcstl.Text = "";
                        nome_completotl.Text = "";
                        codigo_lojatl.Text = "";
                        situacaotl.Text = "";
                        cpftl.Text = "";
                        emailtl.Text = "";
                        perfil_acessotl.Text = "";
                        login_usuariotl.Text = "";
                        senhatl.Text = "";
                        codigo_lojatl.Text = "";
                        senhaConfirma.Text = "";

                        carregaUsuarios();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        id_usuario_dgcstl.Text = "";
                        nome_completotl.Text = "";
                        codigo_lojatl.Text = "";
                        situacaotl.Text = "";
                        cpftl.Text = "";
                        emailtl.Text = "";
                        perfil_acessotl.Text = "";
                        login_usuariotl.Text = "";
                        senhatl.Text = "";
                        codigo_lojatl.Text = "";
                        senhaConfirma.Text = "";

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                    }
                }
                else
                {


                    id_usuario_dgcstl.Text = "";
                    nome_completotl.Text = "";
                    codigo_lojatl.Text = "";
                    situacaotl.Text = "";
                    cpftl.Text = "";
                    emailtl.Text = "";
                    perfil_acessotl.Text = "";
                    login_usuariotl.Text = "";
                    senhatl.Text = "";
                    codigo_lojatl.Text = "";
                    senhaConfirma.Text = "";

                    nome_completo.Text = "";
                    codigo_loja.Text = "";
                    situacao.Text = "";
                    cpf.Text = "";
                    email.Text = "";
                    perfil_acesso.Text = "";
                    login_usuario.Text = "";
                    senha.Text = "";
                    codigo_loja.Text = "";
                    senhaConfirma.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }

                DisplayInfo();
            }
        }
    }
}