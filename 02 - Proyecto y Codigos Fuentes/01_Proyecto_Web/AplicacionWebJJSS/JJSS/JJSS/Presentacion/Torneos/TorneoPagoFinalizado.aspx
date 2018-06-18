﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="TorneoPagoFinalizado.aspx.cs" Inherits="JJSS.Presentacion.TorneoPagoFinalizado" %>

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
            <h1 class="centered">Pago de Torneo</h1>
        </div>

        <div>
            &nbsp;
        </div>

        <div class="container">

            <asp:Panel ID="pnlPago" CssClass="panel panel-default p-1 " runat="server">

                <div class="centered justify-content-center border rounded p-4">

                    <div class="row justify-content-center p-1">
                        <h2>
                            <asp:Label ID="lbl1" runat="server" Text="Participante: "></asp:Label>
                            <asp:Label ID="lbl_participante" runat="server" Text="No hay participante seleccionado"></asp:Label></h2>
                        <p>&nbsp;</p>
                    </div>

                    <!-- Torneo-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label class="pull-left">Torneo</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_torneo" runat="server" Text="Torneo"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>

                    <div class="row centered">
                        &nbsp;
                    </div>

                    <!-- FECHA DE PAGO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <asp:Label  ID="lbl_fecha1" runat="server" Text="Fecha de Pago:&nbsp;"></asp:Label>

                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_fecha" runat="server" Text="12/12/2012"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>
                     

                    <!-- FECHA DEL TORNEO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <asp:Label ID="lbl_fechatorneo1" runat="server" Text="Fecha del Torneo:&nbsp;"></asp:Label>

                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_fechatorneo" runat="server" Text="12/12/2012"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>  

                    <!-- MONTO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <asp:Label  ID="lbl_monto1" runat="server" Text="Monto:&nbsp;"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_monto" runat="server" Text="$"></asp:Label>
                        </div>
                    </div>


                     <div>
                        &nbsp;
                    </div>
                </div>

            </asp:Panel>

            <!-- BOTONES-->
            <div class="container p-1">
                <div class="row centered">
                    <div class="col col-auto">
                        <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="MenuTorneo.aspx"></asp:HyperLink>
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
