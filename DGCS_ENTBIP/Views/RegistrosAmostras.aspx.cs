using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class RegistrosAmostras : System.Web.UI.Page
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

                carregaBipagem(usuarioLogin.Value);

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
        protected void LinkButtonSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }

        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloRegistrosAmostras.aspx", false);
        }

        protected void carregaTotais(string resposavel)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];


            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
      banco.Value, usuario_banco.Value, senha_banco.Value);

            List<RegistroAmostras> list = new List<RegistroAmostras>();

            list = amostraDao.ConsultaRegistroEmAberto(usuarioLogin.Value, servidorCookie.Value);

            if (list.Count > 0)
            {
                int qtdeBipado = 0;
                int qtdeTotalitens = 0;

                foreach (RegistroAmostras amostra in list)
                {
                    qtdeBipado = (Convert.ToInt16(qtdeBipado) + Convert.ToInt16(amostra.qtde));
                    qtdeTotalitens = list.Count;

                }

                qtdeTotal.Text = qtdeBipado.ToString();
                qtdeTotalBipado.Text = qtdeTotalitens.ToString();

            }


        }
        protected void carregaBipagem(string resposavel)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];


            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
      banco.Value, usuario_banco.Value, senha_banco.Value);

            List<RegistroAmostras> list = new List<RegistroAmostras>();

            list = amostraDao.ConsultaRegistroEmAberto(usuarioLogin.Value, servidorCookie.Value);

            if (list.Count > 0)
            {
                cpf.Text = list.ElementAt(list.Count - 1).cpf_cliente;
                cpf.ReadOnly = true;
                Gramostras.DataSource = null;
                Gramostras.DataSource = list;

                Gramostras.DataBind();
            }
            else
            {
                Gramostras.DataSource = null;

                Gramostras.DataBind();
            }

            carregaTotais(usuarioLogin.Value);

        }

        protected void cod_barra_TextChanged(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                RegistroAmostras amostra = new RegistroAmostras();

                RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                try
                {
                    amostra = amostraDao.ConsultaRegistroItem(usuarioLogin.Value, cod_barra.Text, cpf.Text);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar ler registro.', 'error');  ", true);

                }


                if (amostra.produto == null)
                {
                    amostra.cpf_cliente = cpf.Text;
                    amostra.produto = cod_barra.Text;
                    amostra.responsavel = usuarioLogin.Value;
                    amostra.qtde = "1";
                    amostra.cod_filial = codigo_loja.Value;

                    try
                    {

                        amostraDao.Salvar(amostra);
                        mensagem.Visible = true;
                        mensagem.Text = "Produto bipado confirmado!";
                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                        cod_barra.Text = "";
                        cpf.ReadOnly = true;
                        carregaBipagem(usuarioLogin.Value);
                    }
                    catch (Exception erro)
                    {
                        cod_barra.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar gravar amostra.', 'error');  ", true);

                    }


                }
                else
                {
                    try
                    {
                        int qtdeAtualizada = (Convert.ToInt16(amostra.qtde) + 1);
                        amostra.qtde = qtdeAtualizada.ToString();
                        amostraDao.Atualizar(amostra);
                        mensagem.Visible = true;
                        mensagem.Text = "Produto bipado confirmado!";
                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                        cod_barra.Text = "";
                        cpf.ReadOnly = true;
                        carregaBipagem(usuarioLogin.Value);
                    }
                    catch (Exception erro)
                    {
                        cod_barra.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar gravar amostra.', 'error');  ", true);

                    }

                }






            }
            else
            {
                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];


                cod_barra.Text = "";
                cod_barra.Focus();
            }

        }


        public void GerarTXTnotaRetaguarda()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];

            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
banco.Value, usuario_banco.Value, senha_banco.Value);


            List<RegistroAmostras> list = new List<RegistroAmostras>();

            try
            {

                list = amostraDao.ConsultaRegistroEmAberto(usuarioLogin.Value, servidorCookie.Value);
            }
            catch (Exception erro)
            {
                mensagem.Visible = true;
                mensagem.Text = "erro na consulta Aux_Entrada";
                mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
            }

            List<string> scriptTXT = new List<string>();
            try
            {
                ParametrosFtp parametroFTP = new ParametrosFtp();
                ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
              banco.Value, usuario_banco.Value, senha_banco.Value);


                parametroFTP = fTPDao.BuscaFTPcadastrado();

                string cod_filial = "";
                foreach (RegistroAmostras aux_entrada in list)
                {
                    cod_filial = aux_entrada.cod_filial;
                    scriptTXT.Add(amostraDao.SalvarRetaguardatxt(aux_entrada, parametroFTP));

                }

                // Obtém a data e hora atual
                DateTime dataHoraAtual = DateTime.Now;

                // Formata a data e hora no formato de banco de dados
                string dataHoraFormatada = dataHoraAtual.ToString("yyyyMMddHHmmss");
                string nomeArquivo = "AmostraProd" + cod_filial + "_" + dataHoraFormatada + ".txt";
                string caminhoPasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Content", "Write");
                string caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

                // Escrever cada item da lista no arquivo
                using (StreamWriter writer = new StreamWriter(caminhoArquivo))
                {
                    foreach (string linha in scriptTXT)
                    {
                        writer.WriteLine(linha);
                    }


                }


                // enviar ftp
                CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
                parametroFTP.senha = cripSenha.DescriptografarSenhaBanco(parametroFTP.senha);

                FtpClient ftpClient = new FtpClient("ftp://" + parametroFTP.servidor + "/", "" + parametroFTP.usuario + "", "" + parametroFTP.senha + "");


                if (parametroFTP.status != "0")
                {
                    if (ftpClient.TestConnection())
                    {
                        // A conexão é bem-sucedida, você pode prosseguir com as operações FTP
                        string localFilePath = caminhoArquivo;
                        string remoteFilePath = @"/" + parametroFTP.pasta + "/" + nomeArquivo;
                        ftpClient.UploadFile(localFilePath, remoteFilePath);
                    }
                    else
                    {
                        Console.WriteLine("erro ao gravar no diretorio");
                    }

                    Console.WriteLine($"Arquivo gerado em: {caminhoArquivo}");
                }

            }
            catch (Exception erro)
            {
                mensagem.Visible = true;
                mensagem.Text = "erro ao tentar gravar Aux_Entrada na retaguarda";
                mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
            }


        }

        protected void LinkButtonFinalizar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            RegistroAmostras amostra = new RegistroAmostras();
            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
       banco.Value, usuario_banco.Value, senha_banco.Value);


            try
            {
                cod_barra.Text = "";
                cpf.Text = "";
                cpf.ReadOnly = false;
                cpf.Focus();
                amostra.responsavel = usuarioLogin.Value;
                GerarTXTnotaRetaguarda();
                amostraDao.AtualizarStatus(amostra);
                carregaBipagem(usuarioLogin.Value);
                mensagem.Visible = true;
                qtdeTotal.Text = "0";
                qtdeTotalBipado.Text = "0";
                mensagem.Text = "finalizado registro!";
                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
            }
            catch (Exception erro)
            {
                mensagem.Visible = false;
                cod_barra.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar finalizar amostras.', 'error');  ", true);

            }

        }

        protected void LinkButtonRefazer_Click(object sender, EventArgs e)
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            RegistroAmostras amostra = new RegistroAmostras();
            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
       banco.Value, usuario_banco.Value, senha_banco.Value);


            try
            {
                amostra.responsavel = usuarioLogin.Value;
                amostraDao.Remover(amostra);
                cod_barra.Text = "";
                cpf.Text = "";
                cpf.ReadOnly = false;
                carregaBipagem(usuarioLogin.Value);
                mensagem.Visible = true;
                qtdeTotal.Text = "0";
                qtdeTotalBipado.Text = "0";
                mensagem.Text = "Removido a bipagem!";
                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
            }
            catch (Exception erro)
            {
                mensagem.Visible = false;
                cod_barra.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar remover amostras.', 'error');  ", true);

            }


        }

        protected void LinkButtonPausar_Click(object sender, EventArgs e)
        {
            cpf.Text = "";
            cpf.ReadOnly = false;
            cpf.Focus();
        }


        protected void Gramostras_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}