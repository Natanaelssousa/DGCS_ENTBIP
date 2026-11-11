<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroServidores.aspx.cs" Inherits="DGCS_ENTBIP.Views.CadastroServidores" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link rel="icon" type="Views/images/png" href="/favicon-32x32.png" sizes="32x32" />--%>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../logo_natura2.ico" sizes="32x32" />
    <!-- Incluindo o CSS do Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />


    <!-- Incluindo o jQuery que é requisito do JavaScript do Bootstrap localmente-->
    <script src="js/jquery-3.0.0.min.js"></script>
    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/bootstrap.bundle.js"></script>
    <script src="js/sweetalert2.all.js"></script>

    <title>DGCS ENTBIP</title>
</head>
<body runat="server">
    <form runat="server">
        <asp:ScriptManager ID="asm" runat="server" />
        <div class="pos-f-t">

            <nav class="navbar navbar-dark" style="background-color: #ed6227; height: 70px;">
                <div class="logo">

                    <asp:LinkButton ID="LinkButtonMenu2" runat="server" OnClick="LinkButtonMenu_Click"> <img src="images/logo_natura2.jpg"" style="width: 60px; margin-right: 60px" /></asp:LinkButton>
                </div>
                <div class="col-lg-5">
                </div>

                <div class="col-lg-4 d-flex">
                    <asp:Label ID="data_acesso" name="data_acesso" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue" CssClass="mr-3"></asp:Label>
                    <asp:Label ID="usuario" name="usuario" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue" CssClass="mr-3"> </asp:Label>
                    <asp:LinkButton ID="LinkButtonMenu" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue; text-decoration: none" runat="server" OnClick="LinkButtonMenu_Click">Voltar</asp:LinkButton>
                </div>
            </nav>
        </div>

        <div class="alert alert-secondary" role="alert">
            <h4 class="alert-heading" style="color: dimgrey">Cadastro de Servidores e Lojas</h4>
            <p style="color: dimgrey">Atenção! preencha todos os campos para que não acha problemas com as conexões entre o portal e as lojas.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-10">

                <div class="d-flex">
                    <!-- Button trigger modal -->
                    <div class="input-group col-5">
                        <button type="button" class="btn btn-secondary ml-5 " data-toggle="modal" data-target="#CriarServidor">Novo Servidor</button>
                        <button type="button" class="btn btn-info ml-2" data-toggle="modal" data-target="#MudarParametro">Mudar Parametro</button>
                    </div>


                    <div class="input-group col-7">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo codigo da loja ou pelo nome do servidor..." aria-label="Numero..." aria-describedby="button-addon2"></asp:TextBox>
                        <asp:TextBox ID="validaConsulta" runat="server" type="hidden" name="validaConsulta" class="form-control" value=""> </asp:TextBox>
                        <div class="input-group-append" id="divlnkbusca">
                            <asp:LinkButton ID="lnkBuscar" name="lnkBuscar" runat="server" Visible="true" type="button" class="btn btn-danger" Style="background-color: #ed6227;" OnClick="lnkBuscar_Click">Buscar</asp:LinkButton>
                        </div>
                    </div>

                </div>

                <div>



                    <div style="margin: 3px;">
                        <asp:Label runat="server" ID="mensagem" name="mensagem" Text="" Visible="false" class="alert alert-success m-3"></asp:Label>
                        <asp:TextBox ID="btnClicado" runat="server" type="text" name="btnClicado" Style="color: aliceblue; border: none" Text=""></asp:TextBox>
                    </div>
                    <div class="principal d-flex justify-content-center ml-5">

                        <div></div>



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="1200px" Height="350px">
                            <asp:GridView ID="GrServidores" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_servidores_dgcs,situacao,nome_servidor,codigo_loja,ultima_edicao,data_criacao,usuario_banco,senha_banco,nome_db,nome_db_app,parametro_ativo,regiao,estado,parametro_ciclico"
                                OnRowDataBound="GrServidores_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtn" name="imgbtn" ImageUrl="images/edit.png" runat="server" Width="25" Height="25" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="regiao" HeaderText="Região">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nome_servidor" HeaderText="Servidor">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigo_loja" HeaderText="Codigo Loja">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="parametro_ativo" HeaderText="Parametro Entrada">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="parametro_ciclico" HeaderText="Parametro Inventario">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="situacao" HeaderText="Situação">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ultima_edicao" HeaderText="Ultima Alt">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>


                        <div></div>
                    </div>


                </div>
            </div>

            <div class="col-2">
            </div>


        </div>

        <!-- Modal  ALterar Parametro Bipagem-->

        <div class="modal fade bd-example-modal-lg" id="MudarParametro" tabindex="-1" aria-labelledby="MudarParametroLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="MudarParametroLabel">Alterar Parametro Por Região</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="form-row d-flex justify-content-center">

                                    <div class="col-md-6">
                                        <label for="regiaoMuda">Região</label>
                                        <asp:DropDownList ID="regiaoMuda" runat="server" name="regiaoMuda" CssClass="form-control" OnSelectedIndexChanged="regiaoMuda_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="estadoMuda">Estado</label>
                                        <asp:DropDownList ID="estadoMuda" runat="server" name="estadoMuda" CssClass="form-control">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="mudar_parametro">Parametro Entrada</label>
                                <asp:DropDownList ID="mudar_parametro" runat="server" name="mudar_parametro" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-6">
                                <label for="parametro_ciclico">Parametro Inventario</label>
                                <asp:DropDownList ID="parametro_ciclico" runat="server" name="parametro_ciclico" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>



                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">

                        <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonMudaParametro" runat="server" class="btn btn-primary" OnClick="LinkButtonMudaParametro_Click">Confirmar</asp:LinkButton>


                    </div>
                </div>
            </div>
        </div>

        <!-- Modal  servidor-->

        <div class="modal fade bd-example-modal-lg" id="CriarServidor" tabindex="-1" aria-labelledby="CriarServidorLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CriarServidorLabel">Novo Servidor</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="form-row d-flex justify-content-center">

                                    <div class="col-md-6">
                                        <label for="regiao">Região</label>
                                        <asp:DropDownList ID="regiao" runat="server" name="regiao" CssClass="form-control" OnSelectedIndexChanged="regiao_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="estado">Estado</label>
                                        <asp:DropDownList ID="estado" runat="server" name="estado" CssClass="form-control">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-6">
                                <label for="nome_servidor">Servidor</label>
                                <asp:TextBox ID="nome_servidor" runat="server" type="text" name="nome_servidor" class="form-control" placeholder="Digite o nome do servidor..." required=""></asp:TextBox>

                            </div>


                            <div class="col-md-6">
                                <label for="codigo_loja">Codigo Loja</label>
                                <asp:TextBox ID="codigo_loja" runat="server" type="text" name="codigo_loja" class="form-control" placeholder="Codigo da loja" required=""></asp:TextBox>
                            </div>

                        </div>


                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-6">
                                <label for="parametro_ativo">Parametro Entrada</label>
                                <asp:DropDownList ID="parametro_ativo" runat="server" name="parametro_ativo" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="parametro_ciclico1">Parametro Inventario</label>
                                <asp:DropDownList ID="parametro_ciclico1" runat="server" name="parametro_ciclico1" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">


                            <div class="col-md-2">
                                <label for="situacao">Situação</label>
                                <asp:DropDownList ID="situacao" runat="server" name="situacao" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-5">
                                <label for="nome_db">Banco Loja</label>
                                <asp:TextBox ID="nome_db" runat="server" type="text" name="nome_db" class="form-control" placeholder="Digite o nome do banco dados loja" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <label for="nome_db">Banco Aplicação</label>
                                <asp:TextBox ID="nome_db_app" runat="server" type="text" name="nome_db_app" class="form-control" placeholder="Digite o nome do banco dados aplicação" required=""></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="usuario_banco">Usúario DB</label>
                                <asp:TextBox ID="usuario_banco" runat="server" type="text" name="usuario_banco" class="form-control" placeholder="Digite um usuario para o banco" required=""></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label for="senha_banco">Senha DB</label>
                                <asp:TextBox ID="senha_banco" runat="server" type="password" name="senha_banco" class="form-control" placeholder="Digite uma senha para o banco" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label for="cnpj">CNPJ Loja</label>
                                <asp:TextBox ID="cnpj" runat="server" type="number" name="cnpj" class="form-control" placeholder="Digite o numero do cnpj sem ponto ou traço" required=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-12">
                                <label for="securityToken">Chave de Segurança</label>
                                <asp:TextBox ID="securityToken" runat="server" type="password" name="securityToken" class="form-control" placeholder="Informe a chave de segurança" required=""></asp:TextBox>
                            </div>


                        </div>
                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonSalvar" runat="server" class="btn btn-primary" OnClick="LinkButtonConfirmar_Click">Confirmar</asp:LinkButton>


                    </div>
                </div>
            </div>
        </div>

        <!-- Modal  servidor editar-->

        <div class="modal fade bd-example-modal-lg" id="EditarServidor" tabindex="-1" aria-labelledby="EditarServidorLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarServidorLabel">Editar Servidor</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="form-row d-flex justify-content-center">
                                    <div class="col-md-6">
                                        <label for="regiaotl">Região</label>
                                        <asp:DropDownList ID="regiaotl" runat="server" name="regiaotl" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="regiaotl_SelectedIndexChanged1">
                                            <asp:ListItem Text=""></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="estadotl">Estado</label>
                                        <asp:DropDownList ID="estadotl" runat="server" name="estadotl" CssClass="form-control">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-6">
                                <asp:TextBox ID="id_servidores_dgcstl" runat="server" type="hidden" name="id_servidores_dgcstl" class="form-control" value=""> </asp:TextBox>

                                <label for="nome_servidortl">Servidor</label>
                                <asp:TextBox ID="nome_servidortl" runat="server" type="text" name="nome_servidortl" class="form-control" placeholder="Digite o nome do servidor..." required=""></asp:TextBox>

                            </div>


                            <div class="col-md-6">

                                <label for="codigo_lojatl">Codigo Loja</label>
                                <asp:TextBox ID="codigo_lojatl" runat="server" type="text" name="codigo_lojatl" class="form-control" placeholder="Codigo da loja" required=""></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-6">
                                <label for="parametro_ativotl">Parametro Entrada</label>
                                <asp:DropDownList ID="parametro_ativotl" runat="server" name="parametro_ativotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="parametro_ciclico2">Parametro Inventario</label>
                                <asp:DropDownList ID="parametro_ciclico2" runat="server" name="parametro_ciclico2" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>


                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-2">
                                <label for="situacaotl">Situação</label>
                                <asp:DropDownList ID="situacaotl" runat="server" name="situacaotl" CssClass="form-control">
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-5">
                                <label for="nome_dbtl">Banco Loja</label>
                                <asp:TextBox ID="nome_dbtl" runat="server" type="text" name="nome_dbtl" class="form-control" placeholder="Digite o nome do banco dados loja" required=""></asp:TextBox>
                            </div>

                            <div class="col-md-5">
                                <label for="nome_dbtl">Banco Aplicação</label>
                                <asp:TextBox ID="nome_dbtl_app" runat="server" type="text" name="nome_dbtl_app" class="form-control" placeholder="Digite o nome do banco dados aplicação" required=""></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="usuario_bancotl">Usúario DB</label>
                                <asp:TextBox ID="usuario_bancotl" runat="server" type="text" name="usuario_bancotl" class="form-control" placeholder="Digite um usuario para DB" required=""></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label for="senha_bancotl">Senha DB</label>
                                <asp:TextBox ID="senha_bancotl" runat="server" type="password" name="senha_bancotl" class="form-control" placeholder="Digite uma senha para DB" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label for="cnpjtl">CNPJ Loja</label>
                                <asp:TextBox ID="cnpjtl" runat="server" type="number" name="cnpjtl" class="form-control" placeholder="Digite o numero do cnpj sem ponto ou traço" required=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-12">
                                <label for="securityTokentl">Chave de Segurança</label>
                                <asp:TextBox ID="securityTokentl" runat="server" type="password" name="securityTokentl" class="form-control" placeholder="Informe a chave de segurança" required=""></asp:TextBox>
                            </div>


                        </div>
                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:Label runat="server" ID="mensagemErro" name="mansagemErro" Text="" Visible="false" class="alert alert-danger mr-5"></asp:Label>
                        <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-dark" OnClick="gerarUsuarios_Click">Gerar Usuarios Padrão</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonExclusao" runat="server" class="btn btn-danger" OnClick="LinkButtonExclusao_Click">Excluir</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonConfirmar" runat="server" class="btn btn-primary" OnClick="LinkButtonConfirmar_Click">Confirmar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>


    </form>


    <script type="text/javascript">

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }





        function EditarServidor(id_servidores_dgcs, situacao, nome_servidor, codigo_loja, nome_db, usuario_banco, senha_banco, parametro_ativo, nome_dbtl_app, regiao, estado, cnpj, token, parametro_ciclico) {




            document.querySelector("[name='id_servidores_dgcstl']").value = id_servidores_dgcs;
            document.querySelector("[name='situacaotl']").value = situacao;
            document.querySelector("[name='nome_servidortl']").value = nome_servidor;
            document.querySelector("[name='codigo_lojatl']").value = codigo_loja;
            document.querySelector("[name='codigo_lojatl']").value = codigo_loja;
            document.querySelector("[name='nome_dbtl']").value = nome_db;
            document.querySelector("[name='nome_dbtl_app']").value = nome_dbtl_app;
            document.querySelector("[name='usuario_bancotl']").value = usuario_banco;
            document.querySelector("[name='senha_bancotl']").value = senha_banco;
            document.querySelector("[name='parametro_ativotl']").value = parametro_ativo;
            document.querySelector("[name='parametro_ciclico2']").value = parametro_ciclico;
            document.querySelector("[name='regiaotl']").value = regiao;
            document.querySelector("[name='cnpjtl']").value = cnpj;
            document.querySelector("[name='securityTokentl']").value = token;
            if (document.getElementById("estadotl").value == "") {
                var comboEstadosTl = document.getElementById("estadotl");

                var opt0 = document.createElement("option");
                opt0.value = estado;
                opt0.text = estado;
                comboEstadosTl.add(opt0, comboEstadosTl.options[0]);
                comboEstadosTl.value = estado;
            }


            $('#EditarServidor').modal('show');
        }

        function modalCancelar() {


            //editar
            document.querySelector("[name='id_servidores_dgcstl']").value = "";
            document.querySelector("[name='situacaotl']").value = "";
            document.querySelector("[name='nome_servidortl']").value = "";
            document.querySelector("[name='codigo_lojatl']").value = "";
            document.querySelector("[name='usuario_bancotl']").value = "";
            document.querySelector("[name='senha_bancotl']").value = "";
            document.querySelector("[name='nome_dbtl']").value = "";
            document.querySelector("[name='nome_dbtl_app']").value = "";
            document.querySelector("[name='parametro_ativotl']").value = "";
            document.querySelector("[name='parametro_ciclico2']").value = "";
            document.querySelector("[name='regiaotl']").value = "";
            document.querySelector("[name='estadotl']").value = "";
            document.querySelector("[name='cnpjtl']").value = "";
            document.querySelector("[name='securityTokentl']").value = "";
            //criar
            document.querySelector("[name='id_servidores_dgcs']").value = "";
            document.querySelector("[name='situacao']").value = "";
            document.querySelector("[name='nome_servidor']").value = "";
            document.querySelector("[name='codigo_loja']").value = "";
            document.querySelector("[name='usuario_banco']").value = "";
            document.querySelector("[name='senha_banco']").value = "";
            document.querySelector("[name='nome_db']").value = "";
            document.querySelector("[name='nome_db_app']").value = "";
            document.querySelector("[name='parametro_ativo']").value = "";
            document.querySelector("[name='parametro_ciclico1']").value = "";
            document.querySelector("[name='regiao']").value = "";
            document.querySelector("[name='estado']").value = "";
            document.querySelector("[name='cnpj']").value = "";
            document.querySelector("[name='securityToken']").value = "";
            $('#CriarServidor').modal('hide');
            $('#CriarServidor').modal = undefined;


        }


        function removeLabelChamado() {
            // Removendo um nó a partir do pai
            var node = document.getElementById("");

            node.parentNode.removeChild(node);
        }

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }

    </script>





</body>


</html>
