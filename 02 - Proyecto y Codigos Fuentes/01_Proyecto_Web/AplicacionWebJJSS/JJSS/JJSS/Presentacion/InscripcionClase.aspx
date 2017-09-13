<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionClase.aspx.cs" Inherits="JJSS.Presentacion.InscripcionClase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
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
                BB
                <p>&nbsp;</p>
            </div>
        </asp:Panel>

        <div class="row centered">
            <h1>INSCRIPCION A CLASE</h1>
            <p>&nbsp;</p>
        </div>

        <div id="mostrarAlumnowrap">
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 centered center-block">

                <asp:Panel ID="pnl_datos_clase" runat="server">
                    <div class="centered">
                        <h3>DATOS DE LA CLASE</h3>
                        <p>&nbsp;</p>
                    </div>

                    <div style="border: 1px medium gray">
                        <div>
                            <asp:Label ID="lbl_nombre_clase" CssClass="h3" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="lbl_tipo_clase" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="lbl_ubicacion" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="lbl_profesor" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="lbl_precio" runat="server" />
                        </div>


                        <div class="centered center-block">

                            <!--OnRowDataBound="dg_horarios_RowDataBound"-->
                            <asp:GridView ID="dg_horarios" runat="server" DataKeyNames="id_horario" EmptyDataText="No hay horarios actualmente" AutoGenerateColumns="False" GridLines="None" CssClass="table" BorderColor="Black" BorderStyle="Double">

                                <Columns>
                                    <%--     <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Ubicacion" DataField="ubicacion"><HeaderStyle CssClass="text-center" /></asp:BoundField>                                    
                                    <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Profesor" DataField="profesor"><HeaderStyle CssClass="text-center" /></asp:BoundField>--%>
                                    <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Día" DataField="nombre_dia">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Desde" DataField="hora_desde">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </asp:Panel>
            

            </div>

            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 centered">
                <div>
                    <p>&nbsp;</p>
                </div>
            </div>


            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">

                <asp:Panel ID="pnl_listado_alumnos" runat="server">

                    <div class="centered">
                        <h3>LISTADO DE ALUMNOS</h3>
                        <p>&nbsp;</p>
                    </div>

                    <div>
                        <!--Boton-->
                        <div class="row centered">
                            <strong>DNI</strong>
                            <asp:TextBox ID="txt_filtro_dni" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="mayor_dni0" runat="server" ControlToValidate="txt_filtro_dni" CssClass="text text-danger" Display="Dynamic" ErrorMessage="El DNI debe ser un valor mayor a 0" Operator="GreaterThan" Type="Integer" ValidationGroup="vgFiltro" ValueToCompare="0"></asp:CompareValidator>
                            <asp:CompareValidator ID="menor_dni0" runat="server" ControlToValidate="txt_filtro_dni" CssClass="text text-danger" Display="Dynamic" ErrorMessage="DNI demasiado largo" Operator="LessThan" Type="Integer" ValidationGroup="vgFiltro" ValueToCompare="2147483647"></asp:CompareValidator>

                            <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar alumnos" OnClick="btn_buscar_alumno_Click" ValidationGroup="vgFiltro" CssClass="btn btn-default" />
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>
                            <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnPageIndexChanging="gvAlumnos_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar" OnRowCommand="gvAlumnos_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" SortExpression="nombre" />
                                    <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" SortExpression="apellido" />
                                    <asp:BoundField DataField="alu_dni" HeaderText="D.N.I" SortExpression="dni" />
                                    <asp:ButtonField CommandName="inscribir" Text="Inscribir" />
                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                <PagerSettings Mode="NextPrevious" Position="TopAndBottom" />
                            </asp:GridView>

                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_datos_alumnos" runat="server">

                      <div class="centered">
                        <h3>DATOS DEL ALUMNO</h3>
                        <p>&nbsp;</p>
                    </div>

                    <!--Nombre-->
                    <div class="row centered">
                        <div class="col-md-2"></div>
                        <div class="col-xs-2">
                            <label class="pull-left">Nombre</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_alumno_nombre" class="pull-left" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                    </div>

                     <!--Apellido-->
                    <div class="row centered">
                        <div class="col-md-2"></div>
                        <div class="col-xs-2">
                            <label class="pull-left">Apellido</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_alumno_apellido" class="pull-left" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                    </div>                       

                    <!-- DNI-->
                    <div class="row centered">
                        <div class="col-md-2"></div>
                        <div class="col-xs-2">
                            <label class="pull-left">DNI</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="lbl_alumno_dni" class="pull-left" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!-- Datos para seleccionar -->

                    <!--Faja-->
                    <div class="row centered">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <label class="pull-left">Faja:</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!--Boton para aceptar inscripcion-->
                    <div class="row centered">
                        <asp:Button ID="btn_aceptar_inscripcion" runat="server" Text="Aceptar" OnClick="btn_aceptar_inscripcion_Click" />
                    </div>


                </asp:Panel>

            </div>
        </div>


    </form>
</asp:Content>
