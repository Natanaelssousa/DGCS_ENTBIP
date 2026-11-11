using DGCS_ENTBIP.Daos;
using DGCS_ENTBIP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGCS_ENTBIP.Views
{
    public partial class CadastroServidores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HttpCookie usuarioLogin = Request.Cookies["usuario"];
                HttpCookie dt_acesso = Request.Cookies["dt_acesso"];
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                regiaotl.Attributes.Add("readonly", "readonly");
                estadotl.Attributes.Add("readonly", "readonly");
                usuario.Text = "Usúario: " + usuarioLogin.Value;
                data_acesso.Text = "Acesso: " + dt_acesso.Value;
                carregaServidores();
                carregaParametros();
                carregaParametrosCiclico();
                carregaRegioes();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }
        protected void GrServidores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string id_servidores_dgcs = DataBinder.Eval(e.Row.DataItem, "id_servidores_dgcs").ToString();
                string situacao = DataBinder.Eval(e.Row.DataItem, "situacao").ToString();
                string nome_servidor = DataBinder.Eval(e.Row.DataItem, "nome_servidor").ToString();
                string codigo_loja = DataBinder.Eval(e.Row.DataItem, "codigo_loja").ToString();
                string ultima_edicao = DataBinder.Eval(e.Row.DataItem, "ultima_edicao").ToString();
                string usuario_banco = DataBinder.Eval(e.Row.DataItem, "usuario_banco").ToString();
                string senha_banco = cripSenha.DescriptografarSenhaBanco(DataBinder.Eval(e.Row.DataItem, "senha_banco").ToString());
                string nome_db = DataBinder.Eval(e.Row.DataItem, "nome_db").ToString();
                string nome_db_app = DataBinder.Eval(e.Row.DataItem, "nome_db_app").ToString();
                string parametro_ativo = DataBinder.Eval(e.Row.DataItem, "parametro_ativo").ToString();
                string parametro_ciclico = DataBinder.Eval(e.Row.DataItem, "parametro_ciclico").ToString();
                string data_criacao = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "data_criacao")).ToString("dd/MM/yyyy");
                string regiao = DataBinder.Eval(e.Row.DataItem, "regiao").ToString();
                string estados = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                string cnpj = DataBinder.Eval(e.Row.DataItem, "cnpj").ToString();
                string token = DataBinder.Eval(e.Row.DataItem, "token").ToString();
                ImageButton imgUpload = (ImageButton)e.Row.FindControl("imgbtn");
                string tokenOriginal = "";
                if (token != "")
                {
                    tokenOriginal = cripSenha.DescriptografarSenhaBanco(token);
                }


                if (situacao == "1")
                {
                    situacao = "Ativo";
                }
                else if (situacao == "0")
                {
                    situacao = "Inativo";
                }


                String strComando = "EditarServidor('" + id_servidores_dgcs + "','" + situacao + "',String.raw`" + nome_servidor + "`,'"
                    + codigo_loja + "','" + nome_db + "','" + usuario_banco + "','" + senha_banco + "','" + parametro_ativo + "','"
                    + nome_db_app + "','" + regiao + "','" + estados + "','" + cnpj + "','" + tokenOriginal + "','" + parametro_ciclico + "');return false;";
                imgUpload.OnClientClick = strComando;

            }

        }

        private void DisplayInfo()
        {
            // Do what ever thing you want to do
            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        protected void LinkButtonConfirmar_Click(object sender, EventArgs e)
        {
            HttpCookie servidorCookie = Request.Cookies["servidorCookie"];
            HttpCookie bancoCookie = Request.Cookies["banco"];
            HttpCookie usuario_bancoCookie = Request.Cookies["usuario_banco"];
            HttpCookie senha_bancoCookie = Request.Cookies["senha_banco"];
            HttpCookie usuarioLoginCookie = Request.Cookies["usuario"];

            Servidor servidor = new Servidor();
            ServidorDao servidorDao = new ServidorDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());



            if (Session["update"].ToString() == ViewState["update"].ToString())
            {


                string senhaDescriptografada = "";



                if (id_servidores_dgcstl.Text == "" && nome_servidor.Text != "" && codigo_loja.Text != "" && situacao.Text != "" && nome_db.Text != ""
                    && usuario.Text != "" && parametro_ativo.Text != "" && nome_db_app.Text != "" && regiao.Text != "" && estado.Text != "" && cnpj.Text != "" && securityToken.Text != "" && parametro_ciclico1.Text != "")
                {

                    senhaDescriptografada = cripSenha.DescriptografarSenhaBanco(securityToken.Text);

                    string cnpjDGCS = "";
                    string cnpjLoja = "";
                    string loja = "";
                    string senha = "";

                    if (senhaDescriptografada != "")
                    {
                        cnpjDGCS = senhaDescriptografada.Substring(0, 14);
                        cnpjLoja = senhaDescriptografada.Substring(14, 14);
                        loja = senhaDescriptografada.Substring(28, 6);
                        senha = senhaDescriptografada.Substring(34, 12);
                    }



                    SecurityToken validaSecurityToken = new SecurityToken(senha);

                    if (validaSecurityToken.validaChaveToken() && cnpjDGCS == "41179975000106" && cnpjLoja == cnpj.Text && loja == codigo_loja.Text)
                    {



                        if (situacao.Text == "Ativo")
                        {
                            servidor.situacao = "1";
                        }
                        else if (situacao.Text == "Inativo")
                        {
                            servidor.situacao = "0";
                        }
                        servidor.nome_servidor = nome_servidor.Text;
                        servidor.codigo_loja = codigo_loja.Text;
                        servidor.situacao = situacao.Text;
                        servidor.ultima_edicao = usuarioLoginCookie.Value;
                        servidor.nome_db = nome_db.Text;
                        servidor.nome_db_app = nome_db_app.Text;
                        servidor.usuario_banco = usuario_banco.Text;
                        servidor.senha_banco = cripSenha.CriptografarSenhaBanco(senha_banco.Text);
                        servidor.parametro_ativo = parametro_ativo.Text;
                        servidor.regiao = regiao.Text;
                        servidor.estado = estado.Text;
                        servidor.cnpj = cnpj.Text;
                        servidor.token = securityToken.Text;
                        servidor.parametro_ciclico = parametro_ciclico1.Text;
                        try
                        {
                            servidorDao.Salvar(servidor);
                            servidorDao.ServidorDaoInicializar(servidor.nome_servidor,
           servidor.nome_db_app, servidor.usuario_banco, senha_banco.Text);
                            servidorDao.SalvarNaLoja(servidor);
                            carregaServidores();
                            nome_servidortl.Text = "";
                            codigo_lojatl.Text = "";
                            situacaotl.Text = "";
                            nome_dbtl.Text = "";
                            usuario_bancotl.Text = "";
                            senha_bancotl.Text = "";
                            parametro_ativo.Text = "";
                            nome_dbtl_app.Text = "";
                            regiaotl.Text = "";
                            estadotl.Text = "";
                            cnpjtl.Text = "";
                            securityToken.Text = "";
                            parametro_ciclico2.Text = "";

                            nome_servidor.Text = "";
                            codigo_loja.Text = "";
                            situacao.Text = "";
                            nome_db.Text = "";
                            usuario_banco.Text = "";
                            senha_banco.Text = "";
                            parametro_ativotl.Text = "";
                            nome_db_app.Text = "";
                            regiao.Text = "";
                            estado.Text = "";
                            cnpj.Text = "";
                            securityTokentl.Text = "";
                            parametro_ciclico1.Text = "";

                            carregaServidores();

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Salvo com sucesso!', 'success')", true);
                        }
                        catch (Exception erro)
                        {

                            nome_servidortl.Text = "";
                            codigo_lojatl.Text = "";
                            situacaotl.Text = "";
                            nome_dbtl.Text = "";
                            usuario_bancotl.Text = "";
                            senha_bancotl.Text = "";
                            parametro_ativo.Text = "";
                            nome_dbtl_app.Text = "";
                            regiaotl.Text = "";
                            estadotl.Text = "";
                            cnpjtl.Text = "";
                            securityTokentl.Text = "";
                            parametro_ciclico2.Text = "";

                            nome_servidor.Text = "";
                            codigo_loja.Text = "";
                            situacao.Text = "";
                            nome_db.Text = "";
                            usuario_banco.Text = "";
                            senha_banco.Text = "";
                            parametro_ativotl.Text = "";
                            nome_db_app.Text = "";
                            regiao.Text = "";
                            estado.Text = "";
                            cnpj.Text = "";
                            securityToken.Text = "";
                            parametro_ciclico1.Text = "";

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave de segurança não é valido! Por favor informe uma chave valida. ', 'error')", true);
                        securityToken.Text = "";
                    }

                }
                else if (id_servidores_dgcstl.Text != "" && nome_servidortl.Text != "" && codigo_lojatl.Text != "" && situacaotl.Text != "" && nome_dbtl.Text != ""
                    && usuario_bancotl.Text != "" && parametro_ativotl.Text != "" && cnpjtl.Text != "" && securityTokentl.Text != "" && parametro_ciclico2.Text != "")
                {

                    //senhaDescriptografada = cripSenha.DescriptografarSenhaBanco(securityTokentl.Text);
                    senhaDescriptografada = securityTokentl.Text;

                    string cnpjDGCS = "";
                    string cnpjLoja = "";
                    string loja = "";
                    string senha = "";

                    if (senhaDescriptografada != "")
                    {
                        cnpjDGCS = senhaDescriptografada.Substring(0, 14);
                        cnpjLoja = senhaDescriptografada.Substring(14, 14);
                        loja = senhaDescriptografada.Substring(28, 6);
                        senha = senhaDescriptografada.Substring(34, 12);
                    }

                    SecurityToken validaSecurityToken = new SecurityToken(senha);

                    if (validaSecurityToken.validaChaveToken() && cnpjDGCS == "41179975000106" && cnpjLoja == cnpjtl.Text && loja == codigo_lojatl.Text)
                    {

                        servidor.id_servidores_dgcs = id_servidores_dgcstl.Text;
                        servidor.nome_servidor = nome_servidortl.Text;
                        servidor.codigo_loja = codigo_lojatl.Text;
                        if (situacaotl.Text == "Ativo")
                        {
                            servidor.situacao = "1";
                        }
                        else if (situacaotl.Text == "Inativo")
                        {
                            servidor.situacao = "0";
                        }
                        servidor.ultima_edicao = usuarioLoginCookie.Value;
                        servidor.nome_db = nome_dbtl.Text;
                        servidor.nome_db_app = nome_dbtl_app.Text;
                        servidor.usuario_banco = usuario_bancotl.Text;
                        servidor.senha_banco = cripSenha.CriptografarSenhaBanco(senha_bancotl.Text);
                        servidor.parametro_ativo = parametro_ativotl.Text;
                        servidor.regiao = regiaotl.Text;
                        servidor.estado = estadotl.Text;
                        servidor.cnpj = cnpjtl.Text;
                        servidor.token = cripSenha.CriptografarSenhaBanco(securityTokentl.Text);
                        servidor.parametro_ciclico = parametro_ciclico2.Text;
                        try
                        {
                            servidorDao.Atualizar(servidor);
                            servidorDao.ServidorDaoInicializar(servidor.nome_servidor,
                 servidor.nome_db_app, servidor.usuario_banco, senha_bancotl.Text);
                            servidorDao.AtualizarNaLoja(servidor);
                            carregaServidores();

                            id_servidores_dgcstl.Text = "";
                            nome_servidortl.Text = "";
                            codigo_lojatl.Text = "";
                            situacaotl.Text = "";
                            nome_dbtl.Text = "";
                            usuario_bancotl.Text = "";
                            senha_bancotl.Text = "";
                            parametro_ativo.Text = "";
                            nome_dbtl_app.Text = "";
                            regiaotl.Text = "";
                            estadotl.Text = "";
                            cnpjtl.Text = "";
                            securityTokentl.Text = "";
                            parametro_ciclico1.Text = "";
                            parametro_ciclico2.Text = "";
                            nome_servidor.Text = "";
                            codigo_loja.Text = "";
                            situacao.Text = "";
                            nome_db.Text = "";
                            usuario_banco.Text = "";
                            senha_banco.Text = "";
                            parametro_ativotl.Text = "";
                            nome_db_app.Text = "";
                            regiao.Text = "";
                            estado.Text = "";
                            cnpj.Text = "";
                            securityToken.Text = "";
                            carregaServidores();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                        }
                        catch (Exception erro)
                        {
                            id_servidores_dgcstl.Text = "";
                            nome_servidortl.Text = "";
                            codigo_lojatl.Text = "";
                            situacaotl.Text = "";
                            nome_dbtl.Text = "";
                            usuario_bancotl.Text = "";
                            senha_bancotl.Text = "";
                            parametro_ativo.Text = "";
                            nome_dbtl_app.Text = "";
                            regiaotl.Text = "";
                            estadotl.Text = "";
                            cnpjtl.Text = "";
                            securityTokentl.Text = "";
                            parametro_ciclico1.Text = "";
                            parametro_ciclico2.Text = "";
                            nome_servidor.Text = "";
                            codigo_loja.Text = "";
                            situacao.Text = "";
                            nome_db.Text = "";
                            usuario_banco.Text = "";
                            senha_banco.Text = "";
                            parametro_ativotl.Text = "";
                            nome_db_app.Text = "";
                            regiao.Text = "";
                            estado.Text = "";
                            cnpj.Text = "";
                            securityToken.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar salvar cadastro!', 'error')", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Chave de segurança não é valido! Por favor informe uma chave valida. ', 'error')", true);
                        securityTokentl.Text = "";
                    }
                }
                else
                {

                    id_servidores_dgcstl.Text = "";
                    nome_servidortl.Text = "";
                    codigo_lojatl.Text = "";
                    situacaotl.Text = "";
                    nome_dbtl.Text = "";
                    usuario_bancotl.Text = "";
                    senha_bancotl.Text = "";
                    parametro_ativo.Text = "";
                    securityToken.Text = "";
                    securityTokentl.Text = "";
                    parametro_ciclico1.Text = "";
                    parametro_ciclico2.Text = "";
                    nome_dbtl_app.Text = "";
                    regiaotl.Text = "";
                    estadotl.Text = "";
                    cnpjtl.Text = "";
                    nome_servidor.Text = "";
                    codigo_loja.Text = "";
                    situacao.Text = "";
                    nome_db.Text = "";
                    usuario_banco.Text = "";
                    senha_banco.Text = "";
                    parametro_ativotl.Text = "";
                    nome_db_app.Text = "";
                    regiao.Text = "";
                    estado.Text = "";
                    cnpj.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Todos os campos devem estar preenchidos!', 'error')", true);
                }


                DisplayInfo();
            }
        }

        protected void carregaServidores()
        {

            Servidor servidor = new Servidor();
            ServidorDao servidorDao = new ServidorDao();


            List<Servidor> list = servidorDao.BuscaCompletaRetornoLista();

            GrServidores.DataSource = list;
            GrServidores.DataBind();
        }


        protected void carregaParametrosCiclico()
        {

            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT '' NOME_PARAMETRO  UNION SELECT 'Não Permitir' NOME_PARAMETRO UNION " +
                    " SELECT NOME_PARAMETRO FROM PARAMETROS_DGCS WHERE STATUS = 1 AND NOME_PARAMETRO IN ('Inventario Ciclico')", con))

                {

                    parametro_ciclico1.DataTextField = "NOME_PARAMETRO";
                    parametro_ciclico1.DataValueField = "NOME_PARAMETRO";

                    parametro_ciclico1.DataSource = cmd.ExecuteReader();
                    parametro_ciclico1.DataBind();

                    con.Close();
                    con.Open();

                    parametro_ciclico2.DataTextField = "NOME_PARAMETRO";
                    parametro_ciclico2.DataValueField = "NOME_PARAMETRO";

                    parametro_ciclico2.DataSource = cmd.ExecuteReader();
                    parametro_ciclico2.DataBind();

                    con.Close();

                    con.Open();

                    parametro_ciclico.DataTextField = "NOME_PARAMETRO";
                    parametro_ciclico.DataValueField = "NOME_PARAMETRO";

                    parametro_ciclico.DataSource = cmd.ExecuteReader();
                    parametro_ciclico.DataBind();

                    con.Close();
                }


            }
        }
        protected void carregaParametros()
        {

            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT '' NOME_PARAMETRO  UNION" +
                    " SELECT NOME_PARAMETRO FROM PARAMETROS_DGCS WHERE STATUS = 1 AND NOME_PARAMETRO IN ('Entrada bipada','Entrada cega','Estoque Externo')", con))

                {

                    parametro_ativo.DataTextField = "NOME_PARAMETRO";
                    parametro_ativo.DataValueField = "NOME_PARAMETRO";

                    parametro_ativo.DataSource = cmd.ExecuteReader();
                    parametro_ativo.DataBind();

                    con.Close();
                    con.Open();

                    parametro_ativotl.DataTextField = "NOME_PARAMETRO";
                    parametro_ativotl.DataValueField = "NOME_PARAMETRO";

                    parametro_ativotl.DataSource = cmd.ExecuteReader();
                    parametro_ativotl.DataBind();

                    con.Close();

                    con.Open();

                    mudar_parametro.DataTextField = "NOME_PARAMETRO";
                    mudar_parametro.DataValueField = "NOME_PARAMETRO";

                    mudar_parametro.DataSource = cmd.ExecuteReader();
                    mudar_parametro.DataBind();

                    con.Close();
                }


            }
        }

        protected void carregaRegioes()
        {

            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT '' REGIAO  UNION" +
                    " SELECT DISTINCT REGIAO FROM REGIOES", con))

                {

                    regiao.DataTextField = "REGIAO";
                    regiao.DataValueField = "REGIAO";

                    regiao.DataSource = cmd.ExecuteReader();
                    regiao.DataBind();

                    con.Close();
                    con.Open();

                    regiaotl.DataTextField = "REGIAO";
                    regiaotl.DataValueField = "REGIAO";

                    regiaotl.DataSource = cmd.ExecuteReader();
                    regiaotl.DataBind();

                    con.Close();

                    con.Open();

                    regiaoMuda.DataTextField = "REGIAO";
                    regiaoMuda.DataValueField = "REGIAO";

                    regiaoMuda.DataSource = cmd.ExecuteReader();
                    regiaoMuda.DataBind();

                    con.Close();
                }


            }
        }


        protected void LinkButtonMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Menu.aspx", false);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {

            Servidor servidor = new Servidor();
            ServidorDao servidorDao = new ServidorDao();


            string codigo_loja = txtbusca.Text;
            string nome_Servidor = txtbusca.Text;

            List<Servidor> list = servidorDao.BuscaRetornoPalavrasChaves(codigo_loja, nome_Servidor);

            if (list.Count > 0)
            {
                GrServidores.DataSource = list;
                GrServidores.DataBind();
                txtbusca.Text = "";
            }
            else
            {
                txtbusca.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Nenhum cadastro encontrado!', 'error')", true);

            }


        }

        protected void regiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT '' ESTADO  UNION" +
                    " SELECT DISTINCT ESTADO FROM REGIOES where REGIAO ='" + regiao.Text + "' ", con))

                {

                    estado.DataTextField = "ESTADO";
                    estado.DataValueField = "ESTADO";

                    estado.DataSource = cmd.ExecuteReader();
                    estado.DataBind();


                }

                con.Close();
            }
        }



        protected void regiaotl_SelectedIndexChanged1(object sender, EventArgs e)
        {

            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT '' ESTADO  UNION" +
                    " SELECT DISTINCT ESTADO FROM REGIOES where REGIAO ='" + regiaotl.Text + "' ", con))

                {

                    estadotl.DataTextField = "ESTADO";
                    estadotl.DataValueField = "ESTADO";

                    estadotl.DataSource = cmd.ExecuteReader();
                    estadotl.DataBind();


                }

                con.Close();
            }
        }

        protected void regiaoMuda_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DatabaseCrud"].ConnectionString))

            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT ' Todos' ESTADO  UNION" +
                    " SELECT DISTINCT ESTADO FROM REGIOES where REGIAO ='" + regiaoMuda.Text + "' ", con))

                {

                    estadoMuda.DataTextField = "ESTADO";
                    estadoMuda.DataValueField = "ESTADO";

                    estadoMuda.DataSource = cmd.ExecuteReader();
                    estadoMuda.DataBind();


                }

                con.Close();
            }
        }

        protected void LinkButtonMudaParametro_Click(object sender, EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                try
                {
                    CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
                    ServidorDao servidorDao = new ServidorDao();
                    List<Servidor> servidores = new List<Servidor>();

                    if (mudar_parametro.Text != "" && regiaoMuda.Text != "")
                    {
                        servidorDao.AtualizarPorRegiao(mudar_parametro.Text, regiaoMuda.Text, estadoMuda.Text, parametro_ciclico.Text);

                        //busca servidores por região
                        servidores = servidorDao.BuscaServidores(regiaoMuda.Text, estadoMuda.Text);

                        //atualiza servidores na loja
                        foreach (Servidor servidor in servidores)
                        {
                            string senhaBanco = cripSenha.DescriptografarSenhaBanco(servidor.senha_banco);
                            servidorDao.ServidorDaoInicializar(servidor.nome_servidor,
                            servidor.nome_db_app, servidor.usuario_banco, senhaBanco);

                            servidor.parametro_ativo = mudar_parametro.Text;
                            servidor.parametro_ciclico = parametro_ciclico.Text;

                            servidorDao.AtualizarNaLojaParametros(servidor);
                        }
                   
                        carregaServidores();
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Atualizado com sucesso!', 'success')", true);
                        mudar_parametro.Text = "";
                        regiaoMuda.Text = "";
                        parametro_ciclico.Text = "";
                        estadoMuda.SelectedIndex = 0;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Necessario selecionar todos os campos.', 'error')", true);

                    }
                }
                catch (Exception erro)
                {
                  

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro na atualização. ', 'error')", true);

                }
                DisplayInfo();
            }

        }

        protected void gerarUsuarios_Click(object sender, EventArgs e)
        {
            Usuario usuario1 = new Usuario();
            Usuario usuario2 = new Usuario();
            UsuarioDao usuarioDao = new UsuarioDao();
            CriptoSenha cripSenha = new CriptoSenha(SHA512.Create());
            List<Usuario> usuarios1 = new List<Usuario>();
            List<Usuario> usuarios2 = new List<Usuario>();
            usuarios1 = usuarioDao.BuscaRetornoUsuariosPadrao(codigo_lojatl.Text, "Úsuario Padrão Operações");
            usuarios2 = usuarioDao.BuscaRetornoUsuariosPadrao(codigo_lojatl.Text, "Úsuario Padrão TD");

            if (usuarios1.Count > 0 && usuarios2.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(function () {");
                sb.Append("$('#EditarServidor').modal('show');");
                sb.Append(" });");
                sb.Append("</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Servidor já possui usuarios padrão.', 'error')", true);
            }
            else
            {
                usuario1.situacao = "1";
                usuario1.nome_completo = "Úsuario Padrão Operações";
                usuario1.cpf = "";
                usuario1.email = "";
                usuario1.perfil_acesso = "Operações Loja";
                usuario1.login_usuario = "operacao" + codigo_lojatl.Text;
                usuario1.senha = cripSenha.CriptografarSenhaBanco("N@tur@" + codigo_lojatl.Text);
                usuario1.codigo_loja = codigo_lojatl.Text;


                usuario2.situacao = "1";
                usuario2.nome_completo = "Úsuario Padrão TD";
                usuario2.cpf = "";
                usuario2.email = "";
                usuario2.perfil_acesso = "TD Loja";
                usuario2.login_usuario = "td" + codigo_lojatl.Text;
                usuario2.senha = cripSenha.CriptografarSenhaBanco("N@tur@" + codigo_lojatl.Text);
                usuario2.codigo_loja = codigo_lojatl.Text;

                try
                {
                    usuarioDao.Salvar(usuario1);
                    usuarioDao.Salvar(usuario2);

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$(function () {");
                    sb.Append("$('#EditarServidor').modal('show');");
                    sb.Append(" });");
                    sb.Append("</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Úsuarios padrão gerados!', 'success')", true);

                }
                catch (Exception erro)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar gerar úsuarios.', 'error')", true);
                }
            }


        }

        protected void LinkButtonExclusao_Click(object sender, EventArgs e)
        {
            Servidor servidor = new Servidor();
            ServidorDao servidorDao = new ServidorDao();

            bool loja = false, retaguarda = false;


            try
            {
                //  deletando servidor na retaguarda
                servidorDao.RemoverPorId(nome_servidortl.Text);
                retaguarda = true;

                //  deletando servidor na loja
                servidorDao.RemoverServidorLoja(nome_servidortl.Text, nome_servidortl.Text, nome_dbtl_app.Text, usuario_bancotl.Text, senha_bancotl.Text);
                loja = true;


                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Parabéns!', 'Servidor excluido!', 'success')", true);
            }
            catch (Exception erro)
            {
                if (retaguarda == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar remover o servidor na base da retaguarda.', 'error')", true);

                }
                else if (retaguarda == true && loja == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar remover o servidor na base da loja.', 'error')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", " swal('Atenção!', 'Erro ao tentar remover.', 'error')", true);

                }

            }
            carregaServidores();

            id_servidores_dgcstl.Text = "";
            nome_servidortl.Text = "";
            codigo_lojatl.Text = "";
            situacaotl.Text = "";
            nome_dbtl.Text = "";
            usuario_bancotl.Text = "";
            senha_bancotl.Text = "";
            parametro_ativo.Text = "";
            nome_dbtl_app.Text = "";
            regiaotl.Text = "";
            estadotl.Text = "";
            cnpjtl.Text = "";
            securityTokentl.Text = "";
            parametro_ciclico2.Text = "";

            nome_servidor.Text = "";
            codigo_loja.Text = "";
            situacao.Text = "";
            nome_db.Text = "";
            usuario_banco.Text = "";
            senha_banco.Text = "";
            parametro_ativotl.Text = "";
            nome_db_app.Text = "";
            regiao.Text = "";
            estado.Text = "";
            cnpj.Text = "";
            securityToken.Text = "";
            parametro_ciclico1.Text = "";
        }
    }
}