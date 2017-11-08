<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="JJSS.Presentacion.Inicio" %>


<asp:Content ID="InicioMenu" ContentPlaceHolderID="cphMenu" runat="server">


    <a href="#section_home">
        <span class="glyphicon glyphicon-chevron-right"></span>
        INICIO
    </a>
    <a runat="server" id="menuSobreNostros" href="#section_about" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Sobre Nosotros
    </a>
    <a runat="server" id="menuTorneos"  href="#section_torneos" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Torneos
    </a>
    <a runat="server" id="menuClases"  href="#section_clases" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Clases
    </a>
    <a  runat="server" id="menuAlumnos"  href="#section_alumnos" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Alumnos
    </a>
    <a runat="server" id="menuProfesores"  href="#section_profesores" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Profesores
    </a>
    <a runat="server" id="menuTienda"  href="#section_tienda" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Tienda
    </a>
    <a runat="server" id="menuEventos"  href="#section_eventos" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Eventos
    </a>
    <a runat="server" id="menuPermisos"  href="#section_permisos" class="smoothScroll">
        <span class="glyphicon glyphicon-chevron-right"></span>
        Perfiles y Permisos
    </a>

</asp:Content>



<asp:Content ID="InicioEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">

    <section  id="section_home" title="home"></section>
    <img src="../img/logo_mariano.png" class="hidden-lg hidden-md img-responsive" />
    <div id="headerwrap2" class="hidden-xs hidden-sm"></div>
    <! --/headerwrap -->
</asp:Content>



<asp:Content ID="InicioContenido" ContentPlaceHolderID="cphContenido" runat="Server">

    <section id="section_about" title="about"></section>

    <div class="row centered">
        <p>&nbsp;</p>
    </div>

    <!--PRESENTACION DE LA ACADEMIA-->
    <div id="aboutwrap">
        <div class="container">
            <div class="row">
                <div class="col-lg-4 name">
                    <img class="img-responsive" src="../img/Mariano.jpg">
                    <p>MARIANO HINOJAL</p>
                    <div class="name-label"></div>
                </div>
                <! --/col-lg-4-->
			
                <div class="col-lg-8 name-desc">
                    <h2>LOTUS CLUB
                       
                        <br />
                        equipo HINOJAL</h2>
                    <div class="name-zig"></div>

                    <div class="col-md-6">
                        <p align="justify">Somos una academia de artes marciales dedicada a la difusión y aprendizaje de las actividades combate cuerpo a cuerpo, sistemas de defensa personal y entrenamiento físico.</p>
                        <p align="justify">La actividad con mayor desarrollo es el Brazilian Jiu-Jitsu, en la cual, su profesor Mariano Hinojal, cinturón negro 1 grado, brinda todos los aspectos técnicos necesario para realizarla de maneras amateur o profesional y de alto nivel competitivo. Su bagaje pedagógico viene de 14 años de práctica y enseñanza constante.</p>
                    </div>
                    <div class="col-md-6">
                        <p align="justify">Se encuentra desde el año 2008 en la ciudad de Córdoba, en la zona céntrica, y actualmente cuenta con tres lugares más de enseñanza en la ciudad. Además, se imparten clases en Río Ceballos, Villa Carlos Paz y Morteros, como así también en la ciudad de Rosario. La academia Lotus Club tiene su casa central en Sao Paulo, Brasil. La misma cuenta con sucursales en toda Argentina, además de academias en Nueva York y distintas ciudades de Europa. Con lo cual, brinda un respaldo de seriedad, confianza y trabajo responsable en la enseñanza del Brazilian Jiu-Jitsu.</p>
                    </div>

                </div>
                <! --/col-lg-8-->
		
           
            </div>
            <!-- /row -->
        </div>
        <!-- /container -->
    </div>
    <!-- /aboutwrap -->

    <!-- ABOUT SEPARATOR -->

    <form runat="server">

        <!-- ABOUT SEPARATOR -->
        <div class="hidden-sm hidden-xs">
            <div class="sep paraTorneos" data-stellar-background-ratio="0.5"></div>
        </div>

        <!--SECTOR TORNEOS -->

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <section id="section_torneos" title="torneos"></section>
        <div id="torneoswrap">

            <!--APARTADO DE TORNEOS FUTUROS-->
            <div class="container">
                <div class="row mt centered">

                    <h1>ÚLTIMOS TORNEOS</h1>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                    <!---------------------------------**************Muestra de Torneos************---------------------------------------->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:ListView ID="lv_torneos_abiertos" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1" GroupItemCount="3" runat="server" OnItemCommand="lv_torneos_abiertos_ItemCommand">

                            <LayoutTemplate>
                                <table>
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>

                            <GroupTemplate>
                                <tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                </tr>
                            </GroupTemplate>

                            <ItemTemplate>

                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                                    <div>
                                        <asp:Label ID="lv_lbl_nombre" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                                    </div>
                                    <div>
                                        <%--   <img src="../img/Imagen%20por%20Defecto.png" Width="250" Height="404"/>--%>
                                        <asp:Image ID="lv_imagen"   ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                                    </div>
                                    <div>
                                        Fecha:
                                       
                                        <asp:Label ID="lv_lbl_fecha" runat="server" Text='<%# Eval("fecha") %>' />
                                    </div>
                                    <div>
                                        Hora:
                                       
                                        <asp:Label ID="lv_lbl_hora" runat="server" Text='<%# Eval("hora") %>' />
                                    </div>
                                    <div>
                                        <asp:Button ID="lv_btn_inscribir" runat="server" CommandArgument='<%# Eval("id_torneo") %>' CssClass=" btn-link" Text="Inscribir" />
                                    </div>
                                </div>

                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h3>No hay torneos disponibles por el momento</h3>
                            </EmptyDataTemplate>
                        </asp:ListView>


                    </div>
                    <!---------------FIN Cuadricula-------------------->


                    <!--/container -->


                    <!--APARTADO DE ADMINISTRACION DE TORNEOS -->

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container" id="administracion_torneos" runat="server">

                        <div class="row mt centered">
                            <h1>ADMINISTRACIÓN DE TORNEOS</h1>

                            <!--col insrcibir_Torneo-->
                            <div class="col-lg-4 proc" id="item_insrcibir_Torneo" runat="server">
                                <i class="fa fa-pencil"></i>
                                <h3><a href="../Presentacion/InscripcionTorneo.aspx" style="color: #000000">Inscripciones </a></h3>
                                <p>Accede a los próximos torneos e inscríbite para participar.</p>
                            </div>
                            <!--/col-->

                            <%--  <!--col mis_Torneos-->
                            <div class="col-lg-4 proc" id="item_mis_Torneos" runat="server">
                                <i class="fa fa-heart"></i>
                                <h3>Mis torneos</h3>
                                <p>Accede al historial de torneos en los que has competido.</p>
                            </div>
                            <!--/col-->--%>

                            <%--   <!--col visualizar_Torneo-->
                            <div class="col-lg-4 proc" id="item_visualizar_Torneo" runat="server">
                                <i class="fa fa-eye"></i>
                                <h3>Visualizar torneos</h3>
                                <p>Accede a todos los torneos que se han desarrollado y busca el que te interese mediante un filtro.</p>
                            </div>
                            <!--/col-->--%>

                            <!--col Generar_Torneo-->
                            <div class="col-lg-4 proc" id="item_Generar_Torneo" runat="server">
                                <i class="fa fa-cogs"></i>
                                <h3 class="logo"><a href="../Presentacion/CrearTorneo.aspx" style="color: #000000">Generar un nuevo torneo</a></h3>
                                <p>Crea un nuevo torneo para luego ver su seguimiento.</p>
                            </div>
                            <!--/col-->

                            <!--col Generar_Listado_inscriptos-->
                            <div class="col-lg-4 proc" id="item_Generar_Listado_inscriptos" runat="server">
                                <i class="fa fa-cogs"></i>
                                <h3 class="logo"><a id="btn_generar_Listado_inscriptos" href="" data-toggle="modal" data-target="#exportarListado" style="color: #000000">Generar listado de inscriptos</a></h3>
                                <p>Genera e imprime un listado de los inscriptos a un torneo.</p>
                            </div>
                        </div>
                    </div>
                    <!--/col-->
                </div>
                <!--/row -->
            </div>
            <!--/container -->
        </div>
        <!--/Portfoliowrap -->


        <!-- ABOUT SEPARATOR -->
        <div class="hidden-sm hidden-xs">
            <div class="sep paraClases" data-stellar-background-ratio="0.5 "></div>
        </div>

        <!--SECTOR CLASES -->
        <section id="section_clases" title="clases"></section>
        <div id="claseswrap">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <!--APARTADO DE CLASES DISPONIBLES-->
            <div class="container">
                <div class="row mt centered">

                    <h1>CLASES DISPONIBLES</h1>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>


                    <!---------------------------------**************Muestra de Clases************---------------------------------------->
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="muetra_clases_profe_admin" runat="server">
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

                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                                    <div>
                                        <asp:Label ID="lv_lbl_nombre_clase" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
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
                                    <div>
                                        <asp:Button ID="lv_btn_inscribir" runat="server" CommandName="inscribir" CommandArgument='<%# Eval("id_clase") %>' CssClass=" btn-link" Text="Inscribir" />
                                    </div>
                                    <div>
                                        <asp:Button ID="lv_btn_seleccionar" runat="server" CommandName="seleccionar" CommandArgument='<%# Eval("id_clase") %>' CssClass="btn-link" Text="Editar" />
                                    </div>
                                    <div>
                                        <asp:Button ID="lv_btn_eliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id_clase") %>' CssClass=" btn-link" Text="Eliminar" />
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
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 centered" id="muetra_clases_invitado" runat="server">
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

                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                                    <div>
                                        <asp:Label ID="lv_lbl_nombre_clase" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
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


                                </div>

                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h3>No hay clases disponibles por el momento</h3>
                            </EmptyDataTemplate>
                        </asp:ListView>

                    </div>
                    <!---------------FIN Cuadricula-------------------->

                </div>
            </div>





            <div class="row centered">
                <p>&nbsp;</p>
            </div>


            <!--APARTADO DE ADMINISTRACION DE CLASES -->
            <div class="container" id="administracion_clases" runat="server">
                <div class="row mt centered">
                    <h1>ADMINISTRACIÓN DE CLASES</h1>

                    <!--col crear_nueva_clase-->
                    <div class="col-lg-4 proc" id="item_crear_nueva_clase" runat="server">
                        <i class="fa fa-pencil"></i>
                        <h3>
                            <asp:LinkButton ID="lnk_crearClase" OnClick="lnk_crearClase_Click" runat="server" Style="color: #000000">Crear nueva clase</asp:LinkButton>
                        </h3>
                        <p>Crea una nueva clase con sus horarios.</p>
                    </div>
                    <!--/col-->

                    <!--col mis_Clases-->
                    <div class="col-lg-4 proc" id="item_mis_Clases" runat="server">
                        <i class="fa fa fa-heart"></i>
                        <h3 class="logo"><a href="../Presentacion/AlumnoClases.aspx" style="color: #000000">Mis clases</a> </h3>
                        <p>Accede a todas tus clases y paga mediante MercadoPago.</p>
                    </div>
                    <!--/col-->



                    <%--                    <!--col visualizar_Clase-->
                    <div class="col-lg-4 proc" id="item_visualizar_clases" runat="server">
                        <i class="fa fa-eye"></i>
                        <h3>Visualizar horarios</h3>
                        <p>Consulta los horarios en que se desarrollan las diferentes clases que se dictan en la academia</p>
                    </div>
                    <!--/col-->--%>

                    <!--col inscribir_Alumno_Clase-->
                    <div class="col-lg-4 proc" id="item_inscribir_Alumno_Clase" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3 class="logo"><a href="../Presentacion/InscripcionClase.aspx" style="color: #000000">Inscribir a un alumno</a></h3>
                        <p>Inscribe alumnos a una clase.</p>
                    </div>
                    <!--/col-->

                    <!--col modificar_valor_de_recarga_por_atraso-->
                    <div class="col-lg-4 proc" id="item_recarga_por_atraso" runat="server">
                        <i class="fa fa-dollar"></i>
                        <h3 class="logo">
                            <a id="btn_recarga_por_atraso" href="" data-toggle="modal" data-target="#modal_recarga" style="color: #000000">Recargo por atraso</a>
                        </h3>
                        <p>Modifica el valor de recarga en caso de atraso de pago de tus alumnos.</p>
                    </div>
                    <!--/col-->

                    <!--col listado_-->
                    <div class="col-lg-4 proc" id="item_listado_asistencia" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3 class="logo"><a id="btn_generar_Listado_asistencia" href="" data-toggle="modal" data-target="#exportarListadoAsistencia" style="color: #000000">Generar listado de asistencia a clase</a></h3>
                        <p>Genera e imprime un listado de los asistencia del día de una clase.</p>
                    </div>
                    <!--/col-->


                    <!--col registrar_asistencia-->
                    <div class="col-lg-4 proc" id="item_registrar_asistencia" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3 class="logo"><a href="../Presentacion/RegistrarAsistencia.aspx" style="color: #000000">Registrar Asistencia</a></h3>
                        <p>Registra la asistencia de tus alumnos en cada lugar donde se dictan las clases.</p>

                    </div>
                    <!--/col-->



                </div>
                <!--/row -->
            </div>
            <!--/container -->
        </div>
        <!--/Portfoliowrap -->


        <!-- ABOUT SEPARATOR -->
        <div class="hidden-sm hidden-xs">
            <div class="sep paraAlumnos" data-stellar-background-ratio="0.5"></div>
        </div>

        <!--SECTOR ALUMNOS -->
        <section id="alumnos" title="clases" runat="server"></section>
        <div id="alumnoswrap" runat="server">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <!--APARTADO DE ADMINISTRACION DE ALUMNOS -->
            <div class="container" id="administracion_alumnos" runat="server">
                <div class="row mt centered">
                    <h1>ADMINISTRACIÓN DE ALUMNNOS</h1>

                    <!--col registrar_alumno-->
                    <div class="col-lg-4 proc" id="item_registrar_alumno" runat="server">
                        <i class="fa fa-pencil"></i>
                        <h3><%--<a href="../Presentacion/RegistrarAlumno.aspx" style="color: #000000">Registrar Alumno</a>--%></h3>
                        <h3>
                            <asp:LinkButton ID="lnk_registrar_alumno" OnClick="registrarAlumno_Click" runat="server" Style="color: #000000">Registrar Alumno</asp:LinkButton>
                        </h3>
                        <p>Registra un nuevo alumno. De esta forma podrás contar con su información para la posterior administración del mismo.</p>
                    </div>
                    <!--/col-->

                    <!--col administrar_alumnos-->
                    <div class="col-lg-4 proc" id="item_administrar_alumnos" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3>
                            <asp:LinkButton ID="lnk_administrar_alumnos" OnClick="administrarAlumnos_Click" runat="server" Style="color: #000000">Administrar Alumnos</asp:LinkButton></h3>
                        <p>Podrás visualizar todos tus alumnos. Darlos de bajo. Modificar su perfil.</p>
                    </div>
                    <!--/col-->

                    <!--col graduacion_alumnos-->
                    <div class="col-lg-4 proc" id="item_graduacion_alumnos" runat="server">
                        <i class="fa fa-arrow-up"></i>
                        <h3 class="logo"><a href="../Presentacion/GraduarAlumno.aspx" style="color: #000000">Graduar Alumnos</a> </h3>
                        <p>Puedes modificar el cinturón y grado de cada alumno de manera fácil y rápida.</p>
                    </div>
                    <!--/col-->


                </div>
            </div>

        </div>
        <!--/Portfoliowrap -->


        <!--SECTOR PROFESORES -->

        <section id="section_profesores" title="profesores" runat="server"></section>
        <div id="profesoreswrap" runat="server">


            <div class="row centered">
                <p>&nbsp;</p>
            </div>


            <!--APARTADO DE ADMINISTRACION DE PROFESORES -->
            <div class="container" id="administracion_profesores" runat="server">
                <div class="row mt centered">
                    <h1>ADMINISTRACIÓN DE PROFESORES</h1>

                    <!--col registrar_profesor-->
                    <div class="col-lg-4 proc" id="Div2" runat="server">
                        <i class="fa fa-pencil"></i>

                        <h3>
                            <asp:LinkButton ID="lnk_registrar_profe" runat="server" Style="color: #000000" OnClick="lnk_registrar_profe_Click">Registrar Profesor</asp:LinkButton>
                        </h3>
                        <p>Registra un nuevo profesor. De esta forma podrás contar con su información para la posterior administración del mismo.</p>
                    </div>
                    <!--/col-->

                    <!--col administrar_profes-->
                    <div class="col-lg-4 proc" id="Div3" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3>
                            <asp:LinkButton ID="lnk_administrar_profes" runat="server" Style="color: #000000" OnClick="lnk_administrar_profes_Click">Administrar Profesores</asp:LinkButton></h3>
                        <p>Podrás visualizar todos los profesores, darlos de baja y modificar su perfil.</p>
                    </div>
                    <!--/col-->


                </div>
            </div>




        </div>

        <!--SECTOR TIENDA -->
        <!-- ABOUT SEPARATOR -->
        <div class="hidden-sm hidden-xs">
            <div class="sep paraAlumnos" data-stellar-background-ratio="0.5"></div>
        </div>

        <section id="section_tienda" title="tienda" runat="server"></section>
        <div id="tiendawrap" runat="server">


            <div class="row centered">
                <p>&nbsp;</p>
            </div>
            <!--APARTADO DE TIENDA -->
            <div class="container" id="administracion_tienda" runat="server">
                <div class="row mt centered">
                    <h1>TIENDA</h1>

                    <!--col agregar_productos-->
                    <div id="btn_agregarProducto" class="col-lg-4 proc" runat="server">
                        <i class="fa fa-pencil"></i>
                        <h3 class="logo"><a href="../Presentacion/AgregarProducto.aspx" style="color: #000000">Dar de alta productos</a> </h3>
                        <p>Puedes registrar nuevos productos para que luego sean mostrados en la tienda.</p>
                    </div>
                    <!--/col-->

                    <!--col agregar_productos-->
                    <div class="col-lg-4 proc" id="Div6" runat="server">
                        <i class="fa fa-eye"></i>
                        <h3 class="logo"><a href="../Presentacion/Tienda" style="color: #000000">Entrar a la tienda</a> </h3>
                        <p>Puedes ver los productos disponibles para la reserva.</p>
                    </div>
                    <!--/col-->

                    <!--col compra-->
                    <div class="col-lg-4 proc" id="item_compra_producto" runat="server">
                        <i class="fa fa-eye"></i>
                        <h3 class="logo"><a href="../Presentacion/ComprarProducto.aspx" style="color: #000000">Registrar compra de productos</a> </h3>
                        <p>Puedes registrar la compra de un producto a un proveedor, agregar stock al mismo y modificar su precio</p>
                    </div>
                    <!--/col-->

                    <!--col compra-->
                    <div class="col-lg-4 proc" id="Div10" runat="server">
                        <i class="fa fa-eye"></i>
                        <h3 class="logo"><a href="../Presentacion/Reservas.aspx" style="color: #000000">Seguimiento de las reservas</a> </h3>
                        <p>Puedes cancelar o registrar el retiro de una reserva</p>
                    </div>
                    <!--/col-->

                </div>
            </div>

        </div>


        <!--SECTOR EVENTOS -->

        <section id="section_eventos" title="eventos"></section>
        <div class="container">
            <div class="row mt centered">
                <h1>ÚLTIMOS EVENTOS</h1>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
                <!---------------------------------**************Muestra de Torneos************---------------------------------------->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:ListView ID="lv_eventos" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1" GroupItemCount="3" runat="server" OnItemCommand="lv_eventos_ItemCommand">

                        <LayoutTemplate>
                            <table>
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>

                        <GroupTemplate>
                            <tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                            </tr>
                        </GroupTemplate>

                        <ItemTemplate>

                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                                <div>
                                    <asp:Label ID="lv_lbl_nombre" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                                </div>
                                <div>
                                    <%--   <img src="../img/Imagen%20por%20Defecto.png" Width="250" Height="404"/>--%>
                                    <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                                </div>
                                <div>
                                    Fecha:
                                       
                                        <asp:Label ID="lv_lbl_fecha" runat="server" Text='<%# Eval("fecha") %>' />
                                </div>
                                <div>
                                    Hora:
                                       
                                        <asp:Label ID="lv_lbl_hora" runat="server" Text='<%# Eval("hora") %>' />
                                </div>
                                <div>
                                    <asp:Button ID="lv_btn_inscribir" runat="server" CommandArgument='<%# Eval("id_evento") %>' CssClass=" btn-link" Text="Inscribir" />
                                </div>
                            </div>

                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <h3>No hay eventos disponibles por el momento</h3>
                        </EmptyDataTemplate>
                    </asp:ListView>


                </div>

                <!--APARTADO DE EVENTOS -->
                <div class="container" id="Div7" runat="server">
                    <div class="row mt centered">
                        <h1>ADMINISTRACIÓN DE EVENTOS</h1>

                        <!--col insrcibir_evento-->
                        <div class="col-lg-4 proc" id="Div8" runat="server">
                            <i class="fa fa-pencil"></i>
                            <h3><a href="../Presentacion/InscripcionEvento.aspx" style="color: #000000">Inscripciones </a></h3>
                            <p>Accede a los próximos eventos e inscríbite para participar.</p>
                        </div>
                        <!--/col-->

                        <!--col Generar_evento-->
                        <div class="col-lg-4 proc" id="item_crear_evento" runat="server">
                            <i class="fa fa-cogs"></i>
                            <h3 class="logo"><a href="../Presentacion/CrearEvento.aspx" style="color: #000000">Generar un nuevo evento</a></h3>
                            <p>Crea un nuevo evento para luego ver su seguimiento.</p>
                        </div>

                        <!--col Generar_Listado_inscriptos-->
                        <div class="col-lg-4 proc" id="item_listado_evento" runat="server">
                            <i class="fa fa-cogs"></i>
                            <h3 class="logo"><a id="btn_generar_Listado_inscriptos_evento" href="" data-toggle="modal" data-target="#exportarListadoEvento" style="color: #000000">Generar listado de inscriptos</a></h3>
                            <p>Genera e imprime un listado de los inscriptos a un evento.</p>
                        </div>

                    </div>
                </div>
            </div>
        </div>




        <!--SECTOR PERMISOS -->

        <!--APARTADO DE PERMISOS -->
        <section id="section_permisos" title="permisos" runat="server"></section>

        <div class="container" id="administracion_permisos" runat="server">
            <div class="row mt centered">
                <h1>PERFILES Y PERMISOS</h1>

                <!--col Administrar permisos-->
                <div class="col-lg-4 proc" id="Div14" runat="server">
                    <i class="fa fa-pencil"></i>
                    <h3><a href="Usuarios.aspx" style="color: #000000">Administrar Permisos </a></h3>
                    <p>Administra los diferentes permisos de los usuarios.</p>
                </div>
                <!--/col-->



            </div>
        </div>


        <!--/Portfoliowrap -->


        <div class="row centered">
            <p>&nbsp;</p>
        </div>




        <%--<div id="testimonials">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 mt">

                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <!-- Wrapper for slides -->
                        <div class="carousel-inner">
                            <div class="item active mb centered">
                                <h3>MARK WEBBER</h3>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                <p>
                                    <img class="img-circle" src="assets/img/pic-t1.jpg" width="80">
                                </p>
                            </div>

                            <div class="item mb centered">
                                <h3>PAUL LEVINGSTON</h3>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                <p>
                                    <img class="img-circle" src="assets/img/pic-t2.jpg" width="80">
                                </p>
                            </div>

                            <div class="item mb centered">
                                <h3>LUCY LENNIN</h3>
                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                <p>
                                    <img class="img-circle" src="assets/img/pic-t3.jpg" width="80">
                                </p>
                            </div>

                        </div>
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        </ol>
                    </div>

                </div>
                <! --/col-lg-8 -->
            </div>
            <! --/row -->
        </div>
        <! --/container -->
    </div>
        <! --/testimonials -->--%>

        <!-- SERVICE SECTION -->
        <section id="contact" title="contact"></section>




        <!--Implementando ventana emergente --------------------*********     Exportar Listado     *********--------------------->
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="exportarListado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabe2">Exportar listado de inscriptos</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Seleccione Torneo-->
                            <asp:Panel ID="pnl_torneoExportarListado" CssClass="panel panel-default" runat="server">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class="col-sm-1 col-xs-1"></div>
                                    <div class="col-sm-2 col-xs-2">
                                        <label for="recipient-name" class="control-label">Torneo:</label>
                                    </div>
                                    <div class="col-sm-5 col-xs-5">
                                        <asp:DropDownList ID="ddl_torneoExportarListado" CssClass="caja2" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btn_acpetarTorneoExportarLista" CssClass="btn btn-default" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_acpetarTorneoExportarLista_Click" />

                    </div>

                </div>
            </div>
        </div>
        <!----------------------*********  Fin ventana emergente   *********--------------------->

        <!--Implementando ventana emergente --------------------*********     Exportar Listado  Evento   *********--------------------->
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="exportarListadoEvento" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabe21">Exportar listado de inscriptos a evento</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Seleccione Torneo-->
                            <asp:Panel ID="pnl_eventoExportarListado" CssClass="panel panel-default" runat="server">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class="col-sm-1 col-xs-1"></div>
                                    <div class="col-sm-2 col-xs-2">
                                        <label for="recipient-name" class="control-label">Evento:</label>
                                    </div>
                                    <div class="col-sm-5 col-xs-5">
                                        <asp:DropDownList ID="ddl_eventoExportarListado" CssClass="caja2" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btn_acpetarEventoExportarLista" CssClass="btn btn-default" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_acpetarEventoExportarLista_Click" />

                    </div>

                </div>
            </div>
        </div>
        <!----------------------*********  Fin ventana emergente   *********--------------------->

        <!--Implementando ventana emergente --------------------*********     Exportar Listado  Asistencia a Clase   *********--------------------->
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="exportarListadoAsistencia" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabe21">Exportar listado de asistencia del día</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Seleccione Torneo-->
                            <asp:Panel ID="pnl_asistenciaExportarListado" CssClass="panel panel-default" runat="server">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class="col-sm-1 col-xs-1"></div>
                                    <div class="col-sm-2 col-xs-2">
                                        <label for="recipient-name" class="control-label">Clase:</label>
                                    </div>
                                    <div class="col-sm-7 col-xs-7">
                                        <asp:DropDownList ID="ddl_clasesListado" CssClass="caja2" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="Button1" CssClass="btn btn-default" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_acpetarAsistenciaExportarLista_Click" />

                    </div>

                </div>
            </div>
        </div>
        <!----------------------*********  Fin ventana emergente   *********--------------------->


        <!--Implementando ventana emergente --------------------*********     Administrar recarga     *********--------------------->

        <div class="modal fade" id="modal_recarga" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Administrar Recarga</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Valor de Recarga-->
                            <asp:Panel ID="pnl_modal_recarga" CssClass="panel panel-default" runat="server">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class=" col-xs-2"></div>
                                    <div class=" col-xs-2">
                                        <label for="recipient-name" class="control-label">Recarga:</label>
                                    </div>
                                    <div class="col-xs-5">
                                        <asp:TextBox ID="txt_modal_recarga" required="true" type="number" step="0.01" min="0" max="10000" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" formnovalidate class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btn_modal_recarga_aceptar" CssClass="btn btn-default" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_modal_recarga_aceptar_Click" />
                    </div>

                </div>
            </div>
        </div>
        <!----------------------*********  Fin ventana emergente   *********--------------------->
    </form>



    <!---------------------------************              SCRIPTS              **************---------------------------->


    <script type="text/javascript">

        $(document).ready(function () {

            var elements = document.getElementsById('lv_imagen');
            var inscribir = document.getElementsById('lv_btn_inscribir');
            for (var i = 0; i < elements.length; i++) {
                elements[i].Attributes.Add("onclick", "clickAImagenes(inscribir[i]);");
            }

            $("nav").find("a").click(function (e) {
                e.preventDefault();
                var section = $(this).attr("href");
                $("html, body").animate({
                    scrollTop: $(section).offset().top
                });
            });

        });



        function clickAImagenes(id_inscribir) {

            $(id_inscribir).click();
        }

        window.onload = function () {
            var imagenes = $('*[id^="cphContenido_lv_torneos_abiertos_ctrl0_lv_imagen"]');
            var inscribir = $('*[id^="cphContenido_lv_torneos_abiertos_ctrl0_lv_btn_inscribir"]');

            for (var i = 0; i < 3; i++) {
                var imagenes = $('*[id^="cphContenido_lv_torneos_abiertos_ctrl0_lv_imagen"]');
                var inscribir = $('*[id^="cphContenido_lv_torneos_abiertos_ctrl0_lv_btn_inscribir"]');
                imagenes[i].addEventListener('onclick', function () { inscribir[i].click(); });
            };
        }



    </script>


</asp:Content>
