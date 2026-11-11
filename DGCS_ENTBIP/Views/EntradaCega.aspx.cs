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
    public partial class EntradaCega : System.Web.UI.Page
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
                NotasSelecionadas();



            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] =  ViewState["update2"] ;
        }

        private void DisplayInfo()
        {
            // Do what ever thing you want to do
             ViewState["update2"]  = Server.UrlEncode(System.DateTime.Now.ToString());
        }


        protected void carregaBipagem(string romaneio)
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            List<AuxEntrada> list = new List<AuxEntrada>();

            list = auxEntradaDao.ConsultaBipagem(romaneio, usuarioLogin.Value);

            GrNotas.DataSource = null;
            GrNotas.DataSource = list;

            GrNotas.DataBind();
        }

        protected int QtdeTotalNotasSelecionadas()
        {
            int qtde = 0;

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            try
            {
                NotaPendente notas = new NotaPendente();
                NotaPendenteDao pendenteDao = new NotaPendenteDao(servidorCookie.Value,
     banco.Value, usuario_banco.Value, senha_banco.Value);

                List<NotaPendente> list = null;
                HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
                if (NotaSelecionadas != null)
                {
                    string listaObjetosJson = NotaSelecionadas.Value;
                    list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                    // Faça o processamento necessário com a lista de objetos
                }

                string nfs = "";

                foreach (NotaPendente nota in list)
                {
                    qtde += pendenteDao.BuscaQtdeTotalNota(nota.id_xml_nfe);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Falha na consulta de dados!', 'error');  ", true);

            }



            return qtde;
        }


        protected void NotasSelecionadas()
        {
            HttpCookie romaneio_pendente = Request.Cookies["romaneio_pendente"];
            HttpCookie qtde_bipado = Request.Cookies["qtde_bipado"];
            NotaPendente notas = new NotaPendente();
            List<NotaPendente> list = null;
            HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
            if (NotaSelecionadas != null)
            {
                string listaObjetosJson = NotaSelecionadas.Value;
                list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                // Faça o processamento necessário com a lista de objetos
            }
            string nfs = "";
            string ids = "";
            int qtde = 0;

            foreach (NotaPendente nota in list)
            {
                if (nfs == "")
                {
                    nfs = nota.nf_entrada.Trim();
                }
                else
                {
                    nfs += "," + nota.nf_entrada.Trim();
                }

                if (ids == "")
                {
                    ids = nota.id_xml_nfe.Trim();
                }
                else
                {
                    ids += "," + nota.id_xml_nfe.Trim();
                }

            }


            HttpCookie idsCookie = Request.Cookies["ids"];
            if (idsCookie == null)
            {
                HttpCookie idsCookie1 = new HttpCookie("ids");
                idsCookie1.Value = ids;
                Response.Cookies.Add(idsCookie1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                idsCookie.Value = ids;
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(idsCookie);
            }


            if (romaneio_pendente.Value != "")
            {

            
                    romaneioEntrada.Text = romaneio_pendente.Value;

                    if (qtde_bipado.Value != "")
                    {
                        qtdeTotalBipado.Text = qtde_bipado.Value;
                    }
                    else
                    {
                        qtdeTotalBipado.Text = "0";
                    }

                    carregaBipagem(romaneioEntrada.Text);

                    // O cookie foi encontrado, então você pode modificar o valor
                    romaneio_pendente.Value = "";
                    // Atualize o cookie no navegador do cliente
                    Response.SetCookie(romaneio_pendente);
                    // O cookie foi encontrado, então você pode modificar o valor
                    qtde_bipado.Value = "";
                    // Atualize o cookie no navegador do cliente
                    Response.SetCookie(qtde_bipado);

              
            }


            qtde = QtdeTotalNotasSelecionadas();
            notaEmAbertoQtdes();
            notasSelecionadas.Text = nfs;
            qtdeTotalNotas.Text = qtde.ToString();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NotasPendentes.aspx", false);
        }




        public void notaEmAbertoQtdes()
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
        banco.Value, usuario_banco.Value, senha_banco.Value);
            string[] notaPendente = new string[3];

            notaPendente = auxEntradaDao.ConsultaNotaEmAbertoResponsavel(usuarioLogin.Value);

            if (notaPendente[0] != null)
            {
                qtdeTotalBipado.Text = notaPendente[0].Trim();
                romaneioEntrada.Text = notaPendente[1].Trim();
            }



        }
        public List<AuxEntrada> consultaProduto(string chave)
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            List<AuxEntrada> auxEntradaLista = new List<AuxEntrada>();
            AuxEntrada auxEntrada;
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            Produto produto = new Produto();
            ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            List<Produto> listChave;

            List<NotaPendente> list = null;
            HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
            if (NotaSelecionadas != null)
            {
                string listaObjetosJson = NotaSelecionadas.Value;
                list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                // Faça o processamento necessário com a lista de objetos
            }
            AuxEntrada auxEntradaRomaneio = new AuxEntrada();
            auxEntradaRomaneio = auxEntradaDao.ConsultaRomaneioEmAberto(romaneioEntrada.Text, usuarioLogin.Value);

            string[] id_xml_nfe = new string[list.Count];
            int i = 0;
            string romaneio = "";
         


            foreach (NotaPendente nota in list)
            {
                id_xml_nfe[i] = nota.id_xml_nfe;
                i++;
                romaneio += nota.chave_nfe.Substring(42, 2);
            }


            listChave = produtoDao.BuscaProdutoNotasSelecionadasChave(chaveNfe.Text.Trim(), id_xml_nfe);

            foreach (Produto produtoChave in listChave)
            {
                auxEntrada = new AuxEntrada();

                auxEntrada.produto = produtoChave.produto.Trim();
                auxEntrada.cor_produto = produtoChave.cor_produto.Trim();
                auxEntrada.codigo_barra = produtoChave.codigo_barra.Trim();
                auxEntrada.tamanho = produtoChave.tamanho.Trim();
                auxEntrada.qtde_total = produtoChave.qtde_total.Trim();
                auxEntrada.qtde_bipado = auxEntrada.qtde_total;
                auxEntrada.fornecedor = produtoChave.fornecedor.Trim();
                auxEntrada.responsavel = usuarioLogin.Value;
                auxEntrada.desc_produto = produtoChave.desc_produto.Trim();
                if (auxEntradaRomaneio.romaneio_produto == null)
                {
                    romaneio = auxEntradaDao.BuscaProximoRomaneio();
                    auxEntrada.romaneio_produto = "DG" + romaneio.Trim();
                
                  
                }
                else
                {
                    romaneio = auxEntradaRomaneio.romaneio_produto;
                    auxEntrada.romaneio_produto = romaneio.Trim();
                }
                auxEntrada.preco1 = produtoChave.preco1.Trim();
                auxEntrada.ids_nfs_selecionadas = produtoChave.ids_nfs_selecionadas.Trim();
                auxEntrada.qtde_vezes_bipado = "1";
                auxEntrada.chave_nfe = produtoChave.chave_nfe;
                auxEntrada.grupo = produtoChave.grupo;
                auxEntrada.subgrupo = produtoChave.subgrupo;

                auxEntradaLista.Add(auxEntrada);
            }

            auxEntradaDao.AtualizarRomaneio();
            return auxEntradaLista;
        }

        protected void chaveNfe_TextChanged(object sender, EventArgs e)
        {

            if ( ViewState["update2"] .ToString() == ViewState["update"].ToString())
            {

                HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                HttpCookie banco = Request.Cookies["banco"];
                HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                HttpCookie senha_banco = Request.Cookies["senha_banco"];
                HttpCookie usuarioLogin = Request.Cookies["usuario"];

                List<AuxEntrada> auxEntradaList = new List<AuxEntrada>();
                AuxEntrada auxEntrada = new AuxEntrada();
                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                bool chave_jaBipada = false;
                chave_jaBipada = auxEntradaDao.VerificaSeChaveJaFoiBipado(chaveNfe.Text);
                if (chave_jaBipada == false)
                {

                    try
                    {
                        //CONSULTA SE EXISTE NOTA EM ABERTO


                        auxEntrada = auxEntradaDao.ConsultaNotaEmAbertoChave(romaneioEntrada.Text, usuarioLogin.Value);




                        if (auxEntrada.produto == null)
                        {

                            //CONSULTA SE CHAVE PERTENCE AS NOTAS EM ABERTO
                            auxEntradaList = consultaProduto(chaveNfe.Text);

                            if (auxEntradaList.Count > 0)
                            {
                                bool codnull = false;
                                foreach (AuxEntrada nota in auxEntradaList)
                                {

                                    if (nota.codigo_barra == "")
                                    {
                                        codnull = true;
                                    }

                                }

                                if (codnull == false)
                                {
                                    foreach (AuxEntrada nota in auxEntradaList)
                                    {




                                        romaneioEntrada.Text = nota.romaneio_produto;

                                        auxEntradaDao.Salvar(nota);
                                        notaEmAbertoQtdes();
                                        chaveNfe.Focus();
                                        carregaBipagem(romaneioEntrada.Text);

                                    }

                                    //mensagem

                                    mensagem.Visible = true;
                                    mensagem.Text = "Chave bipada confirmada!";
                                    mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave bipada possui produtos sem cadastro. Não é possivel fazer a entrada antes da correção.', 'error');  ", true);
                                    chaveNfe.Text = "";
                                    mensagem.Visible = false;
                                }

                            }
                            else
                            {
                                mensagem.Visible = true;
                                mensagem.Text = "Chave não encontrada!";
                                mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                                chaveNfe.Text = "";
                                mensagem.Visible = false;
                            }
                        }
                        else
                        {
                            romaneioEntrada.Text = auxEntrada.romaneio_produto;
                            //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                            auxEntradaList = consultaProduto(chaveNfe.Text);

                            if (auxEntradaList.Count > 0)
                            {

                                bool codnull = false;
                                foreach (AuxEntrada nota in auxEntradaList)
                                {

                                    if (nota.codigo_barra == "")
                                    {
                                        codnull = true;
                                    }

                                }

                                if (codnull == false)
                                {
                                    foreach (AuxEntrada nota in auxEntradaList)
                                    {

                                        nota.romaneio_produto = auxEntrada.romaneio_produto;

                                        auxEntradaDao.Salvar(nota);
                                        notaEmAbertoQtdes();
                                        chaveNfe.Focus();
                                        carregaBipagem(romaneioEntrada.Text);

                                    }

                                    //mensagem

                                    mensagem.Visible = true;
                                    mensagem.Text = "Chave bipada confirmada!";
                                    mensagem.CssClass = "alert alert-success alert-dismissible fade show";

                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave bipada possui produtos sem cadastro. Não é possivel fazer a entrada antes da correção.', 'error');  ", true);
                                    chaveNfe.Text = "";
                                }
                            }
                            else
                            {
                                mensagem.Visible = true;
                                mensagem.Text = "Chave não encontrada!";
                                mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                            }


                        }





                    }
                    catch (Exception erroFinal)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na consulta da chave nfe', 'error');  ", true);
                        mensagem.Visible = false;
                    }
                }
                else
                {


                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave ja foi bipada!', 'error');  ", true);
                    mensagem.Visible = false;


                }
                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                if (romaneio_sessao == null)
                {
                    HttpCookie romaneio_sessao1 = new HttpCookie("romaneio_sessao");
                    romaneio_sessao1.Value = romaneioEntrada.Text;
                    Response.Cookies.Add(romaneio_sessao1);
                }
                else
                {
                    // O cookie foi encontrado, então você pode modificar o valor
                    romaneio_sessao.Value = romaneioEntrada.Text;
                    // Atualize o cookie no navegador do cliente
                    Response.SetCookie(romaneio_sessao);
                }

                chaveNfe.Text = "";
                chaveNfe.Focus();
                DisplayInfo();
            }
            else
            {

                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];

                if (romaneio_sessao != null)
                {
                    carregaBipagem(romaneio_sessao.Value);
                    romaneioEntrada.Text = romaneio_sessao.Value;
                }
                else
                {
                    carregaBipagem(romaneioEntrada.Text);
                    romaneio_sessao.Value = romaneioEntrada.Text;
                    Response.Cookies.Add(romaneio_sessao);
                }
                chaveNfe.Text = "";
                chaveNfe.Focus();
            }
        }

        protected void LinkButtonFinalizar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            HttpCookie parametro_ativo_Lote = Request.Cookies["parametro_ativo_Lote"];

            if (Convert.ToInt16(qtdeTotalNotas.Text) == Convert.ToInt16(qtdeTotalBipado.Text))
            {
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


                HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
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

                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
     banco.Value, usuario_banco.Value, senha_banco.Value);

                //GRAVA PRIMEIRA BIPAGEM DOS ITENS
                auxEntradaDao.GuardaContagem1(romaneioEntrada.Text, usuarioLogin.Value);

                confirmaEntradaLabel.Text = "Confirma entrada das notas " + notasSelecionadas.Text + " ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append("$('#ModalconfirmaEntradas').modal('show');");
                sb.Append(" });");
                sb.Append("</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Precisa ser bipado todas as notas selecionadas', 'error');  ", true);
            }

        }

        protected void LinkButtonRefazer_Click(object sender, EventArgs e)
        {

            confirmaRefazer.Text = "Confirma refazer a bipagem das notas " + notasSelecionadas.Text + " ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalRefazer').modal('show');");
            sb.Append(" });");
            sb.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

        }

        protected void LinkButtonPausar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/NotasPendentes.aspx", false);
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
            if ( ViewState["update2"] .ToString() == ViewState["update"].ToString())
            {

                HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                HttpCookie banco = Request.Cookies["banco"];
                HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                HttpCookie senha_banco = Request.Cookies["senha_banco"];
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
                HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
                HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];

                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
           banco.Value, usuario_banco.Value, senha_banco.Value);
                try
                {
                    List<NotaPendente> list = null;
                    HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
                    if (NotaSelecionadas != null)
                    {
                        string listaObjetosJson = NotaSelecionadas.Value;
                        list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                        // Faça o processamento necessário com a lista de objetos
                    }

                    string IDnfs = "";
                    int qtde = 0;

                    foreach (NotaPendente nota in list)
                    {
                        if (IDnfs == "")
                        {
                            IDnfs = nota.id_xml_nfe.Trim();

                        }
                        else
                        {
                            IDnfs += "," + nota.id_xml_nfe.Trim();
                        }




                    }
                    auxEntradaDao.RemoverPor(romaneioEntrada.Text, usuarioLogin.Value, IDnfs);

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'Nota Removida!', 'success')", true);

                    romaneioEntrada.Text = "00000000";
                    qtdeTotalBipado.Text = "0";
                    GrNotas.DataSource = null;
                    mensagem.Visible = false;

                    GrNotas.DataBind();
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na tentativa de apagar lista', 'error');  ", true);

                }

                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                if (romaneio_sessao == null)
                {
                    HttpCookie romaneio_sessao1 = new HttpCookie("romaneio_sessao");
                    romaneio_sessao1.Value = romaneioEntrada.Text;
                    Response.Cookies.Add(romaneio_sessao1);
                }
                else
                {
                    // O cookie foi encontrado, então você pode modificar o valor
                    romaneio_sessao.Value = romaneioEntrada.Text;
                    // Atualize o cookie no navegador do cliente
                    Response.SetCookie(romaneio_sessao);
                }

                DisplayInfo();

            }
            else
            {
                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                carregaBipagem(romaneio_sessao.Value);
                romaneioEntrada.Text = romaneio_sessao.Value;
                chaveNfe.Text = "";
                chaveNfe.Focus();
            }
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
            if ( ViewState["update2"] .ToString() == ViewState["update"].ToString())
            {
                HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                HttpCookie banco = Request.Cookies["banco"];
                HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                HttpCookie senha_banco = Request.Cookies["senha_banco"];
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
                HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
                HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
                HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                HttpCookie ids = Request.Cookies["ids"];
                HttpCookie div_item = Request.Cookies["div_item"];


                AuxEntrada auxEntrada = new AuxEntrada();
                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                ProdutosBarraXml produtosBarraXml = new ProdutosBarraXml();
                ProdutosBarraXmlDao produtosBarraXmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


                EntradaDao entradaDao = new EntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                EntradaProdutoDao entradaProdutoDao = new EntradaProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                List<NotaPendente> list = null;
                HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
                if (NotaSelecionadas != null)
                {
                    string listaObjetosJson = NotaSelecionadas.Value;
                    list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                    // Faça o processamento necessário com a lista de objetos
                }
                string[] id_xml_nfe = new string[list.Count];
                int i = 0;
                bool erroFinal = false;
                foreach (NotaPendente nota in list)
                {
                    id_xml_nfe[i] = nota.id_xml_nfe;
                    i++;
                }


                int cont = 0;
                int teste = id_xml_nfe.Count();
                while (id_xml_nfe.Count() > cont)
                {
                    Entrada entrada = null;
                    List<EntradaProduto> listaEntradaProduto = null;
                    List<ProdutosBarraXml> listaProdutosBarras = null;
                    listaProdutosBarras = produtosBarraXmlDao.BuscaNotasParaDarEntrada(id_xml_nfe[cont]);
                    // inserindo informações para a gravação da tabela loja_entradas inicio
                    int qtdeTotal = 0;
                    double valorTotal = 0;

                    foreach (ProdutosBarraXml notaId in listaProdutosBarras)
                    {

                        qtdeTotal += Convert.ToInt16(notaId.qtde);
                        valorTotal += Convert.ToDouble(Convert.ToDecimal(notaId.valor_prod_tot.Replace(".", ",")), System.Globalization.CultureInfo.GetCultureInfo("en-US"));


                    }
                    string romaneio_entrada = entradaDao.consultaSequenciaNotaEntrada();
                    Parametro parametro = new Parametro();
                    ParametroDao parametroDao = new ParametroDao();
                    parametro = parametroDao.BuscaParametro("LOJA_TIPOS_ENTRADA_SAIDA");
                    entrada = new Entrada();
                    entrada.codigo_tab_preco = "00";
                    entrada.tipo_entrada_saida = parametro.valor;
                    entrada.desc_tipo_entrada_saida = parametro.descricao;
                    entrada.utiliza_preco_origem = "1";
                    entrada.especie_serie = "5";
                    entrada.lx_status_entrada = "1";
                    entrada.entrada_cancelada = "0";
                    entrada.entrada_conferida = "1";
                    entrada.entrada_encerrada = "1";
                    entrada.pedido_status_loja = "0";
                    entrada.entrada_por = "1";
                    entrada.valor_nao_conferido = "0.00";
                    entrada.qtde_nao_conferida = "0";
                    entrada.fator_preco = "0.00";
                    entrada.serie_nf_entrada = listaProdutosBarras.ElementAt(0).serie_nf_entrada;
                    entrada.valor_total = valorTotal.ToString().Replace(",", ".");
                    entrada.qtde_total = qtdeTotal.ToString();
                    entrada.entrada_sem_produtos = "0";
                    entrada.chave_nfe = listaProdutosBarras.ElementAt(0).chave_nfe;
                    entrada.obs = "Entrada realizada por sistema de bipagem - DG Consultoria e Sistemas";
                    entrada.data_saida = Convert.ToDateTime(listaProdutosBarras.ElementAt(0).data_saida).ToString("yyyy-MM-dd");
                    entrada.responsavel = usuarioLogin.Value;
                    entrada.numero_nf_transferencia = listaProdutosBarras.ElementAt(0).nf_entrada;
                    entrada.filial = listaProdutosBarras.ElementAt(0).destinatario;
                    entrada.filial_origem = listaProdutosBarras.ElementAt(0).destinatario;
                    entrada.fornecedor = listaProdutosBarras.ElementAt(0).nome_clifor;
                    entrada.cnpj = listaProdutosBarras.ElementAt(0).cnpj;
                    entrada.romaneio_produto = "DG" + Extract((romaneio_entrada + DateTime.Now.Month + DateTime.Now.Day), 6);

                    try
                    {
                        entradaDao.Salvar(entrada);
                        listaEntradaProduto = new List<EntradaProduto>();





                        // inserindo informações para a gravação da tabela loja_entradas fim

                        // inserindo informações para a gravação da tabela loja_entradas_produto inicio
                        foreach (ProdutosBarraXml notaId in listaProdutosBarras)
                        {
                            if (listaProdutosBarras.ElementAt(0).id_xml_nfe == notaId.id_xml_nfe)
                            {

                                EntradaProduto entradaProduto = new EntradaProduto();
                                string[] vetorAtributos = null;
                                vetorAtributos = entradaProduto.retornaVetor();
                                for (int ind = 0; ind < vetorAtributos.Length; ind++)
                                {
                                    string vlr = "en" + notaId.tamanho;
                                    if (vlr.Trim() == vetorAtributos[ind])
                                    {
                                        vetorAtributos[ind] = notaId.qtde;
                                    }
                                    else
                                    {
                                        vetorAtributos[ind] = "0";
                                    }
                                }

                                entradaProduto = entradaProduto.retornaObjeto(vetorAtributos);
                                entradaProduto.filial = notaId.destinatario;
                                entradaProduto.romaneio_produto = "DG" + Extract((romaneio_entrada + DateTime.Now.Month + DateTime.Now.Day), 6);
                                entradaProduto.produto = notaId.produto;
                                entradaProduto.cor_produto = notaId.cor_produto;
                                entradaProduto.qtde_entrada = notaId.qtde;
                                entradaProduto.preco1 = notaId.valor_un;
                                entradaProduto.preco2 = "0.0000000000";
                                entradaProduto.preco3 = "0.0000000000";
                                entradaProduto.preco4 = "0.0000000000";
                                entradaProduto.valor = notaId.valor_prod_tot;
                                entradaProduto.cor_sortida_trocada = "";
                                entradaProduto.pedido_compra = "";
                                entradaProduto.pedido = "";

                                listaEntradaProduto.Add(entradaProduto);
                            }

                        }


                        EntradaProduto entradaProdutoNovo = null;

                        List<EntradaProduto> listaEntradaProdutoFinal = new List<EntradaProduto>();

                        foreach (ProdutosBarraXml item1 in listaProdutosBarras)
                        {
                            if (listaProdutosBarras.ElementAt(0).id_xml_nfe == item1.id_xml_nfe)
                            {
                                entradaProdutoNovo = new EntradaProduto("0");

                                foreach (EntradaProduto item2 in listaEntradaProduto)
                                {


                                    if (item1.produto.Trim() == item2.produto.Trim() && item1.cor_produto.Trim() == item2.cor_produto.Trim())
                                    {



                                        entradaProdutoNovo.cor_sortida_trocada = item2.cor_sortida_trocada;
                                        entradaProdutoNovo.pedido_compra = item2.pedido_compra;
                                        entradaProdutoNovo.pedido = item2.pedido;
                                        entradaProdutoNovo.romaneio_produto = item2.romaneio_produto;
                                        entradaProdutoNovo.filial = item2.filial;
                                        entradaProdutoNovo.produto = item2.produto;
                                        entradaProdutoNovo.cor_produto = item2.cor_produto;
                                        entradaProdutoNovo.preco1 = item2.preco1.ToString().Replace(',', '.');
                                        entradaProdutoNovo.preco2 = item2.preco2.ToString().Replace(',', '.');
                                        entradaProdutoNovo.preco3 = item2.preco3.ToString().Replace(',', '.');
                                        entradaProdutoNovo.preco4 = item2.preco4.ToString().Replace(',', '.');
                                        entradaProdutoNovo.qtde_entrada = (Convert.ToInt32(entradaProdutoNovo.qtde_entrada) + Convert.ToInt32(item2.qtde_entrada)).ToString();
                                        decimal valor = Convert.ToInt16(entradaProdutoNovo.qtde_entrada) * Convert.ToDecimal(entradaProdutoNovo.preco1.Replace('.', ','));
                                        entradaProdutoNovo.valor = (valor).ToString().Replace(',', '.');
                                        entradaProdutoNovo.en1 = (Convert.ToInt32(entradaProdutoNovo.en1) + Convert.ToInt32(item2.en1)).ToString();
                                        entradaProdutoNovo.en2 = (Convert.ToInt32(entradaProdutoNovo.en2) + Convert.ToInt32(item2.en2)).ToString();
                                        entradaProdutoNovo.en3 = (Convert.ToInt32(entradaProdutoNovo.en3) + Convert.ToInt32(item2.en3)).ToString();
                                        entradaProdutoNovo.en4 = (Convert.ToInt32(entradaProdutoNovo.en4) + Convert.ToInt32(item2.en4)).ToString();
                                        entradaProdutoNovo.en5 = (Convert.ToInt32(entradaProdutoNovo.en5) + Convert.ToInt32(item2.en5)).ToString();
                                        entradaProdutoNovo.en6 = (Convert.ToInt32(entradaProdutoNovo.en6) + Convert.ToInt32(item2.en6)).ToString();
                                        entradaProdutoNovo.en7 = (Convert.ToInt32(entradaProdutoNovo.en7) + Convert.ToInt32(item2.en7)).ToString();
                                        entradaProdutoNovo.en8 = (Convert.ToInt32(entradaProdutoNovo.en8) + Convert.ToInt32(item2.en8)).ToString();
                                        entradaProdutoNovo.en9 = (Convert.ToInt32(entradaProdutoNovo.en9) + Convert.ToInt32(item2.en9)).ToString();
                                        entradaProdutoNovo.en10 = (Convert.ToInt32(entradaProdutoNovo.en10) + Convert.ToInt32(item2.en10)).ToString();
                                        entradaProdutoNovo.en11 = (Convert.ToInt32(entradaProdutoNovo.en11) + Convert.ToInt32(item2.en11)).ToString();
                                        entradaProdutoNovo.en12 = (Convert.ToInt32(entradaProdutoNovo.en12) + Convert.ToInt32(item2.en12)).ToString();
                                        entradaProdutoNovo.en13 = (Convert.ToInt32(entradaProdutoNovo.en13) + Convert.ToInt32(item2.en13)).ToString();
                                        entradaProdutoNovo.en14 = (Convert.ToInt32(entradaProdutoNovo.en14) + Convert.ToInt32(item2.en14)).ToString();
                                        entradaProdutoNovo.en15 = (Convert.ToInt32(entradaProdutoNovo.en15) + Convert.ToInt32(item2.en15)).ToString();
                                        entradaProdutoNovo.en16 = (Convert.ToInt32(entradaProdutoNovo.en16) + Convert.ToInt32(item2.en16)).ToString();
                                        entradaProdutoNovo.en17 = (Convert.ToInt32(entradaProdutoNovo.en17) + Convert.ToInt32(item2.en17)).ToString();
                                        entradaProdutoNovo.en18 = (Convert.ToInt32(entradaProdutoNovo.en18) + Convert.ToInt32(item2.en18)).ToString();
                                        entradaProdutoNovo.en19 = (Convert.ToInt32(entradaProdutoNovo.en19) + Convert.ToInt32(item2.en19)).ToString();
                                        entradaProdutoNovo.en20 = (Convert.ToInt32(entradaProdutoNovo.en20) + Convert.ToInt32(item2.en20)).ToString();
                                        entradaProdutoNovo.en21 = (Convert.ToInt32(entradaProdutoNovo.en21) + Convert.ToInt32(item2.en21)).ToString();
                                        entradaProdutoNovo.en22 = (Convert.ToInt32(entradaProdutoNovo.en22) + Convert.ToInt32(item2.en22)).ToString();
                                        entradaProdutoNovo.en23 = (Convert.ToInt32(entradaProdutoNovo.en23) + Convert.ToInt32(item2.en23)).ToString();
                                        entradaProdutoNovo.en24 = (Convert.ToInt32(entradaProdutoNovo.en24) + Convert.ToInt32(item2.en24)).ToString();
                                        entradaProdutoNovo.en25 = (Convert.ToInt32(entradaProdutoNovo.en25) + Convert.ToInt32(item2.en25)).ToString();
                                        entradaProdutoNovo.en26 = (Convert.ToInt32(entradaProdutoNovo.en26) + Convert.ToInt32(item2.en26)).ToString();
                                        entradaProdutoNovo.en27 = (Convert.ToInt32(entradaProdutoNovo.en27) + Convert.ToInt32(item2.en27)).ToString();
                                        entradaProdutoNovo.en28 = (Convert.ToInt32(entradaProdutoNovo.en28) + Convert.ToInt32(item2.en28)).ToString();
                                        entradaProdutoNovo.en29 = (Convert.ToInt32(entradaProdutoNovo.en29) + Convert.ToInt32(item2.en29)).ToString();
                                        entradaProdutoNovo.en30 = (Convert.ToInt32(entradaProdutoNovo.en30) + Convert.ToInt32(item2.en30)).ToString();
                                        entradaProdutoNovo.en31 = (Convert.ToInt32(entradaProdutoNovo.en31) + Convert.ToInt32(item2.en31)).ToString();
                                        entradaProdutoNovo.en32 = (Convert.ToInt32(entradaProdutoNovo.en32) + Convert.ToInt32(item2.en32)).ToString();
                                        entradaProdutoNovo.en33 = (Convert.ToInt32(entradaProdutoNovo.en33) + Convert.ToInt32(item2.en33)).ToString();
                                        entradaProdutoNovo.en34 = (Convert.ToInt32(entradaProdutoNovo.en34) + Convert.ToInt32(item2.en34)).ToString();
                                        entradaProdutoNovo.en35 = (Convert.ToInt32(entradaProdutoNovo.en35) + Convert.ToInt32(item2.en35)).ToString();
                                        entradaProdutoNovo.en36 = (Convert.ToInt32(entradaProdutoNovo.en36) + Convert.ToInt32(item2.en36)).ToString();
                                        entradaProdutoNovo.en37 = (Convert.ToInt32(entradaProdutoNovo.en37) + Convert.ToInt32(item2.en37)).ToString();
                                        entradaProdutoNovo.en38 = (Convert.ToInt32(entradaProdutoNovo.en38) + Convert.ToInt32(item2.en38)).ToString();
                                        entradaProdutoNovo.en39 = (Convert.ToInt32(entradaProdutoNovo.en39) + Convert.ToInt32(item2.en39)).ToString();
                                        entradaProdutoNovo.en40 = (Convert.ToInt32(entradaProdutoNovo.en40) + Convert.ToInt32(item2.en40)).ToString();
                                        entradaProdutoNovo.en41 = (Convert.ToInt32(entradaProdutoNovo.en41) + Convert.ToInt32(item2.en41)).ToString();
                                        entradaProdutoNovo.en42 = (Convert.ToInt32(entradaProdutoNovo.en42) + Convert.ToInt32(item2.en42)).ToString();
                                        entradaProdutoNovo.en43 = (Convert.ToInt32(entradaProdutoNovo.en43) + Convert.ToInt32(item2.en43)).ToString();
                                        entradaProdutoNovo.en44 = (Convert.ToInt32(entradaProdutoNovo.en44) + Convert.ToInt32(item2.en44)).ToString();
                                        entradaProdutoNovo.en45 = (Convert.ToInt32(entradaProdutoNovo.en45) + Convert.ToInt32(item2.en45)).ToString();
                                        entradaProdutoNovo.en46 = (Convert.ToInt32(entradaProdutoNovo.en46) + Convert.ToInt32(item2.en46)).ToString();
                                        entradaProdutoNovo.en47 = (Convert.ToInt32(entradaProdutoNovo.en47) + Convert.ToInt32(item2.en47)).ToString();
                                        entradaProdutoNovo.en48 = (Convert.ToInt32(entradaProdutoNovo.en48) + Convert.ToInt32(item2.en48)).ToString();
                                        entradaProdutoNovo.valor = item2.valor;



                                    }

                                }

                                // ajuste de produto negativo em teste
                                if (Convert.ToInt32(entradaProdutoNovo.qtde_entrada) > 0)
                                {
                                    listaEntradaProdutoFinal.Add(entradaProdutoNovo);
                                }

                            }
                        }

                        // inserindo informações para a gravação da tabela loja_entradas_produto fim


                        // gravando no banco de dados inicio

                        try
                        {
                            foreach (EntradaProduto saveProdEntrada in listaEntradaProdutoFinal)
                            {
                                entradaProdutoDao.Salvar(saveProdEntrada);

                            }
                        }
                        catch (Exception erro)
                        {
                            erroFinal = true;
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_entrada_produtos!', 'error');  ", true);

                        }







                    }
                    catch (Exception erro)
                    {
                        erroFinal = true;
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_entrada!', 'error');  ", true);

                    }

                    cont++;

                    if (id_xml_nfe.Count() == cont)
                    {
                        if (vezes_bipado.Value == "3")
                        {
                            if (validaItensQtde.Value == "true")
                            {
                                auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "SEM DIVERGENCIA");
                      
                            }
                            else
                            {
                                auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "DIVERGENTE");
                          
                            }

                        }
                        else
                        {
                            if (vezes_bipado.Value == "1")
                            {
                                auxEntradaDao.SalvarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "SEM DIVERGENCIA");

                            }
                            else
                            {
                                auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "SEM DIVERGENCIA");

                            }
                    

                        }

                        if (erroFinal == false)
                        {
                            HttpCookie romaneio_pendente = Request.Cookies["romaneio_pendente"];
                            // Define a data de expiração do cookie como uma data no passado
                            romaneio_pendente.Expires = DateTime.Now.AddDays(-1);
                            // Atualize o cookie no navegador do cliente para removê-lo
                            Response.SetCookie(romaneio_pendente);

                            HttpCookie qtde_bipado = Request.Cookies["qtde_bipado"];
                            // Define a data de expiração do cookie como uma data no passado
                            qtde_bipado.Expires = DateTime.Now.AddDays(-1);
                            // Atualize o cookie no navegador do cliente para removê-lo
                            Response.SetCookie(qtde_bipado);


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
                            mensagem.Visible = false;
                        }
                  
                    }
                }


                DisplayInfo();
            }

        }

        protected void LinkButtonEntSucesso_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];

            ProdutosBarraXmlDao xmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            bool retorno = false;
            try
            {
                retorno = xmlDao.ExecutaSP_DGCS_PROCESSOS();

                if (retorno)
                {
                    Response.Redirect("~/Views/NotasPendentes.aspx", false);
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