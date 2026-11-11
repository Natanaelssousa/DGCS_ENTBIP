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
    public partial class RelatorioLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaLogs();
            }
        }


        protected void GrLogs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }



        protected void carregaLogs()
        {

            LogRelatorio log = new LogRelatorio();
            LogRelatorioDao logDao = new LogRelatorioDao();


            List<LogRelatorio> list = logDao.BuscaCompletaRetornoLista();

            GrLogs.DataSource = list;
            GrLogs.DataBind();
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {

            LogRelatorio notas = new LogRelatorio();
            LogRelatorioDao notasDao = new LogRelatorioDao();


            //string palavra = txtbusca.Text;
            if (periodo_inicio.Text != "" && periodo_fim.Text != "" && usuario_log.Text == "" && cnpj.Text == "")
            {
                List<LogRelatorio> list = notasDao.BuscaRetornoListaNFsPorDataRetaguarda(periodo_inicio.Text, periodo_fim.Text);

                if (list.Count > 0)
                {
                    GrLogs.DataSource = list;
                    GrLogs.DataBind();
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                }
                else
                {
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (usuario_log.Text != "" && cnpj.Text == "")
            {
                List<LogRelatorio> list = notasDao.BuscaRetornoNFsPorUsuarioRetaguarda(usuario_log.Text);

                if (list.Count > 0)
                {
                    GrLogs.DataSource = list;
                    GrLogs.DataBind();
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                }
                else
                {
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (periodo_inicio.Text != "" && periodo_fim.Text != "" && usuario_log.Text == "" && cnpj.Text != "")
            {
                List<LogRelatorio> list = notasDao.BuscaRetornoListaNFsPorDataCnpjRetaguarda(periodo_inicio.Text, periodo_fim.Text, cnpj.Text);

                if (list.Count > 0)
                {
                    GrLogs.DataSource = list;
                    GrLogs.DataBind();
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                }
                else
                {
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (usuario_log.Text != "" && cnpj.Text != "")
            {
                List<LogRelatorio> list = notasDao.BuscaRetornoListaNFsPorUsuarioCnpjRetaguarda(usuario_log.Text, cnpj.Text);

                if (list.Count > 0)
                {
                    GrLogs.DataSource = list;
                    GrLogs.DataBind();
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                }
                else
                {
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhuma nota encontrada!', 'error');  ", true);

                }
            }
            else if (periodo_inicio.Text == "" && periodo_fim.Text == "" && usuario_log.Text == "" && cnpj.Text != "")
            {
                List<LogRelatorio> list = notasDao.BuscaRetornoListaNFsPorCnpjRetaguarda(cnpj.Text);

                if (list.Count > 0)
                {
                    GrLogs.DataSource = list;
                    GrLogs.DataBind();
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
                }
                else
                {
                    usuario_log.Text = "";
                    periodo_inicio.Text = "";
                    periodo_fim.Text = "";
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
            gerarExcel(GrLogs);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}