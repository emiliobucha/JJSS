<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PagoMultipleFinalizado.aspx.cs" Inherits="JJSS.Presentacion.PagoMultipleFinalizado" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="pagoTorneo" title="pagoTorneo"></section>
    <form id="formPagoTorneo" runat="server">


        <div class="container">
            <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                <div class="col-md-2"></div>
                <div class="col-md-8">
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
                <div class="col-md-2"></div>
                <div class="col-md-8">
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
        </div>

        <div class="row mt centered justify-content-center ">
            <h1 class="centered">Pagos Realizados</h1>
        </div>
        <div>
            &nbsp;
        </div>
        <div class="container">

            <asp:Panel ID="pnlPago" CssClass="panel panel-default p-1 " runat="server">

                <div class="centered justify-content-center border rounded p-4">

                    <div class="form-group">

                        <div class="row justify-content-center p-1">
                            <h2>
                                <asp:Label ID="lbl1" runat="server" Text="Participante: "></asp:Label>
                                <asp:Label ID="lbl_participante" runat="server" Text="No hay participante seleccionado"></asp:Label></h2>
                            <p>&nbsp;</p>
                        </div>

                        <!-- Torneo-->
                        <div class="row justify-content-center p-1">
                            <div class="col-xs-2">
                                <label>Descripción:&nbsp;</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_descripcion" runat="server" Text="Descripción"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>


                        <!-- FECHA DE PAGO-->
                        <div class="row justify-content-center p-1">
                            <div class="col-xs-2">
                                <asp:Label CssClass="pull-left" ID="lbl_fecha" runat="server" Text="Fecha de Pago:&nbsp;"></asp:Label>

                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_fecha1" runat="server" Text="12/12/2012"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>



                        <!-- MONTO-->
                        <div class="row justify-content-center p-1">
                            <div class="col-xs-2">

                                <asp:Label CssClass="pull-left" ID="lbl_monto1" runat="server" Text="Monto:&nbsp;"></asp:Label>

                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_monto" runat="server" Text="$ "></asp:Label>
                            </div>
                        </div>

                        <div class="row centered">
                            &nbsp;
                        </div>
                        
                        
                        <!-- MONTO-->
                         <div class="row centered justify-content-center">
                                <asp:GridView ID="gvPagos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Inscripcion"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay pagos pendientes para mostrar"
                                    OnRowCommand="gvPagos_RowCommand" AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <asp:BoundField DataField="TipoPago.Tipo" HeaderText="Tipo" SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="DescripcionObjeto"  HeaderText="Descripción" SortExpression="descripcion" />
                                        <asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="monto" />
                                        <asp:ButtonField CommandName="imprimir" Text="Imprimir Comprobante" ItemStyle-ForeColor="#007bff" HeaderText="Comprobante" />
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Mode="NextPrevious" Position="TopAndBottom" />
                                </asp:GridView>
                               

                            </div>



                    </div>
                </div>
            </asp:Panel>

            <!-- BOTONES-->
            <div class="container p-1">
                <div class="row centered">
                    <div class="col col-auto">
                        <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="PagosPanel.aspx"></asp:HyperLink>
                    </div>
                </div>
            </div>

        </div>



    </form>
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
    <script type="text/javascript">
        (function () { function $MPC_load() { window.$MPC_loaded !== true && (function () { var s = document.createElement("script"); s.type = "text/javascript"; s.async = true; s.src = document.location.protocol + "//secure.mlstatic.com/mptools/render.js"; var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(s, x); window.$MPC_loaded = true; })(); } window.$MPC_loaded !== true ? (window.attachEvent ? window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null; })();
    </script>
    <script type="text/javascript">
        function execute_my_onreturn(json) {
            if (json.collection_status == 'approved') {
                location.href = "AlumnoPagoFinalizado.aspx"
            } else if (json.collection_status == 'pending') {
                alert('El usuario no completó el pago');
            } else if (json.collection_status == 'in_process') {
                alert('El pago está siendo revisado');
            } else if (json.collection_status == 'rejected') {
                alert('El pago fué rechazado, el usuario puede intentar nuevamente el pago');
            } else if (json.collection_status == null) {
                alert('El usuario no completó el proceso de pago, no se ha generado ningún pago');
            }
        }
    </script>



</asp:Content>
