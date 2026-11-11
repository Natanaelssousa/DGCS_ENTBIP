<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventarioCiclico.aspx.cs" Inherits="DGCS_ENTBIP.Views.InventarioCiclico" %>


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

    <script>

        function mudaMenu() {
            $('#nav-tab a[href="#perfil"]').tab('show');
        }
    </script>

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

                    <asp:LinkButton ID="LinkButtonMenu2" runat="server"> <img src="images/logo_natura2.jpg"" style="width: 60px; margin-right: 60px" /></asp:LinkButton>
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
            <h4 class="alert-heading" style="color: dimgrey">Inventario Cíclico</h4>
            <p style="color: dimgrey">Atenção! Importe o arquivo txt de acordo com o padrão definido.</p>
            <hr>
            <p class="mb-0"></p>
        </div>

        <div class="row">

            <div class="col-4 mr-5"></div>


            <div class="col-4">

                <asp:Label ID="labelRomaneioCiclico" name="labelRomaneioCiclico" runat="server" Visible="true" Text="Romaneio: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; padding-left: 60px;"> </asp:Label>
                <asp:Label ID="romaneioCiclico" name="romaneioCiclico" runat="server" Text="00000000" Visible="true" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>


                <asp:Label ID="labelTipoInventario" name="labelTipoInventario" runat="server" Text="Tipo: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                <asp:Label ID="tipoInventario" name="tipoInventario" runat="server" Text="" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;" class="badge badge-pill badge-info"> </asp:Label>


            </div>

            <div class="col-4"></div>

        </div>

        <div class="row">

            <div class="col-4"></div>
            <div class="col-5">

                <asp:Label ID="labelTotalEans" name="labelTotalEans" runat="server" Text="Total de sku's contados: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px; font-weight: bold;"> </asp:Label>
                <asp:Label ID="qtdeTotalEans" name="qtdeTotalEans" runat="server" Text="0" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;" class="badge badge-pill badge-success"> </asp:Label>

                <asp:Label ID="labelQtdeImportado" name="labelQtdeImportado" runat="server" Text="Somatória dos itens contados: " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px; font-weight: bold;"> </asp:Label>
                <asp:Label ID="qtdeTotalImportado" name="qtdeTotalImportado" runat="server" Text="0" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;" class="badge badge-pill badge-success"> </asp:Label>

            </div>

            <div class="col-3"></div>
        </div>

        <div class="col-sm-12 text-center mt-2">
            <asp:Label runat="server" ID="mensagem" name="mensagem" Style="font-size: 14px; padding: 8px; margin: 0px; font-weight: bold" Text="" Visible="false" class="alert alert-success" role="alert">
            </asp:Label>
        </div>
        <nav>
            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="nav-home" aria-selected="true" onclick="FechaSpinner();">Bip Cód Barras/Produto</a>
                <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#perfil" role="tab" aria-controls="nav-profile" aria-selected="false" onclick="FechaSpinner();">Importação de Arquivo</a>
            </div>

        </nav>

        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">


                <%--   radio button --%>
                <div class="text-center mb-2">

                    <div class="custom-control custom-radio custom-control-inline">
                        <asp:CheckBox ID="ZerarQtde" name="ZerarQtde" runat="server" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; width: 20px;" onclick="habilitaBusca()" />
                        <label for="ZerarQtde" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 14px;">Item Zero</label>
                    </div>

                </div>
                <%--   radio button --%>


                <div class="row">

                    <div class="col-3"></div>

                    <div class="col-6">

                        <asp:Label ID="cod_barraLabel" name="cod_barraLabel" runat="server" Text="Cód Barras / Cód Prod " Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif;"> </asp:Label>
                        <asp:TextBox ID="cod_barra" runat="server" type="text" class="form-control" Style="font-size: 14px;" name="cod_barra" onchange="minhaFuncao()" AutoPostBack="true" placeholder="Bipe o codigo de barras ou produto..." OnTextChanged="cod_barra_TextChanged" data-toggle="tooltip" data-placement="top" title="Bipar os codigos de barras dos produtos das notas selecionadas"></asp:TextBox>

                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-success mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonFinalizar" OnClick="LinkButtonFinalizar_Click" data-toggle="tooltip" data-placement="top" title="Quando bipar todos os itens das notas selecionadas, clique neste botão para fianlizar o processo.">Finalizar</asp:LinkButton>

                            <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-danger mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonRefazer" OnClick="LinkButtonRefazer_Click" data-toggle="tooltip" data-placement="top" title="Reiniciar toda a bipagem das notas selecionadas.">Refazer</asp:LinkButton>

                            <asp:LinkButton ID="LinkButton3" runat="server" class="btn btn-secondary mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonPausar" OnClick="LinkButtonPausar_Click" data-toggle="tooltip" data-placement="top" title="Pausar a bipagem das notas selecionadas para dar continuidade em outro momento.">Pausar Bipagem</asp:LinkButton>

                        </div>
                    </div>

                    <div class="col-3"></div>

                </div>
            </div>
            <div class="tab-pane fade" id="perfil" role="tabpanel" aria-labelledby="profile-tab">
                <div class="row mt-1">

                    <div class="col-3"></div>

                    <div class="col-6">
                        <div class="card" id="importacao">
                            <div class="card-header alert-secondary">
                                Tipos de arquivo para importação
                            </div>
                            <div class="card-body">
                                <h6 class="card-title">Abaixo marque o formato do texto para que a leitura seja reconhecida.</h6>


                                <div class="row ml-2">
                                    <asp:CheckBox ID="opcao1" runat="server" onclick="marcaDesmarca(this)" />
                                    <label style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; margin-left: 2px; margin-top: 5px;"
                                        for="opcao1" class="cabecario">
                                        "Código de Barras" , Quantidade</label>
                                </div>
                                <div class="row ml-2">
                                    <asp:CheckBox ID="opcao2" runat="server" onclick="marcaDesmarca(this)" />
                                    <label style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; margin-left: 2px; margin-top: 5px;"
                                        for="opcao2" class="cabecario">
                                        "Código de Barras" ; Quantidade</label>
                                </div>
                                <div class="row ml-2">
                                    <asp:CheckBox ID="opcao3" runat="server" onclick="marcaDesmarca(this)" />
                                    <label style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; margin-left: 2px; margin-top: 5px;"
                                        for="opcao3" class="cabecario">
                                        "Código de Barras"</label>
                                </div>



                                <div class="row ml-5">
                                    <%-- file upload control, using to upload the file which will be read and get file information--%>
                                    <asp:FileUpload ID="fileSelect" class="btn btn-light" runat="server" />
                                    <%-- click this button to run read method--%>
                                    <asp:LinkButton ID="LinkButtonImport" runat="server" class="btn btn-outline-primary mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonImport" data-toggle="tooltip" data-placement="top" title="Quando selecionar o arquivo clique nesse butão para realizar a importação." OnClick="LinkButtonImport_Click">Importar Arquivo</asp:LinkButton>

                                </div>


                            </div>
                        </div>

                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="LinkButtonFinalizar" runat="server" class="btn btn-success mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonFinalizar" data-toggle="tooltip" data-placement="top" title="Quando bipar todos os itens das notas selecionadas, clique neste botão para fianlizar o processo." OnClientClick="ocultar()" OnClick="LinkButtonFinalizar_Click">Finalizar</asp:LinkButton>

                            <asp:LinkButton ID="LinkButtonRefazer" runat="server" class="btn btn-danger mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonRefazer" data-toggle="tooltip" data-placement="top" title="Reiniciar toda a bipagem das notas selecionadas." OnClientClick="ocultar()" OnClick="LinkButtonRefazer_Click">Refazer</asp:LinkButton>

                            <asp:LinkButton ID="LinkButtonPausar" runat="server" class="btn btn-secondary mt-2 ml-3" Style="font-size: 14px;" name="LinkButtonPausar" data-toggle="tooltip" data-placement="top" title="Pausar a bipagem das notas selecionadas para dar continuidade em outro momento." OnClick="LinkButtonPausar_Click">Pausar Bipagem</asp:LinkButton>

                        </div>
                    </div>

                    <div class="col-3"></div>

                </div>
            </div>
        </div>



        <div class="principal d-flex justify-content-center ml-5 mt-2">

            <div>
            </div>



            <asp:Panel ID="Panel1" runat="server" Style="overflow-y: auto; max-height: 100%;" HorizontalAlign="Center" Width="980px" Height="335px">
                <asp:GridView ID="GrNotas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="#ed6227" CssClass="table table-bordered table-condensed"
                    DataKeyNames="id_aux_inventario,romaneio_inventario,produto,cor_produto,tamanho,codigo_barra,qtde_importado,
                  desc_produto,grupo,subgrupo,fornecedor,responsavel,qtde_vezes_importado,data_hora,tipo_inventario,status_inventario"
                    OnRowDataBound="GrNotas_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                    <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" Font-Size="Medium" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Size="Small" ForeColor="#333333" />
                    <Columns>



                        <asp:BoundField DataField="desc_produto" HeaderText="Desc Produto">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="produto" HeaderText="Produto">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="grupo" HeaderText="Grupo">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="subgrupo" HeaderText="Subgrupo">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo_barra" HeaderText="Codigo barras">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="qtde_importado" HeaderText="Qtde Importado">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" Width="10%" />
                        </asp:BoundField>

                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnsubtrair" name="btnsubtrair" OnClick="btnsubtrair_Click" ImageUrl="images/subtrair.png" runat="server" Width="25" Height="25" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:ImageButton ID="imgbtn" name="imgbtn" ImageUrl="images/remover.png" runat="server" Width="25" Height="25" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top" Font-Size="Small" ForeColor="Black" />
                        </asp:TemplateField>

                      
                    </Columns>
                </asp:GridView>
            </asp:Panel>


            <div></div>


        </div>


        <!-- Modal excluir item-->
        <div class="modal fade" id="ModalExcluir" tabindex="-1" role="dialog" aria-labelledby="ModalLabelExcluir" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="ModalLabelExcluir">Deseja Excluir o produto abaixo?</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="col-md-12 mb-3">
                                <label for="produto_desc" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;">Produto</label>
                                <asp:TextBox ID="produto" runat="server" type="text" name="produto" class="form-control" Style="font-size: 16px; border: none;"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <label for="desc_produto" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;">Descrição</label>
                                <asp:TextBox ID="desc_produto" runat="server" type="text" name="desc_produto" class="form-control" Style="font-size: 16px; border: none;"></asp:TextBox>
                            </div>
                            <div class="col-md-12 mb-3">
                                <label for="codigo_barras" style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px;">Codigo de barras</label>
                                <asp:TextBox ID="codigo_barras" runat="server" type="text" name="codigo_barras" class="form-control" Style="font-size: 15px; border: none;"></asp:TextBox>
                                <asp:TextBox ID="qtdeItem" runat="server" type="text" name="qtdeItem" class="form-control" Style="font-size: 10px; color: white; border: none;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">

                            <asp:LinkButton ID="LinkButtonExcluirItem" runat="server" class="btn btn-success mt-2 ml-2"
                                name="LinkButtonConfirmar" OnClick="LinkButtonExcluirItem_Click">Sim</asp:LinkButton>

                            <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-danger  mt-2 ml-2"
                                name="LinkButtonNao" OnClientClick="ocultar()">Não</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal de finalização e entrda de notas-->
        <div class="modal fade" id="ModalconfirmaEntradas" tabindex="-1" role="dialog" aria-labelledby="confirmaEntradas" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="confirmaEntradas">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleconfirmaEntradas">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaEntradaLabel" Text="teste" ID="confirmaEntradaLabel" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label><br />
                        <br />
                        <div class="form-row d-flex justify-content-center">
                            <div class="col-md-12">
                                <asp:Label for="desc_inventario" runat="server" name="labelDesc_inventario" Text="teste" ID="labelDesc_inventario" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 15px;">Descrição Inventario</asp:Label>
                                <asp:TextBox ID="desc_inventario" runat="server" type="text" name="desc_inventario" class="form-control" placeholder="Digite a descrição do inventario..." MaxLength="20"></asp:TextBox>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmarFinalizar" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmarFinalizar" OnClientClick="ocultar()" OnClick="LinkButtonConfirmarFinalizar_Click">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNaoConfirmaEntrada" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNaoConfirmaEntrada" OnClick="LinkButtonNaoConfirmaEntrada_Click">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal produto não existe na nota-->
        <div class="modal fade" id="ModalProdFora" tabindex="-1" role="dialog" aria-labelledby="ModalProdFora" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlerta">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaLabel" Text="teste" ID="confirmaLabel" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmar" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmar">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNao" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNao" OnClientClick="ocultar()">>Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>




        <!-- Modal refazer-->
        <div class="modal fade" id="ModalRefazer" tabindex="-1" role="dialog" aria-labelledby="ModalRefazer" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaRefazer">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalrefazer">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" name="confirmaRefazer" Text="teste" ID="confirmaRefazer" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonConfirmaRazer" runat="server" class="btn btn-success  mt-4 ml-5"
                            name="LinkButtonConfirmaRazer" OnClick="LinkButtonConfirmaRazer_Click">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaRefazer" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaRefazer" OnClientClick="ocultar()" OnClick="LinkButtonNegaRefazer_Click">Não, cancelar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal Entrada sucesso-->
        <div class="modal fade" id="ModalEntSucesso" tabindex="-1" role="dialog" aria-labelledby="ModalEntSucesso" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content" id="modalAlertaEntSucesso">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalEntSucesso">Inventario Cíclico</h5>
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
                            name="LinkButtonEntSucesso" OnClientClick="ocultar()" OnClick="LinkButtonEntSucesso_Click">Sim, confirmar</asp:LinkButton>


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
                        <h5 class="modal-title ml-1" id="exampleModalEntAmais">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntAmais" Text="teste" ID="confirmaEntAmais" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntAmais" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntAmais">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaEnt" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaEnt">Não, cancelar</asp:LinkButton>
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
                        <h5 class="modal-title ml-1" id="exampleModalEntForaNota">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntForaNota" Text="teste" ID="confirmaEntForaNota" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntForaNota" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntForaNota">Sim, confirmar</asp:LinkButton>

                        <asp:LinkButton ID="LinkButtonNegaForaNota" runat="server" class="btn btn-danger  mt-4 ml-5"
                            name="LinkButtonNegaForaNota">Não, cancelar</asp:LinkButton>
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
                        <h5 class="modal-title ml-1" id="exampleModalEntComDivergencia">Inventario Cíclico</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onload="setFocus()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:Label runat="server" name="confirmaEntComDivergencia" Text="teste" ID="confirmaEntComDivergencia" Style="font-family: 'Calibri', 'Trebuchet MS', sans-serif; font-size: 18px"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonEntComDivergencia" runat="server" class="btn btn-success  mt-4"
                            name="LinkButtonEntComDivergencia" OnClientClick="ocultar()">Finalizar</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>


        <%--animação no carregamento da pagina--%>

        <div id="SpinnerContainer"></div>
        <div id="Spinner" style="background: url(images/loader.gif) no-repeat center #fff; width: 300px; height: 200px"></div>

    </form>



    <script type="text/javascript">


        function funcaoQueExibeModal(produto, desc_produto, codigo_barra, qtde) {

            document.querySelector("[name='produto']").value = produto;
            document.querySelector("[name='codigo_barras']").value = codigo_barra;
            document.querySelector("[name='desc_produto']").value = desc_produto;
            document.querySelector("[name='qtdeItem']").value = qtde;

            //alert('teste');

            $('#ModalCenterBipados').modal('hide');
            $('#ModalExcluir').modal('show');
        };


        function marcaDesmarca(caller) {
            var checks = document.querySelectorAll('input[type="checkbox"]');
            checks.forEach(c => c.checked = (c == caller));
        }


        let qtdeTotalEans = document.getElementById('qtdeTotalEans')
        let qtdeTotalImportado = document.getElementById('qtdeTotalImportado')
        let codBarra = document.getElementById("cod_barra")


        if (qtdeTotalEans.textContent == "0") {
            document.getElementById('importacao').style.display = 'block';
        }
        else {
            document.getElementById('importacao').style.display = 'none';
        }





    </script>


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

        //$("a").click(function (event) {
        //    if (!($(this).attr("target"))) {

        //        ToggleSpinnerBlock(true);
        //        return true;
        //    }
        //});

        //function minhaFuncao() {
        //    if (!($(this).attr("target"))) {

        //        ToggleSpinnerBlock(true);
        //        return true;
        //    }
        //}


        function exibir() {
            document.getElementById("importacao").style.display = "block";
        }

        function ocultar() {
            document.getElementById("importacao").style.display = "none";
        }


        function ocultarExclusao() {
            $('#ModalExcluir').modal('hide');
        }

        function minhaFuncao() {
            if (!($(this).attr("target"))) {

                ToggleSpinnerBlock(true);
                return true;
            }
        }

        function minhaOcultaFuncao() {
            if (!($(this).attr("target"))) {

                ToggleSpinnerBlock(false);
                return false;
            }
        }

    </script>

</body>
</html>
