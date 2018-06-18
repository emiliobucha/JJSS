<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerListadoAsistencia.aspx.cs" Inherits="JJSS.Presentacion.Clases.VerListadoAsistencia" %>

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
            <h1 class=" centered">Listado de Asistencia</h1>
        </div>



        <div class="container ">
            <!-- Filtros  -->
            <div class="row centered p-2">

                <div class=" col-auto"><a>Clase</a></div>
                <div class="col-md-3 col-lg-3">
                    <asp:DropDownList ID="ddl_clases" class="caja2" runat="server"></asp:DropDownList>
                </div>

                <div class="col-auto "><a>Fecha</a></div>
                <div class="col-md-2 col-lg-2">
                    <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$"></asp:TextBox>
                </div>

                <div class="col justify-content-center ">
                    <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btn_buscar_Click" />
                </div>
            </div>

            <h2 class=" centered mt  ">
                <asp:Label ID="lbl_datos_clase" runat="server" Text=""></asp:Label>
            </h2>
            <p>&nbsp;</p>
            <h3 class="centered mt">
                <asp:Label ID="lbl_hora" runat="server" Text=""></asp:Label>
            </h3>


            <div class=" row mt centered justify-content-center  p-1" runat="server">
                <div class="col-12">
                    <asp:GridView ID="gv_asistencia" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar" AllowPaging="True" OnPageIndexChanging="gv_asistencia_PageIndexChanging" PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="alu_dni" HeaderText="Documento" />
                            <asp:BoundField DataField="alu_telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="alu_sexo" HeaderText="Sexo" />
                            <asp:BoundField DataField="alu_horaT" HeaderText="Hora Ingreso" />
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                    </asp:GridView>
                </div>
                <div class="col justify-content-center ">
                    <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" CssClass="btn btn-outline-dark" OnClick="btn_imprimir_Click" Visible="false" />
                </div>
            </div>
            <div class="row centered p-2">
                <div class="row centered">
                    <div class="col col-auto">
                        <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                    </div>
                </div>

            </div>

        </div>
    </form>
</asp:Content>
