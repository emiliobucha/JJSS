<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Menu_Clase.aspx.cs" Inherits="JJSS.Presentacion.Menu_Clase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
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
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="form1" runat="server">



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
            <h1 class=" centered">Clases</h1>
        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <!---------------------------------**************Menu**********---------------------------------------->


        <div class="container ">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row  centered justify-content-center p-2">

                <div class="row centered">
                    <p>&nbsp;</p>
                </div>

                <%--<!--Incribir-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto">
                    <a class="text-dark" href="InscripcionTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Inscribir.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Incribir</h4>
                            </div>
                        </div>
                    </a>
                </div>--%>

                <!--Crear-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto " runat="server" id="crear_clase">
                    <a class="text-dark" href="CrearClase.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Crear2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Crear</h4>
                            </div>
                        </div>
                    </a>
                </div>

                 <!--Graduar-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto " runat="server" id="graduar_alumno" >
                    <a class="text-dark" href="/Presentacion/Clases/GraduarAlumno.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Graduar.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Graduar Alumnos</h4>
                            </div>
                        </div>
                    </a>
                </div>

                 <!--Asistencia-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto "  runat="server" id="asistencia">
                    <a class="text-dark" href="/Presentacion/Clases/RegistrarAsistencia.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Inscribir.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Registrar Asistencia</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Ver clases-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto "  runat="server" id="calendario">
                    <a class="text-dark" href="/Presentacion/Clases/VerCalendario.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Evento.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Ver Clases</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Ver listado asistencia-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto "  runat="server" id="listado_asistencia">
                    <a class="text-dark" href="/Presentacion/Clases/VerListadoAsistencia.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Historial.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Ver Listado de Asistencia</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Histórico-->
                <%-- <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="HistoricoTorneos.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Historial.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Histórico</h4>
                            </div>
                        </div>
                    </a>
                </div>--%>

                <%--<!--Ver-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="VerTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Ver.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Ver</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Pago-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="PagoClase.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Pago.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagar</h4>
                            </div>
                        </div>
                    </a>
                </div>--%>
            </div>

        </div>

            

        <h2 class=" centered mt  ">Clases Disponibles</h2>
       
        

        <div class="container ">
            <!-- Filtros  -->
                  <div class="row centered p-2">

                    <div class=" col-auto"><a>Nombre</a></div>
                    <div class="col-md-3 col-lg-3">
                        <asp:TextBox ID="txt_filtro_nombre" class="caja2" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-auto "><a>Academia</a></div>
                    <div class="col-md-2 col-lg-2">
                        <asp:DropDownList ID="ddl_academias" class="caja2" runat="server"></asp:DropDownList>
                    </div>

                    <div class="col-auto "><a>Profesor</a></div>
                    <div class="col-md-2 col-lg-2">
                        <asp:DropDownList ID="ddl_profesores" class="caja2" runat="server"></asp:DropDownList>
                    </div>

                    <div class="col justify-content-center ">
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btn_buscar_Click" />
                    </div>
                </div>
            

            <!---------------------------------**************Muestra de Clases************---------------------------------------->

            <div class=" row centered mt justify-content-center p-2" id="muetra_clases_profe_admin" runat="server">

                <asp:ListView ID="lv_clasesDisponibles" GroupPlaceholderID="groupPlaceHolder_clase" ItemPlaceholderID="itemPlaceHolder_clase" GroupItemCount="3" OnItemCommand="lv_clasesDisponibles_ItemCommand" runat="server">

                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder_clase"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>

                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder_clase"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>

                    <ItemTemplate>

                        <div class="col-lg-4 col-md-4  col-sm-12 p-4 "  runat="server">
                            <div runat="server">
                                <asp:Label ID="lv_lbl_nombre_clase" CssClass="h5" runat="server" Text='<%# Eval("nombre") %>' />
                            </div>
                            <%-- <div>                           
                                <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                            </div>--%>
                            <div runat="server">
                                Tipo de Clase:
                                       
                                        <asp:Label ID="lv_lbl_tipo_clase" runat="server" Text='<%# Eval("tipo_clase") %>' />
                            </div>
                            <div runat="server">
                                Ubicación:
                                       
                                        <asp:Label ID="lv_lbl_ubicacion" runat="server" Text='<%# Eval("ubicacion") %>' />
                            </div>
                            <div runat="server">
                                Profesor:
                                       
                                        <asp:Label ID="lv_lbl_profesor" runat="server" Text='<%# Eval("profesor") %>' />
                            </div>
                            <div runat="server">
                                Precio: $<asp:Label ID="lv_lbl_precio" runat="server" Text='<%# Eval("precio") %>' />

                            </div>
                            <div class="p-1" runat="server">
                                
                                <asp:Button ID="lv_btn_ver" Visible='<%#!(Convert.ToBoolean(Eval("MostrarEditar"))) %>' runat="server" CommandName="ver" CommandArgument='<%# Eval("id_clase") %>'  CssClass=" btn btn-outline-dark" Text="Ver" />
                                
                                <asp:Button ID="lv_btn_inscribir" Visible='<%#(Convert.ToBoolean(Eval("MostrarInscribir"))) %>' runat="server" CommandName="inscribir" CommandArgument='<%# Eval("id_clase") %>'  CssClass=" btn btn-outline-dark" Text="Inscribir" />
                      
                                <asp:Button ID="lv_btn_seleccionar" Visible='<%# (Convert.ToBoolean(Eval("MostrarEditar"))) %>' runat="server" CommandName="seleccionar" CommandArgument='<%# Eval("id_clase") %>' CssClass=" btn btn-outline-dark" Text="Editar" />
                    
                                <asp:LinkButton id="lv_btn_eliminar" Visible='<%# (Convert.ToBoolean(Eval("MostrarEditar"))) %>' CommandName ="eliminar" runat="server" CommandArgument ='<%# Eval("id_clase") %>' CssClass=" btn btn-outline-dark"
                                                    OnClientClick='<%# Eval("id_clase", "return openModal({0})") %>' > Eliminar</asp:LinkButton>
                            </div>
                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>No hay clases disponibles por el momento</h3>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>

            <!---------------FIN Cuadricula-------------------->




            <!---------------------------------**************Muestra de Clases Invitado************---------------------------------------->
            
            <div class=" row centered mt justify-content-center p-2" id="muetra_clases_invitado" runat="server">
                <asp:ListView ID="lv_clasesDisponibles_invitado" GroupPlaceholderID="groupPlaceHolder_clase" ItemPlaceholderID="itemPlaceHolder_clase" 
                    GroupItemCount="3" runat="server" OnItemCommand="lv_clasesDisponibles_invitado_ItemCommand">

                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder_clase"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>

                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder_clase"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>

                    <ItemTemplate>

                        <div class="col-lg-4 col-md-4  col-sm-12 p-1" runat="server">
                            <div>
                                <asp:Label ID="lv_lbl_nombre_clase" CssClass="h5" runat="server" Text='<%# Eval("nombre") %>' />
                            </div>
                            <%-- <div>                           
                                <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                            </div>--%>
                            <div>
                                Tipo de Clase:
                                       
                                        <asp:Label ID="lv_lbl_tipo_clase" runat="server" Text='<%# Eval("tipo_clase") %>' />
                            </div>
                            <div>
                                Ubicación:
                                       
                                        <asp:Label ID="lv_lbl_ubicacion" runat="server" Text='<%# Eval("ubicacion") %>' />
                            </div>
                            <div>
                                Profesor:
                                       
                                        <asp:Label ID="lv_lbl_profesor" runat="server" Text='<%# Eval("profesor") %>' />
                            </div>
                            <div>
                                Precio: $<asp:Label ID="lv_lbl_precio" runat="server" Text='<%# Eval("precio") %>' />

                            </div>
                            <div class="p-1" runat="server">
                                <asp:Button ID="lv_btn_verI" runat="server" CommandName="ver" CommandArgument='<%# Eval("id_clase") %>'  CssClass=" btn btn-outline-dark" Text="Ver" />
                            </div>
                             <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>No hay clases disponibles por el momento</h3>
                    </EmptyDataTemplate>
                </asp:ListView>

            </div>

             <!---------------FIN Cuadricula-------------------->
        </div>
       




        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea eliminar la clase?</h4>
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
