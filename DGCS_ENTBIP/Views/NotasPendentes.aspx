<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotasPendentes.aspx.cs" Inherits="DGCS_ENTBIP.Views.NotasPendentes" %>

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
               <asp:TextBox ID="tela_notas_pendentes" runat="server" type="hidden" name="tela_notas_pendentes" class="form-control" value=""> </asp:TextBox>
               <asp:TextBox ID="perfil_acesso" runat="server" type="hidden" name="perfil_acesso" class="form-control" value=""> </asp:TextBox>
                  

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
            <h4 class="alert-heading" style="color: dimgrey">Notas Pendentes</h4>
            <p style="color: dimgrey">Tela para visualização de todas as notas que estão pendentes de entrada na loja. Selecione as notas que deseja dar entrada e clique em
                <d class="text-monospace">Entrada Padrão</d>
                o sem selecionar nenhuma nota clique em
                <d class="text-monospace">Entrada complementar</d>
                caso deseje fazer uma entrada de complemento.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-8">

                <div class="d-flex">


                    <div class="input-group col-12 ml-4">

                        <asp:TextBox ID="txtbusca" runat="server" name="txtbusca" Visible="true" type="text" class="form-control" placeholder="Busque pelo nome do fornecedor ou numero nota..." aria-label="Numero..." aria-describedby="button-addon2"></asp:TextBox>
                        <asp:TextBox ID="validaConsulta" runat="server" type="hidden" name="validaConsulta" class="form-control" value=""> </asp:TextBox>
                        <div class="input-group-append" id="divlnkbusca">
                            <asp:LinkButton ID="lnkBuscar" name="lnkBuscar" runat="server" Visible="true" type="button" class="btn btn-danger" Style="background-color: #ed6227;" OnClick="lnkBuscar_Click" data-toggle="tooltip" data-placement="top" title="Clique para pesquisar uma nota">Buscar</asp:LinkButton>
                        </div>
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



                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="980px" Height="350px">
                            <asp:GridView ID="GrNotas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                DataKeyNames="nf_entrada,nome_clifor,serie_nf_entrada,chave_nfe,id_xml_nfe,qtde,status,responsavel,xped"
                                OnRowDataBound="GrNotas_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#" Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRefazer" name="imgbtnRefazer" ImageUrl="images/logo_natura2.jpg" runat="server" Width="25" Height="25" OnClick="imgbtnRefazer_Click" />
                                            <asp:Label runat="server" name="refazer" Text="" ID="refazer" Font-Size="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marque">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckIDAprova" name="ckIDAprova"  runat="server" Width="20px" Style="vertical-align: middle; text-align: center;" />
                                        </ItemTemplate>
                                        <ControlStyle CssClass="citemStyle" />
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Width="2%" ForeColor="Black" />
                                        <HeaderStyle Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nf_entrada" HeaderText="Nota Fiscal">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="serie_nf_entrada" HeaderText="Serie">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nome_clifor" HeaderText="Fornecedor">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="xped" HeaderText="Id">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="2%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="qtde" HeaderText="Qtde">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="status" HeaderText="Status Entrada">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="responsavel" HeaderText="Responsavel">
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


                <div class="card border-warning mb-3" style="max-width: 11rem; max-height: 9rem;">
                    <div class="card-header text-white" style="background-color: #ed6227; font-size: 12px;"><b>Entrada Bipada</b></div>
                    <div class="card-body text-warning p-2 ml-2">
                        <div class="row">

                            <div class="col-2"></div>
                            <div class="col-9">

                                <asp:LinkButton ID="LinkButtonEntPadrao" name="LinkButtonEntPadrao" runat="server" class="btn btn rounded-circle" Style="width: 75px; height: 72px; background-color: white; color: aliceblue; border-color: darkgreen;" data-toggle="tooltip" data-placement="top" title="Bipar entrada de notas selecionadas" OnClick="LinkButtonEntPadrao_Click">
        <img src="images/icon_play.png" height ="50" width="50" class="mr-2 mt-1"/>
                                </asp:LinkButton>

                            </div>

                            <div class="col-1"></div>
                        </div>

                    </div>
                </div>

                <div class="card border-warning mb-3" style="max-width: 11rem; max-height: 9rem;">
                    <div class="card-header text-white" style="background-color: #ed6227; font-size: 12px;"><b>Entrada Complementar</b></div>
                    <div class="card-body text-warning p-2 ml-2">
                        <div class="row">

                            <div class="col-2"></div>
                            <div class="col-9">

                                <asp:LinkButton ID="LinkButtonEntComplementar" name="LinkButtonEntComplementar" runat="server" class="btn btn rounded-circle" Style="width: 75px; height: 72px; background-color: white; color: aliceblue; border-color: darkgreen;" data-toggle="tooltip" data-placement="top" title="Bipar entrada de nota complementar" OnClick="LinkButtonEntComplementar_Click">
            <img src="images/icon_play.png" height ="50" width="50" class="mr-2 mt-1"/>
                                </asp:LinkButton>

                            </div>

                            <div class="col-1"></div>
                        </div>

                    </div>
                </div>



                     <div class="card border-warning " style="max-width: 11rem; max-height: 9rem;">
                    <div class="card-header text-white" style="background-color: #ed6227; font-size: 12px;"><b>Sincronizar Notas</b></div>
                    <div class="card-body text-warning p-2 ml-2">
                        <div class="row">

                            <div class="col-2"></div>
                            <div class="col-9">

                                <asp:LinkButton ID="LinkButtonBuscaNotas" name="LinkButtonBuscaNotas" runat="server" class="btn btn rounded-circle" Style="width: 75px; height: 72px; background-color: white; color: aliceblue; border-color: darkorange;" data-toggle="tooltip" data-placement="top" title="Busca notas pendentes na base da loja" OnClick="LinkButtonBuscaNotas_Click">
            <img src="images/atualizar.png" height ="50" width="50" class="mr-2 mt-1"/>
                                </asp:LinkButton>

                            </div>

                            <div class="col-1"></div>
                        </div>

                    </div>
                </div>

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

    </script>


    <!-- Incluindo o JavaScript do Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script>



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

        function noBack() { window.history.forward(0) }

        noBack();

        window.onload = noBack;

        window.onpageshow = function (evt) { if (evt.persisted) noBack() }

        window.onunload = function () { void (0) }


    </script>



</body>


</html>
