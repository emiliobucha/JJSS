﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="EventoPago.aspx.cs" Inherits="JJSS.Presentacion.EventoPago" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="pagoEvento" title="pagoEvento"></section>
    <form id="formPagoEvento" runat="server">

        <div class="container centered justify-content-center">
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
            <h1 class="centered">Pago de Evento</h1>
        </div>

        <div>
            &nbsp;
        </div>

        <div class="container ">

            <asp:Panel ID="pnlPago" CssClass="panel panel-default p-1 " runat="server">

                <div class="centered justify-content-center border rounded p-4">

                    <div class="row justify-content-center p-1">
                        <h2>
                            <asp:Label ID="lbl1" runat="server" Text="Participante: "></asp:Label>
                            <asp:Label ID="lbl_participante" runat="server" Text="No hay participante seleccionado"></asp:Label></h2>
                        <p>&nbsp;</p>
                    </div>

                    <!-- Evento-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label >Evento:&nbsp;</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_evento" runat="server" Text="Evento"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>

                   
                    <!-- FECHA DE PAGO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <asp:Label  ID="lbl_fecha" runat="server" Text="Fecha de Pago:&nbsp;"></asp:Label>

                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_fecha1" runat="server" Text="12/12/2012"></asp:Label>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>
 

                    <!-- FECHA DEL EVENTO-->
                   <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <asp:Label  ID="lbl_fechaevento1" runat="server" Text="Fecha del Evento:&nbsp;"></asp:Label>

                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_fechaevento" runat="server" Text="12/12/2012"></asp:Label>
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

                    <div class="row centered">
                        &nbsp;
                    </div>


                </div>

            </asp:Panel>
            <!-- BOTONES-->
            <div class="row centered justify-content-center p-1">
                <div class="col col-auto">
                    <a class="btn btn-outline-dark" runat="server" id="mp_checkout" name="MP-Checkout" mp-mode="modal" onreturn="execute_my_onreturn">Pagar</a>
                </div>
                <div class="col col-auto">
                    <asp:Button ID="btn_cancelar" CssClass="btn btn-outline-dark" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btn_cancelar_Click" />

                </div>
            </div>
        </div>
    </form>


    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
     <script type="text/javascript">
     (function(){function $MPC_load(){window.$MPC_loaded !== true && (function(){var s = document.createElement("script");s.type = "text/javascript";s.async = true;s.src = document.location.protocol+"//secure.mlstatic.com/mptools/render.js";var x = document.getElementsByTagName('script')[0];x.parentNode.insertBefore(s, x);window.$MPC_loaded = true;})();}window.$MPC_loaded !== true ? (window.attachEvent ?window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null;})();
    </script>
  <script type="text/javascript">
      function execute_my_onreturn(json) {
          if (json.collection_status == 'approved') {
              location.href="EventoPagoFinalizado.aspx?Estado=ok"
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
