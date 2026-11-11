<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParametroFTP.aspx.cs" Inherits="DGCS_ENTBIP.Views.ParametroFTP" %>

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
            <h4 class="alert-heading" style="color: dimgrey">Parametro FTP</h4>
            <p style="color: dimgrey">Atenção! preencha todos os campos para que o FTP cadastrado consiga realizar a conexão com sucesso.</p>
            <hr>
            <p class="mb-0"></p>
        </div>



        <div class="card w-75 ml-5 mt-3">
            <h5 class="card-header">Editar FTP</h5>
            <div class="card-body">
                <div class="form-row">
                    <div class="col-md-6 mb-3">

                        <asp:TextBox ID="id" runat="server" type="hidden" name="id" class="form-control" value=""> </asp:TextBox>

                        <label for="validationTooltip01" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Servidor</label>
                         <asp:TextBox ID="servidor" runat="server" type="text" name="servidor" class="form-control" placeholder="Exemplo: ftp.servidor.com.br" required=""></asp:TextBox>
                    </div>

                               <div class="col-md-6 mb-3">

          

                        <label for="validationTooltip01" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Pasta</label>
                        <asp:TextBox ID="pasta" runat="server" type="text" name="pasta" class="form-control"  placeholder="Exemplo: ENTBIP"> </asp:TextBox>

                    </div>
                </div>


                <div class="form-row">
                       <div class="col-md-4 mb-3">


                        <label for="validationTooltip01" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Usuario</label>
                        <asp:TextBox ID="usuarioFTP" runat="server" type="text" name="usuarioFTP" class="form-control" placeholder="Digite um login"> </asp:TextBox>

                    </div>

                               <div class="col-md-4 mb-3">

          

                        <label for="validationTooltip01" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Senha</label>
                        <asp:TextBox ID="senha" runat="server" type="password" name="senha" class="form-control" placeholder="Digite uma senha"> </asp:TextBox>

                    </div>

                         <div class="col-md-4 mb-3">

                                <label for="status" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Status</label>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="status" runat="server" AutoPostBack="True" name="status" CssClass="form-control" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;" data-toggle="tooltip" data-placement="top">
                                             <asp:ListItem Text=""></asp:ListItem>
                                            <asp:ListItem Text="0"></asp:ListItem>
                                            <asp:ListItem Text="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                </div>


              

            </div>
        </div>

        <div class="row">
            <div class="col-4"></div>
            <div class="col-4">
                <asp:LinkButton ID="LinkButtonAtualizar" runat="server" class="btn btn-primary ml-5 mt-3" name="LinkButtonAtualizar" OnClientClick="carregamento();" OnClick="LinkButtonAtualizar_Click" data-toggle="tooltip" data-placement="top" title="Clique para Atualizar">Atualizar</asp:LinkButton>
            </div>
            <div class="col-4"></div>

        </div>



    </form>


    <script type="text/javascript">

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }




        function carregamento() {


            if (!($(this).attr("target"))) {
                ToggleSpinnerBlock(true);
                return true;
            }
        }


    </script>





</body>


</html>
