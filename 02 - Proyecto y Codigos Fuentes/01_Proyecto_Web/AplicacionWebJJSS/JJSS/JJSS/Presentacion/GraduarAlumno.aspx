<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="GraduarAlumno.aspx.cs" Inherits="JJSS.Presentacion.GraduarAlumno" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formGraduacion" runat="server">

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

        </asp:Panel>

        <div class="row mt centered">
            <p>&nbsp;</p>
            <h1>GRADUAR ALUMNOS</h1>
            <p>&nbsp;</p>
        </div>

        <div class="row mt centered">
            <p>&nbsp;</p>
            <h2>Filtros</h2>
            <div class="col-md-2">Disciplina</div>
            <div class="col-md-3">
                <asp:RadioButtonList ID="rb_tipo_clase" CssClass="caja2" runat="server">
                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btn_buscar_Click" CausesValidation="false" />
        </div>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view_elegir_graduacion" runat="server">


                <div class="row centered">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>

                <asp:GridView ID="gv_graduacion" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="idAlu" EmptyDataText="No existen elementos para esta selección">
                    <Columns>
                        <asp:BoundField DataField="alumno" HeaderText="Alumno" />
                        <asp:BoundField DataField="tipo" HeaderText="Disciplina" />
                        <asp:BoundField DataField="faja" HeaderText="Faja Actual" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha de Última Graduación" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_grados" runat="server" CssClass="caja2" Text="0" TextMode="Number"></asp:TextBox>
                                <asp:CompareValidator ID="mayor_grado" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_grados" Type="Integer" ErrorMessage="La cantidad de grados debe ser un valor mayor a 0" ValueToCompare="-1" Operator="GreaterThan" ValidationGroup="vg_grados"></asp:CompareValidator>
                                <asp:CompareValidator ID="menor_grado" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_grados" Type="Integer" ErrorMessage="La cantidad de grados debe ser un valor menor a 20" ValueToCompare="21" Operator="LessThan" ValidationGroup="vg_grados"></asp:CompareValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="table" />
                </asp:GridView>

                <div class="row centered">
                    <asp:Button ID="btn_aceptar" CssClass="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click"  ValidationGroup="vg_grados" />
                </div>
            </asp:View>

            <asp:View ID="view_confirmacion" runat="server">

                <div class="row centered">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>

                <div class="row centered">
                    &nbsp;
                </div>

                <div class="row centered">
                    <asp:Button ID="btn_guardar_contacto" CssClass="btn btn-default" ValidationGroup="vgContacto" runat="server" Text="Aceptar" />
                </div>
            </asp:View>

        </asp:MultiView>
        <div class="row centered">
            <asp:Button ID="btn_cancelar" runat="server" Text="Volver" CssClass="btn btn-default" OnClick="btn_cancelar_Click" CausesValidation="false" />
        </div>
    </form>


</asp:Content>
