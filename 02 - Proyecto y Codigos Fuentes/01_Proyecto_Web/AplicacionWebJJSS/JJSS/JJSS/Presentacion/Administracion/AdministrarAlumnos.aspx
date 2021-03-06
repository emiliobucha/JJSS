﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarAlumnos.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarAlumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
    <script type='text/javascript'>
        var x = 0;
        function button() {
            var objwordstonum = document.getElementById('<%=txtIDSeleccionado.ClientID%>');
                objwordstonum.value = x;
                return true;
        }
        function openModal(id) {
            $('[id*=confirmacion]').modal('show');
            x = id;
            return false;
    }   
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
            <div class="row centered">
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
                        <h1>Listado de Alumnos</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                            <!--Boton-->
                            <div class="row p-2">
                                
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Tipo</strong>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>N° Doc</strong>
                                </div>

                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_dni" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        
                            <div class="row p-2">
                                <div class=" -lg-1 col-md-1 col-sm-12">
                                    <strong>Apellido</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Estado</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID="ddl_filtro_estado" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar" OnClick="btn_buscar_alumno_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="RegistrarAlumno.aspx"></asp:HyperLink>
                            </div>
                            <div>
                                <p>&nbsp;</p>
                            </div>
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="id_alumno" OnPageIndexChanging="gvAlumnos_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar"
                                    OnRowCommand="gvAlumnos_RowCommand" AllowPaging="True" PageSize="20" OnRowDataBound="gvAlumnos_RowDataBound">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="id_alumno" HeaderText="ID" SortExpression="id_alumno" Visible="False"/>
                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="tipo_documento" HeaderText="Tipo" SortExpression="tipo" />
                                        
                                        <asp:BoundField DataField="dni" HeaderText="N° Doc" SortExpression="dni" />
                                        <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar/Editar" ItemStyle-ForeColor="#007bff" HeaderText="Seleccionar/Editar" />
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="aa" CommandName="eliminar" runat="server" CommandArgument='<%#Eval("id_alumno") %>'
                                                    OnClientClick='<%# Eval("id_alumno", "return openModal({0})") %>'> Eliminar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_activar" CommandName="activar" runat="server"  CommandArgument='<%#Eval("id_alumno") %>'> Activar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class=" container">
                <div>
                    <p>&nbsp;</p>
                </div>
                <div class="row pull-left">
                    <div class="col">
                        <asp:LinkButton runat="server" ID="lnk_cancelar" class="btn btn-link " Text="Volver" href="Menu_Administracion.aspx"></asp:LinkButton>
                    </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>
            </div>
        </div>


        <!-- VENTANA EMERGENTE CARGA NUEVO PARTICIPANTE-->
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea eliminar el alumno?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <asp:button ID="btn_si" type="button" runat="server" class="btn btn-outline-dark" OnClientClick="return button()" OnClick="btn_si_Click1"  TExt="SI"/>
                        <Button ID="btn_no" type="button" class="btn btn-default"  value="No" data-dismiss="modal">No</button>

                    </div>

                </div>
            </div>
        </div>
        <asp:TextBox ID ="txtIDSeleccionado" runat="server" Text="" hidden="true"></asp:TextBox>
    </form>
</asp:Content>
