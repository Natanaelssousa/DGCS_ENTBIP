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
    public partial class Parametros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie perfil_usuario = Request.Cookies["perfil_usuario"];
            PerfilAcesso perfilAcesso = new PerfilAcesso();
            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];

                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaParametros();
                if (perfil_usuario != null)
                {
                    string meuObjetoJson = perfil_usuario.Value;
                    perfilAcesso = JsonConvert.DeserializeObject<PerfilAcesso>(meuObjetoJson);
                    // Faça o processamento necessário com o objeto
                }
                if (perfilAcesso != null)
                {
                    
                    perfil_acesso.Text = perfilAcesso.perfil;
                }

            }
        }


        protected void GrParametros_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_parametro_dgcs = DataBinder.Eval(e.Row.DataItem, "id_parametro_dgcs").ToString();
                string nome_parametro = DataBinder.Eval(e.Row.DataItem, "nome_parametro").ToString();
                string descricao = DataBinder.Eval(e.Row.DataItem, "descricao").ToString();
                string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();
                string valor = DataBinder.Eval(e.Row.DataItem, "valor").ToString();
                string responsavel_alteracao = DataBinder.Eval(e.Row.DataItem, "responsavel_alteracao").ToString();
                string data_criacao = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_criacao")).ToString("dd/MM/yyyy");
                string data_alteracao = "";
                if (DataBinder.Eval(e.Row.DataItem, "data_alteracao").ToString() != "" && DataBinder.Eval(e.Row.DataItem, "data_alteracao").ToString() != null)
                {
                    data_alteracao = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_alteracao")).ToString("dd/MM/yyyy");
                }
                string data_inicio = "";
                if (DataBinder.Eval(e.Row.DataItem, "data_alteracao").ToString() != "" && DataBinder.Eval(e.Row.DataItem, "data_inicio").ToString() != null)
                {
                    data_inicio = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_inicio")).ToString("yyyy-MM-dd");
                }
                string data_fim = "";
                if (DataBinder.Eval(e.Row.DataItem, "data_fim").ToString() != "" && DataBinder.Eval(e.Row.DataItem, "data_fim").ToString() != null)
                {
                    data_fim = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_fim")).ToString("yyyy-MM-dd");
                }

                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");

                if (status == "1")
                {
                    status = "Ativo";
                }
                else if (status == "0")
                {
                    status = "Inativo";
                }

                String strComando = "EditarParametro('" + id_parametro_dgcs + "','" + nome_parametro + "','" + descricao + "'," +
                    "'" + status + "','" + data_inicio + "','" + data_fim + "',String.raw`" + valor + "`);return false;";



                imgUpload.OnClientClick = strComando;

            }

        }

        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {
            Parametro parametro = new Parametro();
            ParametroDao parametroDao = new ParametroDao();



            if (id_parametro_dgcstl.Text == "" && nome_parametro.Text != "" && descricao.Text != "" && status.Text != "" && data_inicio.Text != "" && data_fim.Text != "" && valor.Text != "")
            {

                if (status.Text == "Ativo")
                {
                    parametro.status = "1";
                }
                else if (status.Text == "Inativo")
                {
                    parametro.status = "0";
                }
                parametro.nome_parametro = nome_parametro.Text;
                parametro.descricao = descricao.Text;
                parametro.data_inicio = data_inicio.Text;
                parametro.data_fim = data_fim.Text;
                parametro.valor = valor.Text;
                try
                {
                    parametroDao.Salvar(parametro);

                    id_parametro_dgcstl.Text = "";
                    nome_parametro.Text = "";
                    descricao.Text = "";
                    status.Text = "";
                    data_inicio.Text = "";
                    data_fim.Text = "";
                    valor.Text = "";

                    nome_parametrotl.Text = "";
                    descricaotl.Text = "";
                    statustl.Text = "";
                    data_iniciotl.Text = "";
                    data_fimtl.Text = "";
                    valortl.Text = "";

                    carregaParametros();

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                }
                catch (Exception erro)
                {
                    id_parametro_dgcstl.Text = "";
                    nome_parametro.Text = "";
                    descricao.Text = "";
                    status.Text = "";
                    data_inicio.Text = "";
                    data_fim.Text = "";
                    valor.Text = "";

                    nome_parametrotl.Text = "";
                    descricaotl.Text = "";
                    statustl.Text = "";
                    data_iniciotl.Text = "";
                    data_fimtl.Text = "";
                    valortl.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                }
            }
            else if (id_parametro_dgcstl.Text != "" && nome_parametrotl.Text != "" && statustl.Text != "" && descricaotl.Text != "" && data_iniciotl.Text != "" && data_fimtl.Text != "" && valortl.Text != "")
            {


                if (statustl.Text == "Ativo")
                {
                    parametro.status = "1";
                }
                else if (statustl.Text == "Inativo")
                {
                    parametro.status = "0";
                }
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                parametro.id_parametro_dgcs = id_parametro_dgcstl.Text;
                parametro.nome_parametro = nome_parametrotl.Text;
                parametro.descricao = descricaotl.Text;
                parametro.data_inicio = data_iniciotl.Text;
                parametro.data_fim = data_fimtl.Text;
                parametro.responsavel_alteracao = usuarioLogin.Value;
                parametro.valor = valortl.Text;

                try
                {
                    parametroDao.Atualizar(parametro);

                    id_parametro_dgcstl.Text = "";
                    nome_parametro.Text = "";
                    descricao.Text = "";
                    status.Text = "";
                    data_inicio.Text = "";
                    data_fim.Text = "";
                    valor.Text = "";

                    nome_parametrotl.Text = "";
                    descricaotl.Text = "";
                    statustl.Text = "";
                    data_iniciotl.Text = "";
                    data_fimtl.Text = "";
                    valortl.Text = "";

                    carregaParametros();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                }
                catch (Exception erro)
                {
                    id_parametro_dgcstl.Text = "";
                    nome_parametro.Text = "";
                    descricao.Text = "";
                    status.Text = "";
                    data_inicio.Text = "";
                    data_fim.Text = "";
                    valor.Text = "";

                    nome_parametrotl.Text = "";
                    descricaotl.Text = "";
                    statustl.Text = "";
                    data_iniciotl.Text = "";
                    data_fimtl.Text = "";
                    valortl.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                }
            }
            else
            {
                id_parametro_dgcstl.Text = "";
                nome_parametro.Text = "";
                descricao.Text = "";
                status.Text = "";
                data_inicio.Text = "";
                data_fim.Text = "";
                valor.Text = "";

                nome_parametrotl.Text = "";
                descricaotl.Text = "";
                statustl.Text = "";
                data_iniciotl.Text = "";
                data_fimtl.Text = "";
                valortl.Text = "";

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
            }


        }

        protected void carregaParametros()
        {

            Parametro parametro = new Parametro();
            ParametroDao parametroDao = new ParametroDao();


            List<Parametro> list = parametroDao.BuscaCompletaRetornoLista();

            GrParametros.DataSource = list;
            GrParametros.DataBind();
        }



        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {

            Parametro parametro = new Parametro();
            ParametroDao parametroDao = new ParametroDao();



            string nome_parametro = txtbusca.Text;

            List<Parametro> list = parametroDao.BuscaRetornoPalavrasChaves(nome_parametro);

            if (list.Count > 0)
            {
                GrParametros.DataSource = list;
                GrParametros.DataBind();
                txtbusca.Text = "";
            }
            else
            {
                txtbusca.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhum cadastro encontrado!', 'error')", true);

            }


        }
    }
}