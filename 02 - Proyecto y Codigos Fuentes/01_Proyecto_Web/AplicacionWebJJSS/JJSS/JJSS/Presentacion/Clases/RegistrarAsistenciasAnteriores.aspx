<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAsistenciasAnteriores.aspx.cs" Inherits="JJSS.Presentacion.Clases.RegistrarAsistenciasAnteriores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    maxDate: new Date,
                    dateFormat: "dd/mm/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
        );

        function SelectAll(chkbox) {
            var chk = document.getElementById('<%=chkSelectAll.ClientID%>');
            var grid = document.getElementById("<%= gv_inscriptos.ClientID %>");
            var cell;
            if (chk.checked == true) {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        for (var k = 0; k < grid.rows[i].cells.length; k++) {
                            cell = grid.rows[i].cells[k];
                            for (j = 0; j < cell.childNodes.length; j++) {
                                if (cell.childNodes[j].type == "checkbox") {
                                    cell.childNodes[j].checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        for (var k = 0; k < grid.rows[i].cells.length; k++) {
                            cell = grid.rows[i].cells[k];
                            for (j = 0; j < cell.childNodes.length; j++) {
                                if (cell.childNodes[j].type == "checkbox") {
                                    cell.childNodes[j].checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }
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
            <h1 class=" centered">Registrar Asistencias</h1>
        </div>

        <div>
            <p>&nbsp;</p>
        </div>

        <div class="container  ">
            <div class="border rounded p-4">

                <div class="row centered p-2">
                    <div class="col col-1">
                          <a>Clase</a>  
                    </div>
                    <div class="col-md-3 col-lg-3 col-sm-12">
                        <asp:DropDownList ID="ddl_clases" class="caja2" runat="server"></asp:DropDownList>
                    </div>

                    <div class="col col-auto text-left "><a>Fecha</a></div>
                    <div class="col-md-2 col-lg-2 col-sm-12">
                        <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" required="true" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$"></asp:TextBox>
                    </div>

                    <div class="col-md-2 col-lg-2 col-sm-12 centered p-sm-1">
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btn_buscar_Click" />
                    </div>
                </div>

                <%--<div class=" row mt centered justify-content-center  p-1" runat="server">
                <h2 class=" centered mt  ">
                    <asp:Label ID="lbl_datos_clase" runat="server" Text=""></asp:Label>
                </h2>

                <div class="centered">
                    <asp:Label ID="lbl_academina" runat="server" Text=""></asp:Label>
                </div>

                <div class="centered">
                    <asp:Label ID="lbl_direccion" runat="server" Text=""></asp:Label>
                </div>
                <h3 class="centered mt">
                    <asp:Label ID="lbl_hora" runat="server" Text=""></asp:Label>
                </h3>
            </div>--%>

                <div class="row pull-right">
                    <div class="col col-auto">
                        <input type="checkbox" id="chkSelectAll" runat="server" onclick="SelectAll('chkSelectAll')" visible="false" value="Seleccionar Todos" />
                        <asp:Label ID="lbl_select_all" runat="server" Visible="false" Text="Seleccionar Todos"></asp:Label>
                    </div>
                </div>

                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="row centered justify-content-center  p-1" runat="server">
                    <div class="col-12">
                        <asp:GridView ID="gv_inscriptos" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar" AllowPaging="True"
                            OnPageIndexChanging="gv_inscriptos_PageIndexChanging" PageSize="20" OnRowDataBound="gv_inscriptos_RowDataBound" DataKeyNames="alu_id">
                            <Columns>
                                <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" />
                               <asp:BoundField DataField="alu_tipo_documento" HeaderText="Tipo" />
                                <asp:BoundField DataField="alu_documento" HeaderText="Documento" />                
                                <asp:TemplateField HeaderText="Asistió" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_asistio" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="inscr_id" HeaderText="Documento"  Visible="False"/>
                            </Columns>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                        </asp:GridView>
                    </div>
                    <div class="col justify-content-center ">
                        <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CssClass="btn btn-outline-dark" OnClick="btn_aceptar_Click" Visible="false" />
                    </div>
                </div>
            </div>

            <div class="row pull-left p-2">
                <div class="row centered">
                    <div class="col col-auto">
                        <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                    </div>
                </div>

            </div>
            <div>
                <p>&nbsp;</p>
            </div>

        </div>


    </form>
</asp:Content>
