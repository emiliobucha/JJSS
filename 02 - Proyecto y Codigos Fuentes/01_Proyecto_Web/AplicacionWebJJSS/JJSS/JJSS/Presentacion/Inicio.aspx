﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="JJSS.Presentacion.Inicio" %>




<asp:Content ID="InicioMenu" ContentPlaceHolderID="cphMenu" runat="server">
    		<a href="#home" class="smoothScroll">Home</a>
			<a href="#about" class="smoothScroll">About</a>
			<a href="#torneos" class="smoothScroll">Torneos</a> 
            <a href="#services" class="smoothScroll">Services</a>
			<a href="#contact" class="smoothScroll">Contact</a>
</asp:Content>

<asp:Content ID="InicioEncabezado"  ContentPlaceHolderID="cphEncabezado" runat="server">
    <section id="home" title="home"></section>
	<div id="headerwrap">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3">
					<h1>JJSS</h1>
				</div>
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/headerwrap -->
</asp:Content>

<asp:Content ID="InicioContenido" ContentPlaceHolderID="cphContenido" runat="Server">   

    <section id="about" title="about"></section>
	<div id="aboutwrap">
		<div class="container">
			<div class="row">
				<div class="col-lg-4 name">
					<img class="img-responsive" src="assets/img/pic.png">
					<p>HINOJAL</p>
					<div class="name-label"></div>
				</div><! --/col-lg-4-->
				<div class="col-lg-8 name-desc">
					<h2>TALENTED DESIGNER & <br/>FRONT-END DEVELOPER <br/>ENJOYING THE BEAUTIFUL NYC</h2>
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
	
	<! -- ABOUT SEPARATOR -->
	<div class="sep about" data-stellar-background-ratio="0.5"></div>
	
	<! -- TORNEOS SECTION -->
	<section id="torneos" title="torneos"></section>
	<div id="torneoswrap">
		<div class="container">
			<div class="row mt centered">
				<h1>ULTIMOS TORNEOS</h1>
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port04.jpg"><img class="img-responsive" src="assets/img/portfolio/port04.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
				
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port05.jpg"><img class="img-responsive" src="assets/img/portfolio/port05.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
				
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port06.jpg"><img class="img-responsive" src="assets/img/portfolio/port06.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
			</div><!-- /row -->
	
			<div class="row mt">
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port01.jpg"><img class="img-responsive" src="assets/img/portfolio/port01.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
				
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port02.jpg"><img class="img-responsive" src="assets/img/portfolio/port02.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
				
				<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 desc">
					<div class="project-wrapper">
	                    <div class="project">
	                        <div class="photo-wrapper">
	                            <div class="photo">
	                            	<a class="fancybox" href="assets/img/portfolio/port03.jpg"><img class="img-responsive" src="assets/img/portfolio/port03.jpg" alt=""></a>
	                            </div>
	                            <div class="overlay"></div>
	                        </div>
	                    </div>
	                </div>
				</div><!-- col-lg-4 -->
			</div><!-- /row -->
		</div><! --/container -->
		<div class="container">
			<div class="row mt centered">
				<h1>ADMINISTRACION DE TORNEOS</h1>
                <div class="col-lg-4 proc">                    
					<i class="fa fa-pencil" id="Insrcibir_Torneo"></i>               
					<h3><a href="InscripcionTorneo.aspx" style="color: #000000">Inscripciones </a></h3>
					<p>Accede a los torneos que estan pronto a desarrollar e inscribete.</p>
				</div>
				<div class="col-lg-4 proc">                    
					<i class="fa fa-heart"></i>
					<h3>Mis torneos</h3>
					<p>Pudes ver aqui el historial de los torneos en que has competido.</p>
				</div>
				<div class="col-lg-4 proc">
					<i class="fa fa-eye"></i>
					<h3>Visualizar torneos</h3>
					<p>Accede a todos los torneos que se han desarrollado. Un filtro de busqueda te facilitara el trabajo</p>
				</div>
				<div class="col-lg-4 proc" id="Generar_Torneo">
					<i class="fa fa-cogs"></i>
					<h3 class="logo"><a href="CrearTorneo.aspx" style="color: #000000">Generar un nuevo torneo</a></h3>
					<p>Genera un nuevo torneo. Esta herramienta no solo te permitira crearlo, tambien tendras un seguimiento del mismo.</p>
				</div>				
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/Portfoliowrap -->

	<! -- PORTFOLIO SEPARATOR -->
	<div class="sep portfolio" data-stellar-background-ratio="0.5"></div>
	
	
	<! -- SERVICE SECTION -->
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
	
	<! -- SERVICE SECTION -->
	<section id="contact" title="contact"></section>
	<! -- CONTACT SEPARATOR -->
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
					    <label for="exampleInputEmail1">Email address</label>
					    <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
					    <label for="exampleInputText1">Message</label>
					    <textarea class="form-control" rows="3"></textarea>
					  </div>
					  <button type="submit" class="btn btn-default">Submit</button>
					</form>
				</div>
			
			</div><! --/row -->
		</div><!-- /container -->
	</div>
    
</asp:Content>