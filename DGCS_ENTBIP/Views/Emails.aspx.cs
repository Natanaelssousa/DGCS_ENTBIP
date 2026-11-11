using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
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
    public partial class Emails : System.Web.UI.Page
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
                //CarregaServidores();
                carregaEmails();
                carregaEmails2();
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
        protected void GrEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_emails = DataBinder.Eval(e.Row.DataItem, "id_emails").ToString();
                string nome = DataBinder.Eval(e.Row.DataItem, "nome").ToString();
                string email = DataBinder.Eval(e.Row.DataItem, "email").ToString();


                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");



                String strComando = "EditarEmail('" + id_emails + "','" + nome + "','" + email + "');return false;";
                imgUpload.OnClientClick = strComando;

            }

        }


        protected void GrEmails2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_emailsRm = DataBinder.Eval(e.Row.DataItem, "id_emails").ToString();
                string nomeRm = DataBinder.Eval(e.Row.DataItem, "nome").ToString();
                string emailRm = DataBinder.Eval(e.Row.DataItem, "email").ToString();
                string senhaRm = DataBinder.Eval(e.Row.DataItem, "senha").ToString();
                string servidorEmailRm = DataBinder.Eval(e.Row.DataItem, "servidor_email").ToString();
                string host_envioRm = DataBinder.Eval(e.Row.DataItem, "host_envio").ToString();


                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");



                String strComando = "EditarEmail2('" + id_emailsRm + "','" + nomeRm + "','" + emailRm + "','" + senhaRm + "'," +
                    "'" + servidorEmailRm + "','" + host_envioRm + "');return false;";
                imgUpload.OnClientClick = strComando;

            }

        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {
            Email_Divergencia emails = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if ( ViewState["update2"].ToString() == ViewState["update"].ToString())
            {


                if (id_emails.Text == "" && nome.Text != "" && email.Text != "")
                {


                    emails.nome = nome.Text;
                    emails.email = email.Text;


                    try
                    {
                        emailDao.Salvar(emails);

                        nome.Text = "";
                        email.Text = "";


                        carregaEmails();

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        nome.Text = "";
                        email.Text = "";

                        if (erro.Message.Contains("Não é possível inserir a chave duplicada no objeto 'dbo.EMAILS_DIVERGENCIA'"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'email já existe, por gentileza tente outra opção!', 'error')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);

                        }
                    }
                }
                else if (id_emails.Text != "" && nometl.Text != "" && emailtl.Text != "")
                {
                    emails.id_emails = id_emails.Text;
                    emails.nome = nometl.Text;
                    emails.email = emailtl.Text;

                    try
                    {

                        emailDao.Atualizar(emails);

                        id_emails.Text = "";
                        nometl.Text = "";
                        emailtl.Text = "";


                        carregaEmails();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        id_emails.Text = "";
                        nometl.Text = "";
                        emailtl.Text = "";

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                    }
                }
                else
                {


                    id_emails.Text = "";
                    nometl.Text = "";
                    emailtl.Text = "";

                    nome.Text = "";
                    email.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }

                DisplayInfo();
            }
        }

        protected void carregaEmails()
        {

            Email_Divergencia email = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();


            List<Email_Divergencia> list = emailDao.BuscaCompletaRetornoLista();

            GrEmails.DataSource = list;
            GrEmails.DataBind();
        }


        protected void carregaEmails2()
        {

            Email_Divergencia email = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();


            List<Email_Divergencia> list = emailDao.BuscaCompletaRetornoListaEmailEnvio();

            GrEmails2.DataSource = list;
            GrEmails2.DataBind();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            if ( ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                Usuario usuario = new Usuario();
                UsuarioDao usuarioDao = new UsuarioDao();


                string codigo_loja = txtbusca.Text;
                string nome = txtbusca.Text;

                List<Usuario> list = usuarioDao.BuscaRetornoPalavrasChaves(codigo_loja, nome);

                if (list.Count > 0)
                {
                    GrEmails.DataSource = list;
                    GrEmails.DataBind();
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

        protected void LinkButtonConfirmarEnvio_Click(object sender, EventArgs e)
        {
            Email_Divergencia emails = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if ( ViewState["update2"].ToString() == ViewState["update"].ToString())
            {


                if (id_emailsRm.Text == "" && nomeRm.Text != "" && emailRm.Text != "" && senhaRm.Text != "" && servidorEmailRm.Text != "" && host_envioRm.Text != "")
                {


                    emails.nome = nomeRm.Text;
                    emails.email = emailRm.Text;
                    emails.senha = emailRm.Text;
                    emails.servidor_email = emailRm.Text;
                    emails.host_envio = emailRm.Text;



                    try
                    {
                        emailDao.Salvar(emails);

                        nomeRm.Text = "";
                        emailRm.Text = "";
                        emails.senha = "";
                        emails.servidor_email = "";
                        emails.host_envio = "";

                        carregaEmails();

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        nome.Text = "";
                        email.Text = "";

                        if (erro.Message.Contains("Não é possível inserir a chave duplicada no objeto 'dbo.EMAILS_DIVERGENCIA'"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'email já existe, por gentileza tente outra opção!', 'error')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);

                        }
                    }
                }
                else if (id_emailsRm.Text != "" && nomeRm.Text != "" && emailRm.Text != "" && senhaRm.Text != "" && servidorEmailRm.Text != "" && host_envioRm.Text != "")
                {
                    emails.id_emails = id_emailsRm.Text;
                    emails.nome = nomeRm.Text;
                    emails.email = emailRm.Text;
                    emails.senha = senhaRm.Text;
                    emails.servidor_email = servidorEmailRm.Text;
                    emails.host_envio = host_envioRm.Text;


                    try
                    {

                        emailDao.Atualizar_Email_Envio(emails);

                        id_emailsRm.Text = "";
                        nomeRm.Text = "";
                        emailRm.Text = "";
                        emails.senha = "";
                        emails.servidor_email = "";
                        emails.host_envio = "";


                        carregaEmails2();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                    }
                    catch (Exception erro)
                    {

                        id_emailsRm.Text = "";
                        nomeRm.Text = "";
                        emailRm.Text = "";
                        emails.senha = "";
                        emails.servidor_email = "";
                        emails.host_envio = "";

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                    }
                }
                else
                {


                    id_emailsRm.Text = "";
                    nomeRm.Text = "";
                    emailRm.Text = "";
                    emails.senha = "";
                    emails.servidor_email = "";
                    emails.host_envio = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }

                DisplayInfo();
            }
        }
    }
}