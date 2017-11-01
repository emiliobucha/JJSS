<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="ComprarProducto.aspx.cs" Inherits="JJSS.Presentacion.ComprarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegAlumno" runat="server">
        <div class="container">

            <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                <div class="col-md-2 hidden-xs"></div>
                <div class="col-md-8 col-xs-12">
                    <div class="alert alert-success alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <a class="ui-icon ui-icon-check"></a>
                        <strong>
                            <asp:Label ID="lbl_exito" runat="server" Text=""></asp:Label></strong>
                    </div>
                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
                <div class="col-md-2 hidden-xs"></div>
                <div class="col-md-8 col-xs-12">
                    <div class="alert alert-danger alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <a class="ui-icon ui-icon-alert"></a>
                        <strong>Error! </strong>
                        <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </asp:Panel>

            <div class="row centered">
                <h1>REGISTRO DE COMPRA DE PRODUCTO</h1>
                <p>&nbsp;</p>
            </div>

            <div id="mostrarAlumnowrap">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 centered center-block">

                    <asp:Panel ID="pnl_registro" runat="server">
                        <div class="centered">
                            <h3>INGRESO DE DATOS</h3>
                            <p>&nbsp;</p>
                        </div>

                        <div style="border: 1px medium gray">
                            <div>
                                <strong class="pull-left">Producto</strong>
                                <asp:DropDownList class="caja2" ID="ddl_producto" runat="server" OnSelectedIndexChanged="ddl_producto_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div>
                                <strong class="pull-left">Costo ($)</strong>
                                <asp:TextBox ID="txt_costo" runat="server" type="number" step="0.01" min="1" max="9999999" CssClass="form-control" required="true" OnTextChanged="txt_costo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div>
                                <strong class="pull-left">Cantidad</strong>
                                <asp:TextBox ID="txt_cantidad" runat="server" type="number" min="1" max="9999999" CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <div>
                                <strong class="pull-left">Proveedor</strong>
                                <asp:DropDownList class="caja2" ID="ddl_proveedor" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>


                </div>

                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 centered">
                    <div>
                        <p>&nbsp;</p>
                    </div>
                </div>


                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 centered center-block">

                    <asp:Panel ID="pnl_listado_alumnos" runat="server">

                        <div class="centered">
                            <h3>DATOS DEL PRODUCTO</h3>
                            <p>&nbsp;</p>
                        </div>

                        <div>

                            <div class="row centered center-block">

                                <div class="row centered">
                                    <label class="pull-left">Categoria: &nbsp; </label>
                                    <asp:Label ID="lbl_categoria" class="centered  pull-left" runat="server" Text=""></asp:Label>
                                    <p>&nbsp;</p>
                                </div>

                                <div class="row centered">
                                    <label class="pull-left">Precio de venta actual $ &nbsp; </label>
                                    <asp:Label ID="lbl_precio_actual" class="centered pull-left" runat="server" Text=""></asp:Label>
                                    <p>&nbsp;</p>
                                </div>
                       
                                <div class="row centered">
                                    <label class="pull-left">Stock actual: &nbsp;</label>
                                    <asp:Label ID="lbl_stock_actual" class="centered  pull-left" runat="server" Text=""></asp:Label>
                                    <p>&nbsp;</p>
                                </div>                           
                          
                                <div class="row centered ">
                                    <label class="pull-left">Precio de venta ($) &nbsp;</label> 
                                    <asp:TextBox ID="txt_precio_venta" runat="server" CssClass="form-control pull-left caja2" required="true" type="number" min="0" max="9999999" step="0.01"></asp:TextBox>
                                    <p>&nbsp;</p>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>
                    </div>

                    <!--Boton para aceptar registro-->


                <div class="row centered">


                    <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 center-block">
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <asp:Button CssClass="btn btn-default" ID="btn_aceptar" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" />
                        <asp:Button CssClass="btn btn-default" ID="btn_cancelar" formnovalidate="true" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click" />
                    </div>
                </div>

                <div class="row centered">
                        <p>&nbsp;</p>
                    </div>                
            </div>

        </div>
    </form>

</asp:Content>
