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
    public partial class RelatorioAbastecimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaNotas();
            }
        }


        protected void GrNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }



        protected void carregaNotas()
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            Relatorio nota = new Relatorio();
            RelatorioDao relatorioDao = new RelatorioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);


            List<Relatorio> list = relatorioDao.BuscaRetornoNFsDiaAtualRetaguarda();

            GrNotas.DataSource = list;
            GrNotas.DataBind();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie banco = Request.Cookies["banco"];
            HttpCookie usuario_banco = Request.Cookies["usuario_banco"];
            HttpCookie senha_banco = Request.Cookies["senha_banco"];
            HttpCookie usuarioLogin = Request.Cookies["usuario"];

            Relatorio notas = new Relatorio();
            RelatorioDao notasDao = new RelatorioDao(servidorCookie.Value,
          banco.Value, usuario_banco.Value, senha_banco.Value);
            LogRelatorio logRelatorio = new LogRelatorio();
            LogRelatorioDao logRelatorioDao = new LogRelatorioDao();
            //string palavra = txtbusca.Text;
            if (emissao_inicio.Text != "" && emissao_fim.Text != "" && nota_fiscal.Text == "" && cnpj.Text == "")
            {

                logRelatorio.cnpj = "Não informado";
                logRelatorio.nota_fiscal = "Não informado";
                logRelatorio.periodo_inicio = emissao_inicio.Text;
                logRelatorio.periodo_fim = emissao_fim.Text;
                logRelatorio.usuario = usuarioLogin.Value;

                try
                {
                    logRelatorioDao.Salvar(logRelatorio);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'erro de gravação de dados!', 'error');  ", true);

                }

                List<Relatorio> list = notasDao.BuscaRetornoListaNFsPorDataRetaguarda(emissao_inicio.Text, emissao_fim.Text);

                if (list.Count > 0)
                {
                    GrNotas.DataSource = list;
                    GrNotas.DataBind();
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (nota_fiscal.Text != "" && cnpj.Text == "")
            {


                logRelatorio.cnpj = "Não informado";
                logRelatorio.nota_fiscal = nota_fiscal.Text;
                logRelatorio.periodo_inicio = "Não informado";
                logRelatorio.periodo_fim = "Não informado";
                logRelatorio.usuario = usuarioLogin.Value;

                try
                {
                    logRelatorioDao.Salvar(logRelatorio);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'erro de gravação de dados!', 'error');  ", true);

                }

                List<Relatorio> list = notasDao.BuscaRetornoNFsPorNotaRetaguarda(nota_fiscal.Text);

                if (list.Count > 0)
                {
                    GrNotas.DataSource = list;
                    GrNotas.DataBind();
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text != "" && emissao_fim.Text != "" && nota_fiscal.Text == "" && cnpj.Text != "")
            {

                logRelatorio.cnpj = cnpj.Text;
                logRelatorio.nota_fiscal = "Não informado"; 
                logRelatorio.periodo_inicio = emissao_inicio.Text;
                logRelatorio.periodo_fim = emissao_fim.Text;
                logRelatorio.usuario = usuarioLogin.Value;

                try
                {
                    logRelatorioDao.Salvar(logRelatorio);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'erro de gravação de dados!', 'error');  ", true);

                }


                List<Relatorio> list = notasDao.BuscaRetornoListaNFsPorDataCnpjRetaguarda(emissao_inicio.Text, emissao_fim.Text, cnpj.Text);

                if (list.Count > 0)
                {
                    GrNotas.DataSource = list;
                    GrNotas.DataBind();
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if ( nota_fiscal.Text != "" && cnpj.Text != "")
            {

                logRelatorio.cnpj = cnpj.Text;
                logRelatorio.nota_fiscal = nota_fiscal.Text;
                logRelatorio.periodo_inicio = "Não informado";
                logRelatorio.periodo_fim = "Não informado";
                logRelatorio.usuario = usuarioLogin.Value;

                try
                {
                    logRelatorioDao.Salvar(logRelatorio);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'erro de gravação de dados!', 'error');  ", true);

                }

                List<Relatorio> list = notasDao.BuscaRetornoListaNFsPorNfCnpjRetaguarda(nota_fiscal.Text, cnpj.Text);

                if (list.Count > 0)
                {
                    GrNotas.DataSource = list;
                    GrNotas.DataBind();
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (emissao_inicio.Text == "" && emissao_fim.Text == "" && nota_fiscal.Text == "" && cnpj.Text != "")
            {

                logRelatorio.cnpj = cnpj.Text;
                logRelatorio.nota_fiscal = "Não informado";
                logRelatorio.periodo_inicio = "Não informado";
                logRelatorio.periodo_fim = "Não informado";
                logRelatorio.usuario = usuarioLogin.Value;

                try
                {
                    logRelatorioDao.Salvar(logRelatorio);
                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'erro de gravação de dados!', 'error');  ", true);

                }

                List<Relatorio> list = notasDao.BuscaRetornoListaNFsPorCnpjRetaguarda(cnpj.Text);

                if (list.Count > 0)
                {
                    GrNotas.DataSource = list;
                    GrNotas.DataBind();
                    nota_fiscal.Text = "";
                    emissao_inicio.Text = "";
                    emissao_fim.Text = "";
                }
                else
                {
                    nota_fiscal.Text = "";
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


        public void gerarExcel(GridView grdGridView)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=relatorio_notas_fiscais_DGCS.xls");
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
            gerarExcel(GrNotas);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}