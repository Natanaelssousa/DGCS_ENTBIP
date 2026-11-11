<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntradaPadrao.aspx.cs" Inherits="DGCS_ENTBIP.Views.EntradaPadrao" %>


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



        function FechaModal() {


            $('#ModalEntProdAmaisLote').modal('hide');

            $('#Spinner').modal('hide');
            $('#SpinnerContainer').modal('hide');
            //$('.Spinner').hide();
            //$('.SpinnerContainer').hide();

        }

        function verificarTextBoxes() {
            var gridView = document.getElementById('<%= GrSobraLote.ClientID %>');
            var textBoxes = gridView.getElementsByTagName('input'); // Obtém todos os elementos de input no GridView

            var isEmpty = false;

            for (var i = 0; i < textBoxes.length; i++) {
                if (textBoxes[i].type === 'text' && textBoxes[i].value.trim() === '') {
                    isEmpty = true;
                    break;
                }
            }

            if (isEmpty) {
                swal('Atenção!', 'Todos os campos da coluna lote devem possui informação.', 'error');
                return false; // Impede o postback
            } else {
                $('#modallote').modal('hide');
                confirmaEntAmaisLote.textContent = "Confirma as informações de lote inserida e deseja finalizar entrada?";
                $('#ModalEntProdAmaisLote').modal('show');

                return false; // Permite o postback
            }

        }


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

    <script type="text/javascript">



        setInterval(mudaCor, 250);

        function mudaCor() {

            let cor = document.getElementById("modalAlerta");


            if (cor.style.backgroundColor != 'orange') {

                cor.style.backgroundColor = 'orange';
            } else {
                cor.style.backgroundColor = 'white';
            }

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
            <h4 class="alert-heading" style="color: dimgrey">Entrada Bipada</h4>
            <p style="color: dimgrey">Atenção! Bipe todos os skus das notas selecionadas e finalize para gerar as entradas.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-4"></div>


            <div class="col-5">

                <asp:Label ID="nabelNotasSelecionadas" name="nabelNotasSelecionadas" runat="server" Text="Notas Fiscais: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="notasSelecionadas" name="notasSelecionadas" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>

                <asp:Label ID="labelRomaneioEntrada" name="labelRomaneioEntrada" runat="server" Visible="false" Text="Romaneio: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="romaneioEntrada" name="romaneioEntrada" runat="server" Text="00000000" Visible="false" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>

                <asp:Label ID="labelQtdeTotalNotas" name="labelQtdeTotalNotas" runat="server" Text="Qtde Total: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="qtdeTotalNotas" name="qtdeTotalNotas" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>

                <asp:Label ID="labelQtdeBipado" name="labelQtdeBipado" runat="server" Text="Qtde Bipado: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="qtdeTotalBipado" name="qtdeTotalBipado" runat="server" Text="0" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-danger"> </asp:Label>


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

                <asp:Label ID="cod_barraLabel" name="cod_barraLabel" runat="server" Text="Código de Barras: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:TextBox ID="cod_barra" runat="server" type="text" class="form-control" Style="font-size: 14px;" name="cod_barra" onchange="minhaFuncao()" AutoPostBack="true" placeholder="Bipe o codigo de barras..." required="" OnTextChanged="cod_barra_TextChanged" data-toggle="tooltip" data-placement="top" title="Bipar os codigos de barras dos produtos das notas selecionadas"></asp:TextBox>

                <div class="row d-flex justify-content-center">
                    <asp:LinkButton ID="LinkButtonFinalizar" runat="server" class="btn btn-success mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonFinalizar" OnClick="LinkButtonFinalizar_Click" data-toggle="tooltip" data-placement="top" title="Quando bipar todos os itens das notas selecionadas, clique neste botão para fianlizar o processo.">Finalizar</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonRefazer" runat="server" class="btn btn-danger mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonRefazer" OnClick="LinkButtonRefazer_Click" data-toggle="tooltip" data-placement="top" title="Reiniciar toda a bipagem das notas selecionadas.">Refazer</asp:LinkButton>

                    <asp:LinkButton ID="LinkButtonPausar" runat="server" class="btn btn-secondary mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonPausar" OnClick="LinkButtonPausar_Click" data-toggle="tooltip" data-placement="top" title="Pausar a bipagem das notas selecionadas para dar continuidade em outro momento.">Pausar Bipagem</asp:LinkButton>

                </div>
            </div>

            <div class="col-3"></div>

        </div>

        <div class="principal d-flex justify-content-center ml-5 mt-2">

            <div>
            </div>



            <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="980px" Height="335px">
                <asp:GridView ID="GrNotas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                    DataKeyNames="ids_nfs_selecionadas,romaneio_produto,produto,cor_produto,tamanho,
                                codigo_barra,preco1,qtde_total,qtde_bipado,desc_produto,fornecedor,responsavel,entrada_status,grupo,subgrupo"
                    OnRowDataBound="GrNotas_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                    <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                    <Columns>

                        <asp:BoundField DataField="desc_produto" HeaderText="Desc Produto">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="produto" HeaderText="Produto">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="grupo" HeaderText="Grupo">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="subgrupo" HeaderText="Subgrupo">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="qtde_bipado" HeaderText="Qtde Bipado">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="qtde_total" HeaderText="Qtde total" Visible="false">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>


            <div></div>


        </div>




        <!-- Modal de finalização e entrda de notas-->
        <div class="modal fade" id="ModalconfirmaEntradas" tabindex="-1" role="dialog" aria-labelledby="confirmaEntradas" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="confirmaEntradas">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleconfirmaEntradas">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaEntradaLabel" Text="teste" ID="confirmaEntradaLabel" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmarFinalizacao" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmarFinalizacao" OnClick="LinkButtonConfirmar_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonConfirmaEntrada" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNaoConfirmaEntrada">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal produto não existe na nota-->
        <div class="modal fade" id="ModalProdFora" tabindex="-1" role="dialog" aria-labelledby="ModalProdFora" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlerta">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaLabel" Text="teste" ID="confirmaLabel" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmar" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmar" OnClick="LinkButtonConfirmar_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNao" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNao">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>




        <!-- Modal refazer-->
        <div class="modal fade" id="ModalRefazer" tabindex="-1" role="dialog" aria-labelledby="ModalRefazer" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaRefazer">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalrefazer">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaRefazer" Text="teste" ID="confirmaRefazer" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmaRazer" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmaRazer" OnClick="LinkButtonConfirmaRazer_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaRefazer" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaRefazer" OnClick="LinkButtonNegaRefazer_Click">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Entrada sucesso-->
        <div class="modal fade" id="ModalEntSucesso" tabindex="-1" role="dialog" aria-labelledby="ModalEntSucesso" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntSucesso">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalEntSucesso">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="d-flex">
                            <div>
                                <img src="images/icons8-ok-64.png" style="width: 90px;" />
                            </div>

                            <div class="ml-2">

                                <asp:Label runat="server" name="confirmaEntSucesso" Text="teste" ID="confirmaEntSucesso" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;"></asp:Label>

                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntSucesso" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntSucesso" OnClick="LinkButtonEntSucesso_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>


                    </div>
                </div>
            </div>
        </div>




        <!-- Modal Entrada produto a mais-->
        <div class="modal fade" id="ModalEntProdAmais" tabindex="-1" role="dialog" aria-labelledby="ModalEntProdAmais" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntProdAmais">
                    <div class="modal-header">
                        <img src="images/atencao.png" style="width: 40px; margin-right: 40px;" />
                        <h5 class="modal-title ml-1" id="exampleModalEntAmais">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntAmais" Text="teste" ID="confirmaEntAmais" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntAmais" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntAmais" OnClick="LinkButtonEntAmais_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaEnt" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaEnt" OnClick="LinkButtonNegaEnt_Click">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Entrada produto a mais lote-->
        <div class="modal fade" id="ModalEntProdAmaisLote" tabindex="-1" role="dialog" aria-labelledby="ModalEntProdAmaisLote" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntProdAmaisLote">
                    <div class="modal-header">
                        <img src="images/atencao.png" style="width: 40px; margin-right: 40px;" />
                        <h5 class="modal-title ml-1" id="exampleModalEntAmaisLote">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntAmaisLote" Text="teste" ID="confirmaEntAmaisLote" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntAmaisLote" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntAmaisLote" OnClick="LinkButtonEntAmaisLote_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton3" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaEnt" OnClientClick="FechaModal()">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Entrada produto fora da nota-->
        <div class="modal fade" id="ModalEntProdForaNota" tabindex="-1" role="dialog" aria-labelledby="ModalEntProdAmais" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntProdForaNota">
                    <div class="modal-header">
                        <img src="images/atencao.png" style="width: 40px; margin-right: 40px;" />
                        <h5 class="modal-title ml-1" id="exampleModalEntForaNota">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntForaNota" Text="teste" ID="confirmaEntForaNota" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntForaNota" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntForaNota" OnClick="LinkButtonEntForaNota_Click" OnClientClick="ocultarBotao();">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaForaNota" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaForaNota" OnClick="LinkButtonNegaForaNota_Click">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Entrada com divergencia-->
        <div class="modal fade" id="ModalEntComDivergencia" tabindex="-1" role="dialog" aria-labelledby="ModalEntComDivergencia" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntProdComDivergencia">
                    <div class="modal-header">
                        <img src="images/atencao.png" style="width: 40px; margin-right: 40px;" />
                        <h5 class="modal-title ml-1" id="exampleModalEntComDivergencia">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntComDivergencia" Text="teste" ID="confirmaEntComDivergencia" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntComDivergencia" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntComDivergencia" OnClick="LinkButtonConfirmar_Click" OnClientClick="ocultarBotao();">Finalizar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>




        <!-- Modal informa lote sobra -->
        <div class="modal fade" id="modallote" tabindex="-1" role="dialog" aria-labelledby="exampleModalLote" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <!-- Use a classe "modal-lg" para um modal largo -->
                <div class="modal-content">
                    <div class="modal-header">
                        <img src="images/atencao.png" style="width: 40px; margin-right: 40px;" />
                        <h5 class="modal-title" id="exampleModalLote">Entrada bipada</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Fechar">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!-- Conteúdo do modal aqui -->
                        <asp:Label runat="server" name="confirmaLoteSobra" Text="teste" ID="confirmaLoteSobra" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>

                        <div class="principal d-flex justify-content-center mt-2">

                            <div>
                            </div>

                            <asp:Panel ID="Panel2" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="980px" Height="335px">
                                <asp:GridView ID="GrSobraLote" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                                    DataKeyNames="id_aux_entrada_lote,ids_nfs_selecionadas,romaneio_produto,filial,codigo_loja,
                                produto,desc_produto,cor_produto,tamanho,codigo_barra,preco1,grupo,subgrupo,lote,quantidade,xped,chave_nfe"
                                    OnRowDataBound="GrSobraLote_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                                    <Columns>

                                        <asp:BoundField DataField="desc_produto" HeaderText="Desc Produto">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="produto" HeaderText="Produto">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupo" HeaderText="Grupo">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subgrupo" HeaderText="Subgrupo">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="quantidade" HeaderText="Qtde">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" Width="10%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Lote" ControlStyle-Width="80%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="textLote" Font-Names="textLote" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Small" Width="20%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>


                            <div></div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                        <button type="button" class="btn btn-primary" onclick="verificarTextBoxes()">Finalizar</button>

                    </div>
                </div>
            </div>
        </div>


        <%--animação no carregamento da pagina--%>

        <div id="SpinnerContainer"></div>
        <div id="Spinner" style="background: url(images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>

    </form>



    <script type="text/javascript">


        let qtdeTotal = document.getElementById('qtdeTotalNotas')
        let qtdeParcial = document.getElementById('qtdeTotalBipado')
        let codBarra = document.getElementById("cod_barra")


        if (qtdeTotal.textContent == qtdeParcial.textContent) {
            qtdeParcial.className = 'badge badge-pill badge-success'
        }
        else {
            qtdeParcial.className = 'badge badge-pill badge-danger'
        }





    </script>


    <script>

        function ocultarBotao() {
            // Obtém uma referência ao botão
            var botao0 = document.getElementById("LinkButtonConfirmarFinalizacao");
            var botao1 = document.getElementById("LinkButtonConfirmar");
            var botao2 = document.getElementById("LinkButtonEntSucesso");
            var botao3 = document.getElementById("LinkButtonEntAmais");
            var botao4 = document.getElementById("LinkButtonEntAmaisLote");
            var botao5 = document.getElementById("LinkButtonEntComDivergencia");



            // Oculta o botão
            botao0.style.display = "none";
            botao1.style.display = "none";
            botao2.style.display = "none";
            botao3.style.display = "none";
            botao4.style.display = "none";
            botao5.style.display = "none";
        }

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

        $("a").click(function (event) {
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
