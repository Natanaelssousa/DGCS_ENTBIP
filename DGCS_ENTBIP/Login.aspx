<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DGCS_ENTBIP.Login" %>

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
    <script src="Views/js/sweetalert2.all.js"></script>
    <link rel="icon" href="logo_natura2.ico" sizes="32x32"/>
    <!-- Incluindo o CSS do Bootstrap -->
    <link href="Views/css/bootstrap.min.css" rel="stylesheet" />

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
            $('#Spinner').html('<img src="Views/images/spinner.gif" />');
        }

        function BeforePostBack() {
            //$('#PostBackResult').text('');
            //ToggleSpinnerBlock(true);
            return true; //So that allow post back to server
        }


    </script>

    <title>DGCS ENTBIP</title>
</head>
<body class="p-0 m-0">

    <div class="pos-f-t">

        <nav class="navbar navbar-dark" style="background-color: #ed6227; height: 70px;">
            

                 <asp:Label ID="label1" name="label" runat="server" Text="Versão 1.9"  style="color:white;" ></asp:Label>
            
        </nav>
    </div>

    <div id="SpinnerContainer"></div>

    <div id="Spinner" style="background: url(Views/images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>

        
    <div class="container">
             
        <div class="row">

      
                  

            <div class="col-md-12">
                <h2 class="text-center text-black-50" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;">
                    <img src="Views/images/logo_natura.jpg" style="width: 180px;" /></h2>
                <div class="row">
                    <div class="col-md-6 mx-auto">
                          
                        <!-- form card login -->
                        <div class="card rounded-0" style="margin-bottom: 2px;">
                            <div class="card-header" style="background-color: #ed6227">
                                <h3 class="mb-0" style="color: white">Acesso</h3>
                            </div>
                            <div class="card-body">
                                <form id="form1" runat="server">

                                
                                    <div class="form-group">
                                        <asp:Label ID="label" name="label" runat="server" Text="Usúario" CssClass="m-1"></asp:Label>
                                        <asp:TextBox ID="usuario" runat="server" type="text" class="form-control form-control-lg rounded-0" name="usuario" required="" placeholder="Login"></asp:TextBox>
                                        <div class="invalid-feedback">Oops, usuario invalido!.</div>
                                    </div>
                                    <div class="form-group mb-4">
                                        <asp:Label ID="lanel2" name="label2" runat="server" Text="Senha" CssClass="m-1"></asp:Label>
                                        <asp:TextBox ID="senha" runat="server" type="password" class="form-control form-control-lg rounded-0" name="senha" placeholder="Senha" required=""></asp:TextBox>
                                        <div class="invalid-feedback">Insira sua senha também!</div>
                                    </div>

                                    <div class="d-flex">

                                        <asp:Button ID="btnLogin" runat="server" Text="Entrar" type="submit" CssClass="btn btn-secondary float-right w-50 h-50" OnClick="btnLogin_Click" />
                                        <asp:LinkButton ID="esqueceuSenha" runat="server" class="alert-link ml-5 mb-3 " OnClick="esqueceuSenha_Click" Style="color: #ed6227">Esqueceu sua senha?</asp:LinkButton>

                                    </div>
                                    <%--animação no carregamento da pagina--%>
                                </form>
                            </div>
                            <!--/card-block-->
                        </div>
                        <!-- /form card login -->

                    </div>


                </div>
                <!--/row-->

            </div>
            <!--/col-->
        </div>
        <!--/row-->
    </div>

       <div class=""> 

                    <h2 class="text-center text-black-100" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;">
                  <img src="Views/images/logo_dgcs.jpg" style="width: 150px;" /></h2> 
             </div>


    <!-- Incluindo o jQuery que é requisito do JavaScript do Bootstrap localmente-->
    <script src="Views/js/jquery-3.0.0.min.js"></script>
    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="Views/js/bootstrap.min.js"></script>

    <script src="Views/js/popper.min.js"></script>
    <script src="Views/js/jquery-3.3.1.min.js"></script>
    <script>

                function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }

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

        //$("button").click(function (event) {
        //    if (!($(this).attr("target"))) {

        //        ToggleSpinnerBlock(true);
        //        return true;
        //    }
        //});



    </script>

    <!-- Incluindo o jQuery que é requisito do JavaScript do Bootstrap localmente-->
    <script src="Views/js/jquery-3.0.0.min.js"></script>
    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="Views/js/bootstrap.min.js"></script>

</body>
</html>
