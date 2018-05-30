﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Menu_Clase.aspx.cs" Inherits="JJSS.Presentacion.Menu_Clase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
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
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="CrearClase.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Crear2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Crear</h4>
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
                                <asp:Button ID="lv_btn_inscribir" runat="server" CommandName="inscribir" CommandArgument='<%# Eval("id_clase") %>'  CssClass=" btn btn-outline-dark" Text="Inscribir" />
                      
                                <asp:Button ID="lv_btn_seleccionar" runat="server" CommandName="seleccionar" CommandArgument='<%# Eval("id_clase") %>' CssClass=" btn btn-outline-dark" Text="Editar" />
                    
                                <asp:Button ID="lv_btn_eliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id_clase") %>'  CssClass=" btn btn-outline-dark" Text="Eliminar" />
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
                <asp:ListView ID="lv_clasesDisponibles_invitado" GroupPlaceholderID="groupPlaceHolder_clase" ItemPlaceholderID="itemPlaceHolder_clase" GroupItemCount="3" runat="server">

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

    </form>


</asp:Content>