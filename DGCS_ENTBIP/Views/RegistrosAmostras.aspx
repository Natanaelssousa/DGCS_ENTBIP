<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrosAmostras.aspx.cs" Inherits="DGCS_ENTBIP.Views.RegistrosAmostras" %>

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

        //function ocultarBotao() {
        //    // Obtém uma referência ao botão
        //    var botao0 = document.getElementById("LinkButtonConfirmarFinalizacao");
        //    var botao1 = document.getElementById("LinkButtonConfirmar");
        //    var botao2 = document.getElementById("LinkButtonEntSucesso");




        //    // Oculta o botão
        //    botao0.style.display = "none";
        //    botao1.style.display = "none";
        //    botao2.style.display = "none";

        //}

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


        function validarCPF(cpf) {
            cpf = cpf.replace(/[^\d]/g, ''); // Remove caracteres não numéricos

            if (cpf.length !== 11 || !Array.from(cpf).every(c => c === cpf[0])) {
                // Verifica se o CPF não é uma sequência de números iguais
                var cpfArray = cpf.split('').map(Number);
                var soma = cpfArray.slice(0, 9).reduce((acc, val, i) => acc + val * (10 - i), 0);
                var resto = (soma * 10) % 11;

                if (resto === 10 || resto === 11) resto = 0;
                if (resto !== cpfArray[9]) return false;

                soma = cpfArray.slice(0, 10).reduce((acc, val, i) => acc + val * (11 - i), 0);
                resto = (soma * 10) % 11;

                if (resto === 10 || resto === 11) resto = 0;
                if (resto !== cpfArray[10]) return false;

                return true;
            }

            return false;
        }

        function validarInput(input) {
            var valor = input.value;
            var valido = validarCPF(valor);


            if (valido) {
                input.readOnly = true;
                input.style.borderColor = "green";
                document.getElementById("cod_barra").focus();
            } else {
                input.readOnly = false;
                swal('Atenção...', 'CPF inavlido!', 'error');
                input.value = "";
            }
        }

        function TrocarCliente() {

            document.querySelector("[name='cpf']").readOnly = false;
            document.querySelector("[name='cpf']").value = "";
            document.querySelector("[name='cpf']").textContent = "";
            document.querySelector("[name='cpf']").style.borderColor = "lightgray";
            document.getElementById("cpf").focus();
        }

    </script>


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
            <h4 class="alert-heading" style="color: dimgrey">Registros de Amostras</h4>
            <p style="color: dimgrey">Atenção! Bipe o cpf e o codigo dos produtos, para registrar as amostras.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-5"></div>


            <div class="col-3">

                <asp:Label ID="labelQtdeTotal" name="labelQtdeTotal" runat="server" Text="Qtde Total: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="qtdeTotal" name="qtdeTotal" runat="server" Text="0" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>

                <asp:Label ID="labelQtdeBipado" name="labelQtdeBipado" runat="server" Text="Qtde itens: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="qtdeTotalBipado" name="qtdeTotalBipado" runat="server" Text="0" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>


            </div>

            <div class="col-3"></div>

        </div>

        <div class="col-sm-12 text-center mt-2">
            <asp:Label runat="server" ID="mensagem" name="mensagem" Style="font-size: 14px; padding: 8px; margin: 0px; font-weight: bold" Text="" Visible="false" class="alert alert-success" role="alert">
            </asp:Label>
        </div>

        <div class="row">

            <div class="col-3"></div>

            <div class="col-6">

                <div class="d-inline">
                    <asp:Label ID="cpfLabel" name="cpfLabel" runat="server" Text="CPF: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                    <asp:TextBox ID="cpf" runat="server" type="number" class="form-control" Style="font-size: 14px;" name="cpf" placeholder="Bipe ou digite o CPF do cliente sem pontos ou traços..." required="" onchange="validarInput(this)" data-toggle="tooltip" data-placement="top" title="Informe apenas o nº do CPF do cliente sem pontos ou traços"></asp:TextBox>

                    <asp:Label ID="cod_barraLabel" name="cod_barraLabel" runat="server" Text="Código de Barras: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                    <asp:TextBox ID="cod_barra" runat="server" type="text" class="form-control" Style="font-size: 14px;" name="cod_barra" AutoPostBack="true" placeholder="Bipe o codigo do produto..." required="" OnTextChanged="cod_barra_TextChanged" data-toggle="tooltip" data-placement="top" title="Bipar os codigos dos produtos"></asp:TextBox>

                </div>

                <div class="row d-flex justify-content-center">
                    <asp:LinkButton ID="LinkButtonFinalizar" runat="server" class="btn btn-success mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonFinalizar" OnClick="LinkButtonFinalizar_Click" data-toggle="tooltip" data-placement="top" title="Quando bipar todos os itens das notas selecionadas, clique neste botão para fianlizar o processo.">Finalizar</asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonRefazer" runat="server" class="btn btn-danger mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonRefazer" OnClick="LinkButtonRefazer_Click" data-toggle="tooltip" data-placement="top" title="Reiniciar toda a bipagem das notas selecionadas.">Refazer</asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonTrocarCliente" runat="server" class="btn btn-secondary mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonTrocarCliente" OnClick="LinkButtonPausar_Click" data-toggle="tooltip" data-placement="top" title="Reiniciar toda a bipagem das notas selecionadas.">Adicionar Cliente</asp:LinkButton>

                </div>
            </div>

            <div class="col-3"></div>

        </div>

        <div class="principal d-flex justify-content-center ml-5 mt-2">

            <div>
            </div>



            <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="780px" Height="280px">
                <asp:GridView ID="Gramostras" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                    DataKeyNames="id_registros,data_cadastro,cpf_cliente,produto,responsavel,finalizado,cod_filial"
                    OnRowDataBound="Gramostras_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                    <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                    <Columns>
                           <asp:BoundField DataField="cod_filial" HeaderText="Cod Filial">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cpf_cliente" HeaderText="Cpf">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="produto" HeaderText="Produto">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="responsavel" HeaderText="Responsavel">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="qtde" HeaderText="Qtde">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="5%" />
                        </asp:BoundField>

                    </Columns>
                </asp:GridView>
            </asp:Panel>


            <div></div>


        </div>




    </form>
</body>
</html>
