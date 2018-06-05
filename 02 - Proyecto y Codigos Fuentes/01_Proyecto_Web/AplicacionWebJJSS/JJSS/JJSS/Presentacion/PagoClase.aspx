<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PagoClase.aspx.cs" Inherits="JJSS.Presentacion.PagoClase" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="pagoClase" title="pagoClase"></section>
    <form id="formPagoClase" runat="server">

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
            <h1 class="centered">Registro de Pago de Clase</h1>
        </div>

        <div>
            &nbsp;
        </div>

        <div class="container ">

            <asp:Panel ID="pnlPago" CssClass="panel panel-default p-1 " runat="server">

                <div class="centered justify-content-center border rounded p-4">

                    <div class="row justify-content-center p-1">
                        <h2>
                            <asp:Label ID="lbl1" runat="server" Text="Alumno: "></asp:Label>
                            <asp:Label ID="lbl_alumno" runat="server" Text="No hay alumno seleccionado"></asp:Label></h2>
                        <p>&nbsp;</p>
                    </div>

                    <!-- CLASES-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label>Clase:&nbsp;</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="ddl_clase" runat="server" CssClass="caja2" OnSelectedIndexChanged="ddl_clase_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>


                    <!-- MES-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label>Mes a abonar:&nbsp;</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="ddl_mes" runat="server" CssClass="caja2">
                                <asp:ListItem>Enero</asp:ListItem>
                                <asp:ListItem>Febrero</asp:ListItem>
                                <asp:ListItem>Marzo</asp:ListItem>
                                <asp:ListItem>Abril</asp:ListItem>
                                <asp:ListItem>Mayo</asp:ListItem>
                                <asp:ListItem>Junio</asp:ListItem>
                                <asp:ListItem>Julio</asp:ListItem>
                                <asp:ListItem>Agosto</asp:ListItem>
                                <asp:ListItem>Septiembre</asp:ListItem>
                                <asp:ListItem>Octubre</asp:ListItem>
                                <asp:ListItem>Noviembre</asp:ListItem>
                                <asp:ListItem>Diciembre</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>


                    <!-- MONTO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label>Importe:&nbsp;</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_monto" runat="server" CssClass="caja2"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            <asp:RequiredFieldValidator ID="requerido_monro" runat="server" ErrorMessage="Debe ingresar un monto" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_monto" ValidationGroup="vgRegistrarPago"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regex_monto" runat="server" ControlToValidate="txt_monto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido del monto" ValidationGroup="vgRegistrarPago" ValidationExpression="^[0-9]{0,16}(,?[0-9][0-9]{0,1})$"></asp:RegularExpressionValidator>
                        </div>
                    </div>


                    <!-- FORMA PAGO-->
                    <div class="row justify-content-center p-1">
                        <div class="col-xs-2">
                            <label>Forma de pago:&nbsp;</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="ddl_forma_pago" runat="server" CssClass="caja2"></asp:DropDownList>
                        </div>
                        <div class="col-xs-3">
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
                    <%--<asp:Button ID="btn_cancelar" runat="server"Text="Volver a inicio" CssClass="btn-link" CausesValidation="false" OnClick="btn_cancelar_Click1" />--%>
                    <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CssClass=" btn btn-outline-dark" ValidationGroup="vgRegistrarPago" OnClick="btn_aceptar_Click" />
                </div>
            </div>
        </div>




    </form>
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
     <script type="text/javascript">
     (function(){function $MPC_load(){window.$MPC_loaded !== true && (function(){var s = document.createElement("script");s.type = "text/javascript";s.async = true;s.src = document.location.protocol+"//secure.mlstatic.com/mptools/render.js";var x = document.getElementsByTagName('script')[0];x.parentNode.insertBefore(s, x);window.$MPC_loaded = true;})();}window.$MPC_loaded !== true ? (window.attachEvent ?window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null;})();
    </script>
   

   

</asp:Content>
