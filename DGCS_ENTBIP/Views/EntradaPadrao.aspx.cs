using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Emails;
using DGCS_ENTBIP.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class EntradaPadrao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            cod_barra.Focus();
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
            ViewState["update"] = ViewState["update2"];
        }

        private void DisplayInfo()
        {
            // Do what ever thing you want to do
            ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

        protected void carregaBipagemZerada(string romaneio)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<AuxEntrada> list = new List<AuxEntrada>();

            list = auxEntradaDao.ConsultaBipagemZerada(romaneio, usuarioLogin.Value);

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
            NotaPendente notas = new NotaPendente();
            List<NotaPendente> list = null;
            HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
            if (NotaSelecionadas != null)
            {
                string listaObjetosJson = NotaSelecionadas.Value;
                list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                // Faça o processamento necessário com a lista de objetos
            }
            HttpCookie romaneio_pendente = Request.Cookies["romaneio_pendente"];
            HttpCookie qtde_bipado = Request.Cookies["qtde_bipado"];

            string nfs = "";
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

        public bool consultaTodosProdutoSemBiparEgrava()
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

            string[] id_xml_nfe = new string[list.Count];
            int i = 0;

            string romaneio = "";

            foreach (NotaPendente nota in list)
            {
                id_xml_nfe[i] = nota.id_xml_nfe;
                i++;
            }

            AuxEntrada auxEntradaRomaneio = new AuxEntrada();
            auxEntradaRomaneio = auxEntradaDao.ConsultaRomaneioEmAberto(romaneioEntrada.Text, usuarioLogin.Value);
            if (auxEntradaRomaneio.romaneio_produto == null)
            {
                romaneio = auxEntradaDao.BuscaProximoRomaneio();
                romaneio = "DG" + romaneio.Trim();
                auxEntradaDao.AtualizarRomaneio();
            }
            else
            {
                romaneio = auxEntradaRomaneio.romaneio_produto;
                romaneio = romaneio.Trim();
            }

            bool notaErro = false;
            listChave = produtoDao.BuscaProdutoTodosProdutosNotasSemBipar(id_xml_nfe);

            foreach (Produto produtoChave in listChave)
            {
                if (produtoChave.codigo_barra == "" || produtoChave.codigo_barra == null)
                {
                    notaErro = true;
                }
                auxEntrada = new AuxEntrada();

                auxEntrada.produto = produtoChave.produto.Trim();
                auxEntrada.cor_produto = produtoChave.cor_produto.Trim();
                auxEntrada.codigo_barra = produtoChave.codigo_barra.Trim();
                auxEntrada.tamanho = produtoChave.tamanho.Trim();
                auxEntrada.qtde_total = produtoChave.qtde_total.Trim();
                auxEntrada.qtde_bipado = "0";
                auxEntrada.fornecedor = produtoChave.fornecedor.Trim();
                auxEntrada.responsavel = usuarioLogin.Value;
                auxEntrada.desc_produto = produtoChave.desc_produto.Trim();
                //auxEntrada.romaneio_produto = "DG" + romaneio.Trim();
                auxEntrada.romaneio_produto = romaneio.Trim();
                auxEntrada.preco1 = produtoChave.preco1.Trim();
                auxEntrada.ids_nfs_selecionadas = produtoChave.ids_nfs_selecionadas.Trim();
                auxEntrada.qtde_vezes_bipado = "2";
                auxEntrada.chave_nfe = "";
                auxEntrada.grupo = produtoChave.grupo;
                auxEntrada.subgrupo = produtoChave.subgrupo;

                auxEntradaLista.Add(auxEntrada);
            }

            if (auxEntradaLista.Count > 0 && notaErro == false)
            {
                try
                {
                    foreach (AuxEntrada nota in auxEntradaLista)
                    {

                        auxEntradaDao.Salvar(nota);

                    }

                    cod_barra.Focus();
                    notaEmAbertoQtdes();
                    carregaBipagem(romaneioEntrada.Text);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na finalização', 'error');  ", true);
                    cod_barra.Focus();
                }

            }

            return notaErro;

        }


        public AuxEntrada consultaProduto(string codigo_barra)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie banco_loja = Request.Cookies["banco_loja"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            EntradaDao entradaDao = new EntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            Entrada entrada = new Entrada();
            AuxEntrada auxEntrada = new AuxEntrada();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            Produto produto = new Produto();
            ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

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
            if (auxEntradaRomaneio.romaneio_produto == null)
            {
                romaneio = auxEntradaDao.BuscaProximoRomaneio();
                auxEntrada.romaneio_produto = "DG" + romaneio.Trim();
                auxEntradaDao.AtualizarRomaneio();
            }
            else
            {
                romaneio = auxEntradaRomaneio.romaneio_produto;
                auxEntrada.romaneio_produto = romaneio.Trim();
            }


            foreach (NotaPendente nota in list)
            {
                id_xml_nfe[i] = nota.id_xml_nfe;
                i++;
                romaneio += nota.chave_nfe.Substring(42, 2);
            }


            produto = produtoDao.BuscaProdutoNotasSelecionadas(cod_barra.Text.Trim(), id_xml_nfe);

            if (produto.produto != null)
            {
                auxEntrada.qtde_bipado = "1";
                auxEntrada.produto = produto.produto.Trim();
                auxEntrada.cor_produto = produto.cor_produto.Trim();
                auxEntrada.codigo_barra = produto.codigo_barra.Trim();
                auxEntrada.tamanho = produto.tamanho.Trim();
                auxEntrada.qtde_total = produto.qtde_total.Trim();
                auxEntrada.fornecedor = produto.fornecedor.Trim();
                auxEntrada.responsavel = usuarioLogin.Value;
                auxEntrada.desc_produto = produto.desc_produto.Trim();

                auxEntrada.preco1 = produto.preco1.Trim();
                auxEntrada.ids_nfs_selecionadas = produto.ids_nfs_selecionadas.Trim();
                auxEntrada.qtde_vezes_bipado = "1";
                auxEntrada.chave_nfe = produto.chave_nfe;
                auxEntrada.grupo = produto.grupo;
                auxEntrada.subgrupo = produto.subgrupo;
            }
            else
            {
                produto = produtoDao.BuscaProdutoBancoLoja(codigo_barra.Trim(), banco_loja.Value, codigo_loja.Value);
                auxEntrada.qtde_total = "0";
            }

            if (produto.produto != null && auxEntrada.qtde_total == "0")
            {
                string ids = "";
                for (int ind = 0; ind < id_xml_nfe.Count(); ind++)
                {
                    if (ind + 1 == 1 && ind + 1 != id_xml_nfe.Count())
                    {
                        ids = "'" + id_xml_nfe[ind] + "',";
                    }
                    else if (ind + 1 == 1 && ind + 1 == id_xml_nfe.Count())
                    {
                        ids = "'" + id_xml_nfe[ind] + "'";
                    }
                    else if (ind + 1 == id_xml_nfe.Count())
                    {
                        ids += "'" + id_xml_nfe[ind] + "'";
                    }
                    else
                    {
                        ids += "'" + id_xml_nfe[ind] + "',";
                    }

                }

                auxEntrada.qtde_bipado = "1";
                auxEntrada.produto = produto.produto.Trim();
                auxEntrada.cor_produto = produto.cor_produto.Trim();
                auxEntrada.codigo_barra = produto.codigo_barra.Trim();
                auxEntrada.tamanho = produto.tamanho.Trim();
                auxEntrada.qtde_total = "0";
                auxEntrada.fornecedor = produto.fornecedor.Trim();
                auxEntrada.responsavel = usuarioLogin.Value;
                auxEntrada.desc_produto = produto.desc_produto.Trim();

                auxEntrada.preco1 = produto.preco1.Trim();
                auxEntrada.ids_nfs_selecionadas = ids;
                auxEntrada.qtde_vezes_bipado = "1";
                auxEntrada.chave_nfe = "";
                auxEntrada.grupo = produto.grupo;
                auxEntrada.subgrupo = produto.subgrupo;
            }


            return auxEntrada;
        }

        protected void cod_barra_TextChanged(object sender, EventArgs e)
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                AuxEntrada auxEntrada = new AuxEntrada();

                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                try
                {
                    //CONSULTA SE EXISTE NOTA EM ABERTO


                    string[] ent_status = new string[4];


                    ent_status = auxEntradaDao.ConsultaAuxStatus(usuarioLogin.Value);

                    if (ent_status[0] == "" || ent_status[0] == null)  // nota que tem a primeira tentativa de finalização
                    {
                        auxEntrada = auxEntradaDao.ConsultaNotaEmAberto(romaneioEntrada.Text, cod_barra.Text, usuarioLogin.Value);


                        if (auxEntrada.produto == null)
                        {

                            //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                            auxEntrada = consultaProduto(cod_barra.Text);

                            if (auxEntrada.produto != null && auxEntrada.qtde_total != "0")
                            {
                                romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                auxEntradaDao.Salvar(auxEntrada);
                                notaEmAbertoQtdes();
                                cod_barra.Focus();
                                carregaBipagem(romaneioEntrada.Text);
                                //mensagem

                                mensagem.Visible = true;
                                mensagem.Text = "Produto bipado confirmado!";
                                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                cod_barra.Text = "";
                            }
                            else
                            {

                                //confirmaEntForaNota.Text = "O EAN " + cod_barra.Text + " não foi encontrado na nota fiscal! Deseja Incluir mesmo assim?";
                                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                //sb.Append(@"<script type='text/javascript'>");
                                //sb.Append("$(function () {");
                                //sb.Append("$('#ModalEntProdForaNota').modal('show');");
                                //sb.Append(" });");
                                //sb.Append("</script>");
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                                entradaProdFora();

                            }
                        }
                        else
                        {


                            if ((Convert.ToInt16(auxEntrada.qtde_bipado) + 1 <= Convert.ToInt16(auxEntrada.qtde_total)) || (auxEntrada.produto == ""))
                            {
                                int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                auxEntrada.qtde_bipado = qtde.ToString();
                                romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                auxEntradaDao.Atualizar(auxEntrada);
                                notaEmAbertoQtdes();
                                cod_barra.Focus();
                                carregaBipagem(romaneioEntrada.Text);
                                //mensagem

                                mensagem.Visible = true;
                                mensagem.Text = "Produto bipado atualizado!";
                                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                cod_barra.Text = "";
                            }
                            else
                            {

                                if (Convert.ToInt16(auxEntrada.qtde_bipado) + 1 >= Convert.ToInt16(auxEntrada.qtde_total))
                                {
                                    //confirmaEntAmais.Text = "Atenção! Produto bipado vai exceder a quantidade da nota. Confirma incluir mesmo assim?";

                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append(@"<script type='text/javascript'>");
                                    //sb.Append("$(function () {");
                                    //sb.Append("$('#ModalEntProdAmais').modal('show');");
                                    //sb.Append(" });");
                                    //sb.Append("</script>");
                                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                                    int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                    auxEntrada.qtde_bipado = qtde.ToString();
                                    romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                    auxEntradaDao.Atualizar(auxEntrada);
                                    notaEmAbertoQtdes();
                                    cod_barra.Focus();
                                    carregaBipagem(romaneioEntrada.Text);
                                    //mensagem

                                    mensagem.Visible = true;
                                    mensagem.Text = "Produto bipado atualizado!";
                                    mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                    cod_barra.Text = "";

                                }

                            }

                        }

                    }
                    else // nota que tem mais de uma tentativa de finalização
                    {
                        auxEntrada = auxEntradaDao.ConsultaNotaEmAberto(ent_status[0], cod_barra.Text, usuarioLogin.Value);

                        if (auxEntrada.produto == null)
                        {

                            //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                            auxEntrada = consultaProduto(cod_barra.Text);

                            if (auxEntrada.produto != null)
                            {
                                romaneioEntrada.Text = auxEntrada.romaneio_produto;
                                auxEntrada.qtde_vezes_bipado = ent_status[1];
                                auxEntradaDao.Salvar(auxEntrada);
                                notaEmAbertoQtdes();
                                cod_barra.Focus();
                                carregaBipagem(romaneioEntrada.Text);
                                //mensagem

                                mensagem.Visible = true;
                                mensagem.Text = "Produto bipado confirmado!";
                                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                cod_barra.Text = "";
                            }
                            else
                            {
                                confirmaEntForaNota.Text = "O EAN " + cod_barra.Text + " não foi encontrado na nota fiscal! Deseja Incluir mesmo assim?";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#ModalEntProdForaNota').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                            }
                        }
                        else
                        {


                            if (Convert.ToInt16(auxEntrada.qtde_bipado) + 1 <= Convert.ToInt16(auxEntrada.qtde_total))
                            {
                                int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                auxEntrada.qtde_bipado = qtde.ToString();
                                romaneioEntrada.Text = auxEntrada.romaneio_produto;
                                auxEntrada.qtde_vezes_bipado = ent_status[1];
                                auxEntradaDao.Atualizar(auxEntrada);
                                notaEmAbertoQtdes();
                                cod_barra.Focus();
                                carregaBipagem(romaneioEntrada.Text);
                                //mensagem

                                mensagem.Visible = true;
                                mensagem.Text = "Produto bipado atualizado!";
                                mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                cod_barra.Text = "";
                            }
                            else
                            {

                                if (Convert.ToInt16(auxEntrada.qtde_bipado) + 1 >= Convert.ToInt16(auxEntrada.qtde_total))
                                {
                                    //confirmaEntAmais.Text = "Atenção! Produto bipado vai exceder a quantidade da nota. Confirma incluir mesmo assim?";

                                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    //sb.Append(@"<script type='text/javascript'>");
                                    //sb.Append("$(function () {");
                                    //sb.Append("$('#ModalEntProdAmais').modal('show');");
                                    //sb.Append(" });");
                                    //sb.Append("</script>");
                                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                                    int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                    auxEntrada.qtde_bipado = qtde.ToString();
                                    romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                    auxEntradaDao.Atualizar(auxEntrada);
                                    notaEmAbertoQtdes();
                                    cod_barra.Focus();
                                    carregaBipagem(romaneioEntrada.Text);
                                    //mensagem

                                    mensagem.Visible = true;
                                    mensagem.Text = "Produto bipado atualizado!";
                                    mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                    cod_barra.Text = "";
                                }

                            }

                        }
                    }



                }
                catch (Exception erroFinal)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na consulta do codigo de barras', 'error');  ", true);
                    cod_barra.Text = "";
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

                notaEmAbertoQtdes();
                cod_barra.Text = "";
                cod_barra.Focus();
            }

        }

        protected void LinkButtonFinalizar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            HttpCookie parametro_ativo_Lote = Request.Cookies["parametro_ativo_Lote"];


            HttpCookie div_item = Request.Cookies["div_item"];
            if (div_item == null)
            {
                HttpCookie div_item1 = new HttpCookie("div_item");
                div_item1.Value = "";
                Response.Cookies.Add(div_item1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                div_item.Value = "";
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(div_item);
            }

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

                string[] id_xml_nfe = new string[list.Count];
                int ind = 0;


                foreach (NotaPendente nota in list)
                {
                    id_xml_nfe[ind] = nota.id_xml_nfe;
                    ind++;
                }


                string ids = "";
                for (int i = 0; i < id_xml_nfe.Count(); i++)
                {
                    if (i + 1 == 1 && i + 1 != id_xml_nfe.Count())
                    {
                        ids = "'" + id_xml_nfe[i] + "',";
                    }
                    else if (i + 1 == 1 && i + 1 == id_xml_nfe.Count())
                    {
                        ids = "'" + id_xml_nfe[i] + "'";
                    }
                    else if (i + 1 == id_xml_nfe.Count())
                    {
                        ids += "'" + id_xml_nfe[i] + "'";
                    }
                    else
                    {
                        ids += "'" + id_xml_nfe[i] + "',";
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





                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                string[] ent_status = new string[5];


                ent_status = auxEntradaDao.ConsultaAuxStatus(usuarioLogin.Value);

                List<AuxEntrada> listaQtdeDivergente = null;


                listaQtdeDivergente = auxEntradaDao.ConsultaQtdeItemDivergenteNotaEmAberto(romaneioEntrada.Text, usuarioLogin.Value);

                if (Convert.ToInt16(qtdeTotalNotas.Text) == Convert.ToInt16(qtdeTotalBipado.Text) && listaQtdeDivergente.Count == 0)
                {
                    if (ent_status[0] == "" || ent_status[0] == null)
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

                        //GRAVA PRIMEIRA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem1(romaneioEntrada.Text, usuarioLogin.Value);
                    }
                    else
                    {

                        HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                        if (vezes_bipado == null)
                        {
                            HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                            vezes_bipado1.Value = ent_status[1];
                            Response.Cookies.Add(vezes_bipado1);
                        }
                        else
                        {
                            // O cookie foi encontrado, então você pode modificar o valor
                            vezes_bipado.Value = ent_status[1];
                            // Atualize o cookie no navegador do cliente
                            Response.SetCookie(vezes_bipado);
                        }
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

                    if (ent_status[1] == "2")
                    {
                        //GRAVA SEGUNDA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem2(romaneioEntrada.Text, usuarioLogin.Value);
                    }
                    else if (ent_status[1] == "3")
                    {
                        //GRAVA TERCEIRA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem3(romaneioEntrada.Text, usuarioLogin.Value);
                    }


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


                    if (validaItensQtde == null)
                    {
                        HttpCookie validaItensQtde1 = new HttpCookie("validaItensQtde");
                        validaItensQtde1.Value = "false";
                        Response.Cookies.Add(validaItensQtde1);
                    }
                    else
                    {
                        // O cookie foi encontrado, então você pode modificar o valor
                        validaItensQtde.Value = "false";
                        // Atualize o cookie no navegador do cliente
                        Response.SetCookie(validaItensQtde);
                    }

                    if (ent_status[0] == "" || ent_status[0] == null)
                    {
                        bool validaProds = false;
                        validaProds = consultaTodosProdutoSemBiparEgrava();
                        //GRAVA PRIMEIRA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem1(romaneioEntrada.Text, usuarioLogin.Value);

                        foreach (AuxEntrada aux in listaQtdeDivergente)
                        {
                            auxEntradaDao.ZerarQtdeDivergentesRomaneio(aux.romaneio_produto, aux.codigo_barra, usuarioLogin.Value);
                        }



                        if (validaProds == false)
                        {

                            auxEntradaDao.SalvarStatus(romaneioEntrada.Text, "2", "0", usuarioLogin.Value, ids, "DIVERGENTE");
                            notaEmAbertoQtdes();
                            cod_barra.Focus();
                            carregaBipagem(romaneioEntrada.Text);
                            mensagem.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Foi identificado divergência entre o xml e a quantidade bipada. Será zerado os itens com divergencia, por favor bipá-los novamente.', 'warning');  ", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Não vai ser possivel finalizar a nota, existe produtos com erro de cadastro. Verifique e tente novamente.', 'warning');  ", true);

                        }
                    }
                    else if (ent_status[1] == "6") // desativado para usar apenas duas bipagens 30/11/2025
                    {
                        //bool validaProds = false;
                        //validaProds = consultaTodosProdutoSemBiparEgrava(romaneioEntrada.Text);

                        //GRAVA SEGUNDA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem2(romaneioEntrada.Text, usuarioLogin.Value);

                        foreach (AuxEntrada aux in listaQtdeDivergente)
                        {
                            auxEntradaDao.ZerarQtdeDivergentesRomaneio(aux.romaneio_produto, aux.codigo_barra, usuarioLogin.Value);
                        }

                        HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                        if (vezes_bipado == null)
                        {
                            HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                            vezes_bipado1.Value = ent_status[1];
                            Response.Cookies.Add(vezes_bipado1);
                        }
                        else
                        {
                            // O cookie foi encontrado, então você pode modificar o valor
                            vezes_bipado.Value = ent_status[1];
                            // Atualize o cookie no navegador do cliente
                            Response.SetCookie(vezes_bipado);
                        }


                        auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, "3", "0", usuarioLogin.Value, ids, "DIVERGENTE");
                        notaEmAbertoQtdes();
                        cod_barra.Focus();
                        carregaBipagem(romaneioEntrada.Text);
                        mensagem.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Foi identificado divergência entre o xml e a quantidade bipada pela segunda vez. Será zerado os itens com divergencia, por favor bipá-los novamente.', 'warning');  ", true);

                    }
                    else if (ent_status[1] == "2") // mudado o status de 3 para 2 para fianlizar na segunda bipagem
                    {

                        //bool validaProds = false;
                        //validaProds = consultaTodosProdutoSemBiparEgrava(romaneioEntrada.Text);

                        //GRAVA TERCEIRA BIPAGEM DOS ITENS
                        auxEntradaDao.GuardaContagem3(romaneioEntrada.Text, usuarioLogin.Value);

                        foreach (AuxEntrada aux in listaQtdeDivergente)
                        {
                            if (Convert.ToInt32(aux.qtde_bipado) < Convert.ToInt32(aux.qtde_total) && Convert.ToInt32(aux.qtde_total) != 0)
                            {

                                // O cookie foi encontrado, então você pode modificar o valor
                                div_item.Value = "falta";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(div_item);



                            }
                        }


                        if (Convert.ToInt16(qtdeTotalNotas.Text) > Convert.ToInt16(qtdeTotalBipado.Text))
                        {
                            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                            if (vezes_bipado == null)
                            {
                                HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                                vezes_bipado1.Value = ent_status[1];
                                Response.Cookies.Add(vezes_bipado1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                vezes_bipado.Value = ent_status[1];
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(vezes_bipado);
                            }



                            if (status_divergencia == null)
                            {
                                HttpCookie status_divergencia1 = new HttpCookie("status_divergencia");
                                status_divergencia1.Value = "Falta no recebimento de mercadoria";
                                Response.Cookies.Add(status_divergencia1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                status_divergencia.Value = "Falta no recebimento de mercadoria";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(status_divergencia);
                            }



                            List<AuxEntradaLote> listaAuxEntradaLote = new List<AuxEntradaLote>();
                            AuxEntradaLoteDao auxEntradaLoteDao = new AuxEntradaLoteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                            listaAuxEntradaLote = auxEntradaLoteDao.ConsultaLoteAmais(romaneioEntrada.Text, usuarioLogin.Value, id_xml_nfe);




                            if (listaAuxEntradaLote.Count > 0)
                            {

                                if (parametro_ativo_Email.Value == "Ativo" && listaAuxEntradaLote.Count > 0)
                                {
                                    EnviaEmailDivergencia(romaneioEntrada.Text, "Sobra no recebimento de mercadoria");

                                }

                                if (parametro_ativo_Lote.Value == "Ativo")
                                {
                                    confirmaLoteSobra.Text = "Atenção! A nota será finalizada com divergencia entre xml e o físico.  Será gerado uma nota de saída de perda por abastecimento e necessário solicitar a nota complementar para o fornecedor. Por favor informe o nº lote dos itens abaixo:";

                                    GrSobraLote.DataSource = null;
                                    GrSobraLote.DataSource = listaAuxEntradaLote;
                                    GrSobraLote.DataBind();

                                    string listaObjetosJson = JsonConvert.SerializeObject(listaAuxEntradaLote);
                                    HttpCookie listaEntradaLote = new HttpCookie("listaEntradaLote");
                                    listaEntradaLote.Value = listaObjetosJson;
                                    Response.Cookies.Add(listaEntradaLote);


                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("$(function () {");
                                    sb.Append("$('#modallote').modal('show');");
                                    sb.Append(" });");
                                    sb.Append("</script>");
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                                }
                                else
                                {
                                    confirmaEntComDivergencia.Text = "Atenção! A nota será finalizada com divergencia entre xml e o físico.  Será gerado uma nota de saída e será necessário solicitar a nota complementar para o fornecedor.";

                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("$(function () {");
                                    sb.Append("$('#ModalEntComDivergencia').modal('show');");
                                    sb.Append(" });");
                                    sb.Append("</script>");
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                                }



                            }
                            else
                            {
                                confirmaEntComDivergencia.Text = "Atenção! A nota será finalizada com divergencia entre xml e o físico. Será gerado uma nota de saída de perda por abastecimento!";
                                if (parametro_ativo_Email.Value == "Ativo")
                                {
                                    EnviaEmailDivergencia(romaneioEntrada.Text, "Falta no recebimento de mercadoria");
                                }

                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#ModalEntComDivergencia').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                            }



                        }
                        else if (Convert.ToInt16(qtdeTotalNotas.Text) < Convert.ToInt16(qtdeTotalBipado.Text))
                        {
                            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                            if (vezes_bipado == null)
                            {
                                HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                                vezes_bipado1.Value = ent_status[1];
                                Response.Cookies.Add(vezes_bipado1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                vezes_bipado.Value = ent_status[1];
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(vezes_bipado);
                            }



                            if (status_divergencia == null)
                            {
                                HttpCookie status_divergencia1 = new HttpCookie("status_divergencia");
                                status_divergencia1.Value = "Sobra no recebimento de mercadoria";
                                Response.Cookies.Add(status_divergencia1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                status_divergencia.Value = "Sobra no recebimento de mercadoria";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(status_divergencia);
                            }



                            List<AuxEntradaLote> listaAuxEntradaLote = new List<AuxEntradaLote>();
                            AuxEntradaLoteDao auxEntradaLoteDao = new AuxEntradaLoteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                            listaAuxEntradaLote = auxEntradaLoteDao.ConsultaLoteAmais(romaneioEntrada.Text, usuarioLogin.Value, id_xml_nfe);


                            if (parametro_ativo_Email.Value == "Ativo" && listaAuxEntradaLote.Count > 0)
                            {
                                EnviaEmailDivergencia(romaneioEntrada.Text, "Sobra no recebimento de mercadoria");

                            }

                            if (parametro_ativo_Lote.Value == "Ativo")
                            {
                                confirmaLoteSobra.Text = "Atenção! A nota será finalizada com divergencia entre xml e o físico. Será gerado um ajuste";

                                GrSobraLote.DataSource = null;
                                GrSobraLote.DataSource = listaAuxEntradaLote;
                                GrSobraLote.DataBind();

                                string listaObjetosJson = JsonConvert.SerializeObject(listaAuxEntradaLote);
                                HttpCookie listaEntradaLote = new HttpCookie("listaEntradaLote");
                                listaEntradaLote.Value = listaObjetosJson;
                                Response.Cookies.Add(listaEntradaLote);


                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#modallote').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                            }
                            else
                            {
                                //confirmaEntComDivergencia.Text = "Atenção! A nota será finalizada com divergencia entre xml e o físico. Será gerada e processada uma nota complementar automaticamente. Em breve receberá uma notificação por e-mail referente a nota complementar.";
                                confirmaEntComDivergencia.Text = "Atenção! Entrada realizada com divergência, será gerado um inventário de sobra com os itens bipados a mais! Ao finalizar as entradas do dia, o inventário deve ser finalizado no Linx POS Manager! A falta será gerada uma nota fiscal de perda por abastecimento, e será finalizada automaticamente!";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#ModalEntComDivergencia').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                            }
                        }
                        else if (validaItensQtde.Value == "false")
                        {
                            if (status_divergencia == null)
                            {
                                HttpCookie status_divergencia1 = new HttpCookie("status_divergencia");
                                status_divergencia1.Value = "Divergencia no recebimento de mercadoria";
                                Response.Cookies.Add(status_divergencia1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                status_divergencia.Value = "Divergencia no recebimento de mercadoria";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(status_divergencia);
                            }


                            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                            if (vezes_bipado == null)
                            {
                                HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                                vezes_bipado1.Value = ent_status[1];
                                Response.Cookies.Add(vezes_bipado1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                vezes_bipado.Value = ent_status[1];
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(vezes_bipado);
                            }



                            List<AuxEntradaLote> listaAuxEntradaLote = new List<AuxEntradaLote>();
                            AuxEntradaLoteDao auxEntradaLoteDao = new AuxEntradaLoteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                            listaAuxEntradaLote = auxEntradaLoteDao.ConsultaLoteAmais(romaneioEntrada.Text, usuarioLogin.Value, id_xml_nfe);


                            if (parametro_ativo_Email.Value == "Ativo" && listaAuxEntradaLote.Count > 0)
                            {
                                EnviaEmailDivergencia(romaneioEntrada.Text, "Divergencia no recebimento de mercadoria");
                            }

                            if (parametro_ativo_Lote.Value == "Ativo")
                            {
                                confirmaLoteSobra.Text = "Atenção! Nota vai ser finalizada com a quantidade batendo, porém possui divergencia entre a quantidade a receber por item. Necessario solicitar uma nota complementar e uma nota de saida. Por favor informar o numero do lote dos itens abaixo:";

                                GrSobraLote.DataSource = null;
                                GrSobraLote.DataSource = listaAuxEntradaLote;
                                GrSobraLote.DataBind();

                                string listaObjetosJson = JsonConvert.SerializeObject(listaAuxEntradaLote);
                                HttpCookie listaEntradaLote = new HttpCookie("listaEntradaLote");
                                listaEntradaLote.Value = listaObjetosJson;
                                Response.Cookies.Add(listaEntradaLote);


                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#modallote').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                            }
                            else
                            {
                                confirmaEntComDivergencia.Text = "Atenção! Nota vai ser finalizada com a quantidade batendo, porém possui divergencia entre a quantidade a receber por item. Necessario solicitar uma nota complementar e uma nota de saida. ";

                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$(function () {");
                                sb.Append("$('#ModalEntComDivergencia').modal('show');");
                                sb.Append(" });");
                                sb.Append("</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                            }
                        }
                        else
                        {
                            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];
                            if (vezes_bipado == null)
                            {
                                HttpCookie vezes_bipado1 = new HttpCookie("vezes_bipado");
                                vezes_bipado1.Value = ent_status[1];
                                Response.Cookies.Add(vezes_bipado1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                vezes_bipado.Value = ent_status[1];
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(vezes_bipado);
                            }
                        }

                    }
                    else
                    {
                        mensagem.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na finalização, tente novamente.', 'error');  ", true);
                    }

                }

            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar validar finalização, tente novamente.', 'error');  ", true);

            }
        }



        public void GravarNotaRetaguarda(string nota_fiscal, string romaneio_bipagem, string cnpj)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];

            List<AuxEntrada> listAuxEntrada = new List<AuxEntrada>();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            if (status_divergencia != null && (status_divergencia.Value == "Divergencia no recebimento de mercadoria" || status_divergencia.Value == "Sobra no recebimento de mercadoria"))
            {
                try
                {
                    listAuxEntrada = auxEntradaDao.ConsultaBipagem(romaneio_bipagem, usuarioLogin.Value);
                }
                catch (Exception erro)
                {
                    mensagem.Visible = true;
                    mensagem.Text = "erro na consulta Aux_Entrada";
                    mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                }


                try
                {

                    foreach (AuxEntrada aux_entrada in listAuxEntrada)
                    {
                        aux_entrada.cnpj = cnpj;
                        aux_entrada.nota_fiscal = nota_fiscal;
                        auxEntradaDao.SalvarRetaguarda(aux_entrada);

                    }

                }
                catch (Exception erro)
                {
                    mensagem.Visible = true;
                    mensagem.Text = "erro ao tentar gravar Aux_Entrada na retaguarda";
                    mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                }



                // Define a data de expiração do cookie como uma data no passado
                status_divergencia.Expires = DateTime.Now.AddDays(-1);

                // Atualize o cookie no navegador do cliente para removê-lo
                Response.SetCookie(status_divergencia);
            }
            else
            {

            }

        }



        public void GerarTXTnotaRetaguarda(string nota_fiscal, string romaneio_bipagem, string cnpj, string chavenfe)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            ParametrosFtp parametroFTP = new ParametrosFtp();
            ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            List<AuxEntrada> listAuxEntrada = new List<AuxEntrada>();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            if (status_divergencia != null && (status_divergencia.Value == "Falta no recebimento de mercadoria" || status_divergencia.Value == "Divergencia no recebimento de mercadoria" || status_divergencia.Value == "Sobra no recebimento de mercadoria"))
            {
                try
                {
                    listAuxEntrada = auxEntradaDao.ConsultaBipagem(romaneio_bipagem, usuarioLogin.Value);
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

                    parametroFTP = fTPDao.BuscaFTPcadastrado();

                    foreach (AuxEntrada aux_entrada in listAuxEntrada)
                    {

                        aux_entrada.chave_nfe = chavenfe;
                        aux_entrada.cnpj = cnpj;
                        aux_entrada.nota_fiscal = nota_fiscal;
                        scriptTXT.Add(auxEntradaDao.SalvarRetaguardatxt(aux_entrada, parametroFTP));

                    }


                    string nomeArquivo = "DivEntradaNf" + chavenfe + ".txt";
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
                            try
                            {
                                ftpClient.UploadFile(localFilePath, remoteFilePath);
                            }
                            catch (Exception erro)
                            {
                                Console.WriteLine("erro ao gravar no diretorio " + erro.Message);
                            }

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




                // Define a data de expiração do cookie como uma data no passado
                status_divergencia.Expires = DateTime.Now.AddDays(-1);

                // Atualize o cookie no navegador do cliente para removê-lo
                Response.SetCookie(status_divergencia);
            }
            else
            {

            }

        }

        public string Extract(string input, int len)
        {
            if (string.IsNullOrEmpty(input) || input.Length < len)
            {
                return input;
            };

            return input.Substring(input.Length - 6);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "qtde_total").ToString().Equals("0"))
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

        protected void LinkButtonConfirmaRazer_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];


            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

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
                mensagem.Visible = false;
                carregaBipagem(romaneio_sessao.Value);
                romaneioEntrada.Text = romaneio_sessao.Value;
                notaEmAbertoQtdes();
                cod_barra.Text = "";
                cod_barra.Focus();
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
            cod_barra.Text = "";
            mensagem.Visible = false;
        }


        protected string ValidaRomaneioLivreEntrada()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            EntradaDao entradaDao = new EntradaDao(servidorCookie.Value,
      banco.Value, usuario_banco.Value, senha_banco.Value);
            Entrada entrada =null;

           

            bool romaneioLivre = false;
            while (romaneioLivre == false)
            {
                string romaneio_entrada = entradaDao.consultaSequenciaNotaEntrada();
                romaneio_entrada = "DG" + Extract((romaneio_entrada + DateTime.Now.Month + DateTime.Now.Day), 8);
                //verifica se romaneio já existe nas entradas
                entrada = entradaDao.BuscaRomaneioExistenteEntrada(romaneio_entrada);

                if (entrada.romaneio_produto == null)
                {
                    romaneioLivre = true;
                    entrada.romaneio_produto = romaneio_entrada;
                }
                else
                {
                    entradaDao.AtualizaSequencial();
                    romaneioLivre = false;

                }
            }

            return entrada.romaneio_produto;
        }

        protected void EntradaNotaFiscal()
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

            Parametro parametro = new Parametro();
            ParametroDao parametroDao = new ParametroDao();
            parametro = parametroDao.BuscaParametro("LOJA_TIPOS_ENTRADA_SAIDA");
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
                entrada.valor_total = valorTotal.ToString().Replace(",", ".");
                entrada.qtde_total = qtdeTotal.ToString();
                entrada.entrada_sem_produtos = "0";
                entrada.chave_nfe = listaProdutosBarras.ElementAt(0).chave_nfe;
                entrada.serie_nf_entrada = listaProdutosBarras.ElementAt(0).serie_nf_entrada;
                entrada.obs = "Entrada realizada por sistema de bipagem - DG Consultoria e Sistemas";
                entrada.data_saida = Convert.ToDateTime(listaProdutosBarras.ElementAt(0).data_saida).ToString("yyyy-MM-dd");
                entrada.responsavel = usuarioLogin.Value;
                entrada.numero_nf_transferencia = listaProdutosBarras.ElementAt(0).nf_entrada;
                entrada.filial = listaProdutosBarras.ElementAt(0).destinatario;
                entrada.filial_origem = listaProdutosBarras.ElementAt(0).destinatario;
                entrada.fornecedor = listaProdutosBarras.ElementAt(0).nome_clifor;
                entrada.cnpj = listaProdutosBarras.ElementAt(0).cnpj;
                entrada.romaneio_produto = ValidaRomaneioLivreEntrada();




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
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_entrada_produtos!', 'error');  ", true);

                    }







                }
                catch (Exception erro)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_entrada!', 'error');  ", true);

                }

                cont++;
                bool geraSaida = false;

                if (id_xml_nfe.Count() == cont)
                {
                    if (vezes_bipado.Value == "2") // alterado de 3 vezes para 2 30/11/2025
                    {
                        if (validaItensQtde.Value == "true")
                        {
                            auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "SEM DIVERGENCIA");

                        }
                        else
                        {
                            auxEntradaDao.AtualizarStatus(romaneioEntrada.Text, vezes_bipado.Value, "1", usuarioLogin.Value, ids.Value, "DIVERGENTE");

                            if (status_divergencia == null)
                            {
                                HttpCookie status_divergencia1 = new HttpCookie("status_divergencia");
                                status_divergencia1.Value = "Divergencia no recebimento de mercadoria";
                                Response.Cookies.Add(status_divergencia1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                status_divergencia.Value = "Divergencia no recebimento de mercadoria";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(status_divergencia);
                            }

                        }

                        if (Convert.ToInt16(qtdeTotalBipado.Text) < Convert.ToInt16(qtdeTotalNotas.Text) || div_item.Value == "falta")
                        {
                            geraSaida = true;

                            if (status_divergencia == null)
                            {
                                HttpCookie status_divergencia1 = new HttpCookie("status_divergencia");
                                status_divergencia1.Value = "Sobra no recebimento de mercadoria";
                                Response.Cookies.Add(status_divergencia1);
                            }
                            else
                            {
                                // O cookie foi encontrado, então você pode modificar o valor
                                status_divergencia.Value = "Sobra no recebimento de mercadoria";
                                // Atualize o cookie no navegador do cliente
                                Response.SetCookie(status_divergencia);
                            }

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

                    // assume ultima bipagem valida na coluna qtde_bipada
                    auxEntradaDao.AtualizaQtdeBipadoUltValida(romaneioEntrada.Text, usuarioLogin.Value);

                    // GRAVA NOTA NA RETAGUARDA
                    //GravarNotaRetaguarda(listaProdutosBarras.ElementAt(0).nf_entrada, romaneioEntrada.Text, listaProdutosBarras.ElementAt(0).cnpj);

                    // GERA ARQUIVO TXT
                    GerarTXTnotaRetaguarda(listaProdutosBarras.ElementAt(0).nf_entrada, romaneioEntrada.Text, listaProdutosBarras.ElementAt(0).cnpj, listaProdutosBarras.ElementAt(0).chave_nfe);


                    if (geraSaida == false)
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

                        // Define a data de expiração do cookie como uma data no passado
                        vezes_bipado.Expires = DateTime.Now.AddDays(-1);
                        // Atualize o cookie no navegador do cliente para removê-lo
                        Response.SetCookie(vezes_bipado);


                        confirmaEntSucesso.Text = "Parabéns entrada feita com sucesso!";

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$(function () {");
                        sb.Append("$('#ModalEntSucesso').modal('show');");
                        sb.Append(" });");
                        sb.Append("</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                    }
                    else if (geraSaida == true)
                    {
                        try
                        {


                            GerarNotaSaida(romaneioEntrada.Text);


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

                            // Define a data de expiração do cookie como uma data no passado
                            vezes_bipado.Expires = DateTime.Now.AddDays(-1);
                            // Atualize o cookie no navegador do cliente para removê-lo
                            Response.SetCookie(vezes_bipado);

                            confirmaEntSucesso.Text = "Parabéns entrada feita com sucesso! E já gerado o romaneio de saida para os produtos que faltaram no recebimento.";

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$(function () {");
                            sb.Append("$('#ModalEntSucesso').modal('show');");
                            sb.Append(" });");
                            sb.Append("</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                        }
                        catch (Exception erro)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar gerar nota de saida!', 'error');  ", true);

                        }

                    }

                }
            }



        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {

            EntradaNotaFiscal();
        }

        string definirTamanhoNumeroString(string texto, int tamanhoDesejado)
        {
            if (texto.Length < tamanhoDesejado)
            {
                return texto.PadRight(tamanhoDesejado, '0');
            }
            else if (texto.Length > tamanhoDesejado)
            {
                return texto.Substring(Math.Max(0, texto.Length - tamanhoDesejado));
            }
            else
            {
                return texto;
            }
        }


        protected void GerarNotaSaida(string romaneio)
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

            AuxEntrada auxEntrada = new AuxEntrada();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            ProdutosBarraXml produtosBarraXml = new ProdutosBarraXml();
            ProdutosBarraXmlDao produtosBarraXmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            Loja_SaidaDao loja_SaidaDao = new Loja_SaidaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            Loja_Saida_ProdutoDao loja_Saida_ProdutoDao = new Loja_Saida_ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            List<AuxEntrada> list = null;
            int i = 0;


            HttpCookie nf_saida = Request.Cookies["nf_saida"];
            if (nf_saida == null)
            {
                HttpCookie nf_saida1 = new HttpCookie("romaneio_sessao");
                nf_saida1.Value = loja_SaidaDao.consultaSequenciaNotaSaida();
                Response.Cookies.Add(nf_saida1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                nf_saida.Value = loja_SaidaDao.consultaSequenciaNotaSaida();
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(nf_saida);
            }


            list = auxEntradaDao.RetornaItensRomaneioSaida(romaneio, usuarioLogin.Value);


            int cont = 0;
            int teste = list.Count();

            Loja_Saidas saida = null;
            List<Loja_Saidas_Produto> listaSaidaProduto = null;
            // inserindo informações para a gravação da tabela loja_Saida inicio
            int qtdeTotal = 0;
            double valorTotal = 0;

            foreach (AuxEntrada notaId in list)
            {

                qtdeTotal += Convert.ToInt16(notaId.qtde_total);
                valorTotal += Convert.ToInt16(notaId.qtde_total) * Convert.ToDouble(Convert.ToDecimal(notaId.preco1.Replace(".", ",")), System.Globalization.CultureInfo.GetCultureInfo("en-US"));


            }
            string newRomaneio = definirTamanhoNumeroString(loja_SaidaDao.consultaSequenciaNotaSaida(), 6);
            romaneio = "DG" + newRomaneio;

            saida = new Loja_Saidas();
            saida.codigo_tab_preco = "00";
            saida.tipo_entrada_saida = "10";
            saida.filial_destino = list.ElementAt(0).destinatario;
            saida.serie_nf = "1";
            // saida.numero_nf_transferencia = Session["nf_saida"].ToString();
            saida.responsavel = usuarioLogin.Value;
            saida.indica_devolucao = "0";
            saida.fornecedor_devolucao = "";
            saida.qtde_total = qtdeTotal.ToString();
            saida.valor_total = valorTotal.ToString();
            saida.fator_preco = "0.00";
            saida.saida_encerrada = "0";
            saida.saida_cancelada = "0";
            saida.lx_status_saida = "1";
            saida.romaneio_produto = romaneio;
            saida.filial = list.ElementAt(0).destinatario;

            try
            {
                loja_SaidaDao.SalvarNfCh(saida, list.ElementAt(0).chave_nfe, list.ElementAt(0).nota_fiscal);

                listaSaidaProduto = new List<Loja_Saidas_Produto>();





                // inserindo informações para a gravação da tabela loja_Saida fim

                // inserindo informações para a gravação da tabela loja_entradas_produto inicio
                foreach (AuxEntrada notaId in list)
                {

                    Loja_Saidas_Produto saidaProduto = new Loja_Saidas_Produto();
                    string[] vetorAtributos = null;
                    vetorAtributos = saidaProduto.retornaVetor();
                    for (int ind = 0; ind < vetorAtributos.Length; ind++)
                    {
                        string vlr = "en" + notaId.tamanho;
                        if (vlr.Trim() == vetorAtributos[ind])
                        {
                            vetorAtributos[ind] = notaId.qtde_total;
                        }
                        else
                        {
                            vetorAtributos[ind] = "0";
                        }
                    }

                    saidaProduto = saidaProduto.retornaObjeto(vetorAtributos);
                    saidaProduto.filial = notaId.destinatario;
                    saidaProduto.romaneio_produto = romaneio;
                    saidaProduto.produto = notaId.produto;
                    saidaProduto.cor_produto = notaId.cor_produto;
                    saidaProduto.qtde_saida = notaId.qtde_total;
                    saidaProduto.preco1 = notaId.preco1;
                    saidaProduto.preco2 = "0.00";
                    saidaProduto.preco3 = "0.00";
                    saidaProduto.preco4 = "0.00";
                    decimal valor = Convert.ToInt16(saidaProduto.qtde_saida) * Convert.ToDecimal(saidaProduto.preco1.Replace('.', ','));
                    saidaProduto.valor = (valor).ToString().Replace(',', '.');
                    listaSaidaProduto.Add(saidaProduto);


                }


                Loja_Saidas_Produto saidaProdutoNovo = null;

                List<Loja_Saidas_Produto> listaSaidaProdutoFinal = new List<Loja_Saidas_Produto>();

                foreach (AuxEntrada item1 in list)
                {
                    saidaProdutoNovo = new Loja_Saidas_Produto("0");

                    foreach (Loja_Saidas_Produto item2 in listaSaidaProduto)
                    {


                        if (item1.produto.Trim() == item2.produto.Trim() && item1.cor_produto.Trim() == item2.cor_produto.Trim())
                        {


                            saidaProdutoNovo.romaneio_produto = item2.romaneio_produto;
                            saidaProdutoNovo.filial = item2.filial;
                            saidaProdutoNovo.produto = item2.produto;
                            saidaProdutoNovo.cor_produto = item2.cor_produto;
                            saidaProdutoNovo.preco1 = item2.preco1.ToString().Replace(',', '.');
                            saidaProdutoNovo.preco2 = item2.preco2.ToString().Replace(',', '.');
                            saidaProdutoNovo.preco3 = item2.preco3.ToString().Replace(',', '.');
                            saidaProdutoNovo.preco4 = item2.preco4.ToString().Replace(',', '.');
                            saidaProdutoNovo.qtde_saida = (Convert.ToInt32(saidaProdutoNovo.qtde_saida) + Convert.ToInt32(item2.qtde_saida)).ToString();
                            decimal valor = Convert.ToInt16(saidaProdutoNovo.qtde_saida) * Convert.ToDecimal(saidaProdutoNovo.preco1.Replace('.', ','));
                            saidaProdutoNovo.valor = (valor).ToString().Replace(',', '.');
                            saidaProdutoNovo.en1 = (Convert.ToInt32(saidaProdutoNovo.en1) + Convert.ToInt32(item2.en1)).ToString();
                            saidaProdutoNovo.en2 = (Convert.ToInt32(saidaProdutoNovo.en2) + Convert.ToInt32(item2.en2)).ToString();
                            saidaProdutoNovo.en3 = (Convert.ToInt32(saidaProdutoNovo.en3) + Convert.ToInt32(item2.en3)).ToString();
                            saidaProdutoNovo.en4 = (Convert.ToInt32(saidaProdutoNovo.en4) + Convert.ToInt32(item2.en4)).ToString();
                            saidaProdutoNovo.en5 = (Convert.ToInt32(saidaProdutoNovo.en5) + Convert.ToInt32(item2.en5)).ToString();
                            saidaProdutoNovo.en6 = (Convert.ToInt32(saidaProdutoNovo.en6) + Convert.ToInt32(item2.en6)).ToString();
                            saidaProdutoNovo.en7 = (Convert.ToInt32(saidaProdutoNovo.en7) + Convert.ToInt32(item2.en7)).ToString();
                            saidaProdutoNovo.en8 = (Convert.ToInt32(saidaProdutoNovo.en8) + Convert.ToInt32(item2.en8)).ToString();
                            saidaProdutoNovo.en9 = (Convert.ToInt32(saidaProdutoNovo.en9) + Convert.ToInt32(item2.en9)).ToString();
                            saidaProdutoNovo.en10 = (Convert.ToInt32(saidaProdutoNovo.en10) + Convert.ToInt32(item2.en10)).ToString();
                            saidaProdutoNovo.en11 = (Convert.ToInt32(saidaProdutoNovo.en11) + Convert.ToInt32(item2.en11)).ToString();
                            saidaProdutoNovo.en12 = (Convert.ToInt32(saidaProdutoNovo.en12) + Convert.ToInt32(item2.en12)).ToString();
                            saidaProdutoNovo.en13 = (Convert.ToInt32(saidaProdutoNovo.en13) + Convert.ToInt32(item2.en13)).ToString();
                            saidaProdutoNovo.en14 = (Convert.ToInt32(saidaProdutoNovo.en14) + Convert.ToInt32(item2.en14)).ToString();
                            saidaProdutoNovo.en15 = (Convert.ToInt32(saidaProdutoNovo.en15) + Convert.ToInt32(item2.en15)).ToString();
                            saidaProdutoNovo.en16 = (Convert.ToInt32(saidaProdutoNovo.en16) + Convert.ToInt32(item2.en16)).ToString();
                            saidaProdutoNovo.en17 = (Convert.ToInt32(saidaProdutoNovo.en17) + Convert.ToInt32(item2.en17)).ToString();
                            saidaProdutoNovo.en18 = (Convert.ToInt32(saidaProdutoNovo.en18) + Convert.ToInt32(item2.en18)).ToString();
                            saidaProdutoNovo.en19 = (Convert.ToInt32(saidaProdutoNovo.en19) + Convert.ToInt32(item2.en19)).ToString();
                            saidaProdutoNovo.en20 = (Convert.ToInt32(saidaProdutoNovo.en20) + Convert.ToInt32(item2.en20)).ToString();
                            saidaProdutoNovo.en21 = (Convert.ToInt32(saidaProdutoNovo.en21) + Convert.ToInt32(item2.en21)).ToString();
                            saidaProdutoNovo.en22 = (Convert.ToInt32(saidaProdutoNovo.en22) + Convert.ToInt32(item2.en22)).ToString();
                            saidaProdutoNovo.en23 = (Convert.ToInt32(saidaProdutoNovo.en23) + Convert.ToInt32(item2.en23)).ToString();
                            saidaProdutoNovo.en24 = (Convert.ToInt32(saidaProdutoNovo.en24) + Convert.ToInt32(item2.en24)).ToString();
                            saidaProdutoNovo.en25 = (Convert.ToInt32(saidaProdutoNovo.en25) + Convert.ToInt32(item2.en25)).ToString();
                            saidaProdutoNovo.en26 = (Convert.ToInt32(saidaProdutoNovo.en26) + Convert.ToInt32(item2.en26)).ToString();
                            saidaProdutoNovo.en27 = (Convert.ToInt32(saidaProdutoNovo.en27) + Convert.ToInt32(item2.en27)).ToString();
                            saidaProdutoNovo.en28 = (Convert.ToInt32(saidaProdutoNovo.en28) + Convert.ToInt32(item2.en28)).ToString();
                            saidaProdutoNovo.en29 = (Convert.ToInt32(saidaProdutoNovo.en29) + Convert.ToInt32(item2.en29)).ToString();
                            saidaProdutoNovo.en30 = (Convert.ToInt32(saidaProdutoNovo.en30) + Convert.ToInt32(item2.en30)).ToString();
                            saidaProdutoNovo.en31 = (Convert.ToInt32(saidaProdutoNovo.en31) + Convert.ToInt32(item2.en31)).ToString();
                            saidaProdutoNovo.en32 = (Convert.ToInt32(saidaProdutoNovo.en32) + Convert.ToInt32(item2.en32)).ToString();
                            saidaProdutoNovo.en33 = (Convert.ToInt32(saidaProdutoNovo.en33) + Convert.ToInt32(item2.en33)).ToString();
                            saidaProdutoNovo.en34 = (Convert.ToInt32(saidaProdutoNovo.en34) + Convert.ToInt32(item2.en34)).ToString();
                            saidaProdutoNovo.en35 = (Convert.ToInt32(saidaProdutoNovo.en35) + Convert.ToInt32(item2.en35)).ToString();
                            saidaProdutoNovo.en36 = (Convert.ToInt32(saidaProdutoNovo.en36) + Convert.ToInt32(item2.en36)).ToString();
                            saidaProdutoNovo.en37 = (Convert.ToInt32(saidaProdutoNovo.en37) + Convert.ToInt32(item2.en37)).ToString();
                            saidaProdutoNovo.en38 = (Convert.ToInt32(saidaProdutoNovo.en38) + Convert.ToInt32(item2.en38)).ToString();
                            saidaProdutoNovo.en39 = (Convert.ToInt32(saidaProdutoNovo.en39) + Convert.ToInt32(item2.en39)).ToString();
                            saidaProdutoNovo.en40 = (Convert.ToInt32(saidaProdutoNovo.en40) + Convert.ToInt32(item2.en40)).ToString();
                            saidaProdutoNovo.en41 = (Convert.ToInt32(saidaProdutoNovo.en41) + Convert.ToInt32(item2.en41)).ToString();
                            saidaProdutoNovo.en42 = (Convert.ToInt32(saidaProdutoNovo.en42) + Convert.ToInt32(item2.en42)).ToString();
                            saidaProdutoNovo.en43 = (Convert.ToInt32(saidaProdutoNovo.en43) + Convert.ToInt32(item2.en43)).ToString();
                            saidaProdutoNovo.en44 = (Convert.ToInt32(saidaProdutoNovo.en44) + Convert.ToInt32(item2.en44)).ToString();
                            saidaProdutoNovo.en45 = (Convert.ToInt32(saidaProdutoNovo.en45) + Convert.ToInt32(item2.en45)).ToString();
                            saidaProdutoNovo.en46 = (Convert.ToInt32(saidaProdutoNovo.en46) + Convert.ToInt32(item2.en46)).ToString();
                            saidaProdutoNovo.en47 = (Convert.ToInt32(saidaProdutoNovo.en47) + Convert.ToInt32(item2.en47)).ToString();
                            saidaProdutoNovo.en48 = (Convert.ToInt32(saidaProdutoNovo.en48) + Convert.ToInt32(item2.en48)).ToString();
                            saidaProdutoNovo.valor = item2.valor;



                        }

                    }

                    // ajuste de produto negativo em teste
                    if (Convert.ToInt32(saidaProdutoNovo.qtde_saida) > 0)
                    {
                        listaSaidaProdutoFinal.Add(saidaProdutoNovo);
                    }


                }

                // inserindo informações para a gravação da tabela loja_entradas_produto fim


                // gravando no banco de dados inicio

                try
                {
                    foreach (Loja_Saidas_Produto saveProdSaida in listaSaidaProdutoFinal)
                    {
                        loja_Saida_ProdutoDao.Salvar(saveProdSaida);

                    }
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_saida_produtos!', 'error');  ", true);

                }







            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar inserir loja_saida!', 'error');  ", true);

            }

            cont++;







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

        protected void LinkButtonEntAmais_Click(object sender, EventArgs e)
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

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                AuxEntrada auxEntrada = new AuxEntrada();
                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                try
                {
                    //CONSULTA SE EXISTE NOTA EM ABERTO

                    auxEntrada = auxEntradaDao.ConsultaNotaEmAberto(romaneioEntrada.Text, cod_barra.Text, usuarioLogin.Value);

                    if (auxEntrada.produto == null)
                    {

                        //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                        auxEntrada = consultaProduto(cod_barra.Text);

                        if (auxEntrada.produto != null)
                        {
                            romaneioEntrada.Text = auxEntrada.romaneio_produto;

                            auxEntradaDao.Salvar(auxEntrada);
                            notaEmAbertoQtdes();
                            cod_barra.Focus();
                            carregaBipagem(romaneioEntrada.Text);
                            //mensagem

                            mensagem.Visible = true;
                            mensagem.Text = "Produto bipado confirmado!";
                            mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                        }
                        else
                        {
                            mensagem.Visible = true;
                            mensagem.Text = "O EAN bipado não foi encontrado na nota fiscal! ";
                            mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                        }
                    }
                    else
                    {



                        int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                        auxEntrada.qtde_bipado = qtde.ToString();
                        romaneioEntrada.Text = auxEntrada.romaneio_produto;

                        auxEntradaDao.Atualizar(auxEntrada);
                        notaEmAbertoQtdes();
                        cod_barra.Focus();
                        carregaBipagem(romaneioEntrada.Text);
                        //mensagem

                        mensagem.Visible = true;
                        mensagem.Text = "Produto bipado atualizado!";
                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";


                    }





                }
                catch (Exception erroFinal)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na consulta do codigo de barras', 'error');  ", true);

                }


                cod_barra.Text = "";


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
                mensagem.Visible = false;
                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                carregaBipagem(romaneio_sessao.Value);
                romaneioEntrada.Text = romaneio_sessao.Value;
                notaEmAbertoQtdes();
                cod_barra.Text = "";
                cod_barra.Focus();
            }
        }

        protected void LinkButtonNegaEnt_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Atualiza tela 
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalEntProdAmais').modal('hide');$('.modal-backdrop').remove();");
            sb.Append(" });");
            sb.Append("</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Janela", sb.ToString(), false);
            cod_barra.Text = "";
            mensagem.Visible = false;
        }


        protected void EnviaEmailDivergencia(string romaneio, string divergencia)
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

            AuxEntrada auxEntrada = new AuxEntrada();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<AuxEntrada> list = new List<AuxEntrada>();
            string romaneio_div = romaneio;
            string nfs = "";
            Email_Divergencia emails = new Email_Divergencia();
            Email_DivergenciaDao emailDao = new Email_DivergenciaDao();
            emails = emailDao.BuscaEmailEnvio();

            list = auxEntradaDao.RetornaItensDIvergentesEmail(romaneio);

            foreach (AuxEntrada email in list)
            {
                if (nfs == "")
                {
                    nfs = email.ids_nfs_selecionadas;
                }
                else if (nfs != "")
                {
                    if (!nfs.Contains(email.ids_nfs_selecionadas))
                    {
                        nfs += "," + email.ids_nfs_selecionadas;
                    }
                }
            }
            Mensagem mensagemEmail = new Mensagem();

            Email enviaEmail = new Email();
            Email_DivergenciaDao email_DivergenciaDao = new Email_DivergenciaDao();
            List<Email_Divergencia> email_Divergencia = new List<Email_Divergencia>();
            email_Divergencia = email_DivergenciaDao.BuscaEmailGrupo("Prevenção de Perdas");
            try
            {


                StringBuilder texto = mensagemEmail.mensagemEntradaDivergenciaFornec(list, romaneio_div, list.ElementAt(0).destinatario, nfs);

                foreach (Email_Divergencia email in email_Divergencia)
                {
                    //email
                    enviaEmail.EnviarMail(email.email, "E-mail Web DGCS_ENTBIP (" + divergencia + ")", texto, emails.email);

                }





            }
            catch (Exception err)
            {


                mensagem.Visible = true;
                mensagem.Text = "Erro ao tentar enviar E-mail! Erro:" + err.Message;
                mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
            }


        }

        public static bool ValidarEAN13(string CodigoEAN13)
        {
            bool result = (CodigoEAN13.Length == 13 || CodigoEAN13.Length == 14);

            return result;
        }

        public void entradaProdFora()
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

            AuxEntrada auxEntrada = new AuxEntrada();
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
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

            Produto produto = new Produto();
            ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            if (ValidarEAN13(cod_barra.Text.Trim()) == true)
            {


                try
                {


                    produto = produtoDao.BuscaProdutoBancoLoja(cod_barra.Text.Trim(), banco_loja.Value, codigo_loja.Value);

                    string romaneio = auxEntradaDao.BuscaProximoRomaneio();

                    foreach (NotaPendente nota in list)
                    {
                        id_xml_nfe[i] = nota.id_xml_nfe;
                        i++;
                        romaneio += nota.chave_nfe.Substring(42, 2);
                    }




                    if (ViewState["update2"].ToString() == ViewState["update"].ToString())
                    {

                        try
                        {
                            //CONSULTA SE EXISTE NOTA EM ABERTO


                            string[] ent_status = new string[4];


                            ent_status = auxEntradaDao.ConsultaAuxStatus(usuarioLogin.Value);

                            if (ent_status[0] == "" || ent_status[0] == null)  // nota que tem a primeira tentativa de finalização
                            {
                                auxEntrada = auxEntradaDao.ConsultaNotaEmAberto(romaneioEntrada.Text, cod_barra.Text.Trim(), usuarioLogin.Value);

                                if (auxEntrada.produto == null)
                                {

                                    //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO

                                    auxEntrada.produto = produto.produto.Trim();
                                    auxEntrada.cor_produto = produto.cor_produto.Trim();
                                    auxEntrada.codigo_barra = produto.codigo_barra.Trim();
                                    auxEntrada.tamanho = produto.tamanho.Trim();
                                    auxEntrada.fornecedor = produto.fornecedor.Trim();
                                    auxEntrada.desc_produto = produto.desc_produto.Trim();
                                    auxEntrada.grupo = produto.grupo.Trim();
                                    auxEntrada.subgrupo = produto.subgrupo.Trim();
                                    auxEntrada.preco1 = produto.preco1.Trim();

                                    auxEntrada.qtde_total = "0";
                                    auxEntrada.qtde_bipado = "1";
                                    auxEntrada.responsavel = usuarioLogin.Value;
                                    auxEntrada.romaneio_produto = "DG" + romaneio.Trim();
                                    auxEntrada.ids_nfs_selecionadas = id_xml_nfe[0];
                                    auxEntrada.qtde_vezes_bipado = "1";
                                    auxEntrada.codigo_barra = cod_barra.Text.Trim();

                                    if (romaneioEntrada.Text == "00000000")
                                    {
                                        romaneioEntrada.Text = auxEntrada.romaneio_produto;
                                    }
                                    else
                                    {
                                        auxEntrada.romaneio_produto = romaneioEntrada.Text;
                                    }

                                    if (auxEntrada.codigo_barra != null)
                                    {


                                        auxEntradaDao.Salvar(auxEntrada);
                                        notaEmAbertoQtdes();
                                        cod_barra.Focus();
                                        carregaBipagem(romaneioEntrada.Text);
                                        //mensagem

                                        mensagem.Visible = true;
                                        mensagem.Text = "Produto bipado confirmado!";
                                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                        cod_barra.Text = "";
                                    }
                                    else
                                    {

                                        //mensagem

                                        mensagem.Visible = true;
                                        mensagem.Text = "Erro ao tentar gravar produto!";
                                        mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                                        cod_barra.Text = "";

                                    }
                                }
                                else
                                {



                                    int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                    auxEntrada.qtde_bipado = qtde.ToString();
                                    romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                    auxEntradaDao.Atualizar(auxEntrada);
                                    notaEmAbertoQtdes();
                                    cod_barra.Focus();
                                    carregaBipagem(romaneioEntrada.Text);
                                    //mensagem

                                    mensagem.Visible = true;
                                    mensagem.Text = "Produto bipado atualizado!";
                                    mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                    cod_barra.Text = "";


                                }

                            }
                            else // nota que tem mais de uma tentativa de finalização
                            {
                                auxEntrada = auxEntradaDao.ConsultaNotaEmAberto(ent_status[0], cod_barra.Text.Trim(), usuarioLogin.Value);

                                if (auxEntrada.produto == null)
                                {

                                    //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                                    auxEntrada = consultaProduto(cod_barra.Text);

                                    if (auxEntrada.produto != null)
                                    {
                                        romaneioEntrada.Text = auxEntrada.romaneio_produto;
                                        auxEntrada.qtde_vezes_bipado = ent_status[1];
                                        auxEntradaDao.Salvar(auxEntrada);
                                        notaEmAbertoQtdes();
                                        cod_barra.Focus();
                                        carregaBipagem(romaneioEntrada.Text);
                                        //mensagem

                                        mensagem.Visible = true;
                                        mensagem.Text = "Produto bipado confirmado!";
                                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                        cod_barra.Text = "";
                                    }
                                    else
                                    {
                                        confirmaEntForaNota.Text = "O EAN " + cod_barra.Text.Trim() + " não foi encontrado na nota fiscal! Deseja Incluir mesmo assim?";
                                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                        sb.Append(@"<script type='text/javascript'>");
                                        sb.Append("$(function () {");
                                        sb.Append("$('#ModalEntProdForaNota').modal('show');");
                                        sb.Append(" });");
                                        sb.Append("</script>");
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                                    }
                                }
                                else
                                {


                                    if (Convert.ToInt16(auxEntrada.qtde_bipado) + 1 <= Convert.ToInt16(auxEntrada.qtde_total))
                                    {
                                        int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                        auxEntrada.qtde_bipado = qtde.ToString();
                                        romaneioEntrada.Text = auxEntrada.romaneio_produto;
                                        auxEntrada.qtde_vezes_bipado = ent_status[1];
                                        auxEntradaDao.Atualizar(auxEntrada);
                                        notaEmAbertoQtdes();
                                        cod_barra.Focus();
                                        carregaBipagem(romaneioEntrada.Text);
                                        //mensagem

                                        mensagem.Visible = true;
                                        mensagem.Text = "Produto bipado atualizado!";
                                        mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                        cod_barra.Text = "";
                                    }
                                    else
                                    {

                                        if (Convert.ToInt16(auxEntrada.qtde_bipado) + 1 >= Convert.ToInt16(auxEntrada.qtde_total))
                                        {
                                            //confirmaEntAmais.Text = "Atenção! Produto bipado vai exceder a quantidade da nota. Confirma incluir mesmo assim?";

                                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                            //sb.Append(@"<script type='text/javascript'>");
                                            //sb.Append("$(function () {");
                                            //sb.Append("$('#ModalEntProdAmais').modal('show');");
                                            //sb.Append(" });");
                                            //sb.Append("</script>");
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);

                                            int qtde = Convert.ToInt16(auxEntrada.qtde_bipado) + 1;
                                            auxEntrada.qtde_bipado = qtde.ToString();
                                            romaneioEntrada.Text = auxEntrada.romaneio_produto;

                                            auxEntradaDao.Atualizar(auxEntrada);
                                            notaEmAbertoQtdes();
                                            cod_barra.Focus();
                                            carregaBipagem(romaneioEntrada.Text);
                                            //mensagem

                                            mensagem.Visible = true;
                                            mensagem.Text = "Produto bipado atualizado!";
                                            mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                                            cod_barra.Text = "";
                                        }

                                    }

                                }
                            }



                        }
                        catch (Exception erroFinal)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Código de barras não existe na base da loja, 'error');  ", true);
                            cod_barra.Text = "";
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
                        DisplayInfo();
                    }
                    else
                    {
                        HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                        carregaBipagem(romaneio_sessao.Value);
                        romaneioEntrada.Text = romaneio_sessao.Value;
                        notaEmAbertoQtdes();
                        cod_barra.Text = "";
                        cod_barra.Focus();
                    }

                }
                catch (Exception erro)
                {
                    //mensagem

                    mensagem.Visible = true;
                    mensagem.Text = "Código de barras não existe na base da loja";
                    mensagem.CssClass = "alert alert-warning alert-dismissible fade show";
                    cod_barra.Text = "";
                }

            }
            else
            {
                //mensagem

                mensagem.Visible = true;
                mensagem.Text = "Código de barras invalido!";
                mensagem.CssClass = "alert alert-warning alert-dismissible fade show";
                cod_barra.Text = "";
            }
        }

        protected void LinkButtonEntForaNota_Click(object sender, EventArgs e)
        {
            entradaProdFora();
        }

        protected void LinkButtonNegaForaNota_Click(object sender, EventArgs e)
        {
            cod_barra.Text = "";
        }

        protected void GrSobraLote_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void LinkButtonEntAmaisLote_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie banco_loja = Request.Cookies["banco_loja"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];
            string floja = "";

            List<NotaPendente> list = null;
            HttpCookie NotaSelecionadas = Request.Cookies["NotaSelecionadas"];
            if (NotaSelecionadas != null)
            {
                string listaObjetosJson = NotaSelecionadas.Value;
                list = JsonConvert.DeserializeObject<List<NotaPendente>>(listaObjetosJson);
                // Faça o processamento necessário com a lista de objetos
            }


            string IDnfs = "";

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

            List<AuxEntradaLote> listLotePopulada = new List<AuxEntradaLote>();

            List<AuxEntradaLote> listLote = null;
            HttpCookie listaEntradaLote = Request.Cookies["listaEntradaLote"];

            if (listaEntradaLote != null)
            {
                string listaObjetosJson = listaEntradaLote.Value;
                listLote = JsonConvert.DeserializeObject<List<AuxEntradaLote>>(listaObjetosJson);
                // Faça o processamento necessário com a lista de objetos
            }


            AuxEntradaLote auxEntradaLote = null;
            AuxEntradaLoteDao auxEntradaLoteDao = new AuxEntradaLoteDao(servidorCookie.Value,
            banco.Value, usuario_banco.Value, senha_banco.Value);

            ParametrosFtp parametroFTP = new ParametrosFtp();
            ParametroFtpDao fTPDao = new ParametroFtpDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            parametroFTP = fTPDao.BuscaFTPcadastrado();

            try
            {

                int indice = 0;
                foreach (AuxEntradaLote objeto in listLote)
                {
                    auxEntradaLote = new AuxEntradaLote();

                    auxEntradaLote.ids_nfs_selecionadas = IDnfs.ToString();
                    auxEntradaLote.romaneio_produto = romaneioEntrada.Text;
                    auxEntradaLote.filial = objeto.filial;
                    floja = objeto.filial;
                    auxEntradaLote.codigo_loja = codigo_loja.Value;

                    auxEntradaLote.produto = objeto.produto;
                    auxEntradaLote.desc_produto = objeto.desc_produto;
                    auxEntradaLote.cor_produto = objeto.cor_produto;
                    auxEntradaLote.tamanho = objeto.tamanho;
                    auxEntradaLote.codigo_barra = objeto.codigo_barra;
                    auxEntradaLote.preco1 = objeto.preco1;
                    auxEntradaLote.grupo = objeto.grupo;
                    auxEntradaLote.subgrupo = objeto.subgrupo;
                    auxEntradaLote.quantidade = objeto.quantidade;
                    auxEntradaLote.xped = objeto.xped;
                    auxEntradaLote.chave_nfe = objeto.chave_nfe;
                    TextBox textLote = (TextBox)GrSobraLote.Rows[indice].FindControl("textLote");

                    if (textLote.Text == null)
                    {
                        auxEntradaLote.lote = "";
                    }
                    else
                    {
                        auxEntradaLote.lote = textLote.Text;
                    }

                    listLotePopulada.Add(auxEntradaLote);
                    auxEntradaLoteDao.SalvarRetaguardaftptxt(auxEntradaLote, parametroFTP);
                    auxEntradaLoteDao.Salvar(auxEntradaLote);

                    indice++;
                }


                EntradaNotaFiscal();
                Mensagem mensagemEmail = new Mensagem();

                Email enviaEmail = new Email();
                Email_DivergenciaDao email_DivergenciaDao = new Email_DivergenciaDao();
                List<Email_Divergencia> email_Divergencia = new List<Email_Divergencia>();
                StringBuilder texto = mensagemEmail.mensagemEntradaDivergenciaFornecLote(floja);

                if (parametro_ativo_Email.Value == "Ativo")
                {
                    try
                    {

                        email_Divergencia = email_DivergenciaDao.BuscaEmailGrupo("Nota Complementar");

                        foreach (Email_Divergencia email in email_Divergencia)
                        {
                            //email
                            enviaEmail.EnviarMailComArquivo(email.email, "E-mail Web DGCS_ENTBIP ", texto, listLotePopulada);


                        }





                    }
                    catch (Exception err)
                    {


                        mensagem.Visible = true;
                        mensagem.Text = "Erro ao tentar enviar E-mail! Erro:" + err.Message;
                        mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                    }

                }



            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na gravação das informações do lote.', 'error');  ", true);

            }


        }



    }
}