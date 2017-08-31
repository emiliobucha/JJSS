<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="GraduarAlumno.aspx.cs" Inherits="JJSS.Presentacion.GraduarAlumno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>
</asp:Content>
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
            <h1>GRADUAR ALUMNOS</h1>
            <p>&nbsp;</p>
        </div>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view_elegir_graduacion" runat="server">


                <div class="row centered">
                    
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>
                
                <asp:GridView ID="gv_graduacion" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="idAlu">
                    <Columns>
                        <asp:BoundField DataField="alumno" HeaderText="Alumno" />
                        <asp:BoundField DataField="tipo" HeaderText="Disciplina" />
                        <asp:BoundField DataField="faja" HeaderText="Faja Actual" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha de Última Graduación" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_grados" runat="server" CssClass="caja2" Text="0"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="row centered">
                    <asp:Button ID="btn_aceptar" CssClass="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" />
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
            <asp:Button ID="btn_cancelar" runat="server" Text="Volver" CssClass="btn btn-default" OnClick="btn_cancelar_Click" />
        </div>
    </form>


</asp:Content>
