<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="DGCS_ENTBIP.Views.Menu" %>

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
                    <asp:LinkButton ID="LinkButtonSair" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; color: aliceblue; text-decoration: none" runat="server" OnClick="LinkButtonSair_Click">Sair</asp:LinkButton>
                </div>
            </nav>
        </div>

        <div class="alert alert-secondary" role="alert">
            <h4 class="alert-heading" style="color: dimgrey">Menu</h4>
            <p style="color: dimgrey">Seja bem vindo ao Portal! As opções abaixo estão disponiveis para os processos de recebimento de mercadorias.</p>
            <hr>
            <p class="mb-0"></p>
        </div>


        <div class="card">
            <div class="card-body">

                <div class="row">

                    <div class="col-sm-6 mb-3" id="1">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Módulo Entrada Para Revenda</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade exibir um menu com os processos de entrada para revenda.</p>
                                <asp:Button ID="acessarNotasPendentes" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarNotasPendentes_Click" />
                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="mensagem6" name="mensagem6" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="2">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Cadastro de Servidores</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade realizar o cadastro de servidores e lojas utilizados no portal.</p>
                                <asp:Button ID="acessarCadastroServidores" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarCadastroServidores_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label1" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:TextBox ID="perfil_acesso" runat="server" type="hidden" name="perfil_acesso" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_notas_pendentes" runat="server" type="hidden" name="tela_notas_pendentes" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_cadastro_servidor" runat="server" type="hidden" name="tela_cadastro_servidor" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_cadastro_usuario" runat="server" type="hidden" name="tela_cadastro_usuario" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_parametros" runat="server" type="hidden" name="tela_parametros" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_relatorio" runat="server" type="hidden" name="tela_relatorio" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_emails" runat="server" type="hidden" name="tela_emails" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_perfis_acesso" runat="server" type="hidden" name="tela_perfis_acesso" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_relatorio_abastecimento" runat="server" type="hidden" name="tela_relatorio_abastecimento" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_relatorio_log" runat="server" type="hidden" name="tela_relatorio_log" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_pedido_consumiveis" runat="server" type="hidden" name="tela_pedido_consumiveis" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_inventario_ciclico" runat="server" type="hidden" name="tela_inventario_ciclico" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_parametro_ftp" runat="server" type="hidden" name="tela_parametro_ftp" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_registro_amostra" runat="server" type="hidden" name="tela_registro_amostra" class="form-control" value=""> </asp:TextBox>
                    <asp:TextBox ID="tela_escala" runat="server" type="hidden" name="tela_escala" class="form-control" value=""> </asp:TextBox>





                    <div class="col-sm-6 mb-3" id="3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Cadastro de Usúarios</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade realizar o cadastro dos usuarios e suas permissões no portal.</p>
                                <asp:Button ID="acessarCadastroUsuarios" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarCadastroUsuarios_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="mensagem" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="4">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Parametros</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade realizar alterações em parametros do portal.</p>
                                <asp:Button ID="acessarParametros" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarParametros_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label2" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="5">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Relatorio</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite consultar notas que foram realizado entradas e seus status.</p>
                                <asp:Button ID="acessarRelatorio" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="acessarRelatorio_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label3" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="6">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Emails</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela é para cadastro de e-mails responsaveis por sobra e perda de mercadoria.</p>
                                <asp:Button ID="ButtonEmails" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonEmails_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label4" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6 mb-3" id="7">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Perfil de Acesso</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela é para cadastro e Edição de perfis de acesso.</p>
                                <asp:Button ID="ButtonPerfis" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonPerfis_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label5" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6 mb-3" id="8">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Relatorio Abastecimento</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite consultar notas que foram realizado entradas com sobra.</p>
                                <asp:Button ID="ButtonRelAbastecimento" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonRelAbastecimento_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label6" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6 mb-3" id="9">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Relatorio Logs</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite consultar os logs das consultas da tela de relatorio de reabastecimento.</p>
                                <asp:Button ID="relLog" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="relLog_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label7" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="10">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Módulo Entrada de Consumível</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite realizar a entrada de pedido de consumiveis.</p>
                                <asp:Button ID="ButtonPedConsumiveis" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonPedConsumiveis_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label8" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6 mb-3" id="11">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Módulo Inventário</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite realizar o inventário cíclico de produtos.</p>
                                <asp:Button ID="ButtonInventarioCiclico" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonInventarioCiclico_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label9" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="12">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Parametro FTP</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela permite realizar a edição do ftp para envio de arquivos.</p>
                                <asp:Button ID="Buttonftp" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="Buttonftp_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label10" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="13">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Módulo Registro Amostra</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade permitir o registro de produtos e clientes.</p>
                                <asp:Button ID="ButtonRegistroAmostra" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonRegistroAmostra_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label11" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 mb-3" id="14">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title" style="font-size: 15px;">Módulo Escala</h5>
                                <p class="card-text" style="font-size: 12px;">Está tela tem como finalidade permitir o acesso a escalas.</p>
                                <asp:Button ID="ButtonEscala" runat="server" Text="Acessar" CssClass="btn btn-secondary" OnClick="ButtonEscala_Click" />

                                <div class="col-sm-12 text-center">
                                    <asp:Label runat="server" ID="Label12" name="mensagem" Style="font-size: 10px; padding: 5px; margin: 0px;" Text="" Visible="false" class="alert alert-success" role="alert">
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


        var tela_cadastro_servidor = document.querySelector("[name='tela_cadastro_servidor']").value;
        var tela_cadastro_usuario = document.querySelector("[name='tela_cadastro_usuario']").value;
        var tela_emails = document.querySelector("[name='tela_emails']").value;
        var tela_notas_pendentes = document.querySelector("[name='tela_notas_pendentes']").value;
        var tela_parametros = document.querySelector("[name='tela_parametros']").value;
        var tela_perfis_acesso = document.querySelector("[name='tela_perfis_acesso']").value;
        var tela_relatorio = document.querySelector("[name='tela_relatorio']").value;
        var tela_relatorio_abastecimento = document.querySelector("[name='tela_relatorio_abastecimento']").value;
        var tela_relatorio_log = document.querySelector("[name='tela_relatorio_log']").value;
        var tela_pedido_consumiveis = document.querySelector("[name='tela_pedido_consumiveis']").value;
        var tela_inventario_ciclico = document.querySelector("[name='tela_inventario_ciclico']").value;
        var tela_parametro_ftp = document.querySelector("[name='tela_parametro_ftp']").value;
        var tela_registro_amostra = document.querySelector("[name='tela_registro_amostra']").value;
        var tela_escala = document.querySelector("[name='tela_escala']").value;


        if (tela_notas_pendentes == "Sem acesso") {
            $("#1").detach();

        }
        if (tela_cadastro_servidor == "Sem acesso") {
            $("#2").detach();

        }
        if (tela_cadastro_usuario == "Sem acesso") {
            $("#3").detach();

        }
        if (tela_emails == "Sem acesso") {
            $("#6").detach();

        }
        if (tela_parametros == "Sem acesso") {
            $("#4").detach();

        }
        if (tela_perfis_acesso == "Sem acesso") {
            $("#7").detach();

        }
        //if (tela_relatorio == "Sem acesso") {
        $("#5").detach();

        //}
        if (tela_relatorio_abastecimento == "Sem acesso") {
            $("#8").detach();

        }

        if (tela_relatorio_log == "Sem acesso") {
            $("#9").detach();

        }

        if (tela_pedido_consumiveis == "Sem acesso") {
            $("#10").detach();

        }

        if (tela_inventario_ciclico == "Sem acesso") {
            $("#11").detach();

        }

        if (tela_parametro_ftp == "Sem acesso") {
            $("#12").detach();

        }

        if (tela_registro_amostra == "Sem acesso") {
            $("#13").detach();

        }

        if (tela_escala == "Sem acesso") {
            $("#14").detach();

        }

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }


    </script>

</body>

</html>




