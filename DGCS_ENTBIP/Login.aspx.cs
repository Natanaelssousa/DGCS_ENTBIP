using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void esqueceuSenha_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ResetarSenha.aspx", false);
        }

        public static string RemoveSpecialCharacters(string text, bool allowSpace = false)
        {
            string ret;

            if (allowSpace)
                ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", string.Empty);
            else
                ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);

            return ret;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuarioModel = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            Servidor servidor = new Servidor();
            ServidorDao servidorDao = new ServidorDao();
            Parametro parametro = new Parametro();
            Parametro parametroEmail = new Parametro();
            Parametro parametroEscala = new Parametro();
            Parametro parametroLote = new Parametro();
            ParametroDao parametroDao = new ParametroDao();
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            PerfilAcessoDao perfilAcessoDao = new PerfilAcessoDao();

            string senhaCriptografada =  cripSenha.CriptografarSenhaBanco(senha.Text);

            try
            {
                usuarioModel = usuarioDao.BuscaUsuarioLogin(usuario.Text, senhaCriptografada);


                if (usuarioModel.situacao == "Ativo")
                {
                    perfilAcesso = perfilAcessoDao.BuscaPorDataRetornoObjeto(usuarioModel.perfil_acesso);

                    HttpCookie perfil_usuario = new HttpCookie("perfil_usuario");
                    string perfil_usuarioJson = JsonConvert.SerializeObject(perfilAcesso);
                    perfil_usuario.Value = perfil_usuarioJson;
                    Response.Cookies.Add(perfil_usuario);

                    HttpCookie usuarioLogin = new HttpCookie("usuario");
                    usuarioLogin.Value = usuarioModel.login_usuario;
                    Response.Cookies.Add(usuarioLogin);


                    HttpCookie dt_acesso = new HttpCookie("dt_acesso");
                    dt_acesso.Value = DateTime.Now.ToString();
                    Response.Cookies.Add(dt_acesso);



                    servidor = servidorDao.BuscaPorDataRetornoObjeto(usuarioModel.codigo_loja);
                    parametro = parametroDao.BuscaParametro(servidor.parametro_ativo);
                    parametroEmail = parametroDao.BuscaParametro("Enviar E-mail");
                    parametroLote = parametroDao.BuscaParametro("Lote");
                    parametroEscala = parametroDao.BuscaParametro("Acesso Escala");
                    if (servidor != null)
                    {
                        HttpCookie token = new HttpCookie("token");
                        token.Value = cripSenha.DescriptografarSenhaBanco(servidor.token);
                        Response.Cookies.Add(token);

                        HttpCookie pareametro_ciclico = new HttpCookie("pareametro_ciclico");
                        pareametro_ciclico.Value = servidor.parametro_ciclico;
                        Response.Cookies.Add(pareametro_ciclico);


                        HttpCookie codigo_loja = new HttpCookie("codigo_loja");
                        codigo_loja.Value = servidor.codigo_loja;
                        Response.Cookies.Add(codigo_loja);

                        HttpCookie cnpjLoja = new HttpCookie("cnpjLoja");
                        cnpjLoja.Value = servidor.cnpj;
                        Response.Cookies.Add(cnpjLoja);

                        HttpCookie banco_loja = new HttpCookie("banco_loja");
                        banco_loja.Value = servidor.nome_db;
                        Response.Cookies.Add(banco_loja);

                        HttpCookie parametro_data = new HttpCookie("parametro_data");
                        parametro_data.Value = parametro.data_fim;
                        Response.Cookies.Add(parametro_data);


                        HttpCookie servidorCookie = new HttpCookie("servidorCookie");
                        servidorCookie.Value = servidor.nome_servidor;
                        Response.Cookies.Add(servidorCookie);


                        HttpCookie banco = new HttpCookie("banco");
                        banco.Value = servidor.nome_db_app;
                        Response.Cookies.Add(banco);

                        HttpCookie usuario_banco = new HttpCookie("usuario_banco");
                        usuario_banco.Value = servidor.usuario_banco;
                        Response.Cookies.Add(usuario_banco);

                        HttpCookie parametro_ativo = new HttpCookie("parametro_ativo");
                        parametro_ativo.Value = servidor.parametro_ativo;
                        Response.Cookies.Add(parametro_ativo);

                        HttpCookie parametro_ativo_Email = new HttpCookie("parametro_ativo_Email");
                        parametro_ativo_Email.Value = parametroEmail.status;
                        Response.Cookies.Add(parametro_ativo_Email);

                        HttpCookie parametro_ativo_Lote = new HttpCookie("parametro_ativo_Lote");
                        parametro_ativo_Lote.Value = parametroLote.status;
                        Response.Cookies.Add(parametro_ativo_Lote);

                        HttpCookie parametro_escala = new HttpCookie("acessoEscala");
                        parametro_escala.Value = parametroEscala.valor;
                        Response.Cookies.Add(parametro_escala);

                        HttpCookie senha_banco = new HttpCookie("senha_banco");
                        senha_banco.Value = cripSenha.DescriptografarSenhaBanco(servidor.senha_banco);
                        Response.Cookies.Add(senha_banco);


                        Response.Redirect("~/Views/Menu.aspx", false);
                    }
                    usuario.Text = "";
                    senha.Text = "";

                    if ((usuarioModel.perfil_acesso == "TD") && servidor != null)
                    {

                        Response.Redirect("~/Views/Menu.aspx", false);
                    }
                    else if (usuarioModel.perfil_acesso == "Master" && (servidor == null || servidor != null) && (usuarioModel.login_usuario == "natanael.santos" || usuarioModel.login_usuario == "daniel.carvalho"))
                    {
                        if (servidor != null)
                        {
                            HttpCookie token = new HttpCookie("token");
                            token.Value = cripSenha.DescriptografarSenhaBanco(servidor.token);
                            Response.Cookies.Add(token);


                            HttpCookie pareametro_ciclico = new HttpCookie("pareametro_ciclico");
                            pareametro_ciclico.Value = servidor.parametro_ciclico;
                            Response.Cookies.Add(pareametro_ciclico);


                            HttpCookie codigo_loja = new HttpCookie("codigo_loja");
                            codigo_loja.Value = servidor.codigo_loja;
                            Response.Cookies.Add(codigo_loja);


                            HttpCookie cnpjLoja = new HttpCookie("cnpjLoja");
                            cnpjLoja.Value = servidor.cnpj;
                            Response.Cookies.Add(cnpjLoja);

                            HttpCookie servidorCookie = new HttpCookie("servidorCookie");
                            servidorCookie.Value = servidor.nome_servidor;
                            Response.Cookies.Add(servidorCookie);



                            HttpCookie banco = new HttpCookie("banco");
                            banco.Value = servidor.nome_db_app;
                            Response.Cookies.Add(banco);


                            HttpCookie banco_loja = new HttpCookie("banco_loja");
                            banco_loja.Value = servidor.nome_db;
                            Response.Cookies.Add(banco_loja);

                            HttpCookie usuario_banco = new HttpCookie("usuario_banco");
                            usuario_banco.Value = servidor.usuario_banco;
                            Response.Cookies.Add(usuario_banco);

                            HttpCookie parametro_ativo = new HttpCookie("parametro_ativo");
                            parametro_ativo.Value = servidor.parametro_ativo;
                            Response.Cookies.Add(parametro_ativo);

                            HttpCookie senha_banco = new HttpCookie("senha_banco");
                            senha_banco.Value = cripSenha.DescriptografarSenhaBanco(servidor.senha_banco);
                            Response.Cookies.Add(senha_banco);
                        }
                        else
                        {
                            ViewState["servidor"] = "";

                        }


                        Response.Redirect("~/Views/Menu.aspx", false);
                    }
                    else if ((usuarioModel.perfil_acesso != "Master" && usuarioModel.perfil_acesso != "TD") && servidor == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Servidor vinculado com o usuario não encontrado! Solicite o cadastro', 'error')", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Usúario esta inativo!', 'error')", true);
                    usuario.Text = "";
                    senha.Text = "";
                }



            }
            catch (Exception erro)
            {
                string input = erro.Message.ToString();
                string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
                string replacement = "";
                Regex rgx = new Regex(pattern);
                string result = rgx.Replace(input, replacement);

                string erros = result;
                if (servidor.nome_servidor == null && usuarioModel != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Falha ao tentar se conectar ao servidor. Verifique a Rede (VPN)!', 'error')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "swal('Atenção', 'Usúario e senha invalidos!', 'error')", true);
                }


                usuario.Text = "";
                senha.Text = "";
            }
        }
    }
}