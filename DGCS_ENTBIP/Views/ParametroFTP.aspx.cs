using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class ParametroFTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaUsuarios();
            }
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
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


        protected void carregaUsuarios()
        {
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];


            ParametrosFtp parametroFTP = new ParametrosFtp();
            ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            parametroFTP = fTPDao.BuscaFTPcadastrado();

            if(parametroFTP != null)
            {
                parametroFTP.senha = cripSenha.DescriptografarSenhaBanco(parametroFTP.senha);

                servidor.Text = parametroFTP.servidor;
                pasta.Text = parametroFTP.pasta;
                usuarioFTP.Text = parametroFTP.usuario;
                senha.Text = parametroFTP.senha;
                status.Text = parametroFTP.status;

            }

        }



        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            ParametrosFtp parametroFTP = new ParametrosFtp();
            ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                parametroFTP = fTPDao.BuscaFTPcadastrado();

                if (parametroFTP != null && servidor.Text != "" && pasta.Text != "" && usuarioFTP.Text != "" && senha.Text != "" && status.Text != "")
                {
                    try
                    {
                        parametroFTP.servidor = servidor.Text;
                        parametroFTP.pasta = pasta.Text;
                        parametroFTP.usuario = usuarioFTP.Text;
                        parametroFTP.senha = cripSenha.CriptografarSenhaBanco(senha.Text);
                        parametroFTP.status = status.Text;


                        fTPDao.Atualizar(parametroFTP);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);

                    }
                    catch (Exception erro)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar Atualizar FTP!', 'error')", true);

                    }
                }
                else if (parametroFTP == null && servidor.Text != "" && pasta.Text != "" && usuarioFTP.Text != "" && senha.Text != "" && status.Text != "")
                {
                    try
                    {
                        parametroFTP = new ParametrosFtp();
                        parametroFTP.servidor = servidor.Text;
                        parametroFTP.pasta = pasta.Text;
                        parametroFTP.usuario = usuarioFTP.Text;
                        parametroFTP.senha = cripSenha.CriptografarSenhaBanco(senha.Text);
                        parametroFTP.status = status.Text;


                        fTPDao.Salvar(parametroFTP);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);

                    }
                    catch (Exception erro)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar Atualizar FTP!', 'error')", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }

            }

            DisplayInfo();
        }
    }
}