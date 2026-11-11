<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="DGCS_ENTBIP.Views.CadastroUsuario" %>

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
            <h4 class="alert-heading" style="color: dimgrey">Cadastro de Usúarios</h4>
            <p style="color: dimgrey">Atenção! preencha todos os campos para que o usuario cadastrado tenha total permissão nas funções designadas.</p>
            <hr>
            <p class="mb-0"></p>
        </div>


        <div class="row">

            <div class="col-8">

                <div class="d-flex">
                    <!-- Button trigger modal -->
                    <div class="input-group col-3">
                        <button type="button" class="btn btn-secondary ml-5" data-toggle="modal" data-target="#CriarUsuario">Novo Usúario</button>

                    </div>


                    <div class="input-group col-9">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo nome do usuario ou codigo loja..." aria-label="Numero..." aria-describedby="button-addon2"></asp:TextBox>
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



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="930px" Height="350px">
                            <asp:GridView ID="GrUsuarios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="id_usuario_dgcs,nome_completo,cpf,email,perfil_acesso,login_usuario,senha,situacao,data_criacao,codigo_loja"
                                OnRowDataBound="GrUsuarios_RowDataBound">
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
                                    <asp:BoundField DataField="nome_completo" HeaderText="Nome">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigo_loja" HeaderText="Codigo Loja">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="perfil_acesso" HeaderText="Perfil de Acesso">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="situacao" HeaderText="Situação">
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



        <!-- Modal  Usuario Novo-->

        <div class="modal fade bd-example-modal-lg" id="CriarUsuario" tabindex="-1" aria-labelledby="CriarUsuarioLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CriarUsuarioLabel">Novo Usuario</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-10">
                                <label for="nome_completo">Nome Completo</label>
                                <asp:TextBox ID="nome_completo" runat="server" type="text" name="nome_completo" class="form-control" placeholder="Digite o nome completo..." required=""></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                                <label for="situacao">Situação</label>
                                <asp:DropDownList ID="situacao" runat="server" name="situacao" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-5">
                                <label for="cpf">CPF</label>
                                <asp:TextBox ID="cpf" runat="server" type="number" name="cpf" class="form-control" placeholder="Digite o CPF" onchange="ValidaCpf();" MaxLength="11"></asp:TextBox>
                            </div>



                            <div class="col-md-7">
                                <label for="email">E-mail</label>
                                <asp:TextBox ID="email" runat="server" type="email" name="email" class="form-control" placeholder="Digite o E-mail" required=""></asp:TextBox>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-3">
                                <label for="perfil_acesso">Perfil de Acesso</label>
                                <asp:DropDownList ID="perfil_acesso" runat="server" name="perfil_acesso" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>


                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label for="codigo_loja">Codigo Loja</label>
                                        <asp:DropDownList ID="codigo_loja" runat="server" name="codigo_loja" CssClass="form-control">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-5">
                                <label for="login_usuario">Login</label>
                                <asp:TextBox ID="login_usuario" runat="server" type="text" name="login_usuario" class="form-control" placeholder="Digite um login" required=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="senha">Senha</label>
                                <asp:TextBox ID="senha" runat="server" type="password" name="senha" class="form-control" placeholder="Digite uma senha" required="" aria-describedby="passwordHelpBlock1" onchange="ValidaSenha()"></asp:TextBox>
                                <small id="passwordHelpBlock1" class="form-text text-muted">Sua senha deve ter entre 8 e 20 caracteres, os quais devem ser letras e números e caracteres especiais, não deve ter espaços ou emojis.
                                </small>
                            </div>

                            <div class="col-md-6">
                                <label for="senhaConfirma">Confirmação de Senha</label>
                                <asp:TextBox ID="senhaConfirma" runat="server" type="password" name="senhaConfirma" class="form-control" placeholder="Digite a senha novamente" required="" onKeyDown="validaSenha();" onKeyPress="validaSenha();" onKeyUp="validaSenha();"></asp:TextBox>

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

        <div class="modal fade bd-example-modal-lg" id="EditarUsuario" tabindex="-1" aria-labelledby="EditarUsuarioLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="EditarUsuarioLabel">Editar Usuario</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-10">
                                <asp:TextBox ID="id_usuario_dgcstl" runat="server" type="hidden" name="id_usuario_dgcstl" class="form-control" value=""> </asp:TextBox>

                                <label for="nome_completotl">Nome Completo</label>
                                <asp:TextBox ID="nome_completotl" runat="server" type="text" name="nome_completotl" class="form-control" placeholder="Digite o nome completo..." required=""></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                                <label for="situacaotl">Situação</label>
                                <asp:DropDownList ID="situacaotl" runat="server" name="situacaotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo"></asp:ListItem>
                                    <asp:ListItem Text="Inativo"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-5">
                                <label for="cpftl">CPF</label>
                                <asp:TextBox ID="cpftl" runat="server" type="number" name="cpftl" class="form-control" placeholder="Digite o CPF" onchange="ValidaCpftl();"></asp:TextBox>
                            </div>



                            <div class="col-md-7">
                                <label for="emailtl">E-mail</label>
                                <asp:TextBox ID="emailtl" runat="server" type="email" name="emailtl" class="form-control" placeholder="Digite o E-mail" required=""></asp:TextBox>
                            </div>

                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="perfil_acessotl">Perfil de Acesso</label>
                                <asp:DropDownList ID="perfil_acessotl" runat="server" name="perfil_acessotl" CssClass="form-control">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text=""></asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-6">
                                <label for="login_usuariotl">Login</label>
                                <asp:TextBox ID="login_usuariotl" runat="server" type="text" name="login_usuariotl" class="form-control" placeholder="Digite um login" required=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row d-flex justify-content-center">

                            <div class="col-md-6">
                                <label for="senhatl">Senha</label>
                                <asp:TextBox ID="senhatl" runat="server" type="password" name="senhatl" class="form-control" placeholder="Digite uma senha" required="" aria-describedby="passwordHelpBlock" onchange="ValidaSenhatl()"></asp:TextBox>
                                <small id="passwordHelpBlock" class="form-text text-muted">Sua senha deve ter entre 8 e 20 caracteres, os quais devem ser letras e números e caracteres especiais, não deve ter espaços ou emojis.
                                </small>
                            </div>

                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label for="codigo_lojatl">Codigo Loja</label>
                                        <asp:DropDownList ID="codigo_lojatl" runat="server" name="codigo_lojatl" CssClass="form-control" AutoPostBack="True">
                                            <asp:ListItem Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                    </div>

                    <div class="modal-footer">
                        <asp:Label runat="server" ID="mensagemErro" name="mansagemErro" Text="" Visible="false" class="alert alert-danger mr-5"></asp:Label>

                        <asp:LinkButton ID="LinkButtonCancelar" runat="server" class="btn btn-secondary" data-dismiss="modal" aria-label="Close" OnClientClick="modalCancelar();">Fechar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonEditar" runat="server" class="btn btn-primary" OnClick="LinkButtonEditar_Click">Confirmar</asp:LinkButton>

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




        function EditarUsuario(id_usuario_dgcs, nome_completo, cpf, email, perfil_acesso, login_usuario, senha, situacao, codigo_loja) {


            document.querySelector("[name='id_usuario_dgcstl']").value = id_usuario_dgcs;
            document.querySelector("[name='situacaotl']").value = situacao;
            document.querySelector("[name='nome_completotl']").value = nome_completo;
            document.querySelector("[name='cpftl']").value = cpf;
            document.querySelector("[name='codigo_lojatl']").value = codigo_loja;
            document.querySelector("[name='emailtl']").value = email;
            document.querySelector("[name='perfil_acessotl']").value = perfil_acesso;
            document.querySelector("[name='login_usuariotl']").value = login_usuario;
            document.querySelector("[name='senhatl']").value = senha;
            document.querySelector("[name='codigo_lojatl']").value = codigo_loja;

            $('#EditarUsuario').modal('show');
        }

        function modalCancelar() {


            //editar
            document.querySelector("[name='id_usuario_dgcstl']").value = "";
            document.querySelector("[name='situacaotl']").value = "";
            document.querySelector("[name='nome_completotl']").value = "";
            document.querySelector("[name='cpftl']").value = "";
            document.querySelector("[name='codigo_lojatl']").value = "";
            document.querySelector("[name='emailtl']").value = "";
            document.querySelector("[name='perfil_acessotl']").value = "";
            document.querySelector("[name='login_usuariotl']").value = "";
            document.querySelector("[name='senhatl']").value = "";
            document.querySelector("[name='codigo_lojatl']").value = "";
            //criar

            document.querySelector("[name='situacao']").value = "";
            document.querySelector("[name='nome_completo']").value = "";
            document.querySelector("[name='cpf']").value = "";
            document.querySelector("[name='codigo_loja']").value = "";
            document.querySelector("[name='email']").value = "";
            document.querySelector("[name='perfil_acesso']").value = "";
            document.querySelector("[name='login_usuario']").value = "";
            document.querySelector("[name='senha']").value = "";
            document.querySelector("[name='codigo_loja']").value = "";
            document.querySelector("[name='senhaConfirma']").value = "";

        }

        function validaSenha() {


            NovaSenha = document.getElementById('senha').value;
            CNovaSenha = document.getElementById('senhaConfirma').value;
            if (NovaSenha != CNovaSenha) {
                document.getElementById('senhaConfirma').style.borderColor = 'red'


            } else {
                document.getElementById('senhaConfirma').style.borderColor = 'green'
            }
        }


        function removeLabelChamado() {
            // Removendo um nó a partir do pai
            var node = document.getElementById("");

            node.parentNode.removeChild(node);
        }



        function cpf(cpf) {
            cpf = cpf.replace(/\D/g, '');
            if (cpf.toString().length != 11 || /^(\d)\1{10}$/.test(cpf)) return false;
            var result = true;
            [9, 10].forEach(function (j) {
                var soma = 0, r;
                cpf.split(/(?=)/).splice(0, j).forEach(function (e, i) {
                    soma += parseInt(e) * ((j + 2) - (i + 1));
                });
                r = soma % 11;
                r = (r < 2) ? 0 : 11 - r;
                if (r != cpf.substring(j, j + 1)) result = false;
            });
            return result;
        }


        function ValidaCpf() {

            let retorno = cpf(document.querySelector("[name='cpf']").value)

            if (retorno == false) {
                swal('Atenção!', 'CPF invalido!', 'error')
                document.querySelector("[name='cpf']").value = "";
            }
        }


        function ValidaCpftl() {

            let retorno = cpf(document.querySelector("[name='cpftl']").value)

            if (retorno == false) {
                swal('Atenção!', 'CPF invalido!', 'error')
                document.querySelector("[name='cpftl']").value = "";
            }
        }

        // (?=(?:.*?[A-Z]){3}) - Mínimo 3 letras maiúsculas
        // (?=(?:.*?[0-9]){2}) - Mínimo 2 números
        // (?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#;$%*(){}_+^&] - Mínimo 1 caractere especial

        function ValidaSenha() {
            var senha = document.querySelector("[name='senha']").value;

            var regex = /^(?=(?:.*?[A-Z]){1})(?=(?:.*?[0-9]){1})(?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#$%;*(){}_+^&]*$/;

            if (senha.length < 8) {
                swal('Atenção!', 'A senha deve conter no minímo 8 digitos!', 'error')
                document.querySelector("[name='senha']").value = "";
            }
            else if (!regex.exec(senha)) {
                swal('Atenção!', 'A senha deve conter no mínimo 1 caracteres em maiúsculo, 1 números e 1 caractere especial!', 'error')
                document.querySelector("[name='senha']").value = "";
            }
        }

        function ValidaSenhatl() {
            var senha = document.querySelector("[name='senhatl']").value;
            var regex = /^(?=(?:.*?[A-Z]){1})(?=(?:.*?[0-9]){1})(?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?!.*\s)[0-9a-zA-Z!@#$%;*(){}_+^&]*$/;

            if (senha.length < 8) {
                swal('Atenção!', 'A senha deve conter no minímo 8 digitos!', 'error')
                document.querySelector("[name='senhatl']").value = "";
            }
            else if (!regex.exec(senha)) {
                swal('Atenção!', 'A senha deve conter no mínimo 1 caracteres em maiúsculo, 1 números e 1 caractere especial!', 'error')
                document.querySelector("[name='senhatl']").value = "";

            }

        }

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }

    </script>





</body>


</html>
