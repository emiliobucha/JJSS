<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PagosMes.aspx.cs" Inherits="JJSS.Presentacion.Pagos.PagosMes" %>

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
    <form id="formRegAlumno" runat="server">

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
            <div>
                <p>&nbsp;</p>
            </div>
        </asp:Panel>

        <div id="grillawrap">

            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Pagos por Periodo</h1>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div runat="server" class="row centered justify-content-center" id="divFiltros">


                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Fecha desde:</label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-12">

                            <asp:TextBox ID="dp_fecha_desde" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                        </div>


                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Fecha hasta: </label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-12">

                            <asp:TextBox ID="dp_fecha_hasta" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                        </div>

                        <!--Boton-->
                        <div class="col-md-1 col-xl-auto">

                            <asp:Button ID="btnBuscar" runat="server" formnovalidate="true" UseSubmitBehaviour="false" CausesValidation="false" Text="Buscar"
                                CssClass="btn btn-outline-dark" ValidationGroup="grupoDni" OnClick="btnBuscar_Click" />

                        </div>


                    </div>


                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">
                        <div class="row centered justify-content-center">
                            <h3>Total cobrado: $ <strong><asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></strong></h3> 
                        </div>
                        <div class="row centered justify-content-center">
                            <h3>Total pendiente: $ <strong><asp:Label ID="lbl_total_pendiente" runat="server" Text=""></asp:Label></strong></h3>
                        </div>
                    </div>
                    
                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">
                        <div class="row centered justify-content-center">
                            <h3>Cobros realizados</h3>
                        </div>
                        <div class="form-group border rounded p-4 ">


                            <div class="row justify-content-center">
                                <asp:GridView ID="gvPagos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Inscripcion"
                                    OnPageIndexChanging="gvPagos_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None"
                                    AllowPaging="True" PageSize="10"
                                    AutoGenerateColumns="False" EmptyDataText="No hay pagos para mostrar"
                                    OnRowDataBound="gvPagos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" SortExpression="fechaPago" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NombreParticipante" HeaderText="Nombre Completo" SortExpression="participante" />
                                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Doc." SortExpression="tipoDni" />
                                        <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="numero" />
                                        <asp:BoundField DataField="TipoPago.Tipo" HeaderText="Tipo Act." SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Act." SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Act." SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <%-- <asp:BoundField DataField="DescripcionObjeto"  HeaderText="Descripción" SortExpression="descripcion" />--%>
                                        <asp:BoundField DataField="MontoString" HeaderText="Monto" SortExpression="monto"  />
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>



                    </div>
                    <div class="container">
                        <div class="row justify-content-center">
                            <h3>Pendientes de cobro</h3>
                        </div>
                        <div class="form-group border rounded p-4 ">


                            <div class="row justify-content-center">
                                <asp:GridView ID="gvPendientes" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Inscripcion"
                                    OnPageIndexChanging="gvPendientes_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None"
                                    AllowPaging="True" PageSize="10"
                                    AutoGenerateColumns="False" EmptyDataText="No hay pagos para mostrar"
                                    OnRowDataBound="gvPagos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="NombreParticipante" HeaderText="Nombre Completo" SortExpression="participante" />
                                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Doc." SortExpression="tipoDni" />
                                        <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="numero" />
                                        <asp:BoundField DataField="TipoPago.Tipo" HeaderText="Tipo Act." SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Act." SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Act." SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <%--                                        <asp:BoundField DataField="DescripcionObjeto"  HeaderText="Descripción" SortExpression="descripcion" />--%>
                                        <asp:BoundField DataField="MontoString" HeaderText="Monto" SortExpression="monto" />
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>



                    </div>
                    <div class="row">
                        <asp:HyperLink runat="server" Text="Volver" CssClass="btn btn-link" href="../MenuInicial.aspx"></asp:HyperLink>

                    </div>
                </div>
            </asp:Panel>
        </div>

    </form>
</asp:Content>
