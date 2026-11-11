<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emails.aspx.cs" Inherits="DGCS_ENTBIP.Views.Emails" %>

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
            <h4 class="alert-heading" style="color: dimgrey">Emails</h4>
            <p style="color: dimgrey">Atenção! clique na opção criar ou no editar para atualizar um novo e-mail, que recebera notificações de perdas e sobras.</p>
            <hr>
            <p class="mb-0"></p>
        </div>


        <div class="row">

            <div class="col-8">

                <div class="d-flex">
                    <!-- Button trigger modal -->
                    <div class="input-group col-3">
                        <button type="button" class="btn btn-secondary ml-5" data-toggle="modal" data-target="#CriarUsuario">Novo E-mail</button>

                    </div>


                    <div class="input-group col-9">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo nome ou pelo endereço de e-mail..." aria-label="Numero..." aria-describedby="button-addon2"></asp:TextBox>
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
                    <div class="principal justify-content-center ml-5">

                        <div></div>
                        <asp:Label runat="server" ID="label_emailEnvio" name="label_emailEnvio" Text="Email de Remetente:" Visible="true" class="m-1" Style="color: dimgrey"></asp:Label>
                        <asp:Panel ID="Panel2" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="930px" Height="120px">
                            <asp:GridView ID="GrEmails2" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_emails, nome, email, senha, servidor_email, host_envio"
                                OnRowDataBound="GrEmails2_RowDataBound">
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
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nome" HeaderText="Conta">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="E-mail">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="servidor_email" HeaderText="Servidor E-mail">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="host_envio" HeaderText="Host de Envio">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Label runat="server" ID="label_emails_envios" name="label_emails_envios" Text="Destinatarios:" Visible="true" class="m-1" Style="color: dimgrey"></asp:Label>
                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="930px" Height="350px">
                            <asp:GridView ID="GrEmails" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_emails,nome,email"
                                OnRowDataBound="GrEmails_RowDataBound">
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
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nome" HeaderText="Nome/Grupo">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="E-mail">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
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



        <!-- Modal  Usuario Novo-->

        <div class="modal fade bd-example-modal-lg" id="CriarUsuario" tabindex="-1" aria-labelledby="CriarUsuarioLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CriarUsuarioLabel">Novo E-mail</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-12">
                                <label for="nome_completo">Nome ou Grupo</label>
                                <asp:TextBox ID="nome" runat="server" type="text" name="nome" class="form-control" placeholder="Digite o nome pessoal ou grupo" required=""></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-12">
                                <label for="email">E-mail</label>
                                <asp:TextBox ID="email" runat="server" type="email" name="email" class="form-control" placeholder="Digite o E-mail" required=""></asp:TextBox>
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

        <!-- Modal  email editar-->

        <div class="modal fade bd-example-modal-lg" id="EditarEmail" tabindex="-1" aria-labelledby="EditarEmailLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarUsuarioLabel">Editar Email</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-12">
                                <asp:TextBox ID="id_emails" runat="server" type="hidden" name="id_emails" class="form-control" value=""> </asp:TextBox>

                                <label for="nometl">Nome ou grupo</label>
                                <asp:TextBox ID="nometl" runat="server" type="text" name="nometl" class="form-control" placeholder="Digite o nome ou grupo..." required=""></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-12">
                                <label for="emailtl">E-mail</label>
                                <asp:TextBox ID="emailtl" runat="server" type="email" name="emailtl" class="form-control" placeholder="Digite o E-mail" required=""></asp:TextBox>
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


        <!-- Modal email remetente editar-->

        <div class="modal fade bd-example-modal-lg" id="EditarEmailRemetente" tabindex="-1" aria-labelledby="EditarEmailRemetenteLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarEmailRemetenteLabel">Editar Email</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-12">
                                <asp:TextBox ID="id_emailsRm" runat="server" type="hidden" name="id_emailsRm" class="form-control" value=""> </asp:TextBox>

                                <label for="nomeRm">Nome</label>
                                <asp:TextBox ID="nomeRm" runat="server" type="text" name="nomeRm" class="form-control" placeholder="Digite o nome..." required=""></asp:TextBox>

                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-12">
                                <label for="emailRm">E-mail</label>
                                <asp:TextBox ID="emailRm" runat="server" type="email" name="emailRm" class="form-control" placeholder="Digite o E-mail" required=""></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="senhaRm">Senha</label>
                                <asp:TextBox ID="senhaRm" runat="server" type="password" name="senhaRm" class="form-control" placeholder="Digite uma senha" required="" aria-describedby="passwordHelpBlock1" onchange="ValidaSenha()"></asp:TextBox>
                                <small id="passwordHelpBlock1" class="form-text text-muted">Sua senha deve ter entre 8 e 20 caracteres, os quais devem ser letras e números e caracteres especiais, não deve ter espaços ou emojis.
                                </small>
                            </div>

                            <div class="col-md-6">
                                <label for="senhaConfirmaRm">Confirmação de Senha</label>
                                <asp:TextBox ID="senhaConfirmaRm" runat="server" type="password" name="senhaConfirmaRm" class="form-control" placeholder="Digite a senha novamente" required="" onKeyDown="validaSenha();" onKeyPress="validaSenha();" onKeyUp="validaSenha();"></asp:TextBox>

                            </div>
                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="servidorEmailRm">Servidor E-mail</label>
                                <asp:TextBox ID="servidorEmailRm" runat="server" type="text" name="servidorEmailRm" class="form-control" placeholder="Digite o Servidor de E-mail" required=""></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="host_envioRm">Host E-mail</label>
                                <asp:TextBox ID="host_envioRm" runat="server" type="text" name="host_envioRm" class="form-control" placeholder="Digite o Host de E-mail" required=""></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:Label runat="server" ID="Label1" name="mansagemErro" Text="" Visible="false" class="alert alert-danger mr-5"></asp:Label>

                        <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonConfirmarEnvio" runat="server" class="btn btn-primary" OnClick="LinkButtonConfirmarEnvio_Click">Confirmar</asp:LinkButton>

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


        function validaSenha() {


            NovaSenha = document.getElementById('senhaRm').value;
            CNovaSenha = document.getElementById('senhaConfirmaRm').value;
            if (NovaSenha != CNovaSenha) {
                document.getElementById('senhaConfirmaRm').style.borderColor = 'red'


            } else {
                document.getElementById('senhaConfirmaRm').style.borderColor = 'green'
            }
        }

        function EditarEmail(id_emails, nome, email) {


            document.querySelector("[name='id_emails']").value = id_emails;
            document.querySelector("[name='nometl']").value = nome;
            document.querySelector("[name='emailtl']").value = email;


            $('#EditarEmail').modal('show');
        }


        function EditarEmail2(id_emailsRm, nomeRm, emailRm, senhaRm, servidorEmailRm, host_envioRm) {


            document.querySelector("[name='id_emailsRm']").value = id_emailsRm;
            document.querySelector("[name='nomeRm']").value = nomeRm;
            document.querySelector("[name='emailRm']").value = emailRm;
            document.querySelector("[name='senhaRm']").value = senhaRm;
            document.querySelector("[name='senhaConfirmaRm']").value = senhaRm;
            document.querySelector("[name='servidorEmailRm']").value = servidorEmailRm;
            document.querySelector("[name='host_envioRm']").value = host_envioRm;


            $('#EditarEmailRemetente').modal('show');
        }

        function modalCancelar() {


            //editar
            document.querySelector("[name='id_emails']").value = "";
            document.querySelector("[name='nometl']").value = "";
            document.querySelector("[name='emailtl']").value = "";
            //criar
            document.querySelector("[name='nome']").value = "";
            document.querySelector("[name='email']").value = "";

        }

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }

    </script>





</body>


</html>
