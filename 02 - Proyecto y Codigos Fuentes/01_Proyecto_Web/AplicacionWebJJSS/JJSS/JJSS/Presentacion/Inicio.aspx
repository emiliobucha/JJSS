<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="JJSS.Presentacion.Inicio" %>


<asp:Content ID="InicioMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="#home" class="smoothScroll">Home</a>
    <a href="#about" class="smoothScroll">About</a>
    <a href="#torneos" class="smoothScroll">Torneos</a>
    <a href="#clases" class="smoothScroll">Clases</a>
    <a href="#alumnos" class="smoothScroll">Alumnos</a>
    <%--<a><asp:LinkButton ID="LinkButton1" runat="server">Cerrar Sesión</asp:LinkButton></a>--%>
</asp:Content>



<asp:Content ID="InicioEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
    <
    <section id="home" title="home"></section>
    <div id="headerwrap2">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-md-offset-1">
                    <h1></h1>

                </div>
            </div>
            <! --/row -->
        </div>
        <! --/container -->
    </div>
    <! --/headerwrap -->
</asp:Content>



<asp:Content ID="InicioContenido" ContentPlaceHolderID="cphContenido" runat="Server">
    <section id="about" title="about"></section>

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
    <div class="sep torneo" data-stellar-background-ratio="0.5"></div>

    <form runat="server">

        <!--SECTOR PERFIL -->
        <section id="perfil" title="clases" runat="server"></section>
        <div id="perfilwrap">

            <!--APARTADO DE PERFIL-->
            <div class="container">
                <div class="row mt centered">

                    <h1>MI PERFIL</h1>

                </div>
            </div>

            <div class="container">
                <div class="row mt centered">


                    <!--col modificar_perfil-->
                    <div class="col-lg-4 proc" id="item_modificar_perfil" runat="server">
                        <i class="fa fa-pencil"></i>
                        <h3><a href="../Presentacion/Perfil.aspx" style="color: #000000">Modificar mi perfil</a></h3>
                        <p>Permite modificar los datos del usuario.</p>
                    </div>
                    <!--/col-->

                    <!--col cerrar_sesion-->
                    <div class="col-lg-4 proc" id="item_cerrar_sesion">
                        <i class="fa fa-pencil"></i>
                        <h3>
                            <asp:Button ID="btn_cerrar_sesion" runat="server" Text="Cerrar Sesión" OnClick="btn_cerrar_sesion_Click" /></h3>


                    </div>

                    <!--col iniciar_sesion-->
                    <div class="col-lg-4 proc" id="item_iniciar_sesion">
                        <i class="fa fa-pencil"></i>
                        <h3>
                            <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" OnClick="Button1_Click" /></h3>


                    </div>

                </div>
                <!--/row -->
            </div>
            <!--/container -->
        </div>
        <!--/Portfoliowrap -->


        <!-- ABOUT SEPARATOR -->
        <div class="sep torneo" data-stellar-background-ratio="0.5"></div>




        <!--SECTOR TORNEOS -->
        <section id="torneos" title="torneos"></section>
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

                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="border: 1px medium gray">
                                    <div>
                                        <asp:Label ID="lv_lbl_nombre" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                                    </div>
                                    <div>
                                        <%--   <img src="../img/Imagen%20por%20Defecto.png" Width="250" Height="404"/>--%>
                                        <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                                    </div>
                                    <div>
                                        <asp:Label ID="lv_lbl_fecha" runat="server" Text='<%# Eval("fecha") %>' />
                                    </div>
                                    <div>
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
                    <div class="container" id="administracion_torneos" runat="server">
                        >
                        <div class="row mt centered">
                            <h1>ADMINISTRACIÓN DE TORNEOS</h1>

                            <!--col insrcibir_Torneo-->
                            <div class="col-lg-4 proc" id="item_insrcibir_Torneo" runat="server">
                                <i class="fa fa-pencil"></i>
                                <h3><a href="../Presentacion/InscripcionTorneo.aspx" style="color: #000000">Inscripciones </a></h3>
                                <p>Accede a los torneos que estan pronto a desarrollar e inscríbete.</p>
                            </div>
                            <!--/col-->

                            <!--col mis_Torneos-->
                            <div class="col-lg-4 proc" id="item_mis_Torneos" runat="server">
                                <i class="fa fa-heart"></i>
                                <h3>Mis torneos</h3>
                                <p>Puedes ver aquí el historial de los torneos en que has competido.</p>
                            </div>
                            <!--/col-->

                            <!--col visualizar_Torneo-->
                            <div class="col-lg-4 proc" id="item_visualizar_Torneo" runat="server">
                                <i class="fa fa-eye"></i>
                                <h3>Visualizar torneos</h3>
                                <p>Accede a todos los torneos que se han desarrollado. Un filtro de búsqueda te facilitará el trabajo</p>
                            </div>
                            <!--/col-->

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
                                <p>Genera un listado de los inscriptos a un torneo. Con esta herramienta podrás imprimir un listados con los inscriptos a un torneo.</p>
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
        <div class="sep torneo" data-stellar-background-ratio="0.5"></div>


        <!--SECTOR CLASES -->
        <section id="clases" title="clases"></section>
        <div id="claseswrap">

            <!--APARTADO DE CLASES DISPONIBLES-->
            <div class="container">
                <div class="row mt centered">

                    <h1>CLASES DISPONIBLES</h1>

                    <%--  <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                    <!---------------------------------**************Grilla de clase************---------------------------------------->
                    <asp:GridView ID="gv_clasesDisponibles" runat="server" DataKeyNames="id_clase" CssClass="table" AutoGenerateColumns="False" EmptyDataText="No hay clases por el momento" OnRowCommand="gv_clasesDisponibles_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id_clase" HeaderText="ID de clase" />
                            <asp:BoundField DataField="nombre" HeaderText="Clase" />
                            <asp:BoundField DataField="tipo_clase" HeaderText="Tipo de Clase" />
                            <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" />
                            <asp:BoundField DataField="profesor" HeaderText="Profesor" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />

                            <asp:CommandField SelectText="Inscribir" EditText="Inscribir" ShowEditButton="True" />



                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="button" id="btn_inscribirClase" class="btn btn-link " onclick="seleccionarClaseInscripcion(this)" value="Inscribir" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:ButtonField Text="Seleccionar" CommandName="Seleccionar" />
                            <asp:ButtonField Text="Eliminar" CommandName="Eliminar" />

                        </Columns>
                    </asp:GridView>--%>

<%--                </div>
            </div>--%>
            <!---------------FIN Grilla-------------------->

            <!---------------------------------**************Muestra de Clases************---------------------------------------->
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <asp:ListView ID="lv_clasesDisponibles" GroupPlaceholderID="groupPlaceHolder_clase" ItemPlaceholderID="itemPlaceHolder_clase" GroupItemCount="3" OnItemCommand="lv_clasesDisponibles_ItemCommand" runat="server" >

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

                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="border: 1px medium gray">
                            <div>
                                <asp:Label ID="lv_lbl_nombre_clase" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                            </div>
                            <%-- <div>                           
                                <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                            </div>--%>
                            <div>
                                <asp:Label ID="lv_lbl_tipo_clase" runat="server" Text='<%# Eval("tipo_clase") %>' />
                            </div>
                            <div>
                                <asp:Label ID="lv_lbl_ubicacion" runat="server" Text='<%# Eval("ubicacion") %>' />
                            </div>
                            <div>
                                <asp:Label ID="lv_lbl_profesor" runat="server" Text='<%# Eval("profesor") %>' />
                            </div>
                            <div>
                                <asp:Label ID="lv_lbl_precio" runat="server" Text='<%# Eval("precio") %>' />
                            </div>
                            <div>
                                <asp:Button ID="lv_btn_inscribir" runat="server" CommandArgument='<%# Eval("id_clase") %>' CssClass=" btn-link" Text="Inscribir"  />
                            </div>
                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>No hay clases disponibles por el momento</h3>
                    </EmptyDataTemplate>
                </asp:ListView>

            </div>
                                                </div>
                        </div>
            <!---------------FIN Cuadricula-------------------->




            <!--APARTADO DE ADMINISTRACION DE CLASES -->
            <div class="container" id="administracion_clases" runat="server">
                <div class="row mt centered">
                    <h1>ADMINISTRACIÓN DE CLASES</h1>

                    <!--col crear_nueva_clase-->
                    <div class="col-lg-4 proc" id="item_crear_nueva_clase" runat="server">
                        <i class="fa fa-pencil"></i>
                        <h3><a href="../Presentacion/CrearClase.aspx" style="color: #000000">Crear nueva clase</a></h3>
                        <p>Crea una nueva clase con sus respectivos horarios para administrarla.</p>
                    </div>
                    <!--/col-->

                      <!--col mis_Clases-->
                    <div class="col-lg-4 proc" id="item_mis_Clases" runat="server">
                        <i class="fa fa fa-heart"></i>
                        <h3 class="logo"><a href="../Presentacion/AlumnoClases.aspx" style="color: #000000">Mis clases</a> </h3>
                        <p>Puedes ver todas tus clases y pagar mediante MercadoPago.</p>
                    </div>
                    <!--/col-->

                   

                    <!--col visualizar_Clase-->
                    <div class="col-lg-4 proc" id="item_visualizar_clases" runat="server">
                        <i class="fa fa-eye"></i>
                        <h3>Visualizar horarios</h3>
                        <p>Consulta los horarios en que se desarrollan las diferentes clases que se dictan en la academia</p>
                    </div>
                    <!--/col-->

                    <!--col inscribir_Alumno_Clase-->
                    <div class="col-lg-4 proc" id="item_inscribir_Alumno_Clase" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3 class="logo"><a style="color: #000000">Inscribir a un alumno</a></h3>
                        <p>Puedes inscribir a un alumno a una determinada clase. Esto te permitirá realizar una administración del mismo.</p>
                    </div>
                    <!--/col-->

                    <!--col modificar_valor_de_recarga_por_atraso-->
                    <div class="col-lg-4 proc" id="item_recarga_por_atraso" runat="server">
                        <i class="fa fa-dollar"></i>
                        <h3 class="logo">
                            <a id="btn_recarga_por_atraso" href="" data-toggle="modal" data-target="#modal_recarga" style="color: #000000">Recargo por atraso</a>
                        </h3>
                        <p>Puedes modificar el valor de recarga en caso de atraso de pago de tus alumnos.</p>
                    </div>
                    <!--/col-->

                    <!--col registrar_asistencia-->
                    <div class="col-lg-4 proc" id="item_registrar_asistencia" runat="server">
                        <i class="fa fa-cogs"></i>
                        <h3 class="logo"><a href="../Presentacion/RegistrarAsistencia.aspx" style="color: #000000">Registrar Asistencia</a></h3>
                        <p>Puedes mantener un registro de asistencia en cada uno de los puntos donde se dictan las clases.</p>

                    </div>
                    <!--/col-->


                </div>
                <!--/row -->
            </div>
            <!--/container -->
        </div>
        <!--/Portfoliowrap -->


        <!-- ABOUT SEPARATOR -->
        <div class="sep torneo" data-stellar-background-ratio="0.5"></div>

        <!--SECTOR ALUMNOS -->
        <section id="alumnos" title="clases" runat="server"></section>
        <div id="alumnoswrap">

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
                        <h3 class="logo"><a href="../Presentacion/RegistrarAlumno.aspx" style="color: #000000">Graduar Alumnos</a> </h3>
                        <p>Puedes modificar el cinturón y grado de cada alumno de manera fácil y rápida.</p>
                    </div>
                    <!--/col-->

                  

                </div>
            </div>

        </div>
        <!--/Portfoliowrap -->


        <!-- SERVICE SECTION -->
        <section id="services" title="services"></section>
        <div id="servicewrap">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8-offset-2 centered">
                        <h1>AN OVERVIEW OF MY SERVICES</h1>
                        <h3>I'll do all the work for you</h3>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                    </div>
                    <!-- /col-lg-8 -->
                </div>
                <! --/row -->
			
			<div class="row mt">
                <div class="col-lg-3 service">
                    <i class="fa fa-star"></i>
                    <p>
                        PREMIUM QUALITY<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-cloud"></i>
                    <p>
                        CLOUD SERVICES<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-shield"></i>
                    <p>
                        SECURED ACCOUNTS<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-heart"></i>
                    <p>
                        100% SATISFACTION<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
            </div>
                <! --/row -->
			<div class="row mt">
                <div class="col-lg-3 service">
                    <i class="fa fa-trophy"></i>
                    <p>
                        PREMIUM QUALITY<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-globe"></i>
                    <p>
                        CLOUD SERVICES<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-lock"></i>
                    <p>
                        SECURED ACCOUNTS<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
                <div class="col-lg-3 service">
                    <i class="fa fa-thumbs-up"></i>
                    <p>
                        100% SATISFACTION<br />
                        <small>LOREM IPSUM DOLOR</small>
                    </p>
                    <p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
                </div>
            </div>
                <! --/row -->
			
            </div>
            <! --/container -->
        </div>
        <! --/servicewrap -->
	
	<div id="testimonials">
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
        <! --/testimonials -->
	
	<!-- SERVICE SECTION -->
        <section id="contact" title="contact"></section>
        <!-- CONTACT SEPARATOR -->
        <div class="sep contact" data-stellar-background-ratio="0.5"></div>

        <div id="contactwrap">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6">
                        <p>CONTACT ME RIGHT NOW!</p>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
                        <p>
                            <small>5th Avenue, 987<br />
                                38399, New York,<br />
                                USA.</small>
                        </p>
                        <p>
                            <small>Tel. 9888-4394<br />
                                Mail. Hello@coolfolks.com<br />
                                Skype. NYCDesign</small>
                        </p>

                    </div>

                    <div class="col-lg-6">
                        <form role="form">
                            <div class="form-group">
                                <label for="exampleInputName1">Your Name</label>
                                <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter name">
                                <label for="exampleInputText1">Message</label>
                                <textarea class="form-control" rows="3"></textarea>
                            </div>
                            <button type="submit" class="btn btn-default">Submit</button>
                        </form>
                    </div>

                </div>
                <!--/row -->
            </div>
            <!-- /container -->
        </div>




        <!--Implementando ventana emergente --------------------*********     Exportar Listado     *********--------------------->
        <div class="modal fade" id="exportarListado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
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
                                    <div class=" col-xs-2"></div>
                                    <div class=" col-xs-2">
                                        <label for="recipient-name" class="control-label">Torneo:</label>
                                    </div>
                                    <div class="col-xs-5">
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

        <!--Implementando ventana emergente --------------------*********     Inscribir alumno a Clase     *********--------------------->
        <div class="modal fade" id="modal_inscribirAlumunoClase" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Inscribir alumno a clase</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Ingresar DNI para inscribir alumno-->
                            <asp:Panel ID="pnl_inscribirClase" CssClass="panel panel-default" runat="server">

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row centered">
                                    <div class=" col-xs-2"></div>
                                    <div class=" col-xs-8">
                                        <asp:Label ID="Label1" runat="server" Text="Clase"></asp:Label>

                                        <!--Nombre Clase-->
                                        <asp:Label ID="lbl_claseSeleccionada_nombre" runat="server" Text=""></asp:Label>

                                        <!--Id Clase-->
                                        <asp:Label ID="Label2" runat="server" Text="("></asp:Label>
                                        <asp:Label ID="lbl_claseSeleccionada_id" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text=")"></asp:Label>
                                        <asp:HiddenField ID="hf_claseSeleccionada_id" runat="server" />
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class=" col-xs-2"></div>
                                    <div class=" col-xs-2">
                                        <label for="recipient-name" class="control-label">DNI:</label>
                                    </div>
                                    <!-- DNI-->
                                    <div class="col-xs-5">
                                        <asp:TextBox ID="txt_inscripcionClase_dni" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requeridoDni" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un DNI" ControlToValidate="txt_inscripcionClase_dni" Display="Dynamic" ValidationGroup="inscripcionClase"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_inscripcionClase_dni" CssClass="text-danger" Display="Dynamic" ValidationGroup="inscripcionClase" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btn_inscripcionClase_aceptar" CssClass="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_inscripcionClase_aceptar_Click" ValidationGroup="inscripcionClase" />
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
                                        <asp:TextBox ID="txt_modal_recarga" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:RequiredFieldValidator ID="requeridoMonto" ValidationGroup="vg_recarga" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un valor de recarga" ControlToValidate="txt_modal_recarga" Display="Dynamic"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regex_monto" ValidationGroup="vg_recarga" runat="server" ControlToValidate="txt_modal_recarga" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido del valor de recarga" ValidationExpression="^[0-9]{0,16}(,?[0-9][0-9]{0,1})$"> </asp:RegularExpressionValidator>
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
                        <asp:Button ID="btn_modal_recarga_aceptar" CssClass="btn btn-default" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_modal_recarga_aceptar_Click" ValidationGroup="vg_recarga" />
                    </div>

                </div>
            </div>
        </div>
        <!----------------------*********  Fin ventana emergente   *********--------------------->
    </form>



    <!---------------------------************              SCRIPTS              **************---------------------------->

    <script type="text/javascript">

        var popup;

      <%--  function seleccionarClaseInscripcion(row) {
            var rowData = row.parentNode.parentNode;
            var rowId = rowData.rowIndex;
            var gridView = document.getElementById('<%=gv_clasesDisponibles.ClientID %>')
            var idClase = gridView.rows[rowId].cells[0].innerHTML;
            var nombre = gridView.rows[rowId].cells[1].innerHTML;

            //alert(rowId + idClase + nombre);   

            document.getElementById("<%=hf_claseSeleccionada_id.ClientID%>").value = idClase;
            document.getElementById('<%=lbl_claseSeleccionada_id.ClientID %>').innerText = idClase;
            document.getElementById('<%=lbl_claseSeleccionada_nombre.ClientID %>').textContent = nombre;

            popup = $("#modal_inscribirAlumunoClase").modal('show');
            //popup.focus();--%>

    <%--    function seleccionarClaseInscripcion(x) {
            var listview = document.getElementById('<%=lv_clasesDisponibles.ClientID %>')
            var idClase = listview.getElementsByTagName('<%=lv_lbl_nombre_clase.ClientID %>')
            var nombre = gridView.rows[rowId].cells[1].innerHTML;

            document.getElementById("<%=hf_claseSeleccionada_id.ClientID%>").value = idClase;
            document.getElementById('<%=lbl_claseSeleccionada_id.ClientID %>').innerText = idClase;
            document.getElementById('<%=lbl_claseSeleccionada_nombre.ClientID %>').textContent = nombre;

            popup = $("#modal_inscribirAlumunoClase").modal('show');
        }--%>


    </script>

    <script type="text/javascript">

        $(document).ready(function () {

            var elements = document.getElementsById('lv_imagen');
            var inscribir = document.getElementsById('lv_btn_inscribir');
            for (var i = 0; i < elements.length; i++) {
                elements[i].Attributes.Add("onclick", "clickAImagenes(inscribir[i]);");
            }
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
