<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="GestionarAlumnos.aspx.cs" Inherits="JJSS.Presentacion.GestionarAlumnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="registrarAlumno" title="registrarAlumno"></section>

    <form id="formRegAlumno" runat="server">

        <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

            <div id="mostrarAlumnowrap">

                <div class="container">
                    <div class="row mt centered">
                        <h1>LISTADO DE ALUMNOS</h1>
                        <p>&nbsp;</p>
                    </div>
                    <div class="form-group ">
                        <!--Boton-->
                        <div class="row centered">
                            <strong>Apellido a buscar:</strong>
                            <asp:TextBox ID="txt_filtro_apellido" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar alumnos" OnClick="btn_buscar_alumno_Click" CausesValidation="false" CssClass="btn btn-default" />

                            <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnSelectedIndexChanged="gvAlumnos_SelectedIndexChanged" OnPageIndexChanging="gvAlumnos_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                <Columns>

                                    <asp:CommandField HeaderText="Eliminar" SelectText="Eliminar" ShowCancelButton="True" ShowDeleteButton="False" ShowSelectButton="True" />
                                    <asp:BoundField DataField="alu_dni" HeaderText="D.N.I" SortExpression="dni" />
                                    <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" SortExpression="apellido" />
                                    <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" SortExpression="nombre" />
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Content>

