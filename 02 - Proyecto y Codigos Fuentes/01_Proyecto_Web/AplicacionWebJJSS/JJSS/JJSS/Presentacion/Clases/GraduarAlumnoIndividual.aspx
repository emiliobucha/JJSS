<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="GraduarAlumnoIndividual.aspx.cs" Inherits="JJSS.Presentacion.Clases.GraduarAlumnoIndividual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
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

        <div id="container">

            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Graduar Alumno</h1>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div runat="server" class="row centered justify-content-center">

                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Documento:</label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblDni"></label>
                        </div>

                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Nombre Completo:</label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblNombre"></label>
                        </div>


                    </div>

                    <div runat="server" class="row centered justify-content-center">
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Disciplina: </label>
                        </div>
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left" runat="server" id="lbl_disciplina"></label>
                        </div>

                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Faja Actual: </label>
                        </div>
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left" runat="server" id="lbl_faja_actual"></label>
                        </div>
                    </div>

                    <div runat="server" class="row centered justify-content-center">
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Grados a aumentar: </label>
                        </div>
                        <div class="col-md-2 col-xl-auto">
                            <asp:TextBox runat="server" ID="txt_grados" CssClass="caja2" type="number" step="1" Text="0" required="true"> </asp:TextBox>
                        </div>
                    </div>



                    <div class="row centered justify-content-center p-1">
                        <div class="col-xl-auto">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-outline-dark" OnClick="btnAceptar_Click"/>
                        </div>
                    </div>
                </div>

                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="col-xl-auto">
                    <asp:HyperLink runat="server" href="GraduarAlumno.aspx" CssClass="btn btn-link"> Volver</asp:HyperLink>
                </div>

            </asp:Panel>
        </div>

    </form>
</asp:Content>
