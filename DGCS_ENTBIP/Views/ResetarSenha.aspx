<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetarSenha.aspx.cs" Inherits="DGCS_ENTBIP.Views.ResetarSenha" %>

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
    <link rel="icon" href="../../logo_natura2.ico" sizes="32x32"/>

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
       <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
    <link href="css/font-awesome.css" rel="stylesheet" />

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
    <title>DGCS ENTBIP</title>
</head>
<body>
  
    <div class="pos-f-t">
        <nav class="navbar navbar-dark" style="background-color:#ed6227; height: 70px;">
            <div class="logo">
                <img src="images/logo_natura2.jpg"" style="width: 60px; margin-right: 60px" />
            </div>

        </nav>
    </div>




    <div class="container py-5 ">
        <div class="row">
            <div class="col-md-12">
                <div class="alert alert-danger d-flex align-items-center" role="alert">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class="bi bi-exclamation-triangle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" aria-label="Warning:">
                        <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z  " />
                    </svg>
                    <div class="ml-2">
                        Informe seu CPF e seu e-mail para resetar a senha antiga!
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 mx-auto">

                        <!-- form card login -->
                        <div class="card rounded-0" style="margin-bottom: 2px;">

                            <div class="card-body">
                                <form id="form1" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="label" name="label" runat="server" Text="Informe seu CPF"></asp:Label>
                                        <asp:TextBox ID="nCpf" runat="server" type="text" class="form-control form-control-lg rounded-0" name="nCpf" required="" placeholder="CPF" onchange="ValidaCpf()"></asp:TextBox>
                                        <div class="invalid-feedback">Oops, usuario invalido!.</div>
                                    </div>
                                    <div class="form-group mb-4">
                                        <asp:Label ID="labelEmail" name="labelEmail" runat="server" Text="Informe seu E-mail"></asp:Label>
                                        <asp:TextBox ID="email" runat="server" type="email" class="form-control form-control-lg rounded-0" name="email" required="" placeholder="E-mail"></asp:TextBox>
                                        <div class="invalid-feedback">Insira sua senha também!</div>
                                    </div>

                                        <asp:LinkButton ID="btnResetar" runat="server"  type="submit" CssClass="btn btn-secondary  w-25 h-25 m-4" OnClick="btnResetar_Click" >Resetar</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonCancelar" runat="server" class="btn btn-danger  w-25 h-25" name="LinkButtonCancelar" OnClick="btnCancelar_Click">Voltar</asp:LinkButton>
                       <%--animação no carregamento da pagina--%>

         <div id="SpinnerContainer"></div> 
   <div id="Spinner" style="background:url(images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>
                                    

                                       <!-- Modal  Usuario reset-->

        <div class="modal fade bd-example-modal-lg" id="resetUsuario" tabindex="-1" aria-labelledby="ResetUsuarioLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ResetUsuarioLabel">Resete de Senha</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                      
                   

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
                        <asp:LinkButton ID="LinkButtonSalvar" runat="server" class="btn btn-primary" OnClick="LinkButtonSalvar_Click">Confirmar</asp:LinkButton>


                    </div>
                </div>
            </div>
        </div>


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


  

                <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
       <script>

           //código usando jQuery
           //$(document).ready(function () {
           //    $('.Spinner').hide();
           //    $('.SpinnerContainer').hide();
           //});

           $("form").submit(function (event) {

               if (!($(this).attr("target"))) {
                   ToggleSpinnerBlock(true);
                   return true;
               }
           });

           //$("a").click(function (event) {
           //    if (!($(this).attr("target"))) {

           //        ToggleSpinnerBlock(true);
           //        return true;
           //    }
           //});
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

               let retorno = cpf(document.querySelector("[name='nCpf']").value)

               if (retorno == false) {
                   swal('Atenção!', 'CPF invalido!', 'error')
                   document.querySelector("[name='nCpf']").value = "";
               }
           }


           function noBack() { window.history.forward(0) }

           noBack();

           window.onload = noBack;

           window.onpageshow = function (evt) { if (evt.persisted) noBack() }

           window.onunload = function () { void (0) }

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

           function modalCancelar() {



               //criar
               document.querySelector("[name='senha']").value = "";
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

</script>
</body>
</html>
<!-- Incluindo o jQuery que é requisito do JavaScript do Bootstrap localmente-->
<script src="js/jquery-3.0.0.min.js"></script>
<!-- Incluindo o JavaScript do Bootstrap -->
<script src="js/bootstrap.min.js"></script>

