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
    public partial class NotasPendentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];
                HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                string meuObjetoJson = perfil_usuario.Value;
                perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                if (perfilAcesso != null)
                {

                    tela_notas_pendentes.Text = perfilAcesso.tela_notas_pendentes;
                    perfil_acesso.Text = perfilAcesso.perfil;
                }
                carregaNotasPendentes();
            }
        }


        protected void GrNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string nf_entrada = DataBinder.Eval(e.Row.DataItem, "nf_entrada").ToString();
                string nome_clifor = DataBinder.Eval(e.Row.DataItem, "nome_clifor").ToString();
                string serie_nf_entrada = DataBinder.Eval(e.Row.DataItem, "serie_nf_entrada").ToString();
                string chave_nfe = DataBinder.Eval(e.Row.DataItem, "chave_nfe").ToString();
                string id_xml_nfe = DataBinder.Eval(e.Row.DataItem, "id_xml_nfe").ToString();
                string qtde = DataBinder.Eval(e.Row.DataItem, "qtde").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();
                string responsavel = DataBinder.Eval(e.Row.DataItem, "responsavel").ToString();
                string xped = DataBinder.Eval(e.Row.DataItem, "xped").ToString();

                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtnRefazer");
                Label refazer = (Label)e.Row.FindControl("refazer");
                CheckBox check = (CheckBox)e.Row.FindControl("ckIDAprova");

                if (status == "Em andamento")
                {
                    imgUpload.ImageUrl = "images/remover.png";

                }
                else
                {
                    imgUpload.Enabled = false;
                    imgUpload.ImageUrl = "images/logo_natura2.jpg";
                }

                for (int indice = 0; indice <= GrNotas.Rows.Count; indice++)
                {
                    refazer.Text = (1 + indice).ToString();
                }

                if ((tela_notas_pendentes.Text == "Total" && perfil_acesso.Text == "Master") || (tela_notas_pendentes.Text == "Total" && perfil_acesso.Text == "Gerente Loja"))
                {
                    imgUpload.Visible = true;
                    refazer.Visible = false;
                    check.Visible = true;
                }
                else if (tela_notas_pendentes.Text == "Consulta")
                {
                    imgUpload.Visible = false;
                    refazer.Visible = true;
                    check.Visible = false;
                }
                else
                {
                    imgUpload.Visible = false;
                    refazer.Visible = true;
                    check.Visible = true;
                }

            }

        }



        protected void carregaNotasPendentes()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];

            NotaPendente notaPendente = new NotaPendente();
            NotaPendenteDao notaPendenteDao = new NotaPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            List<NotaPendente> list = notaPendenteDao.BuscaCompletaRetornoListaNFs();

            GrNotas.DataSource = list;
            GrNotas.DataBind();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloEntradaRevenda.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];

            NotaPendente notas = new NotaPendente();
            NotaPendenteDao notasDao = new NotaPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            string palavra = txtbusca.Text;


            List<NotaPendente> list = notasDao.BuscaRetornoPalavrasChaves(palavra);

            if (list.Count > 0)
            {
                GrNotas.DataSource = list;
                GrNotas.DataBind();
                txtbusca.Text = "";
            }
            else
            {
                txtbusca.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhum cadastro encontrado!', 'error');  ", true);

            }


        }

        protected void LinkButtonEntComplementar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie parametro_ativo = Request.Cookies["parametro_ativo"];
            
            if (perfilAcesso.perfil == "Master" || tela_notas_pendentes.Text == "Total")
            {

                string[] notaPendente = new string[3];
                string notaEmAberto = "";
                bool notaUsada = false;
                NotaPendente notas;
                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                List<NotaPendente> list = new List<NotaPendente>();
                int count = 0;
                for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                {
                    CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");
                    if (ckIDAprova.Checked)
                    {
                        count++;

                        if (notaUsada == false)
                        {
                            notaUsada = auxEntradaDao.ConsultaNotaEmBIpagemPorOUtroUsuario(GrNotas.DataKeys[indice]["id_xml_nfe"].ToString(), usuarioLogin.Value);

                        }


                    }
                }
                string[] antilha = new string[3];
                int countAntilha = 0;

                if (count > 0 && count <= 3 && notaUsada == false)
                {
                    for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                    {
                        CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");

                        if (ckIDAprova.Checked)
                        {

                            notas = new NotaPendente();
                            notas.chave_nfe = GrNotas.DataKeys[indice]["chave_nfe"].ToString();
                            notas.serie_nf_entrada = GrNotas.DataKeys[indice]["serie_nf_entrada"].ToString();
                            notas.id_xml_nfe = GrNotas.DataKeys[indice]["id_xml_nfe"].ToString();
                            notas.nf_entrada = GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                            notas.nome_clifor = GrNotas.DataKeys[indice]["nome_clifor"].ToString();
                            antilha[countAntilha] = GrNotas.DataKeys[indice]["nome_clifor"].ToString();
                            list.Add(notas);

                            countAntilha++;
                        }
                    }

                    notaPendente = auxEntradaDao.ConsultaNotaEmAbertoResponsavel(usuarioLogin.Value);
                    bool entrada = true;
                    if (notaPendente[0] != null)
                    {

                        foreach (NotaPendente nf in list)
                        {
                            if (!notaPendente[2].Contains(nf.id_xml_nfe))
                            {
                                entrada = false;
                            }

                        }

                    }
                    else
                    {
                        entrada = true;
                    }

                    if (entrada == true)
                    {
                        HttpCookie romaneio_pendente = new HttpCookie("romaneio_pendente");
                        romaneio_pendente.Value = notaPendente[1];
                        Response.Cookies.Add(romaneio_pendente);

                        HttpCookie qtde_bipado = new HttpCookie("qtde_bipado");
                        qtde_bipado.Value = notaPendente[0];
                        Response.Cookies.Add(qtde_bipado);

                        string listaObjetosJson = JsonConvert.SerializeObject(list);
                        HttpCookie NotaSelecionadas = new HttpCookie("NotaSelecionadas");
                        NotaSelecionadas.Value = listaObjetosJson;
                        Response.Cookies.Add(NotaSelecionadas);


                        DateTime dtParametro = Convert.ToDateTime(parametro_data.Value);
                        List<Produto> listChave;
                        bool notaErro = false;
                        ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                        string[] ids_xmls = new string[4];
                        int cont = 0;
                        foreach (NotaPendente nf in list)
                        {
                            ids_xmls[cont] = nf.id_xml_nfe;
                            cont++;
                        }

                        listChave = produtoDao.BuscaProdutoTodosProdutosNotasSemBipar(ids_xmls);
                        string descNotas = "";
                        foreach (Produto produtoChave in listChave)
                        {
                            if (produtoChave.codigo_barra == "" || produtoChave.codigo_barra == null)
                            {

                                notaErro = true;
                                if (descNotas == "")
                                {
                                    descNotas = produtoChave.nota_fiscal;
                                }
                                else
                                {
                                    descNotas += "," + produtoChave.nota_fiscal;
                                }
                            }

                        } // aqui

                        bool validaAntilha = true;

                        for (int i = 0; i < antilha.Length; i++)
                        {
                            if (antilha[i] != null && (!antilha[i].Contains("ANTILHAS")))
                            {
                                validaAntilha = false;
                            }

                        }


                        if (parametro_ativo.Value == "Entrada bipada" && dtParametro >= DateTime.Now && notaErro == false && validaAntilha == false)
                        {

                            for (int i = 0; i < antilha.Length; i++)
                            {
                                if (antilha[i] != null && (antilha[i].Contains("ANTILHAS")))
                                {
                                    validaAntilha = true;
                                }

                            }
                            if (validaAntilha == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario retirar a nota de antilhas para dar continuidade na entrada, ou trocar todas as outras por notas de antilhas.', 'error');  ", true);

                            }
                            else
                            {
                                //Response.Redirect("~/Views/EntradaComplementar.aspx", false);
                                Response.Redirect("~/Views/EntradaComplementarChaveNfe.aspx", false);
                            }

                        }
                        else if ((parametro_ativo.Value == "Estoque Externo" && dtParametro >= DateTime.Now && notaErro == false))
                        {
                            //  Response.Redirect("~/Views/EntEstoqueExterno.aspx", false);

                            Response.Redirect("~/Views/EntradaComplementarChaveNfe.aspx", false);
                        }
                        else if ((parametro_ativo.Value == "Entrada cega" && dtParametro >= DateTime.Now && notaErro == false) || validaAntilha == true)
                        {

                            Response.Redirect("~/Views/EntradaComplementarChaveNfe.aspx", false);


                        }
                        else
                        {
                            if (dtParametro < DateTime.Now && notaErro == false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'A data fim do parametro venceu.', 'error');  ", true);
                            }
                            else if (notaErro != false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Uma ou mais notas selecionadas estão com erro de cadastro, verifique. Notas: " + descNotas + "', 'error');  ", true);

                            }

                        }

                    }
                    else
                    {

                        for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                        {
                            CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");
                            ckIDAprova.Checked = false;

                            if (notaPendente[2].Contains(GrNotas.DataKeys[indice]["id_xml_nfe"].ToString()))
                            {
                                if (notaEmAberto != "")
                                {
                                    notaEmAberto += "," + GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                                }
                                else
                                {
                                    notaEmAberto += GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                                }



                            }
                        }



                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Não é possivel iniciar entrada pois já possui uma bipagem em aberto desse responsavel! As notas em aberto são " + notaEmAberto + "', 'error');  ", true);

                    }


                }
                else
                {
                    if (notaUsada == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Uma ou mais notas selecionadas estão com o status pendente impossibilitando a entrada. Verifique!', 'error');  ", true);

                    }
                    else if (count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario selecionar ao menos uma nota!', 'error');  ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Foi excedido o limite de seleção de notas!', 'error');  ", true);

                    }


                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Seu perfil de usuario não tem permissão de acesso.', 'error');  ", true);

            }
        }

        protected void LinkButtonEntPadrao_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie parametro_ativo = Request.Cookies["parametro_ativo"];

            if (perfilAcesso.perfil == "Master" || tela_notas_pendentes.Text == "Total")
            {

                string[] notaPendente = new string[3];
                string notaEmAberto = "";
                bool notaUsada = false;
                NotaPendente notas;
                AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                List<NotaPendente> list = new List<NotaPendente>();
                int count = 0;
                for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                {
                    CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");
                    if (ckIDAprova.Checked)
                    {
                        count++;

                        if (notaUsada == false)
                        {
                            notaUsada = auxEntradaDao.ConsultaNotaEmBIpagemPorOUtroUsuario(GrNotas.DataKeys[indice]["id_xml_nfe"].ToString(), usuarioLogin.Value);

                        }


                    }
                }
                string[] antilha = new string[3];
                int countAntilha = 0;
                int notasAbertoUsuario = 0;
                if (count > 0 && count <= 3 && notaUsada == false)
                {
                    for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                    {
                        CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");

                        if (ckIDAprova.Checked)
                        {

                            notas = new NotaPendente();
                            notas.chave_nfe = GrNotas.DataKeys[indice]["chave_nfe"].ToString();
                            notas.serie_nf_entrada = GrNotas.DataKeys[indice]["serie_nf_entrada"].ToString();
                            notas.id_xml_nfe = GrNotas.DataKeys[indice]["id_xml_nfe"].ToString();
                            notas.nf_entrada = GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                            notas.nome_clifor = GrNotas.DataKeys[indice]["nome_clifor"].ToString();
                            antilha[countAntilha] = GrNotas.DataKeys[indice]["nome_clifor"].ToString();
                            countAntilha++;
                            list.Add(notas);
                        }

                        if (GrNotas.DataKeys[indice]["status"].ToString() == "Em andamento" && usuarioLogin.Value == GrNotas.DataKeys[indice]["responsavel"].ToString())
                        {
                            notasAbertoUsuario++;
                        }

                    }



                    notaPendente = auxEntradaDao.ConsultaNotaEmAbertoResponsavel(usuarioLogin.Value);
                    bool entrada = true;
                    if (notaPendente[0] != null)
                    {

                        foreach (NotaPendente nf in list)
                        {
                            if (!notaPendente[2].Contains(nf.id_xml_nfe))
                            {
                                entrada = false;
                            }

                        }

                        if (list.Count != notasAbertoUsuario)
                        {
                            entrada = false;
                        }

                    }
                    else
                    {
                        entrada = true;
                    }

                    if (entrada == true)
                    {
                     

                        HttpCookie romaneio_pendente = new HttpCookie("romaneio_pendente");
                        romaneio_pendente.Value = notaPendente[1];
                        Response.Cookies.Add(romaneio_pendente);

                        HttpCookie qtde_bipado = new HttpCookie("qtde_bipado");
                        qtde_bipado.Value = notaPendente[0];
                        Response.Cookies.Add(qtde_bipado);

                        string listaObjetosJson = JsonConvert.SerializeObject(list);
                        HttpCookie NotaSelecionadas = new HttpCookie("NotaSelecionadas");
                        NotaSelecionadas.Value = listaObjetosJson;
                        Response.Cookies.Add(NotaSelecionadas);

                        DateTime dtParametro = Convert.ToDateTime(parametro_data.Value);
                        List<Produto> listChave;
                        bool notaErro = false;
                        ProdutoDao produtoDao = new ProdutoDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
                        string[] ids_xmls = new string[4];
                        int cont = 0;
                        foreach (NotaPendente nf in list)
                        {
                            ids_xmls[cont] = nf.id_xml_nfe;
                            cont++;
                        }

                        listChave = produtoDao.BuscaProdutoTodosProdutosNotasSemBipar(ids_xmls);
                        string descNotas = "";
                        foreach (Produto produtoChave in listChave)
                        {
                            if (produtoChave.codigo_barra == "" || produtoChave.codigo_barra == null)
                            {

                                notaErro = true;
                                if (descNotas == "")
                                {
                                    descNotas = produtoChave.nota_fiscal;
                                }
                                else
                                {
                                    descNotas += "," + produtoChave.nota_fiscal;
                                }
                            }

                        }
                        bool validaAntilha = true;

                        for (int i = 0; i < antilha.Length; i++)
                        {
                            if (antilha[i] != null && (!antilha[i].Contains("ANTILHAS")))
                            {
                                validaAntilha = false;
                            }

                        }

                        if (parametro_ativo.Value == "Entrada bipada" && dtParametro >= DateTime.Now && notaErro == false && validaAntilha == false)
                        {
                            for (int i = 0; i < antilha.Length; i++)
                            {
                                if (antilha[i] != null && (antilha[i].Contains("ANTILHAS")))
                                {
                                    validaAntilha = true;
                                }

                            }
                            if (validaAntilha == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario retirar a nota de antilhas para dar continuidade na entrada, ou trocar todas as outras por notas de antilhas.', 'error');  ", true);

                            }
                            else
                            {
                                Response.Redirect("~/Views/EntradaPadrao.aspx", false);
                            }

                        }
                        else if ((parametro_ativo.Value == "Estoque Externo" && dtParametro >= DateTime.Now && notaErro == false && list.Count == 1))
                        {
                            if (validaAntilha == true)
                            {
                                Response.Redirect("~/Views/EntradaCega.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("~/Views/EntEstoqueExterno.aspx", false);
                            }
                        
                        }
                        else if (parametro_ativo.Value == "Entrada cega" && dtParametro >= DateTime.Now && notaErro == false || validaAntilha == true)
                        {

                            Response.Redirect("~/Views/EntradaCega.aspx", false);


                        }
                        else
                        {
                            if (dtParametro < DateTime.Now && notaErro == false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'A data fim do parametro venceu.', 'error');  ", true);
                            }
                            else if ((parametro_ativo.Value == "Estoque Externo" && dtParametro >= DateTime.Now && notaErro == false && list.Count != 1))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Permitido selecionar apenas uma nota por vez para o seguimento do processo, verifique.', 'error');  ", true);

                            }
                            else if (notaErro != false)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Uma ou mais notas selecionadas estão com erro de cadastro, verifique. Notas: " + descNotas + "', 'error');  ", true);

                            }

                        }

                    }
                    else
                    {

                        for (int indice = 0; indice < GrNotas.Rows.Count; indice++)
                        {
                            CheckBox ckIDAprova = (CheckBox)GrNotas.Rows[indice].FindControl("ckIDAprova");
                            ckIDAprova.Checked = false;

                            if (notaPendente[2].Contains(GrNotas.DataKeys[indice]["id_xml_nfe"].ToString()))
                            {
                                if (notaEmAberto != "")
                                {
                                    notaEmAberto += "," + GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                                }
                                else
                                {
                                    notaEmAberto += GrNotas.DataKeys[indice]["nf_entrada"].ToString();
                                }



                            }
                        }


                        if (list.Count != notasAbertoUsuario && notaPendente[0] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Já existem notas em aberto para esse usuario as notas são: " + notaEmAberto + "', 'error');  ", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Não é possivel iniciar entrada pois já possui uma bipagem em aberto desse responsavel! As notas em aberto são " + notaEmAberto + "', 'error');  ", true);

                        }

                    }


                }
                else
                {
                    if (notaUsada == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Uma das notas selecionadas já está sendo bipado por outro úsuario. Verifique!', 'error');  ", true);

                    }
                    else if (count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario selecionar ao menos uma nota!', 'error');  ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Foi excedido o limite de seleção de notas!', 'error');  ", true);

                    }


                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Seu perfil de usuario não tem permissão de acesso.', 'error');  ", true);

            }



        }

        protected void imgbtnRefazer_Click(object sender, ImageClickEventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie parametro_ativo = Request.Cookies["parametro_ativo"];


            NotaPendente notas = new NotaPendente();
            NotaPendenteDao notasDao = new NotaPendenteDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            AuxEntradaDao auxEntradaDao = new AuxEntradaDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            // Identifica o botão que foi clicado
            ImageButton clickedButton = (ImageButton)sender;
            // Identifica a linha que contém o botão
            GridViewRow row = (GridViewRow)clickedButton.Parent.Parent;

            // Acessa o valor da célula 0
            string cell1 = row.Cells[2].Text;



            try
            {
                notas = notasDao.BuscaNotaClicada(cell1);
                auxEntradaDao.RemoverPorIdNfs(notas.id_xml_nfe);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'A nota fiscal pendente foi " +
                    "reiniciada! Se a nota pendente tenha sido iniciada com outras notas, todas serão reiniciadas!" +
                    " Notas reinciadas:" + notas.nf_entrada + "', 'warning');  ", true);

                carregaNotasPendentes();
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar apagar bipagem!', 'error');  ", true);

            }

        }

        protected void LinkButtonConfirmaRazer_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButtonBuscaNotas_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            string meuObjetoJson = perfil_usuario.Value;
            perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
            HttpCookie usuarioLogin = Request.Cookies["usuario"];
            HttpCookie parametro_data = Request.Cookies["parametro_data"];
            HttpCookie codigo_loja = Request.Cookies["codigo_loja"];

            ProdutosBarraXmlDao xmlDao = new ProdutosBarraXmlDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);

            bool retorno = false;
            try
            {
                retorno = xmlDao.ExecutaSP_DGCS_PROCESSA_NFE_XML_LOJA(codigo_loja.Value);

                if (retorno)
                {
                    carregaNotasPendentes();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção', 'Notas sincronizadas com sucesso!', 'success')", true);
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