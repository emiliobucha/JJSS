<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="JJSS.Presentacion.Inicio" %>


<asp:Content ID="InicioMenu" ContentPlaceHolderID="cphMenu" runat="server">
            <a href="#home" class="smoothScroll">Home</a>
			<a href="#about" class="smoothScroll">About</a>
			<a href="#torneos" class="smoothScroll">Torneos</a> 
            <a href="#clases" class="smoothScroll">Clases</a>
			<a href="#alumnos" class="smoothScroll">Alumnos</a>
</asp:Content>

<asp:Content ID="InicioEncabezado"  ContentPlaceHolderID="cphEncabezado" runat="server">
    <section id="home" title="home"></section>
	<div id="headerwrap2">
		<div class="container">
			<div class="row">
				<div class="col-md-3 col-md-offset-1">
					<h1></h1>
				</div>
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/headerwrap -->
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
				</div><! --/col-lg-4-->
				<div class="col-lg-8 name-desc">
					<h2>THE JIU JITSU <br/>LIFESTILE</h2>
					<div class="name-zig"></div>
					
					<div class="col-md-6">
						<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
						<p>It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
					</div>
					<div class="col-md-6">
						<p>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.</p>
						<p> Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. </p>
					</div>
					
				</div><! --/col-lg-8-->
		
			</div><!-- /row -->
		</div><!-- /container -->
	</div><!-- /aboutwrap -->
	
	<!-- ABOUT SEPARATOR -->
	<div class="sep torneo" data-stellar-background-ratio="0.5"></div>

    <!--SECTOR TORNEOS -->
    <section id="torneos" title="torneos"></section>
    <div id="torneoswrap">

        <!--APARTADO DE TORNEOS FUTUROS-->
        <div class="container">
            <div class="row mt centered">

                <h1>ULTIMOS TORNEOS</h1>
                <!--Primer Torneo-->
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
                    <div class="project-wrapper">
                        <div class="project">
                            <div class="photo-wrapper">
                                <div class="photo">
                                    <a class="fancybox" href="../img/torneo01.jpg">
                                        <img class="img-responsive" src="../img/torneo01.jpg" alt=""></a>
                                </div>
                                <div class="overlay"></div>
                            </div>
                        </div>
                    </div>                    
                <button type="button" class="btn" data-toggle="modal" data-target="#inscripcionTorneo" data-whatever="@mdo">Inscribir</button>
                </div>
                <!--/col-->

                <!--Segundo Torneo-->
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
                    <div class="project-wrapper">
                        <div class="project">
                            <div class="photo-wrapper">
                                <div class="photo">
                                    <a class="fancybox" href="../img/torneo02.jpg">
                                        <img class="img-responsive" src="../img/torneo02.jpg" alt=""></a>
                                </div>
                                <div class="overlay"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/col-->

                <!--Tercer Torneo-->
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
                    <div class="project-wrapper">
                        <div class="project">
                            <div class="photo-wrapper">
                                <div class="photo">
                                    <a class="fancybox" href="../img/torneo03.jpg">
                                        <img class="img-responsive" src="../img/torneo03.jpg" alt=""></a>
                                </div>
                                <div class="overlay"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/col-->
            </div>
            <!-- /row -->
        </div>
        <!--/container -->


        <!--APARTADO DE ADMINISTRACION DE TORNEOS -->
        <div class="container">
            <div class="row mt centered">
                <h1>ADMINISTRACION DE TORNEOS</h1>

                <!--col insrcibir_Torneo-->
                <div class="col-lg-4 proc">
                    <i class="fa fa-pencil" id="insrcibir_Torneo"></i>
                    <h3><a href="InscripcionTorneo.aspx" style="color: #000000">Inscripciones </a></h3>
                    <p>Accede a los torneos que estan pronto a desarrollar e inscribete.</p>
                </div><!--/col-->

                <!--col mis_Torneos-->
                <div class="col-lg-4 proc" id="mis_Torneos">
                    <i class="fa fa-heart"></i>
                    <h3>Mis torneos</h3>
                    <p>Pudes ver aqui el historial de los torneos en que has competido.</p>
                </div><!--/col-->

                <!--col visualizar_Torneo-->
                <div class="col-lg-4 proc" id="visualizar_Torneo">
                    <i class="fa fa-eye"></i>
                    <h3>Visualizar torneos</h3>
                    <p>Accede a todos los torneos que se han desarrollado. Un filtro de busqueda te facilitara el trabajo</p>
                </div><!--/col-->

                <!--col Generar_Torneo-->
                <div class="col-lg-4 proc" id="Generar_Torneo">
                    <i class="fa fa-cogs"></i>
                    <h3 class="logo"><a href="CrearTorneo.aspx" style="color: #000000">Generar un nuevo torneo</a></h3>
                    <p>Genera un nuevo torneo. Esta herramienta no solo te permitira crearlo, tambien tendras un seguimiento del mismo.</p>
                </div><!--/col-->

                <!--col Generar_Listado_inscriptos-->
                <div class="col-lg-4 proc" id="Generar_Listado_inscriptos">
                    <i class="fa fa-cogs"></i>
                    <h3 class="logo"><a id="btn_generar_Listado_inscriptos" href="javascript:__doPostBack('btnGenerarListado_Click','')" style="color: #000000">Generar listado de inscriptos</a></h3>
                    <p>Genera un listado de los inscriptos a un torneo. Con esta herramienta podrás imprimir un listados con los inscriptos a un torneo.</p>
                </div><!--/col-->
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

      <!--APARTADO DE ADMINISTRACION DE CLASES -->
        <div class="container">
            <div class="row mt centered">
                <h1>ADMINISTRACION DE CLASES</h1>

                <!--col crear_nueva_clase-->
                <div class="col-lg-4 proc">
                    <i class="fa fa-pencil" id="crear_nueva_clase"></i>
                    <h3><a href="CrearClase.aspx"  style="color: #000000">Crear nueva clase</a></h3>
                    <p>Crea una nueva clase con sus respectivos horarios para administrarla.</p>
                </div><!--/col-->

                <!--col mis_Clases-->
                <div class="col-lg-4 proc" id="mis_Clases">
                    <i class="fa fa-heart"></i>
                    <h3>Mis clases</h3>
                    <p>Pudes ver aqui las clases en las que te has inscripto.</p>
                </div><!--/col-->

                <!--col visualizar_Clase-->
                <div class="col-lg-4 proc" id="">
                    <i class="fa fa-eye"></i>
                    <h3>Visualizar horarios</h3>
                    <p>Consulta los horarios en que se desarrollan las diferentes clases que se dictan en la academia</p>
                </div><!--/col-->

                <!--col inscribir_Alumno_Clase-->
                <div class="col-lg-4 proc" id="inscribir_Alumno_Clase">
                    <i class="fa fa-cogs"></i>
                    <h3 class="logo"><a style="color: #000000">Inscribir a un alumno</a></h3>
                    <p>Podras inscribir a un alumno a una determinada clase. Esto te permitira realizar una administracion del mismo.</p>
                </div><!--/col-->
             
            </div>
            <!--/row -->
        </div>
        <!--/container -->
    </div>
    <!--/Portfoliowrap -->


    <!-- ABOUT SEPARATOR -->
	<div class="sep torneo" data-stellar-background-ratio="0.5"></div>
    	
     <!--SECTOR ALUMNOS -->
    <section id="alumnos" title="clases"></section>
    <div id="alumnoswrap">

        <!--APARTADO DE ADMINISTRACION DE ALUMNOS -->
        <div class="container">
            <div class="row mt centered">
                <h1>ADMINISTRACION DE ALUMNNOS</h1>

                <!--col registrar_alumno-->
                <div class="col-lg-4 proc">
                    <i class="fa fa-pencil" id="registrar_alumno"></i>
                    <h3><a href="RegistrarAlumno.aspx" style="color: #000000">Registrar Alumno</a></h3>
                    <p>Registra un nuevo alumno. De esta forma podras contar con sus informacion para la posterior administracion del mismo.</p>
                </div>
                <!--/col-->

                  <!--col administrar_alumnos-->
                <div class="col-lg-4 proc">
                    <i class="fa fa-cogs" id="administrar_alumnos"></i>
                    <h3><a style="color: #000000">Administrar Alumnos</a></h3>
                    <p>Podras visualizar todos tus alumnos. Darlos de bajo. Modificar su perfil.</p>
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
				</div><!-- /col-lg-8 -->
			</div><! --/row -->
			
			<div class="row mt">
				<div class="col-lg-3 service">
					<i class="fa fa-star"></i><p>PREMIUM QUALITY<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-cloud"></i><p>CLOUD SERVICES<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-shield"></i><p>SECURED ACCOUNTS<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-heart"></i><p>100% SATISFACTION<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
			</div><! --/row -->
			<div class="row mt">
				<div class="col-lg-3 service">
					<i class="fa fa-trophy"></i><p>PREMIUM QUALITY<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-globe"></i><p>CLOUD SERVICES<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-lock"></i><p>SECURED ACCOUNTS<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
				<div class="col-lg-3 service">
					<i class="fa fa-thumbs-up"></i><p>100% SATISFACTION<br/><small>LOREM IPSUM DOLOR</small></p>
					<p class="text">Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer.</p>
				</div>
			</div><! --/row -->
			
		</div><! --/container -->
	</div><! --/servicewrap -->
	
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
					      <p><img class="img-circle" src="assets/img/pic-t1.jpg" width="80"></p>
					    </div>

					    <div class="item mb centered">
					      <h3>PAUL LEVINGSTON</h3>
					      <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
					      <p><img class="img-circle" src="assets/img/pic-t2.jpg" width="80"></p>
					    </div>

					    <div class="item mb centered">
					      <h3>LUCY LENNIN</h3>
					      <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
					      <p><img class="img-circle" src="assets/img/pic-t3.jpg" width="80"></p>
					    </div>

					  </div>
					  <!-- Indicators -->
					  <ol class="carousel-indicators">
					    <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
					    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
					    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
					  </ol>
					</div>
					  
				</div><! --/col-lg-8 -->
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/testimonials -->
	
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
						<small>5th Avenue, 987<br/>
						38399, New York,<br/>
						USA.</small>
					</p>
					<p>
						<small>Tel. 9888-4394<br/>
						Mail. Hello@coolfolks.com<br/>
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
			
			</div><!--/row -->
		</div><!-- /container -->
	</div>

    <form runat="server">

        <!--IMPLEMENTANDO PRUEBA DE VENTANA EMERGENTE-->
        <div class="modal fade" id="inscripcionTorneo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabel">Inscripción</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!-- PANEL DNI-->
                            <asp:Panel ID="pnl_dni" CssClass="panel panel-default" runat="server" Visible="false">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Ingresar DNI-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-1">
                                        <label class="pull-left"> DNI:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDni" class="caja2" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" ValidationGroup="grupoDni" OnClick="btnBuscarDni_Click"  UseSubmitBehavior="false" />
                                    </div>
                                    
                                </div>
                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ValidationGroup="grupoDni" ControlToValidate="txtDni" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                            </asp:Panel>

                            <!--PANEL DE INSCRIPCION-->

                            <asp:Panel ID="pnl_Inscripcion" CssClass="panel panel-default" runat="server" Visible="false">

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <div class="row centered">
                                    <div class="col centered">
                                        <asp:Label ID="Label6" runat="server" Text="Datos del Participante" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Ingresar nombre-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Ingresar nombre:</label>
                                    </div>
                                    <div class="col-md-4  pull-left">
                                        <asp:TextBox ID="txt_nombre" class="caja2" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="requeridoNombre" runat="server" ErrorMessage="Debe ingresar el nombre" Text="*" ControlToValidate="txt_nombre" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Ingresar apellido-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Ingresar apellido:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_apellido" class="caja2" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="requeridoApellido" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido" CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Sexo-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Sexo:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem>Femenino</asp:ListItem>
                                            <asp:ListItem>Masculino</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>


                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Peso-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Peso:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox class="caja2" ID="txt_peso" runat="server" placeholder="Ingrese peso"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="requeridoPeso" runat="server" ErrorMessage="Debe ingresar un peso" ControlToValidate="txt_peso" CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                        <%--<asp:CompareValidator ID="tipoPeso" runat="server" ErrorMessage="El peso debe ser un valor numérico" ControlToValidate="txt_peso" CssClass="text-danger" Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                        <asp:CompareValidator ID="positivoPeso" runat="server" ErrorMessage="El peso debe ser un valor mayor a 0" ControlToValidate="txt_peso" CssClass="text-danger" Type="Double" ValueToCompare="0" Operator="GreaterThan" Display="Dynamic" Text="*"></asp:CompareValidator>

                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Edad-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Edad</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox class="caja2" ID="txt_edad" runat="server" placeholder="Ingrese edad"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="requeridoEdad" runat="server" ErrorMessage="Debe ingresar la edad" ControlToValidate="txt_edad" CssClass="text-danger" Text="*"></asp:RequiredFieldValidator>
                                        <%--<asp:CompareValidator ID="tipoEdad" runat="server" ErrorMessage="La edad debe ser un valor numérico" ControlToValidate="txt_edad" CssClass="text-danger" Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                        <asp:CompareValidator ID="positivoEdad" runat="server" ErrorMessage="La edad debe ser un valor mayor a 0" ControlToValidate="txt_edad" CssClass="text-danger" Type="Double" ValueToCompare="0" Operator="GreaterThan" Display="Dynamic" Text="*"></asp:CompareValidator>

                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Faja-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Faja:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>                               
                            </asp:Panel>
                            <!-- Panel de errores -->
                             <asp:Panel ID="pnl_Errores" CssClass="panel panel-default" runat="server" Visible="true"> 
                                 <asp:ValidationSummary ID="ValidationSummary1"  HeaderText="Han ocurrido los siguentes errores:" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" CssClass="text-danger"  runat="server" />
                             </asp:Panel>                             
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer" runat="server">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!--IMPLEMENTANDO PRUEBA DE VENTANA EMERGENTE         ***DNI***       -->
        <div class="modal fade" id="inscripcionTorneoDNI" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2" >
            <div class="modal-dialog" role="document" >
                <div class="modal-content" >


                    <!--Cabecera-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabe2">Inscripción</h4>
                    </div>

                    <!--Cuerpo-->

                    <div class="modal-body">
                        <div class="form-group">

                            <!--Ingresar DNI-->
                            <asp:Panel ID="pnl_dni1" CssClass="panel panel-default" runat="server">
                                  <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row center-block">
                                    <div class="col-lg-3"></div>
                                    <div class=" col-lg-1">
                                        <label for="recipient-name" class="control-label">DNI:</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txt_dni" class="caja2" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <%--<asp:Button ID="btn_confirmarDni" onClick="btn_confirmarDni_Click" type="submit" class="btn btn-default" runat="server" Text="Aceptar" />--%>
                        
                       <!-- <asp:Button ID="btn_confirmar_" runat="server" CssClass="btn btn-default" Text="Aceptar" type="submit" />
                        -->

                       <!--
                      <asp:Button ID="btn_confirmar_dni" runat="server" Text="Aceptar" OnClick="btn_confirmar_dni_Click" CssClass="btn btn-default" />
                       -->
                        <!--
                        <INPUT  type="button" NAME="confirmarDNI" id="btn_confirmar_dni" value="aceptar" onclick="btn_confirmar_dni_onclick()">
                        -->
                        <!--
                            <asp:Button ID="btn_confirmar_dni1" runat="server" Text="Aceptar" CssClass="btn btn-default" OnClick="btn_confirmar_dni_Click"  type="submit" />
                      -->
                       <!--
                        <button id="btn_confirmar_dni" onclick="btn_confirmar_dni_Click()" type="button" class="btn" data-toggle="modal" data-target="#inscripcionTorneo" data-dismiss="modal" data-whatever="@mdo">Inscribir</button>
                         -->               
                        <!--
                        <a id="btn_confirmar_dni" class="btn btn-default" href="javascript:__doPostBack('btn_confirmar_dni_Click','')"  >Aceptar</a>
                        -->
                    </div>

                </div>
            </div>
        </div>
      
    <!--SCRIPT-->


    <script> 
        $('#inscripcionTorneo').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) 
            var recipient = button.data('whatever') 
            var modal = $(this)
            modal.find('.modal-title').text('New message to ' + recipient)
            modal.find('.modal-body input').val(recipient)
        })
        

        $('#inscripcionTorneoDNI').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var recipient = button.data('whatever') // Extract info from data-* attributes
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this)
            modal.find('.modal-title').text('New message to ' + recipient)
            modal.find('.modal-body input').val(recipient)
        })
    </script>
        
    </form>

    </asp:Content>