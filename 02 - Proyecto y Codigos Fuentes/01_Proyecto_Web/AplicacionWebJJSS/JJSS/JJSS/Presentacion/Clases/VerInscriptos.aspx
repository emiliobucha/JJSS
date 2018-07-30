<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerInscriptos.aspx.cs" Inherits="JJSS.Presentacion.Clases.VerInscriptos" %>


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
            <h1 class=" centered">Listado de Inscripciones</h1>
        </div>

        <div class="row centered justify-content-center " runat="server" id="div_nombre_clase">
            <div>
                <asp:Label ID="lbl_nombre_clase" CssClass="h3" runat="server" />
            </div>
        </div>


        <div class="row centered justify-content-center " runat="server" id="div_combo_clase" visible="false">
            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                <strong>Clase</strong>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <asp:DropDownList ID="ddl_clase" class="caja2" runat="server" placeholder="Clases"></asp:DropDownList>
            </div>
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

                <asp:Button ID="btn_buscar" runat="server" formnovalidate="true" UseSubmitBehaviour="false" CausesValidation="false" Text="Buscar"
                    CssClass="btn btn-outline-dark" ValidationGroup="grupoDni" OnClick="btn_buscar_Click" />

            </div>


        </div>


        <div>
            <p>&nbsp;</p>
        </div>






        <asp:Panel ID="pnl_listado" runat="server">
            <div class="container">
                <div class=" row centered justify-content-center" runat="server">
                    <div class="form-group border rounded p-4">
                        <asp:GridView ID="gv_inscripciones" runat="server" DataKeyNames="inscr_id_alumno" CssClass="table table-hover"
                            OnRowCommand="gv_inscripciones_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                            EmptyDataText="No hay alumnos para mostrar" AllowPaging="True"
                            OnPageIndexChanging="gv_participantes_PageIndexChanging" PageSize="30">
                            <Columns>
                                <asp:BoundField DataField="inscr_apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="inscr_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="inscr_sexo" HeaderText="Sexo" />
                                <asp:BoundField DataField="inscr_tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="inscr_dni" HeaderText="Documento" />
                                <asp:BoundField DataField="inscr_faja" HeaderText="Faja" />
                                <asp:BoundField DataField="inscr_fecha_vto_mensual" HeaderText="Prox Vto" />
                                <asp:BoundField DataField="inscr_pago" HeaderText="Pagó mes" />
                                <asp:BoundField DataField="inscr_recargo" HeaderText="Moroso" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="aa" CommandName="modificar" runat="server" CommandArgument='<%#Eval("inscr_id_alumno") %>'> Modificar Vto</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                                <asp:ButtonField CommandName="modificar" Text="Modificar Vto" HeaderText="" />--%>
                            </Columns>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                        </asp:GridView>
                    </div>

                </div>
                <%-- <div class="row centered justify-content-center ">
                    <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" CssClass="btn btn-outline-dark" OnClick="btn_imprimir_Click" Visible="true" />
                </div>--%>
                <div class="row centered p-2">
                    <div class="row centered">
                        <div class="col col-auto">
                            <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Content>
