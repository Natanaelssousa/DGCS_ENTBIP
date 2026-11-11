<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuloRegistrosAmostras.aspx.cs" Inherits="DGCS_ENTBIP.Views.ModuloRegistrosAmostras" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link rel="icon" type="Views/images/png" href="/favicon-32x32.png" sizes="32x32" />--%>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../logo_natura2.ico" sizes="32x32" />
    <!-- Incluindo o CSS do Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <script src="js/sweetalert2.all.js"></script>
    <title>DGCS ENTBIP</title>

    <style>
        div#SpinnerContainer {
            position: absolute;
            display: none;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: #fff;
            opacity: 0.4;
            filter: alpha(opacity=40); /* For IE8 and earlier */
            z-index: 1000; /* Important to set this */
        }


        div#Spinner {
            position: absolute;
            display: none;
            width: 25%;
            height: 15%;
            top: 38%;
            left: 38%;
            z-index: 1001;
            overflow: auto;
        }
    </style>

    <script>

        function ToggleSpinnerBlock(Visible) {
            var displayValue = Visible ? "block" : "none";
            document.getElementById("SpinnerContainer").style.display = displayValue;
            document.getElementById("Spinner").style.display = displayValue;

            var isIE = navigator.userAgent.indexOf('MSIE') > 0;
            if (Visible && isIE) {
                $('#Spinner').css("backgroundImage", "");
                setTimeout("AppendSpinnerImageForIE();", 10);
            }
        }

        function AppendSpinnerImageForIE() {
            $('#Spinner').html('<img src="images/spinner.gif" />');
        }

        function BeforePostBack() {
            //$('#PostBackResult').text('');
            //ToggleSpinnerBlock(true);
            return true; //So that allow post back to server
        }


    </script>
</head>
<body runat="server">
    <form runat="server">

        <div class="pos-f-t">

            <nav class="navbar navbar-dark" style="background-color: #ed6227; height: 70px;">
                <div class="logo">

                    <asp:LinkButton ID="LinkButtonMenu" runat="server" OnClick="LinkButtonMenu_Click"> <img src="images/logo_natura2.jpg"" style="width: 60px; margin-right: 60px" /></asp:LinkButton>
                </div>
                <div class="col-lg-5">
                </div>

                <div class="col-lg-4 d-flex">
                    <asp:Label ID="data_acesso" name="data_acesso" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue" CssClass="mr-3"></asp:Label>
                    <asp:Label ID="usuario" name="usuario" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue" CssClass="mr-3"> </asp:Label>
                    <asp:LinkButton ID="LinkButtonSair" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue; text-decoration: none" runat="server" OnClick="LinkButtonMenu_Click">Voltar</asp:LinkButton>
                </div>
            </nav>
        </div>

        <div class="alert alert-secondary" role="alert">
            <h4 class="alert-heading" style="color: dimgrey">Módulo Registros de Amostras</h4>
            <p style="color: dimgrey">Módulo de registros de amostras para clientes.</p>
            <hr>
            <p class="mb-0"></p>
        </div>


        <div class="card">
            <div class="card-body">

                <div class="row">


                    <asp:TextBox ID="perfil_acesso" runat="server" type="hidden" name="perfil_acesso" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_registro_amostra" runat="server" type="hidden" name="tela_registro_amostra" class="form-control" value=""> </asp:TextBox>


                    <div class="col-sm-6 mb-3" id="1">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Registro de Amostras</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade permitir o registro de produtos e clientes.</p>
                                <asp:Button ID="ButtonRegistroAmostra" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonRegistroAmostra_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label9" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="2">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Relatorio</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite consultar regitros de amostras de clientes.</p>
                                <asp:Button ID="acessarRelatorio" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarRelatorio_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label3" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <%--animação no carregamento da pagina--%>

        <div id="SpinnerContainer"></div>

        <div id="Spinner" style="background: url(images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>


    </form>
    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script>

        //código usando jQuery
        $(document).ready(function () {
            $('.Spinner').hide();
            $('.SpinnerContainer').hide();
        });

        $("form").submit(function (event) {

            if (!($(this).attr("target"))) {
                ToggleSpinnerBlock(true);
                return true;
            }
        });

        $("a").click(function (event) {
            if (!($(this).attr("target"))) {

                ToggleSpinnerBlock(true);
                return true;
            }
        });



        var tela_registro_amostra = document.querySelector("[name='tela_registro_amostra']").value;



        if (tela_registro_amostra == "Sem acesso") {
            $("#1").detach();

        }


        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }


    </script>

</body>

</html>

