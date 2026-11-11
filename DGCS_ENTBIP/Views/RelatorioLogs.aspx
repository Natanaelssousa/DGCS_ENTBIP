<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioLogs.aspx.cs" Inherits="DGCS_ENTBIP.Views.RelatorioLogs" %>

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
    <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>

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
            <h4 class="alert-heading" style="color: dimgrey">Relatorio de Logs</h4>
            <p style="color: dimgrey">
                Tela para visualização de logs de consulta no relatorio de Abastecimento 
                <d class="text-monospace">DGCS_ENTBIP.</d>
                <hr>
                <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-10">

                <div class="d-flex">

                    <div class="col-2 ml-5">

                        <label for="periodo_inicio">Data Inicio</label>
                        <asp:TextBox ID="periodo_inicio" runat="server" type="date" name="periodo_inicio" class="form-control"></asp:TextBox>

                    </div>
                    <div class=" col-2">

                        <label for="periodo_fim">Data Fim</label>
                        <asp:TextBox ID="periodo_fim" runat="server" type="date" name="periodo_fim" class="form-control"></asp:TextBox>

                    </div>
                    <div class="col-2 ">
                        <label for="usuario_log">Usuario</label>
                        <asp:TextBox ID="usuario_log" runat="server" type="text" name="usuario_log" class="form-control" placeholder="Informe o usuario..."></asp:TextBox>
                    </div>
                    <div class="col-4 ">
                        <label for="cnpj">CNPJ</label>
                        <asp:TextBox ID="cnpj" runat="server" type="number" name="cnpj" class="form-control" placeholder="Informe o CNPJ da loja sem pontuação"></asp:TextBox>
                    </div>
                    <div class="col-3 mt-3">
                        <asp:LinkButton ID="buscarNota" runat="server" class="btn btn-success btn-lg"
                            name="buscarNota" OnClick="lnkBuscar_Click" OnClientClick="minhaFuncao()">Pesquisar</asp:LinkButton>


                        <asp:LinkButton ID="LinkButtonExportar" name="LinkButtonExportar" runat="server" class="btn btn rounded-circle ml-1" Style="width: 75px; height: 72px; background-color: white; color: aliceblue; border-color: darkgreen;" data-toggle="tooltip" data-placement="top" title="Exportar notas fiscais" OnClick="LinkButtonExportar_Click">
        <img src="images/exportar_Excel.jpg" height ="50" width="50" class="mr-2 mt-1"/>
                        </asp:LinkButton>

                    </div>
                </div>

                <div>



                    <div style="margin: 3px;">
                        <asp:Label runat="server" ID="mensagem" name="mensagem" Text="" Visible="false" class="alert alert-success m-3"></asp:Label>
                        <asp:TextBox ID="btnClicado" runat="server" type="text" name="btnClicado" Style="color: aliceblue; border: none" Text=""></asp:TextBox>
                    </div>
                    <div class="principal d-flex justify-content-center ml-5">

                        <div>
                        </div>



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="1200px" Height="350px">
                            <asp:GridView ID="GrLogs" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="usuario,cnpj,periodo_inicio,periodo_fim,nota_fiscal,data_consulta"
                                OnRowDataBound="GrLogs_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundField DataField="data_consulta" HeaderText="Data Consulta">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="usuario" HeaderText="Usúario">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cnpj" HeaderText="Cnpj">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="periodo_inicio" HeaderText="Dt Consulta Inicio">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="periodo_fim" HeaderText="Dt Consulta Fim">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nota_fiscal" HeaderText="Nota Fiscal">
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



        <%--animação no carregamento da pagina--%>

        <div id="SpinnerContainer"></div>
        <div id="Spinner" style="background: url(images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>




    </form>


    <script type="text/javascript">

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }











        function removeLabelChamado() {
            // Removendo um nó a partir do pai
            var node = document.getElementById("");

            node.parentNode.removeChild(node);
        }

    </script>


    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script>
        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }


        $("form").submit(function (event) {

            if (!($(this).attr("target"))) {
                ToggleSpinnerBlock(true);
                return true;
            }
        });

        function minhaFuncao() {
            if (!($(this).attr("target"))) {

                ToggleSpinnerBlock(true);
                return true;
            }
        }



    </script>



</body>


</html>

