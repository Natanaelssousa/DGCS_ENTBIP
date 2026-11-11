<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parametros.aspx.cs" Inherits="DGCS_ENTBIP.Views.Parametros" %>

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
    <link rel="icon" href="../logo_natura2.ico" sizes="32x32"/>
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

           <nav class="navbar navbar-dark" style="background-color:#ed6227; height: 70px;">
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
            <h4 class="alert-heading" style="color:dimgrey">Parametros</h4>
            <p style="color:dimgrey">Atenção! Tela com  a finalidade de alterar parametros que inpactam diretamente na usabilidade do sistema.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-8">

                <div class="d-flex">
                    <!-- Button trigger modal -->
                    <div class="input-group col-3" id="novo_parametro">
                        <button type="button"  class="btn btn-secondary ml-5" data-toggle="modal" data-target="#CriarParametro">Novo Parametro</button>
                           <asp:TextBox ID="perfil_acesso" runat="server" type="hidden" name="perfil_acesso" class="form-control" value=""> </asp:TextBox>

                    </div>


                    <div class="input-group col-9" id="campBusca">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo nome do parametro..." aria-label="Nome..." aria-describedby="button-addon2"></asp:TextBox>
                        <asp:TextBox ID="validaConsulta" runat="server" type="hidden" name="validaConsulta" class="form-control" value=""> </asp:TextBox>
                        <div class="input-group-append" id="divlnkbusca">
                            <asp:LinkButton ID="lnkBuscar" name="lnkBuscar" runat="server" Visible="true" type="button" class="btn btn-danger" style="background-color:#ed6227;" OnClick="lnkBuscar_Click">Buscar</asp:LinkButton>
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



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="930px" Height="350px">
                            <asp:GridView ID="GrParametros" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_parametro_dgcs,nome_parametro,descricao,status,responsavel_alteracao,data_alteracao,data_criacao,data_inicio,data_fim"
                                OnRowDataBound="GrParametros_RowDataBound">
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
                                    <asp:BoundField DataField="nome_parametro" HeaderText="Parametro">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                                    </asp:BoundField>
                                         <asp:BoundField DataField="valor" HeaderText="Valor">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="data_inicio" HeaderText="Data Inicio">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="data_fim" HeaderText="Data Fim">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="status" HeaderText="Status">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="responsavel_alteracao" HeaderText="Ultima Alteração">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>


                        <div></div>
                    </div>


                </div>
            </div>

            <div class="col-4">
            </div>


        </div>



        <!-- Modal  Parametro-->

        <div class="modal fade bd-example-modal-lg" id="CriarParametro" tabindex="-1" aria-labelledby="CriarParametroLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CriarParametroLabel">Novo Parametro</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-8">
                                <label for="nome_parametro">Parametro</label>
                                <asp:TextBox ID="nome_parametro" runat="server" type="text" name="nome_parametro" class="form-control" placeholder="Digite o nome do parametro..." required=""></asp:TextBox>

                            </div>

                             <div class="col-md-4">
                                <label for="valor">Valor</label>
                                <asp:TextBox ID="valor" runat="server" type="text" name="valor" class="form-control" placeholder="Digite o valor..." required=""></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-5 mb-2">
                                <label for="data_inicio">Data Inicio</label>
                                <asp:TextBox ID="data_inicio" runat="server" type="date" name="data_inicio" class="form-control" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-5 mb-2">
                                <label for="data_fim">Data Fim</label>
                                <asp:TextBox ID="data_fim" runat="server" type="date" name="data_fim" class="form-control" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label for="status">Status</label>
                                <asp:DropDownList ID="status" runat="server" name="status" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row d-flex justify-content-center mt-4">

                            <div class="col-md-12">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Descrição</span>
                                    </div>
                                    <asp:TextBox class="form-control" type="text"
                                        ID="descricao" name="descricao" runat="server" Height="100" Width="100" MaxLength="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                </div>
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

        <!-- Modal  Parametro editar-->
        
        <div class="modal fade bd-example-modal-lg" id="EditarParametro" tabindex="-1" aria-labelledby="EditarParametroLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarParametroLabel">Editar Parametro</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-8">
                                <asp:TextBox ID="id_parametro_dgcstl" runat="server" type="hidden" name="id_parametro_dgcstl" class="form-control" value=""> </asp:TextBox>
                                <label for="nome_parametrotl">Parametro</label>
                                <asp:TextBox ID="nome_parametrotl" runat="server" type="text" name="nome_parametrotl" class="form-control" placeholder="Digite o nome do parametro..." required=""></asp:TextBox>

                            </div>
                              <div class="col-md-4">
                                <label for="valortl">Valor</label>
                                <asp:TextBox ID="valortl" runat="server" type="text" name="valortl" class="form-control" placeholder="Digite o valor..." required=""></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-5 mb-2">
                                <label for="data_iniciotl">Data Inicio</label>
                                <asp:TextBox ID="data_iniciotl" runat="server" type="date" name="data_iniciotl" class="form-control" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-5 mb-2">
                                <label for="validationTooltip05">Data Fim</label>
                                <asp:TextBox ID="data_fimtl" runat="server" type="date" name="data_fimtl" class="form-control" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label for="statustl">Status</label>
                                <asp:DropDownList ID="statustl" runat="server" name="statustl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center mt-4">

                            <div class="col-md-12">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Descrição</span>
                                    </div>
                                    <asp:TextBox class="form-control" type="text"
                                        ID="descricaotl" name="descricaotl" runat="server" Height="100" Width="100" MaxLength="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                </div>
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





        function EditarParametro(id_parametro_dgcs, nome_parametro, descricao, status, data_inicio, data_fim,valortl) {


            

            if (data_inicio != '')
                document.querySelector("[name='data_iniciotl']").value = data_inicio;

            if (data_fim != '')
                document.querySelector("[name='data_fimtl']").value = data_fim;


            document.querySelector("[name='id_parametro_dgcstl']").value = id_parametro_dgcs;
            document.querySelector("[name='nome_parametrotl']").value = nome_parametro;
            document.querySelector("[name='descricaotl']").value = descricao;
            document.querySelector("[name='statustl']").value = status;
            document.querySelector("[name='valortl']").value = valortl;

            $('#EditarParametro').modal('show');
        }

        function modalCancelar() {


            //editar
            document.querySelector("[name='id_parametro_dgcstl']").value = "";
            document.querySelector("[name='nome_parametrotl']").value = "";
            document.querySelector("[name='descricaotl']").value = "";
            document.querySelector("[name='statustl']").value = "";
            document.querySelector("[name='data_iniciotl']").value = "";
            document.querySelector("[name='data_fimtl']").value = "";
             document.querySelector("[name='valortl']").value = "";
            //criar
            document.querySelector("[name='id_parametro_dgcstl']").value = "";
            document.querySelector("[name='nome_parametro']").value = "";
            document.querySelector("[name='descricao']").value = "";
            document.querySelector("[name='status']").value = "";
            document.querySelector("[name='data_inicio']").value = "";
            document.querySelector("[name='data_fim']").value = "";
              document.querySelector("[name='valor']").value = "";
            $('#CriarParametro').modal('hide');
            $('#CriarParametro').modal = undefined;


        }


        function removeLabelParametro() {
            // Removendo um nó a partir do pai
            var node = document.getElementById("");

            node.parentNode.removeChild(node);
        }

            var acesso = document.querySelector("[name='perfil_acesso']").value;
           if (acesso != "Master") {
               $("#novo_parametro").detach();
               $("#campBusca").removeClass();
                $("#campBusca").addClass("input-group col-11 ml-5 ");
        }

                function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }

    </script>





</body>


</html>
