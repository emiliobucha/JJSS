<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AlumnoPagoClase.aspx.cs" Inherits="JJSS.Presentacion.AlumnoPagoClase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="pagoClase" title="pagoClase"></section>
    <form id="formPagoClase" runat="server">
        <div id="registrowrap">

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

            <asp:Panel ID="pnlPago" runat="server">
                <div class="container">
                    <div class="row mt centered">
                       
                        <h1>PAGO DE CLASE</h1>
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group">
                        <div class="row centered">
                            <h2>
                                <asp:Label ID="lbl1" runat="server" Text="Alumno: "></asp:Label>
                                <asp:Label ID="lbl_alumno" runat="server" Text="No hay alumno seleccionado"></asp:Label></h2>
                            <p>&nbsp;</p>
                        </div>

                        <!-- CLASES-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <label class="pull-left">Clase</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_clase" runat="server" Text="Clase"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                                
                            </div>
                        </div>

                         <div class="row centered">
                            &nbsp;
                        </div>
                        
                         <!-- FECGA-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <asp:Label cssClass="pull-left" ID="lbl_fecha" runat="server" Text="Fecha"></asp:Label>

                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_fecha1" runat="server" Text="12/12/2012"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                                
                            </div>
                        </div>

                         <div class="row centered">
                            &nbsp;
                        </div>


                        <!-- RECARGO-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <asp:Label cssClass="pull-left" ID="lbl_recargo" runat="server" Text="Recargo"></asp:Label>

                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_recargoMonto" runat="server" Text="$"></asp:Label>
                            </div>
                            <div class="col-xs-3">
                                
                            </div>
                        </div>

                         <div class="row centered">
                            &nbsp;
                        </div>

                        <!-- MONTO-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                
                                <asp:Label cssClass="pull-left" ID="lbl_monto1" runat="server" Text="Monto"></asp:Label>

                            </div>
                            <div class="col-xs-3">
                                <asp:Label ID="lbl_monto" runat="server" Text="$"></asp:Label>
                            </div>

                        </div>

                         <div class="row centered">
                            &nbsp;
                        </div>
                            
                     

                        <!-- BOTONES-->
                        <div class="row centered">

                             <a class="ui-button" runat="server" id="mp_checkout" name="MP-Checkout" mp-mode="modal" onreturn="execute_my_onreturn">Pagar</a>     
                            <asp:Button ID="btn_cancelar" CssClass="ui-button" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btn_cancelar_Click" />

                        </div>

                    </div>
                </div>

            </asp:Panel>
        </div>




    </form>
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
     <script type="text/javascript">
     (function(){function $MPC_load(){window.$MPC_loaded !== true && (function(){var s = document.createElement("script");s.type = "text/javascript";s.async = true;s.src = document.location.protocol+"//secure.mlstatic.com/mptools/render.js";var x = document.getElementsByTagName('script')[0];x.parentNode.insertBefore(s, x);window.$MPC_loaded = true;})();}window.$MPC_loaded !== true ? (window.attachEvent ?window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null;})();
    </script>
  <script type="text/javascript">
      function execute_my_onreturn(json) {
          if (json.collection_status == 'approved') {
              location.href="AlumnoPagoFinalizado.aspx?Estado=ok"
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
