<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionClase.aspx.cs" Inherits="JJSS.Presentacion.InscripcionClase" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="formRegAlumno" runat="server">
        <div class="container">


            <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                <div class="col-md-2 hidden-xs"></div>
                <div class="col-md-8 col-xs-12 col-sm-12">
                    <div class="alert alert-success alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <div>
                            <a class="ui-icon ui-icon-check"></a>
                            <strong>
                                <asp:Label ID="lbl_exito" runat="server" Text=""></asp:Label>
                                <br />
                            </strong>
                        </div>

                        <asp:Panel ID="pnl_comprobante" runat="server" Visible="False">
                            <a class="ui-icon ui-icon-check"></a>
                            <strong>Descargar comprobante <a runat="server" id="btn_descargar">Aquí</a>
                                <br />
                            </strong>
                        </asp:Panel>

                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
                <div class="col-md-2 hidden-xs"></div>
                <div class="col-md-8 col-xs-12">
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


            <div class="row centered">
                <p>&nbsp;</p>
            </div>
            <div class="row centered justify-content-center p-4">
                <h1>Inscripción a Clase</h1>
            </div>
            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row container " id="mostrarAlumnowrap">

                <div class="col-lg-4 col-md-4 col-sm-12 centered ">

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
                                        <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Desde (hs)" DataField="hora_desde">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Hasta (hs)" DataField="hora_hasta">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </asp:Panel>


                </div>

                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 centered ">
                    <div>
                        <p>&nbsp;</p>
                    </div>
                </div>

                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 ">

                    <asp:Panel ID="pnl_listado_alumnos" runat="server">

                        <div class="centered">
                            <h3>LISTADO DE ALUMNOS</h3>
                            <p>&nbsp;</p>
                        </div>

                        <div>
                            <!--Boton-->
                            <div class="row ">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Tipo</strong>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>N° Doc</strong>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_filtro_dni"  CssClass="form-control"  runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                    <strong class="text-left">Apellido</strong>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_filtro_apellido" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12 centered">
                                    <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar" OnClick="btn_buscar_alumno_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="row centered">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 centered">

                                    <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_id" OnPageIndexChanging="gvAlumnos_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar" OnRowCommand="gvAlumnos_RowCommand" AllowPaging="True" PageSize="20" OnRowDataBound="gvAlumnos_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="alu_apellido" HeaderText="Apellido"/>
                                            <asp:BoundField DataField="alu_nombre" HeaderText="Nombre"/>
                                            <asp:BoundField DataField="alu_tipoDocumento" HeaderText="Tipo de Documento"/>
                                            <asp:BoundField DataField="alu_dni" HeaderText="Número de Documento"/>
                                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_inscribir" runat="server" class="btn btn-link" CommandName="inscribir" CommandArgument=<%# Eval("alu_id") %>/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_desinscribir" runat="server" class="btn btn-link" CommandName="desinscribir" CommandArgument=<%# Eval("alu_id") %>/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="inscripto" HeaderText="" Visible="false"/>
                                        </Columns>
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                        <PagerSettings Position="TopAndBottom" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 centered">
                                <asp:Button ID="btn_registrar_alumno" CssClass="btn btn-link" runat="server" formnovalidate="true" Text="Registrar nuevo alumno" CausesValidation="false" OnClick="btn_registrar_alumno_Click" />
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="pnl_datos_alumnos" runat="server">
                        
                      
                        <div class="centered">
                            <h3>DATOS DEL ALUMNO</h3>
                            <p>&nbsp;</p>
                        </div>
                        <asp:Label ID="txtIdAlumno" class="" Visible="False" runat="server" Text=""></asp:Label>

                        <!--Nombre-->
                        <div class="row justify-content-center pt-1">
                            <div class="col col-sm-1 col-md-2 col-lg-2">
                                <label>Nombre: </label>
                            </div>
                            <div class="col col-sm-6 col-md-4 col-lg-4">
                                <asp:Label ID="lbl_alumno_nombre" class="" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <!--Apellido-->
                        <div class="row justify-content-center   pt-1">
                            <div class="col col-sm-1 col-md-2 col-lg-2">
                                <label>Apellido: </label>
                            </div>
                            <div class="col col-sm-6 col-md-4 col-lg-4">
                                <asp:Label ID="lbl_alumno_apellido" class="" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <!-- DNI-->
                        <div class="row justify-content-center   pt-1">
                            <div class="col col-sm-1 col-md-2 col-lg-2">
                                <label>Documento: </label>
                            </div>
                            <div class="col col-sm-6 col-md-4 col-lg-4">
                                <asp:Label ID="lbl_alumno_tipoDoc" class="" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl_alumno_dni" class="" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <!-- Datos para seleccionar -->

                        <!--Faja-->
                        <div class="row justify-content-center   pt-1">
                            <div class="col col-sm-1 col-md-2 col-lg-2">
                                <label>Faja:</label>
                            </div>
                            <div class="col col-sm-6 col-md-4 col-lg-4">
                                <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Boton para aceptar inscripcion-->
                        <div class="row centered">
                            <div class=" col-12">
                                <asp:Button CssClass="btn btn-outline-dark" ID="btn_aceptar_inscripcion" runat="server" formnovalidate="true" Text="Aceptar" OnClick="btn_aceptar_inscripcion_Click" />
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row text-left">
                            <div class=" col-3">
                                <asp:Button CssClass="btn btn-link" ID="btn_alumnos" runat="server" Text="Volver a Alumnos" formnovalidate="true" OnClick="btn_alumnos_Click" />
                            </div>
                        </div>

                    </asp:Panel>


                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
                <asp:Button ID="btn_Cancelar" runat="server" Text="Volver" CssClass="btn btn-link" CausesValidation="false" formnovalidate="true" OnClick="btn_Cancelar_Click1" />
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </div>

        </div>
    </form>
</asp:Content>
