<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PagosCambioVto.aspx.cs" Inherits="JJSS.Presentacion.Pagos.PagosCambioVto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "dd/mm/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
        );
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form runat="server">
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
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


        <div class="row centered justify-content-center ">
            <h1 class=" centered">Cambio de Fecha de Vencimiento</h1>
        </div>

        <div runat="server" id="div_nombre_alumno">
            <div class="row centered justify-content-center ">
                <div>
                    <asp:Label ID="lbl_nombre_alumno" runat="server" Text="AAAAAAA" />
                </div>
            </div>
            <div class="row centered justify-content-center " runat="server" id="div1">
                <div>
                    <asp:Label ID="lbl_tipo_dni" runat="server" Text="AAAAAAA" />
                </div>
            </div>
            <div class="row centered justify-content-center " runat="server" id="div2">
                <div>
                    <asp:Label ID="lbl_dni" runat="server" Text="AAAAAAA" />
                </div>
            </div>
        </div>

        <div class="row centered justify-content-center " runat="server" id="div_nombre_clase">
            <div>
                <asp:Label ID="lbl_clase_nombre" runat="server" Text="AAAAAAA" />
            </div>
        </div>


        <!--Boton-->
        <div class="row centered justify-content-center " runat="server" id="div_combo_clase" Visible="False">
            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                <strong>Clase</strong>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <asp:DropDownList ID="ddl_clase" class="caja2" runat="server" placeholder="Clases"></asp:DropDownList>
            </div>

            <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12 centered">
                <asp:Button ID="btn_buscar" runat="server" Text="Buscar" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
            </div>
        </div>



        <br />
        <br/>
        <asp:Panel ID="pnl_fecha_vto" runat="server">
            <div class="row centered justify-content-center">
                <div class="col-lg-2 col-md-2 col-sm-12">
                    <label class="text-left">Fecha de Vencimiento <a class="text-danger">*</a></label>
                </div>
                <div class="col col-lg-3 col-md-3 col-sm-12">

                    <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                </div>
            </div>


            <br/>
            <div runat="server" class="row centered justify-content-center p-1">
                <div class="col col-auto" runat="server">
                    <asp:Button ID="btn_guardar" formnovalidate="true" CausesValidation="false" runat="server" Text="Guardar" CssClass="btn btn-outline-dark" OnClick="btn_guardar_OnClick"/>

                </div>
                <div runat="server" class="col col-auto">
                    <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="../Clases/VerInscriptos.aspx"></asp:HyperLink>
                </div>
            </div>

        </asp:Panel>
    </form>
</asp:Content>


