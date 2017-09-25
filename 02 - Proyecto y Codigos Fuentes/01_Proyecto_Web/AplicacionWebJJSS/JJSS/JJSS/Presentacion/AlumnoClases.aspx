
<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="AlumnoClases.aspx.cs" Inherits="JJSS.Presentacion.AlumnoClases" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="alumnoClases" title="alumnoClases"></section>
    <form id="formAlumnoclases" runat="server">
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

            <asp:Panel ID="pnlClases" runat="server">
                <div class="container">
                    <div class="row mt centered">

                        <h1>CLASES</h1>
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group">
                        <div id="grillawrap">
                            <asp:Panel ID="pnl_mostrar_clases" runat="server">

                                <div id="alumnosClases">

                                    <div class="container">
                                        <div class="form-group ">
                                            <!--Boton-->
                                            <div class="row centered">

                                                <asp:GridView ID="gvClases" runat="server" CssClass="table" CellPadding="4" DataKeyNames="id_clase" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay clases para mostrar" OnRowCommand="gvClases_RowCommand">
                                                    <Columns>
                                                      <%--  <asp:BoundField DataField="id_clase" HeaderText="ID de clase" />--%>
                                                        <asp:BoundField DataField="nombre" HeaderText="Clase" />
                                                        <asp:BoundField DataField="tipo_clase" HeaderText="Tipo de Clase" />
                                                        <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" />
                                                        <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                                                        <asp:BoundField DataField="precio" HeaderText="Precio" />
                                                        <asp:ButtonField CommandName="pago" Text="Registrar pago" />
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                                   
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>


                        </div>

                    </div>
                </div>

            </asp:Panel>
        </div>




    </form>
    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
    <script type="text/javascript">
        (function () { function $MPC_load() { window.$MPC_loaded !== true && (function () { var s = document.createElement("script"); s.type = "text/javascript"; s.async = true; s.src = document.location.protocol + "//secure.mlstatic.com/mptools/render.js"; var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(s, x); window.$MPC_loaded = true; })(); } window.$MPC_loaded !== true ? (window.attachEvent ? window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null; })();
    </script>




</asp:Content>
