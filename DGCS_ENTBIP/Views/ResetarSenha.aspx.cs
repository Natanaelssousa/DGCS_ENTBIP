using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Emails;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class ResetarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());


            if (nCpf.Text != "" && email.Text != "")
            {

                try
                {
                    usuario = usuarioDao.BuscaUsuarioCPF(nCpf.Text, email.Text);

                    if (usuario != null && usuario.situacao == "Ativo")
                    {
                        usuario.senha = cripSenha.CriptografarSenhaBanco("senhaNova@123");

                        //usuarioDao.ResetDeSenha(usuario);
                        //EnviaEmailReset(usuario.email);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$(function () {");
                        sb.Append("$('#resetUsuario').modal('show');");
                        sb.Append(" });");
                        sb.Append("</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);



                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Usuario resetado com sucesso! Verifique sua caixa de e-mail.', 'success')", true);

                    }
                    else
                    {
                        if (usuario != null && usuario.situacao == "Inativo")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Usúario esta inativado!', 'error')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Usúario não encontrado ou alguma informação está errada!', 'error')", true);

                        }

                        nCpf.Text = "";
                        email.Text = "";
                    }

                  
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Algo deu errado, verifque as informações inseridas!', 'error')", true);
                    nCpf.Text = "";
                    email.Text = "";
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem ser informados!', 'error')", true);

            }


        }

        protected void EnviaEmailReset(string emailEnvio)
        {


            Email enviaEmail = new Email();



            try
            {
                Email_Divergencia emails = new Email_Divergencia();
                Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
                emails = emailDao.BuscaEmailEnvio();
                StringBuilder textoEmail = new StringBuilder();
                textoEmail.AppendFormat("Olá, foi solicitado o reset da sua senha no portal DGCS_ENBIP. Sua nova senha é: senhaNova@123, após o acesso faça a alteração de senha.");
                StringBuilder texto = textoEmail;

                enviaEmail.EnviarMail(emailEnvio, "E-mail DGCS_ENTBIP", texto, emails.email);


            }
            catch (Exception err)
            {


                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Algo deu errado, verifque as informações inseridas!', 'error')", true);
                nCpf.Text = "";
                email.Text = "";
            }


        }


        protected void LinkButtonCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButtonSalvar_Click(object sender, EventArgs e)
        {

            Usuario usuario = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            usuario = usuarioDao.BuscaUsuarioCPF(nCpf.Text, email.Text);

            if (senha.Text == senhaConfirma.Text)
            {
                usuario.senha = cripSenha.CriptografarSenhaBanco(senha.Text);
                usuario.cpf = nCpf.Text;
                try
                {
                    usuarioDao.ResetDeSenhaCPF(usuario);

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Usuario resetado com sucesso!.', 'success')", true);
                    nCpf.Text = "";
                    email.Text = "";
                    senha.Text = "";
                    senhaConfirma.Text = "";
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Algo deu errado, tente novamente!', 'error')", true);
                    nCpf.Text = "";
                    email.Text = "";
                    senha.Text = "";
                    senhaConfirma.Text = "";
                }



            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Senhas estão diferentes!', 'error')", true);

            }


        }
    }
}