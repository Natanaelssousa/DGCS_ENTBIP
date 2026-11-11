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
    public partial class RelatorioAmostras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaBipagem();
            }
        }

        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuloRegistrosAmostras.aspx", false);
        }

        protected void carregaBipagem()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];


            RegistroAmostrasDao amostraDao = new RegistroAmostrasDao(servidorCookie.Value,
      banco.Value, usuario_banco.Value, senha_banco.Value);

            List<RegistroAmostras> list = new List<RegistroAmostras>();

            list = amostraDao.BuscaCompletaRetornoLista();

            if (list.Count > 0)
            {
                Gramostras.DataSource = null;
                Gramostras.DataSource = list;

                Gramostras.DataBind();
            }
            else
            {
                Gramostras.DataSource = null;

                Gramostras.DataBind();
            }

        }
        protected void Gramostras_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void gerarExcel(GridView grdGridView)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=relatorio_registro_amostras_DGCS.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            grdGridView.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        protected void LinkButtonExportar_Click(object sender, EventArgs e)
        {
            gerarExcel(Gramostras);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void buscarNota_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            RegistroAmostras amostras = new RegistroAmostras();
            RegistroAmostrasDao amostrasDao = new RegistroAmostrasDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            //string palavra = txtbusca.Text;
            if (emissao_inicio.Text != "" && emissao_fim.Text != "" && cpf.Text == "" && produto.Text == "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorData(emissao_inicio.Text, emissao_fim.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }

            }
            else if (emissao_inicio.Text != "" && emissao_fim.Text != "" && cpf.Text != "" && produto.Text != "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorDataProdutoCPF(emissao_inicio.Text, emissao_fim.Text, cpf.Text, produto.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text != "" && emissao_fim.Text != "" && cpf.Text == "" && produto.Text != "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorDataProduto(emissao_inicio.Text, emissao_fim.Text, produto.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text != "" && emissao_fim.Text != "" && cpf.Text != "" && produto.Text == "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorDataCpf(emissao_inicio.Text, emissao_fim.Text, cpf.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text == "" && emissao_fim.Text == "" && cpf.Text != "" && produto.Text == "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorCpf(cpf.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text == "" && emissao_fim.Text == "" && cpf.Text == "" && produto.Text != "")
            {
                List<RegistroAmostras> list = amostrasDao.BuscaRetornoListaNFsPorProduto(produto.Text);

                if (list.Count > 0)
                {
                    Gramostras.DataSource = list;
                    Gramostras.DataBind();
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    cpf.Text = "";
                    produto.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Informe o período ou o numero da nota para realizar a pesquisa!', 'error');  ", true);

            }

            }
        }


    }