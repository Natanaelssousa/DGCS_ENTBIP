using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class InventarioCiclico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];
                HttpCookie pareametro_ciclico = Request.Cookies["pareametro_ciclico"];

                ViewState["update2"] = Server.UrlEncode(System.DateTime.Now.ToString());
                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                tipoInventario.Text = pareametro_ciclico.Value;
                carregaBipagem();
                validaImport();
            }

        }



        protected bool verificaDisponilidadeInventario()
        {

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie pareametro_ciclico = Request.Cookies["pareametro_ciclico"];

            Parametro parametroCiclico = new Parametro();
            ParametroDao parametroDao = new ParametroDao();

            parametroCiclico = parametroDao.BuscaParametro("Inventario Ciclico");

            InventarioDao inventarioDao = new InventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<Inventario> inventarios = new List<Inventario>();

            inventarios = inventarioDao.BuscaCompletaRetornoLista();

            int qtdeInventariosDia = 0;

            qtdeInventariosDia = Convert.ToInt16(parametroCiclico.valor);


            if (qtdeInventariosDia > inventarios.Count)
            {
                return true;
            }
            else
            {
                return false;
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
        protected void validaImport()
        {
            if (qtdeTotalEans.Text != "0")
            {
                // Atualiza tela 
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append(" document.getElementById('importacao').style.display = 'none';");
                sb.Append(" });");
                sb.Append("</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Janela", sb.ToString(), false);
            }
            else
            {
                // Atualiza tela 
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append("       document.getElementById('importacao').style.display = 'block';");
                sb.Append(" });");
                sb.Append("</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Janela", sb.ToString(), false);
            }

            ClientScript.RegisterStartupScript(this.GetType(), "selectTabScript", "  mudaMenu() ;", true);
        }

        protected void carregaBipagemCodBarra()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<AuxInventario> list = new List<AuxInventario>();
            string tEans = "";
            list = auxEntradaDao.ConsultaBipagem(usuarioLogin.Value);
            tEans = auxEntradaDao.ConsultaTotalEans(usuarioLogin.Value);
            int qtde = 0;
            if (list.Count > 0)
            {

                string romaneio = "";
                foreach (AuxInventario aux in list)
                {

                    qtde = qtde + Convert.ToInt16(aux.qtde_importado);

                    romaneio = aux.romaneio_inventario;
                }

                qtdeTotalImportado.Text = qtde.ToString();
                qtdeTotalEans.Text = tEans;
                romaneioCiclico.Text = romaneio;
                GrNotas.DataSource = null;
                GrNotas.DataSource = list;

                GrNotas.DataBind();
            }
            else
            {
                qtdeTotalEans.Text = "0";
                qtdeTotalImportado.Text = "0";
                romaneioCiclico.Text = "00000000";
                GrNotas.DataSource = null;
                GrNotas.DataBind();
            }




            //validaImport();
        }

        protected void carregaBipagem()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            List<AuxInventario> list = new List<AuxInventario>();
            string tEans = "";
            list = auxEntradaDao.ConsultaBipagem(usuarioLogin.Value);
            tEans = auxEntradaDao.ConsultaTotalEans(usuarioLogin.Value);
            int qtde = 0;
            if (list.Count > 0)
            {

                string romaneio = "";
                foreach (AuxInventario aux in list)
                {

                    qtde = qtde + Convert.ToInt16(aux.qtde_importado);

                    romaneio = aux.romaneio_inventario;
                }

                qtdeTotalImportado.Text = qtde.ToString();
                qtdeTotalEans.Text = tEans;
                romaneioCiclico.Text = romaneio;
                
                GrNotas.DataSource = null;
                GrNotas.DataSource = list;

                GrNotas.DataBind();
            }
            else
            {

                qtdeTotalEans.Text = "0";
                qtdeTotalImportado.Text = "0";
                romaneioCiclico.Text = "00000000";
                GrNotas.DataSource = null;
                GrNotas.DataBind();
            }
            validaImport();
        }


        public string[,] RetornaCodAgrupados(string[] lines)
        {
            string[,] matrizNota;
            matrizNota = new string[lines.Count(), 2];
            bool verdade = false;
            for (int i = 0; i < lines.Count(); i++)
            {

                for (int j = 0; j < lines.Count(); j++)
                {
                    if (matrizNota[j, 0] == lines[i])
                    {
                        matrizNota[j, 0] = lines[i];
                        matrizNota[j, 1] = (Convert.ToInt16(matrizNota[j, 1]) + 1).ToString();
                        verdade = true;
                    }

                }

                if (verdade == false)
                {
                    bool gravou = false;
                    for (int j = 0; j < lines.Count(); j++)
                    {
                        if (matrizNota[j, 0] == null && gravou == false)
                        {
                            matrizNota[j, 0] = lines[i];
                            matrizNota[j, 1] = "1";
                            gravou = true;
                        }

                    }

                }

                verdade = false;

            }



            int count = 0;

            for (int b = 0; b < (matrizNota.Length / 2); b++)
            {


                try
                {
                    if (matrizNota[b, 0] != null)
                    {
                        count++;
                    }
                }
                catch (Exception erro)
                {

                }

            }

            string[,] matrizNota2 = new string[count, 2];


            for (int b = 0; b < (matrizNota.Length / 2); b++)
            {
                if (matrizNota[b, 0] != null)
                {
                    matrizNota2[b, 0] = matrizNota[b, 0];
                    matrizNota2[b, 1] = matrizNota[b, 1];
                }
            }


            return matrizNota2;
        }

        protected void LinkButtonImport_Click(object sender, EventArgs e)
        {

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                bool validacaoInventario = verificaDisponilidadeInventario();


                if (validacaoInventario)
                {

                    bool retorno = false;
                    //var file = new FileInfo(System.IO.Path.GetFullPath(fileSelect.FileName));
                    string caminho = fileSelect.FileName; // file.ToString();

                    int arquivosSalvos = 0;
                    string tipoArquivo = this.Request.Params[0];




                    bool PossuiChave = false;
                    bool arqFormatoCorreto = true;


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase arquivo = new HttpPostedFileWrapper(Request.Files[i]);

                        //Salva o arquivo na pasta Uploads do projeto
                        if (arquivo.ContentLength > 0)
                        {
                            var uploadPath = Server.MapPath("~/Views/Content/Uploads");
                            caminho = Path.Combine(@uploadPath,
                           Path.GetFileName(arquivo.FileName));

                            arquivo.SaveAs(caminho);

                            arquivosSalvos++;
                        }
                    }

                    string[,] matrizNota;

                    // Lê um arquivo linha a linha
                    try
                    {
                        var lines1 = File.ReadAllLines(caminho);
                    }
                    catch (Exception erro)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Arquivo não encontrado!', 'error');  ", true);
                        return;
                    }
                    var lines = File.ReadAllLines(caminho);
                    int ind = 0;
                    matrizNota = new string[lines.Count(), 2];

                    if (opcao1.Checked == false && opcao2.Checked == false && opcao3.Checked == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Necessario selecionar um tipo de arquivo para leitura!', 'error');  ", true);
                        return;
                    }
                    else
                    {
                        foreach (string line in lines)
                        {

                            if (opcao1.Checked == true)
                            {
                                try
                                {
                                    matrizNota[ind, 0] = line.Substring(0, line.IndexOf(","));
                                    matrizNota[ind, 1] = line.Split(',').Last();

                                    ind++;
                                }
                                catch (Exception erro)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Arquivo selecionado não esta no padrão selecionado!', 'error');  ", true);
                                    return;
                                }

                            }


                            if (opcao2.Checked == true)
                            {
                                try
                                {
                                    matrizNota[ind, 0] = line.Substring(0, line.IndexOf(";"));
                                    matrizNota[ind, 1] = line.Split(';').Last();

                                    ind++;
                                }
                                catch (Exception erro)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Arquivo selecionado não esta no padrão selecionado!', 'error');  ", true);
                                    return;
                                }
                            }


                        }

                        if (opcao3.Checked == true)
                        {
                            try
                            {

                                if (lines[0].Contains(",") || lines[0].Contains(";") || lines[0].Contains(" "))
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Arquivo selecionado não esta no padrão selecionado!', 'error');  ", true);
                                    return;
                                }
                                else
                                {

                                    matrizNota = RetornaCodAgrupados(lines);
                                }





                            }
                            catch (Exception erro)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Arquivo selecionado não esta no padrão selecionado!', 'error');  ", true);

                            }
                        }

                        string codsIvalidos = "";

                        for (int i = 0; i < (matrizNota.Length / 2); i++)
                        {

                            retorno = lerCodigoBarras(matrizNota[i, 0], matrizNota[i, 1]);

                            if (retorno == false)
                            {
                                codsIvalidos += matrizNota[i, 0] + ",";

                            }
                        }



                        if (codsIvalidos != "")
                        {
                            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                            HttpCookie banco = Request.Cookies["banco"];
                            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                            HttpCookie senha_banco = Request.Cookies["senha_banco"];
                            HttpCookie usuarioLogin = Request.Cookies["usuario"];

                            List<AuxInventario> listaQtdeDivergente = null;

                            AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
                              banco.Value, usuario_banco.Value, senha_banco.Value);
                            bool validaProds = false;


                            if (codsIvalidos != "")
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção...', 'Os codigos a seguir não foram importados pois são invalidos! " + codsIvalidos + "', 'error');  ", true);

                        }

                        carregaBipagem();

                    }

                    if (System.IO.File.Exists(caminho))
                    {
                        System.IO.File.Delete(caminho);
                    }

                }
                else
                {
                    Response.Redirect("~/Views/Menu.aspx", false);
                }
            } // fim
        }

        public AuxInventario consultaProduto(string codigo_barra)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie banco_loja = Request.Cookies["banco_loja"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            AuxInventario auxEntrada = new AuxInventario();
            AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            Produto produto = new Produto();
            ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            AuxInventario auxEntradaRomaneio = new AuxInventario();
            auxEntradaRomaneio = auxEntradaDao.ConsultaRomaneioEmAberto(romaneioCiclico.Text, usuarioLogin.Value);

            int i = 0;
            string romaneio = "";
            if (auxEntradaRomaneio.romaneio_inventario == null)
            {
                romaneio = auxEntradaDao.BuscaProximoRomaneio();
                auxEntrada.romaneio_inventario = "DGI" + romaneio.Trim();
                auxEntradaDao.AtualizarRomaneio();
            }
            else
            {
                romaneio = auxEntradaRomaneio.romaneio_inventario;
                auxEntrada.romaneio_inventario = romaneio.Trim();
            }



            produto = produtoDao.BuscaProdutoBancoLoja(codigo_barra.Trim(), banco_loja.Value, codigo_loja.Value);

            if (produto.produto != null)
            {
                auxEntrada.qtde_importado = "1";
                auxEntrada.produto = produto.produto.Trim();
                auxEntrada.cor_produto = produto.cor_produto.Trim();
                auxEntrada.codigo_barra = produto.codigo_barra.Trim();
                auxEntrada.tamanho = produto.tamanho.Trim();
                auxEntrada.fornecedor = produto.fornecedor.Trim();
                auxEntrada.responsavel = usuarioLogin.Value;
                auxEntrada.desc_produto = produto.desc_produto.Trim();
                auxEntrada.filial = produto.filial.Trim();

                auxEntrada.qtde_vezes_importado = "1";
                auxEntrada.grupo = produto.grupo;
                auxEntrada.subgrupo = produto.subgrupo;
            }


            return auxEntrada;
        }




        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloInventario.aspx", false);
        }

        protected void LinkButtonRefazer_Click(object sender, EventArgs e)
        {
            confirmaRefazer.Text = "Confirma refazer a bipagem do romaneio " + romaneioCiclico.Text + " ? ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalRefazer').modal('show'); ");
            sb.Append("document.getElementById('importacao').style.display = 'none';");
            sb.Append(" });");
            sb.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
        }

        protected void LinkButtonPausar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void LinkButtonConfirmaRazer_Click(object sender, EventArgs e)
        {

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {
                HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
                HttpCookie banco = Request.Cookies["banco"];
                HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
                HttpCookie senha_banco = Request.Cookies["senha_banco"];
                HttpCookie usuarioLogin = Request.Cookies["usuario"];

                AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                try
                {

                    auxEntradaDao.RemoverPor(romaneioCiclico.Text, usuarioLogin.Value);

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'Inventario Removido!', 'success')", true);

                    romaneioCiclico.Text = "00000000";
                    qtdeTotalImportado.Text = "0";
                    qtdeTotalEans.Text = "0";
                    GrNotas.DataSource = null;
                    mensagem.Visible = false;

                    GrNotas.DataBind();
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na tentativa de apagar Inventario', 'error');  ", true);

                }

                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];

                // O cookie foi encontrado, então você pode modificar o valor
                romaneio_sessao.Value = romaneioCiclico.Text;
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(romaneio_sessao);

                DisplayInfo();
            }
            else
            {
                mensagem.Visible = false;
                carregaBipagem();
                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                if (romaneio_sessao == null)
                {
                    HttpCookie romaneio_sessao1 = new HttpCookie("romaneio_sessao");
                    romaneio_sessao1.Value = romaneioCiclico.Text;
                    Response.Cookies.Add(romaneio_sessao1);
                }
                else
                {

                    romaneioCiclico.Text = romaneio_sessao.Value;

                }


            }
        }

        protected void LinkButtonNegaRefazer_Click(object sender, EventArgs e)
        {
            validaImport();
        }

        protected void LinkButtonFinalizar_Click(object sender, EventArgs e)
        {
            confirmaEntradaLabel.Text = "Confirma a finalização do inventario romaneio " + romaneioCiclico.Text + "? Se sim, por favor informe a descrição do inventario abaixo e depois clique em finalizar. ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(function () {");
            sb.Append("$('#ModalconfirmaEntradas').modal('show');");
            sb.Append("   document.getElementById('importacao').style.display = 'none'");
            sb.Append(" });");
            sb.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
        }

        protected void LinkButtonNaoConfirmaEntrada_Click(object sender, EventArgs e)
        {
            validaImport();
        }

        protected void LinkButtonConfirmarFinalizar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];


            AuxInventarioDao auxEntradaDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            Inventario inventario = new Inventario();
            InventarioConfiguracao inventarioConfiguracao = new InventarioConfiguracao();
            InventarioProduto inventarioProduto = new InventarioProduto();
            InventarioSetor inventarioSetor = new InventarioSetor();

            InventarioDao inventarioDao = new InventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            InventarioConfiguracaoDao inventarioConfiguracaoDao = new InventarioConfiguracaoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            InventarioProdutoDao inventarioProdutoDao = new InventarioProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            InventarioSetorDao inventarioSetorDao = new InventarioSetorDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            List<AuxInventario> auxInventarios = new List<AuxInventario>();

            auxInventarios = auxEntradaDao.ConsultaBipagem(usuarioLogin.Value);

            // tabela INVENTARIO
            // contabilizando o total
            int qtdeTotal = 0;
            foreach (AuxInventario notaId in auxInventarios)
            {
                qtdeTotal += Convert.ToInt16(notaId.qtde_importado);
            }
            inventario.codigo_inventario = auxInventarios.ElementAt(0).romaneio_inventario;
            inventario.filial = auxInventarios.ElementAt(0).filial;
            inventario.desc_inventario = desc_inventario.Text;
            inventario.responsavel = usuarioLogin.Value;
            inventario.emissao = DateTime.Now.ToString("yyyy-MM-dd");
            inventario.arquivo_contagem = "DGCS";
            inventario.qtde_total = qtdeTotal.ToString();

            inventario.todos_produtos = "0";

            inventario.valido_e_finalizado = "0";
            inventario.data_para_transferencia = DateTime.Now.ToString("yyyy-MM-dd");
            inventario.obs = "Inventario realizado por sistema de bipagem - DG Consultoria e Sistemas";
            inventario.estoque_ajustado = "0";
            inventario.data_ajuste = DateTime.Now.ToString("yyyy-MM-dd");
            inventario.responsavel_ajuste = usuarioLogin.Value;
            inventario.data_finalizacao = DateTime.Now.ToString("yyyy-MM-dd");
            inventario.data_saldo = DateTime.Now.ToString("yyyy-MM-dd");
            inventario.lx_status_inventario = "1";


            //tabela INVENTARIO_CONFIGURACAO
            inventarioConfiguracao.codigo_inventario = auxInventarios.ElementAt(0).romaneio_inventario;
            inventarioConfiguracao.filial = auxInventarios.ElementAt(0).filial;
            inventarioConfiguracao.local = "1";
            inventarioConfiguracao.nome_setor = "LOJA";
            inventarioConfiguracao.iniciado = DateTime.Now.AddMinutes(-3).ToString("yyyy-MM-dd");
            inventarioConfiguracao.encerrado = DateTime.Now.ToString("yyyy-MM-dd");
            inventarioConfiguracao.qtde_contada = qtdeTotal.ToString();
            inventarioConfiguracao.qtde_contagens = auxInventarios.ElementAt(0).qtde_vezes_importado;
            inventarioConfiguracao.ultima_qtde_contada = qtdeTotal.ToString();
            inventarioConfiguracao.numero_contagem = auxInventarios.ElementAt(0).qtde_vezes_importado;


            //tabela INVENTARIO_PRODUTO

            List<InventarioProduto> ListaInventarioProduto = new List<InventarioProduto>();
            // inserindo informações para a gravação da tabela loja_entradas_produto inicio
            foreach (AuxInventario notaId in auxInventarios)
            {
                //if (auxInventarios.ElementAt(0).codigo_barra == notaId.codigo_barra)
                //{

                InventarioProduto inventarioProduto1 = new InventarioProduto();
                string[] vetorAtributos = null;
                vetorAtributos = inventarioProduto1.retornaVetor();
                for (int ind = 0; ind < vetorAtributos.Length; ind++)
                {
                    string vlr = "en" + notaId.tamanho;
                    if (vlr.Trim() == vetorAtributos[ind])
                    {
                        vetorAtributos[ind] = notaId.qtde_importado;
                    }
                    else
                    {
                        vetorAtributos[ind] = "0";
                    }
                }

                inventarioProduto1 = inventarioProduto1.retornaObjeto(vetorAtributos);
                inventarioProduto1.filial = notaId.filial;
                inventarioProduto1.codigo_inventario = auxInventarios.ElementAt(0).romaneio_inventario;
                inventarioProduto1.produto = notaId.produto;
                inventarioProduto1.cor_produto = notaId.cor_produto;
                inventarioProduto1.qtde = notaId.qtde_importado;

                inventarioProduto1.data_para_transferencia = DateTime.Now.ToString("yyyy-MM-dd");

                ListaInventarioProduto.Add(inventarioProduto1);
                //}

            }


            InventarioProduto inventarioProdutoNovo = null;

            List<InventarioProduto> listaInventarioProdutoFinal = new List<InventarioProduto>();



            foreach (AuxInventario item1 in auxInventarios)
            {
                //if (auxInventarios.ElementAt(0).codigo_barra == item1.codigo_barra)
                //{

                inventarioProdutoNovo = new InventarioProduto("0");

                foreach (InventarioProduto item2 in ListaInventarioProduto)
                {


                    if (item1.produto.Trim() == item2.produto.Trim() && item1.cor_produto.Trim() == item2.cor_produto.Trim())
                    {


                        inventarioProdutoNovo.codigo_inventario = item2.codigo_inventario;
                        inventarioProdutoNovo.filial = item2.filial;
                        inventarioProdutoNovo.produto = item2.produto;
                        inventarioProdutoNovo.cor_produto = item2.cor_produto;
                        inventarioProdutoNovo.qtde = (Convert.ToInt32(inventarioProdutoNovo.qtde) + Convert.ToInt32(item2.qtde)).ToString();
                        inventarioProdutoNovo.en1 = (Convert.ToInt32(inventarioProdutoNovo.en1) + Convert.ToInt32(item2.en1)).ToString();
                        inventarioProdutoNovo.en2 = (Convert.ToInt32(inventarioProdutoNovo.en2) + Convert.ToInt32(item2.en2)).ToString();
                        inventarioProdutoNovo.en3 = (Convert.ToInt32(inventarioProdutoNovo.en3) + Convert.ToInt32(item2.en3)).ToString();
                        inventarioProdutoNovo.en4 = (Convert.ToInt32(inventarioProdutoNovo.en4) + Convert.ToInt32(item2.en4)).ToString();
                        inventarioProdutoNovo.en5 = (Convert.ToInt32(inventarioProdutoNovo.en5) + Convert.ToInt32(item2.en5)).ToString();
                        inventarioProdutoNovo.en6 = (Convert.ToInt32(inventarioProdutoNovo.en6) + Convert.ToInt32(item2.en6)).ToString();
                        inventarioProdutoNovo.en7 = (Convert.ToInt32(inventarioProdutoNovo.en7) + Convert.ToInt32(item2.en7)).ToString();
                        inventarioProdutoNovo.en8 = (Convert.ToInt32(inventarioProdutoNovo.en8) + Convert.ToInt32(item2.en8)).ToString();
                        inventarioProdutoNovo.en9 = (Convert.ToInt32(inventarioProdutoNovo.en9) + Convert.ToInt32(item2.en9)).ToString();
                        inventarioProdutoNovo.en10 = (Convert.ToInt32(inventarioProdutoNovo.en10) + Convert.ToInt32(item2.en10)).ToString();
                        inventarioProdutoNovo.en11 = (Convert.ToInt32(inventarioProdutoNovo.en11) + Convert.ToInt32(item2.en11)).ToString();
                        inventarioProdutoNovo.en12 = (Convert.ToInt32(inventarioProdutoNovo.en12) + Convert.ToInt32(item2.en12)).ToString();
                        inventarioProdutoNovo.en13 = (Convert.ToInt32(inventarioProdutoNovo.en13) + Convert.ToInt32(item2.en13)).ToString();
                        inventarioProdutoNovo.en14 = (Convert.ToInt32(inventarioProdutoNovo.en14) + Convert.ToInt32(item2.en14)).ToString();
                        inventarioProdutoNovo.en15 = (Convert.ToInt32(inventarioProdutoNovo.en15) + Convert.ToInt32(item2.en15)).ToString();
                        inventarioProdutoNovo.en16 = (Convert.ToInt32(inventarioProdutoNovo.en16) + Convert.ToInt32(item2.en16)).ToString();
                        inventarioProdutoNovo.en17 = (Convert.ToInt32(inventarioProdutoNovo.en17) + Convert.ToInt32(item2.en17)).ToString();
                        inventarioProdutoNovo.en18 = (Convert.ToInt32(inventarioProdutoNovo.en18) + Convert.ToInt32(item2.en18)).ToString();
                        inventarioProdutoNovo.en19 = (Convert.ToInt32(inventarioProdutoNovo.en19) + Convert.ToInt32(item2.en19)).ToString();
                        inventarioProdutoNovo.en20 = (Convert.ToInt32(inventarioProdutoNovo.en20) + Convert.ToInt32(item2.en20)).ToString();
                        inventarioProdutoNovo.en21 = (Convert.ToInt32(inventarioProdutoNovo.en21) + Convert.ToInt32(item2.en21)).ToString();
                        inventarioProdutoNovo.en22 = (Convert.ToInt32(inventarioProdutoNovo.en22) + Convert.ToInt32(item2.en22)).ToString();
                        inventarioProdutoNovo.en23 = (Convert.ToInt32(inventarioProdutoNovo.en23) + Convert.ToInt32(item2.en23)).ToString();
                        inventarioProdutoNovo.en24 = (Convert.ToInt32(inventarioProdutoNovo.en24) + Convert.ToInt32(item2.en24)).ToString();
                        inventarioProdutoNovo.en25 = (Convert.ToInt32(inventarioProdutoNovo.en25) + Convert.ToInt32(item2.en25)).ToString();
                        inventarioProdutoNovo.en26 = (Convert.ToInt32(inventarioProdutoNovo.en26) + Convert.ToInt32(item2.en26)).ToString();
                        inventarioProdutoNovo.en27 = (Convert.ToInt32(inventarioProdutoNovo.en27) + Convert.ToInt32(item2.en27)).ToString();
                        inventarioProdutoNovo.en28 = (Convert.ToInt32(inventarioProdutoNovo.en28) + Convert.ToInt32(item2.en28)).ToString();
                        inventarioProdutoNovo.en29 = (Convert.ToInt32(inventarioProdutoNovo.en29) + Convert.ToInt32(item2.en29)).ToString();
                        inventarioProdutoNovo.en30 = (Convert.ToInt32(inventarioProdutoNovo.en30) + Convert.ToInt32(item2.en30)).ToString();
                        inventarioProdutoNovo.en31 = (Convert.ToInt32(inventarioProdutoNovo.en31) + Convert.ToInt32(item2.en31)).ToString();
                        inventarioProdutoNovo.en32 = (Convert.ToInt32(inventarioProdutoNovo.en32) + Convert.ToInt32(item2.en32)).ToString();
                        inventarioProdutoNovo.en33 = (Convert.ToInt32(inventarioProdutoNovo.en33) + Convert.ToInt32(item2.en33)).ToString();
                        inventarioProdutoNovo.en34 = (Convert.ToInt32(inventarioProdutoNovo.en34) + Convert.ToInt32(item2.en34)).ToString();
                        inventarioProdutoNovo.en35 = (Convert.ToInt32(inventarioProdutoNovo.en35) + Convert.ToInt32(item2.en35)).ToString();
                        inventarioProdutoNovo.en36 = (Convert.ToInt32(inventarioProdutoNovo.en36) + Convert.ToInt32(item2.en36)).ToString();
                        inventarioProdutoNovo.en37 = (Convert.ToInt32(inventarioProdutoNovo.en37) + Convert.ToInt32(item2.en37)).ToString();
                        inventarioProdutoNovo.en38 = (Convert.ToInt32(inventarioProdutoNovo.en38) + Convert.ToInt32(item2.en38)).ToString();
                        inventarioProdutoNovo.en39 = (Convert.ToInt32(inventarioProdutoNovo.en39) + Convert.ToInt32(item2.en39)).ToString();
                        inventarioProdutoNovo.en40 = (Convert.ToInt32(inventarioProdutoNovo.en40) + Convert.ToInt32(item2.en40)).ToString();
                        inventarioProdutoNovo.en41 = (Convert.ToInt32(inventarioProdutoNovo.en41) + Convert.ToInt32(item2.en41)).ToString();
                        inventarioProdutoNovo.en42 = (Convert.ToInt32(inventarioProdutoNovo.en42) + Convert.ToInt32(item2.en42)).ToString();
                        inventarioProdutoNovo.en43 = (Convert.ToInt32(inventarioProdutoNovo.en43) + Convert.ToInt32(item2.en43)).ToString();
                        inventarioProdutoNovo.en44 = (Convert.ToInt32(inventarioProdutoNovo.en44) + Convert.ToInt32(item2.en44)).ToString();
                        inventarioProdutoNovo.en45 = (Convert.ToInt32(inventarioProdutoNovo.en45) + Convert.ToInt32(item2.en45)).ToString();
                        inventarioProdutoNovo.en46 = (Convert.ToInt32(inventarioProdutoNovo.en46) + Convert.ToInt32(item2.en46)).ToString();
                        inventarioProdutoNovo.en47 = (Convert.ToInt32(inventarioProdutoNovo.en47) + Convert.ToInt32(item2.en47)).ToString();
                        inventarioProdutoNovo.en48 = (Convert.ToInt32(inventarioProdutoNovo.en48) + Convert.ToInt32(item2.en48)).ToString();
                        inventarioProdutoNovo.filial = item2.filial;
                        inventarioProdutoNovo.codigo_inventario = item2.codigo_inventario;
                        inventarioProdutoNovo.data_para_transferencia = item2.data_para_transferencia;
                    }

                }

                // ajuste de produto negativo em teste
                if (Convert.ToInt32(inventarioProdutoNovo.qtde) >= 0)
                {
                    listaInventarioProdutoFinal.Add(inventarioProdutoNovo);
                }

                //}
            }


            //tabela INVENTARIO_SETOR

            List<InventarioSetor> ListaInventarioSetor = new List<InventarioSetor>();
            // inserindo informações para a gravação da tabela loja_entradas_produto inicio
            foreach (AuxInventario notaId in auxInventarios)
            {
                //if (auxInventarios.ElementAt(0).codigo_barra == notaId.codigo_barra)
                //{


                InventarioSetor inventarioSetor1 = new InventarioSetor();
                string[] vetorAtributos = null;
                vetorAtributos = inventarioSetor1.retornaVetor();
                for (int ind = 0; ind < vetorAtributos.Length; ind++)
                {
                    string vlr = "q" + notaId.tamanho;
                    if (vlr.Trim() == vetorAtributos[ind])
                    {
                        vetorAtributos[ind] = notaId.qtde_importado;
                    }
                    else
                    {
                        vetorAtributos[ind] = "0";
                    }
                }

                inventarioSetor1 = inventarioSetor1.retornaObjeto(vetorAtributos);
                inventarioSetor1.filial = notaId.filial;
                inventarioSetor1.local = "1";
                inventarioSetor1.codigo_inventario = auxInventarios.ElementAt(0).romaneio_inventario;
                inventarioSetor1.produto = notaId.produto;
                inventarioSetor1.cor_produto = notaId.cor_produto;
                inventarioSetor1.qtde_contagem = notaId.qtde_importado;
                inventarioSetor1.ordem_entrada = "";
                inventarioSetor1.data_para_transferencia = DateTime.Now.ToString("yyyy-MM-dd");

                ListaInventarioSetor.Add(inventarioSetor1);
                //}

            }




            InventarioSetor inventarioSetorNovo = null;

            List<InventarioSetor> listaInventarioSetorFinal = new List<InventarioSetor>();



            foreach (AuxInventario item1 in auxInventarios)
            {
                //if (auxInventarios.ElementAt(0).codigo_barra == item1.codigo_barra)
                //{

                inventarioSetorNovo = new InventarioSetor("0");

                foreach (InventarioSetor item2 in ListaInventarioSetor)
                {


                    if (item1.produto.Trim() == item2.produto.Trim() && item1.cor_produto.Trim() == item2.cor_produto.Trim())
                    {


                        inventarioSetorNovo.codigo_inventario = item2.codigo_inventario;
                        inventarioSetorNovo.filial = item2.filial;
                        inventarioSetorNovo.produto = item2.produto;
                        inventarioSetorNovo.cor_produto = item2.cor_produto;
                        inventarioSetorNovo.qtde_contagem = (Convert.ToInt32(inventarioSetorNovo.qtde_contagem) + Convert.ToInt32(item2.qtde_contagem)).ToString();
                        inventarioSetorNovo.q1 = (Convert.ToInt32(inventarioSetorNovo.q1) + Convert.ToInt32(item2.q1)).ToString();
                        inventarioSetorNovo.q2 = (Convert.ToInt32(inventarioSetorNovo.q2) + Convert.ToInt32(item2.q2)).ToString();
                        inventarioSetorNovo.q3 = (Convert.ToInt32(inventarioSetorNovo.q3) + Convert.ToInt32(item2.q3)).ToString();
                        inventarioSetorNovo.q4 = (Convert.ToInt32(inventarioSetorNovo.q4) + Convert.ToInt32(item2.q4)).ToString();
                        inventarioSetorNovo.q5 = (Convert.ToInt32(inventarioSetorNovo.q5) + Convert.ToInt32(item2.q5)).ToString();
                        inventarioSetorNovo.q6 = (Convert.ToInt32(inventarioSetorNovo.q6) + Convert.ToInt32(item2.q6)).ToString();
                        inventarioSetorNovo.q7 = (Convert.ToInt32(inventarioSetorNovo.q7) + Convert.ToInt32(item2.q7)).ToString();
                        inventarioSetorNovo.q8 = (Convert.ToInt32(inventarioSetorNovo.q8) + Convert.ToInt32(item2.q8)).ToString();
                        inventarioSetorNovo.q9 = (Convert.ToInt32(inventarioSetorNovo.q9) + Convert.ToInt32(item2.q9)).ToString();
                        inventarioSetorNovo.q10 = (Convert.ToInt32(inventarioSetorNovo.q10) + Convert.ToInt32(item2.q10)).ToString();
                        inventarioSetorNovo.q11 = (Convert.ToInt32(inventarioSetorNovo.q11) + Convert.ToInt32(item2.q11)).ToString();
                        inventarioSetorNovo.q12 = (Convert.ToInt32(inventarioSetorNovo.q12) + Convert.ToInt32(item2.q12)).ToString();
                        inventarioSetorNovo.q13 = (Convert.ToInt32(inventarioSetorNovo.q13) + Convert.ToInt32(item2.q13)).ToString();
                        inventarioSetorNovo.q14 = (Convert.ToInt32(inventarioSetorNovo.q14) + Convert.ToInt32(item2.q14)).ToString();
                        inventarioSetorNovo.q15 = (Convert.ToInt32(inventarioSetorNovo.q15) + Convert.ToInt32(item2.q15)).ToString();
                        inventarioSetorNovo.q16 = (Convert.ToInt32(inventarioSetorNovo.q16) + Convert.ToInt32(item2.q16)).ToString();
                        inventarioSetorNovo.q17 = (Convert.ToInt32(inventarioSetorNovo.q17) + Convert.ToInt32(item2.q17)).ToString();
                        inventarioSetorNovo.q18 = (Convert.ToInt32(inventarioSetorNovo.q18) + Convert.ToInt32(item2.q18)).ToString();
                        inventarioSetorNovo.q19 = (Convert.ToInt32(inventarioSetorNovo.q19) + Convert.ToInt32(item2.q19)).ToString();
                        inventarioSetorNovo.q20 = (Convert.ToInt32(inventarioSetorNovo.q20) + Convert.ToInt32(item2.q20)).ToString();
                        inventarioSetorNovo.q21 = (Convert.ToInt32(inventarioSetorNovo.q21) + Convert.ToInt32(item2.q21)).ToString();
                        inventarioSetorNovo.q22 = (Convert.ToInt32(inventarioSetorNovo.q22) + Convert.ToInt32(item2.q22)).ToString();
                        inventarioSetorNovo.q23 = (Convert.ToInt32(inventarioSetorNovo.q23) + Convert.ToInt32(item2.q23)).ToString();
                        inventarioSetorNovo.q24 = (Convert.ToInt32(inventarioSetorNovo.q24) + Convert.ToInt32(item2.q24)).ToString();
                        inventarioSetorNovo.q25 = (Convert.ToInt32(inventarioSetorNovo.q25) + Convert.ToInt32(item2.q25)).ToString();
                        inventarioSetorNovo.q26 = (Convert.ToInt32(inventarioSetorNovo.q26) + Convert.ToInt32(item2.q26)).ToString();
                        inventarioSetorNovo.q27 = (Convert.ToInt32(inventarioSetorNovo.q27) + Convert.ToInt32(item2.q27)).ToString();
                        inventarioSetorNovo.q28 = (Convert.ToInt32(inventarioSetorNovo.q28) + Convert.ToInt32(item2.q28)).ToString();
                        inventarioSetorNovo.q29 = (Convert.ToInt32(inventarioSetorNovo.q29) + Convert.ToInt32(item2.q29)).ToString();
                        inventarioSetorNovo.q30 = (Convert.ToInt32(inventarioSetorNovo.q30) + Convert.ToInt32(item2.q30)).ToString();
                        inventarioSetorNovo.q31 = (Convert.ToInt32(inventarioSetorNovo.q31) + Convert.ToInt32(item2.q31)).ToString();
                        inventarioSetorNovo.q32 = (Convert.ToInt32(inventarioSetorNovo.q32) + Convert.ToInt32(item2.q32)).ToString();
                        inventarioSetorNovo.q33 = (Convert.ToInt32(inventarioSetorNovo.q33) + Convert.ToInt32(item2.q33)).ToString();
                        inventarioSetorNovo.q34 = (Convert.ToInt32(inventarioSetorNovo.q34) + Convert.ToInt32(item2.q34)).ToString();
                        inventarioSetorNovo.q35 = (Convert.ToInt32(inventarioSetorNovo.q35) + Convert.ToInt32(item2.q35)).ToString();
                        inventarioSetorNovo.q36 = (Convert.ToInt32(inventarioSetorNovo.q36) + Convert.ToInt32(item2.q36)).ToString();
                        inventarioSetorNovo.q37 = (Convert.ToInt32(inventarioSetorNovo.q37) + Convert.ToInt32(item2.q37)).ToString();
                        inventarioSetorNovo.q38 = (Convert.ToInt32(inventarioSetorNovo.q38) + Convert.ToInt32(item2.q38)).ToString();
                        inventarioSetorNovo.q39 = (Convert.ToInt32(inventarioSetorNovo.q39) + Convert.ToInt32(item2.q39)).ToString();
                        inventarioSetorNovo.q40 = (Convert.ToInt32(inventarioSetorNovo.q40) + Convert.ToInt32(item2.q40)).ToString();
                        inventarioSetorNovo.q41 = (Convert.ToInt32(inventarioSetorNovo.q41) + Convert.ToInt32(item2.q41)).ToString();
                        inventarioSetorNovo.q42 = (Convert.ToInt32(inventarioSetorNovo.q42) + Convert.ToInt32(item2.q42)).ToString();
                        inventarioSetorNovo.q43 = (Convert.ToInt32(inventarioSetorNovo.q43) + Convert.ToInt32(item2.q43)).ToString();
                        inventarioSetorNovo.q44 = (Convert.ToInt32(inventarioSetorNovo.q44) + Convert.ToInt32(item2.q44)).ToString();
                        inventarioSetorNovo.q45 = (Convert.ToInt32(inventarioSetorNovo.q45) + Convert.ToInt32(item2.q45)).ToString();
                        inventarioSetorNovo.q46 = (Convert.ToInt32(inventarioSetorNovo.q46) + Convert.ToInt32(item2.q46)).ToString();
                        inventarioSetorNovo.q47 = (Convert.ToInt32(inventarioSetorNovo.q47) + Convert.ToInt32(item2.q47)).ToString();
                        inventarioSetorNovo.q48 = (Convert.ToInt32(inventarioSetorNovo.q48) + Convert.ToInt32(item2.q48)).ToString();
                        inventarioSetor.filial = item2.filial;
                        inventarioSetor.local = "LOJA";
                        inventarioSetor.codigo_inventario = item2.codigo_inventario;
                        inventarioSetor.ordem_entrada = item2.ordem_entrada;
                        inventarioSetor.data_para_transferencia = item2.data_para_transferencia;


                    }

                }

                // ajuste de produto negativo em teste
                if (Convert.ToInt32(inventarioProdutoNovo.qtde) >= 0)
                {
                    listaInventarioSetorFinal.Add(inventarioSetorNovo);
                }

                //}
            }

            string mensagemErro = "";

            // salvado inventario nas tabelas

            // tabela INVENTARIO
            try
            {
                inventarioDao.Salvar(inventario);
            }
            catch (Exception erro)
            {
                mensagemErro = "Erro ao grava tabela INVENTARIO";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao grava tabela INVENTARIO', 'error');  ", true);

            }

            //tabela INVENTARIO_CONFIGURACAO
            try
            {
                inventarioConfiguracaoDao.Salvar(inventarioConfiguracao);
            }
            catch (Exception erro)
            {
                mensagemErro += "Erro ao grava tabela INVENTARIO_CONFIGURACAO";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao grava tabela INVENTARIO_CONFIGURACAO', 'error');  ", true);
            }

            //tabela INVENTARIO_PRODUTO
            try
            {

                foreach (InventarioProduto saveInvProd in listaInventarioProdutoFinal)
                {
                    inventarioProdutoDao.Salvar(saveInvProd);

                }

            }
            catch (Exception erro)
            {
                mensagemErro += "Erro ao grava tabela INVENTARIO_PRODUTO";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao grava tabela INVENTARIO_PRODUTO', 'error');  ", true);

            }

            //tabela INVENTARIO_SETOR
            try
            {
                foreach (InventarioSetor saveInvSetor in listaInventarioSetorFinal)
                {
                    inventarioSetorDao.Salvar(saveInvSetor);

                }
            }
            catch (Exception erro)
            {
                mensagemErro += "Erro ao grava tabela INVENTARIO_SETOR";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao grava tabela INVENTARIO_SETOR', 'error');  ", true);
            }



            if (mensagemErro == "")
            {

                AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

                try
                {
                    auxInventarioDao.AtualizarStatus(usuarioLogin.Value, romaneioCiclico.Text);

                    romaneioCiclico.Text = "00000000";
                    qtdeTotalEans.Text = "0";
                    qtdeTotalImportado.Text = "0";
                    mensagem.Visible = false;
                    carregaBipagem();
                }
                catch (Exception erro)
                {

                }



                confirmaEntSucesso.Text = "Parabéns inventario realizado com sucesso!";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append("$('#ModalEntSucesso').modal('show');");
                sb.Append(" });");
                sb.Append("</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
            }

            validaImport();
            desc_inventario.Text = "";
        }

        public bool lerCodigoBarras(string codigo_barra, string qtde_item)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie pareametro_ciclico = Request.Cookies["pareametro_ciclico"];

            bool retorno = false;

            AuxInventario auxInventario = new AuxInventario();

            AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            try
            {
                //CONSULTA SE EXISTE NOTA EM ABERTO

                auxInventario = auxInventarioDao.ConsultaNotaEmAberto(romaneioCiclico.Text, codigo_barra, usuarioLogin.Value);


                if (auxInventario.produto == null)
                {

                    //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                    auxInventario = consultaProduto(codigo_barra);

                    if (auxInventario.produto != null)
                    {
                        auxInventario.qtde_vezes_importado = "1";
                        auxInventario.qtde_importado = qtde_item;
                        romaneioCiclico.Text = auxInventario.romaneio_inventario;
                        auxInventario.status_inventario = "EM ABERTO";
                        auxInventario.tipo_inventario = pareametro_ciclico.Value;
                        auxInventarioDao.Salvar(auxInventario);
                        //mensagem de retorno positivo
                        retorno = true;
                    }
                    else
                    {

                        //mensagem de retorno negativo "O EAN não foi encontrado na nota fiscal! Deseja Incluir mesmo assim?"
                        retorno = false;
                    }
                }
                else
                {


                    if (auxInventario.produto == "")
                    {
                        int qtde = Convert.ToInt16(auxInventario.qtde_importado) + 1;
                        auxInventario.qtde_importado = qtde.ToString();
                        romaneioCiclico.Text = auxInventario.romaneio_inventario;
                        auxInventario.qtde_vezes_importado = "1";
                        auxInventario.status_inventario = "EM ABERTO";
                        auxInventario.tipo_inventario = pareametro_ciclico.Value;
                        auxInventarioDao.Atualizar(auxInventario);
                        //notaEmAbertoQtdes();

                        //mensagem de retorno positivo
                        retorno = true;
                    }
                    else
                    {



                        //mensagem de retorno negativo "Atenção! Produto bipado vai exceder a quantidade da nota. Confirma incluir mesmo assim?"
                        retorno = false;


                    }

                }






            }
            catch (Exception erroFinal)
            {

                //mensagem de retorno negativo "Atenção!', 'Erro na consulta do codigo de barras"
                retorno = false;

            }



            HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
            if (romaneio_sessao == null)
            {
                HttpCookie romaneio_sessao1 = new HttpCookie("romaneio_sessao");
                romaneio_sessao1.Value = romaneioCiclico.Text;
                Response.Cookies.Add(romaneio_sessao1);
            }
            else
            {
                // O cookie foi encontrado, então você pode modificar o valor
                romaneio_sessao.Value = romaneioCiclico.Text;
                // Atualize o cookie no navegador do cliente
                Response.SetCookie(romaneio_sessao);
            }


            DisplayInfo();


            return retorno;

        }
        protected void cod_barra_TextChanged(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie pareametro_ciclico = Request.Cookies["pareametro_ciclico"];

            if (ViewState["update2"].ToString() == ViewState["update"].ToString())
            {

                AuxInventario auxInventario = new AuxInventario();

                AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
              banco.Value, usuario_banco.Value, senha_banco.Value);

                try
                {
                    //CONSULTA SE EXISTE NOTA EM ABERTO


                    auxInventario = auxInventarioDao.ConsultaNotaEmAberto(romaneioCiclico.Text, cod_barra.Text, usuarioLogin.Value);


                    if (auxInventario.produto == null)
                    {

                        //CONSULTA SE PRODUTO EXISTE NAS NOTAS EM ABERTO
                        auxInventario = consultaProduto(cod_barra.Text);

                        if (auxInventario.produto != null)
                        {
                            auxInventario.qtde_vezes_importado = "1";
                            if (ZerarQtde.Checked == true)
                            {
                                auxInventario.qtde_importado = "0";
                            }
                            else
                            {
                                auxInventario.qtde_importado = "1";
                            }

                            romaneioCiclico.Text = auxInventario.romaneio_inventario;
                            auxInventario.status_inventario = "EM ABERTO";
                            auxInventario.tipo_inventario = pareametro_ciclico.Value;
                            auxInventarioDao.Salvar(auxInventario);

                            ZerarQtde.Checked = false;
                            carregaBipagemCodBarra();

                            cod_barra.Focus();
                            //mensagem de retorno positivo
                            mensagem.Visible = true;
                            mensagem.Text = "Produto bipado confirmado!";
                            mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                            cod_barra.Text = "";
                        }
                        else
                        {

                            mensagem.Visible = true;
                            mensagem.Text = "Produto bipado não encontrado!";
                            mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                            cod_barra.Text = "";
                        }
                    }
                    else
                    {


                        if (auxInventario.produto != "" && auxInventario != null)
                        {


                            if (ZerarQtde.Checked == true)
                            {
                                auxInventario.qtde_importado = "0";
                            }
                            else
                            {
                                int qtde = Convert.ToInt16(auxInventario.qtde_importado) + 1;
                                auxInventario.qtde_importado = qtde.ToString();
                            }

                            romaneioCiclico.Text = auxInventario.romaneio_inventario;
                            auxInventario.qtde_vezes_importado = "1";
                            auxInventario.status_inventario = "EM ABERTO";
                            auxInventario.tipo_inventario = pareametro_ciclico.Value;
                            auxInventarioDao.Atualizar(auxInventario);
                            carregaBipagemCodBarra();
                            cod_barra.Focus();
                            ZerarQtde.Checked = false;
                            //mensagem

                            mensagem.Visible = true;
                            mensagem.Text = "Produto bipado atualizado!";
                            mensagem.CssClass = "alert alert-success alert-dismissible fade show";
                            cod_barra.Text = "";
                        }
                        else
                        {

                            mensagem.Visible = true;
                            mensagem.Text = "Erro Na bipagem!";
                            mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                            cod_barra.Text = "";


                        }

                    }






                }
                catch (Exception erroFinal)
                {

                    mensagem.Visible = true;
                    mensagem.Text = "Erro Na bipagem!";
                    mensagem.CssClass = "alert alert-danger alert-dismissible fade show";
                    cod_barra.Text = "";

                }



                HttpCookie romaneio_sessao = Request.Cookies["romaneio_sessao"];
                if (romaneio_sessao == null)
                {
                    HttpCookie romaneio_sessao1 = new HttpCookie("romaneio_sessao");
                    romaneio_sessao1.Value = romaneioCiclico.Text;
                    Response.Cookies.Add(romaneio_sessao1);
                }
                else
                {
                    // O cookie foi encontrado, então você pode modificar o valor
                    romaneio_sessao.Value = romaneioCiclico.Text;
                    // Atualize o cookie no navegador do cliente
                    Response.SetCookie(romaneio_sessao);
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

            AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            try
            {
                auxInventarioDao.ExecutaSP_DGCS_PROCESSOS();
                Response.Redirect("~/Views/Menu.aspx", false);
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Processo concluido, porém aconteceu um erro ao atualizar dados.', 'error');  ", true);
            }

            desc_inventario.Text = "";
        }

        protected void GrNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string desc_produto = DataBinder.Eval(e.Row.DataItem, "desc_produto").ToString();
                string produto = DataBinder.Eval(e.Row.DataItem, "produto").ToString();
                string grupo = DataBinder.Eval(e.Row.DataItem, "grupo").ToString();
                string subgrupo = DataBinder.Eval(e.Row.DataItem, "subgrupo").ToString();
                string codigo_barra = DataBinder.Eval(e.Row.DataItem, "codigo_barra").ToString();
                string qtde_importado = DataBinder.Eval(e.Row.DataItem, "qtde_importado").ToString();


                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");
                String strComando = "funcaoQueExibeModal('" + produto + "','" + desc_produto + "','" + codigo_barra + "','" + qtde_importado + "');";
                imgUpload.OnClientClick = strComando;


            }

            // Verifica se a linha é uma linha de dados (não é o cabeçalho ou rodapé)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtém o índice da linha
                int rowIndex = e.Row.RowIndex;

                // Encontra o ImageButton dentro da linha
                ImageButton btn = (ImageButton)e.Row.FindControl("btnsubtrair");

                // Define o CommandArgument do ImageButton para o índice da linha
                btn.CommandArgument = rowIndex.ToString();
            }
        }

        protected void LinkButtonExcluirItem_Click(object sender, EventArgs e)
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

            AuxInventario auxInventario = new AuxInventario();

            AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            auxInventario.romaneio_inventario = romaneioCiclico.Text;
            auxInventario.responsavel = usuarioLogin.Value;
            auxInventario.codigo_barra = codigo_barras.Text;

            try
            {
                auxInventarioDao.RemoverPorItem(auxInventario.romaneio_inventario, auxInventario.responsavel, auxInventario.codigo_barra);
                carregaBipagemCodBarra();
                mensagem.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Item excluido com sucesso.', 'success');  ", true);

            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar excluir item.', 'error');  ", true);

            }


        }



    
   

        protected void btnsubtrair_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int rowIndex = Convert.ToInt32(btn.CommandArgument);

            GridViewRow row = GrNotas.Rows[rowIndex];
            string cod_barra = row.Cells[4].Text; // codigo de barra
            string qtde_importado = row.Cells[5].Text; // qtde importado

            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie validaItensQtde = Request.Cookies["validaItensQtde"];
            HttpCookie status_divergencia = Request.Cookies["status_divergencia"];
            HttpCookie parametro_ativo_Email = Request.Cookies["parametro_ativo_Email"];
            HttpCookie vezes_bipado = Request.Cookies["vezes_bipado"];

            AuxInventario auxInventario = new AuxInventario();

            AuxInventarioDao auxInventarioDao = new AuxInventarioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            auxInventario.romaneio_inventario = romaneioCiclico.Text;
            auxInventario.responsavel = usuarioLogin.Value;
            auxInventario.codigo_barra = cod_barra;
            auxInventario.qtde_importado = (Convert.ToInt16(qtde_importado)-1).ToString();


            if ((Convert.ToInt16(qtde_importado) - 1) >= 0)
            {
                try
                {
                    auxInventarioDao.Atualizar(auxInventario);
                    carregaBipagemCodBarra();
                    mensagem.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Item excluido com sucesso.', 'success');  ", true);

                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar excluir item.', 'error');  ", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar excluir item. Quantidade não pode ser negativa.', 'error');  ", true);

            }

        }
    }
}