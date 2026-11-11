<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfisAcesso.aspx.cs" Inherits="DGCS_ENTBIP.Views.PerfisAcesso" %>

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
    <form runat="server" class="pure-form">
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
            <h4 class="alert-heading" style="color: dimgrey">Perfis de Acesso</h4>
            <p style="color: dimgrey">Atenção! preencha todos os campos para que o perfil cadastrado tenha total permissão nas funções designadas.</p>
            <hr>
            <p class="mb-0"></p>
        </div>


        <div class="row">

            <div class="col-10">

                <div class="d-flex">
                    <!-- Button trigger modal -->
                    <div class="input-group col-2">
                        <button type="button" class="btn btn-secondary ml-5" data-toggle="modal" data-target="#CriarPerfil">Novo Perfil</button>

                    </div>


                    <div class="input-group col-10 mr-5">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo nome do Perfil..." aria-label="Numero..." aria-describedby="button-addon2"></asp:TextBox>
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
                    <div class="principal d-flex justify-content-center ml-5 mr-5">

                        <div></div>



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="1500px" Height="350px">
                            <asp:GridView ID="GrPerfis" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_perfil,perfil,tela_notas_pendentes,tela_cadastro_servidor,tela_cadastro_usuario,tela_parametros
                                ,tela_relatorio,tela_emails,tela_perfis_acesso,tela_relatorio_abastecimento,tela_relatorio_log,tela_pedidos_consumiveis,
                                tela_inventario_ciclico,tela_parametro_ftp,tela_registro_amostra,tela_escala"
                                OnRowDataBound="GrPerfis_RowDataBound">
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
                                    <asp:BoundField DataField="perfil" HeaderText="Perfil">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_notas_pendentes" HeaderText="TL Notas Pendentes">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_cadastro_servidor" HeaderText="TL Servidor">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_cadastro_usuario" HeaderText="TL Usuario">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_parametros" HeaderText="TL Parametros">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_relatorio" HeaderText="TL Relatorio">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_emails" HeaderText="TL E-mails">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_perfis_acesso" HeaderText="TL Perfis">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_relatorio_abastecimento" HeaderText="TL Rel abastecimento">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_relatorio_log" HeaderText="TL Rel Logs">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_pedidos_consumiveis" HeaderText="TL Pedidos Consumiveis">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_inventario_ciclico" HeaderText="TL Inventario Ciclico">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_parametro_ftp" HeaderText="TL Parametro FTP">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_registro_amostra" HeaderText="TL Registro Amostra">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tela_escala" HeaderText="TL Escala">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
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



        <!-- Modal  Perfil Novo-->

        <div class="modal fade bd-example-modal-lg" id="CriarPerfil" tabindex="-1" aria-labelledby="CriarPerfilLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CriarPerfilLabel">Novo Perfil</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-4">
                                <label for="perfil">Perfil</label>
                                <asp:TextBox ID="perfil" runat="server" type="text" name="perfil" class="form-control" placeholder="Digite o perfil..." required=""></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <label for="tela_notas_pendentes">Tela Notas Pendentes</label>
                                <asp:DropDownList ID="tela_notas_pendentes" runat="server" name="tela_notas_pendentes" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_escala">Tela Escala</label>
                                <asp:DropDownList ID="tela_escala" runat="server" name="tela_escala" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_cadastro_servidor">Tela Cadastro Servidor</label>
                                <asp:DropDownList ID="tela_cadastro_servidor" runat="server" name="tela_cadastro_servidor" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>



                            <div class="col-md-4">
                                <label for="tela_cadastro_usuario">Tela Cadastro Usuario</label>
                                <asp:DropDownList ID="tela_cadastro_usuario" runat="server" name="tela_cadastro_usuario" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_parametros">Tela Parametros</label>
                                <asp:DropDownList ID="tela_parametros" runat="server" name="tela_parametros" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_relatorio">Tela Relatorio</label>
                                <asp:DropDownList ID="tela_relatorio" runat="server" name="tela_relatorio" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_emails">Tela E-mails</label>
                                <asp:DropDownList ID="tela_emails" runat="server" name="tela_emails" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_perfis_acesso">Tela Perfis Acesso</label>
                                <asp:DropDownList ID="tela_perfis_acesso" runat="server" name="tela_perfis_acesso" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_relatorio_abastecimento">Tela Relatorio Abastecimento</label>
                                <asp:DropDownList ID="tela_relatorio_abastecimento" runat="server" name="tela_relatorio_abastecimento" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_relatorio_logs">Tela Relatorio Logs</label>
                                <asp:DropDownList ID="tela_relatorio_logs" runat="server" name="tela_relatorio_logs" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_entrada_pedido_consumiveis">Tela Entrada Pedido de consumiveis</label>
                                <asp:DropDownList ID="tela_entrada_pedido_consumiveis" runat="server" name="tela_entrada_pedido_consumiveis" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">


                            <div class="col-md-4">
                                <label for="tela_inventario_ciclico">Tela Inventario Ciclico</label>
                                <asp:DropDownList ID="tela_inventario_ciclico" runat="server" name="tela_inventario_ciclico" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_inventario_ciclico">Tela Parametro FTP</label>
                                <asp:DropDownList ID="tela_parametro_ftp" runat="server" name="tela_parametro_ftp" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_registro_amostra">Tela Amostra Registro</label>
                                <asp:DropDownList ID="tela_registro_amostra" runat="server" name="tela_registro_amostra" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:Label runat="server" ID="mansagemErro1" name="mansagemErro1" Text="" Visible="false" class="alert alert-danger mr-5"></asp:Label>

                        <asp:LinkButton ID="LinkCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonSalvar" runat="server" class="btn btn-primary" OnClick="LinkButtonConfirmar_Click">Confirmar</asp:LinkButton>


                    </div>
                </div>
            </div>
        </div>

        <!-- Modal  Usuario editar-->

        <div class="modal fade bd-example-modal-lg" id="EditarPerfil" tabindex="-1" aria-labelledby="EditarPefilLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarPerfilLabel">Editar Perfil</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-row d-flex justify-content-center">
                            <asp:TextBox ID="id_perfil_dgcstl" runat="server" type="hidden" name="id_perfil_dgcstl" class="form-control" value=""> </asp:TextBox>

                            <div class="col-md-4">
                                <label for="perfiltl">Perfil</label>
                                <asp:TextBox ID="perfiltl" runat="server" type="text" name="perfiltl" class="form-control" placeholder="Digite o perfil..." required=""></asp:TextBox>

                            </div>
                            <div class="col-md-4">
                                <label for="tela_notas_pendentestl">Tela Notas Pendentes</label>
                                <asp:DropDownList ID="tela_notas_pendentestl" runat="server" name="tela_notas_pendentestl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="tela_escalatl">Tela Escala</label>
                                <asp:DropDownList ID="tela_escalatl" runat="server" name="tela_escalatl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_cadastro_servidortl">Tela Cadastro Servidor</label>
                                <asp:DropDownList ID="tela_cadastro_servidortl" runat="server" name="tela_cadastro_servidortl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>



                            <div class="col-md-4">
                                <label for="tela_cadastro_usuariotl">Tela Cadastro Usuario</label>
                                <asp:DropDownList ID="tela_cadastro_usuariotl" runat="server" name="tela_cadastro_usuariotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_parametrostl">Tela Parametros</label>
                                <asp:DropDownList ID="tela_parametrostl" runat="server" name="tela_parametrostl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_relatoriotl">Tela Relatorio</label>
                                <asp:DropDownList ID="tela_relatoriotl" runat="server" name="tela_relatoriotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_emailstl">Tela E-mails</label>
                                <asp:DropDownList ID="tela_emailstl" runat="server" name="tela_emailstl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_perfis_acessotl">Tela Perfis Acesso</label>
                                <asp:DropDownList ID="tela_perfis_acessotl" runat="server" name="tela_perfis_acessotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-4">
                                <label for="tela_relatorio_abastecimentotl">Tela Relatorio Abastecimento</label>
                                <asp:DropDownList ID="tela_relatorio_abastecimentotl" runat="server" name="tela_relatorio_abastecimentotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_relatorio_logstl">Tela Relatorio Logs</label>
                                <asp:DropDownList ID="tela_relatorio_logstl" runat="server" name="tela_relatorio_logstl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_inventario_ciclico">Tela Parametro FTP</label>
                                <asp:DropDownList ID="tela_parametro_ftptl" runat="server" name="tela_parametro_ftptl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>


                        </div>



                        <div class="form-row d-flex justify-content-center">



                            <div class="col-md-4">
                                <label for="tela_entrada_pedido_consumiveistl">Tela Entrada Pedido de consumiveis</label>
                                <asp:DropDownList ID="tela_entrada_pedido_consumiveistl" runat="server" name="tela_entrada_pedido_consumiveistl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_inventario_ciclicotl">Tela Inventario Ciclico</label>
                                <asp:DropDownList ID="tela_inventario_ciclicotl" runat="server" name="tela_inventario_ciclicotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">
                                <label for="tela_registro_amostratl">Tela Amostra Registro</label>
                                <asp:DropDownList ID="tela_registro_amostratl" runat="server" name="tela_registro_amostratl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Consulta"></asp:ListItem>
                                    <asp:ListItem Text="Total"></asp:ListItem>
                                    <asp:ListItem Text="Sem acesso"></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                        </div>



                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:Label runat="server" ID="mensagemErro" name="mansagemErro" Text="" Visible="false" class="alert alert-danger mr-5"></asp:Label>

                        <asp:LinkButton ID="LinkButtonCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>

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




        function EditarPerfil(id_perfil, perfil, tela_notas_pendentes, tela_cadastro_servidor, tela_cadastro_usuario,
            tela_parametros, tela_relatorio, tela_emails, tela_perfis_acesso, tela_relatorio_abastecimento, tela_relatorio_logs,
            tela_entrada_pedido_consumiveis, tela_inventario_ciclico, tela_parametro_ftp, tela_registro_amostra, tela_escala) {



            document.querySelector("[name='id_perfil_dgcstl']").value = id_perfil;
            document.querySelector("[name='perfiltl']").value = perfil;
            document.querySelector("[name='tela_notas_pendentestl']").value = tela_notas_pendentes;
            document.querySelector("[name='tela_cadastro_servidortl']").value = tela_cadastro_servidor;
            document.querySelector("[name='tela_cadastro_usuariotl']").value = tela_cadastro_usuario;
            document.querySelector("[name='tela_parametrostl']").value = tela_parametros;
            document.querySelector("[name='tela_relatoriotl']").value = tela_relatorio;
            document.querySelector("[name='tela_emailstl']").value = tela_emails;
            document.querySelector("[name='tela_perfis_acessotl']").value = tela_perfis_acesso;
            document.querySelector("[name='tela_inventario_ciclicotl']").value = tela_inventario_ciclico;

            document.querySelector("[name='tela_relatorio_abastecimentotl']").value = tela_relatorio_abastecimento;
            document.querySelector("[name='tela_relatorio_logstl']").value = tela_relatorio_logs;
            document.querySelector("[name='tela_entrada_pedido_consumiveistl']").value = tela_entrada_pedido_consumiveis;
            document.querySelector("[name='tela_parametro_ftptl']").value = tela_parametro_ftp;
            document.querySelector("[name='tela_registro_amostratl']").value = tela_registro_amostra;
            document.querySelector("[name='tela_escalatl']").value = tela_escala;


            $('#EditarPerfil').modal('show');
        }

        function modalCancelar() {


            //editar
            document.querySelector("[name='id_perfil_dgcstl']").value = "";
            document.querySelector("[name='perfiltl']").value = "";
            document.querySelector("[name='tela_notas_pendentestl']").value = "";
            document.querySelector("[name='tela_cadastro_servidortl']").value = "";
            document.querySelector("[name='tela_cadastro_usuariotl']").value = "";
            document.querySelector("[name='tela_parametrostl']").value = "";
            document.querySelector("[name='tela_relatoriotl']").value = "";
            document.querySelector("[name='tela_emailstl']").value = "";
            document.querySelector("[name='tela_perfis_acessotl']").value = "";
            document.querySelector("[name='tela_relatorio_abastecimentotl']").value = "";
            document.querySelector("[name='tela_relatorio_logstl']").value = "";
            document.querySelector("[name='tela_entrada_pedido_consumiveistl']").value = "";
            document.querySelector("[name='tela_inventario_ciclicotl']").value = "";
            document.querySelector("[name='tela_parametro_ftptl']").value = "";
            document.querySelector("[name='tela_registro_amostratl']").value = "";
            document.querySelector("[name='tela_escalatl']").value = "";
            //criar

            document.querySelector("[name='id_perfil_dgcstl']").value = "";
            document.querySelector("[name='perfil']").value = "";
            document.querySelector("[name='tela_notas_pendentes']").value = "";
            document.querySelector("[name='tela_cadastro_servidor']").value = "";
            document.querySelector("[name='tela_cadastro_usuario']").value = "";
            document.querySelector("[name='tela_parametros']").value = "";
            document.querySelector("[name='tela_relatorio']").value = "";
            document.querySelector("[name='tela_emails']").value = "";
            document.querySelector("[name='tela_perfis_acesso']").value = "";
            document.querySelector("[name='tela_relatorio_abastecimento']").value = "";
            document.querySelector("[name='tela_relatorio_logs']").value = "";
            document.querySelector("[name='tela_entrada_pedido_consumiveis']").value = "";
            document.querySelector("[name='tela_inventario_ciclico']").value = "";
            document.querySelector("[name='tela_parametro_ftp']").value = "";
            document.querySelector("[name='tela_registro_amostra']").value = "";
            document.querySelector("[name='tela_escala']").value = "";
        }






    </script>





</body>


</html>
