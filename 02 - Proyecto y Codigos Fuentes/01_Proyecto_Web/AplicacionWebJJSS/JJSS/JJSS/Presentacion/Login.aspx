<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JJSS.Presentacion.Login" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="assets/ico/favicon.ico">

    <title>JJSS</title>

    <!-- Bootstrap core CSS -->
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
      <link href="../css/style.css" rel="stylesheet" />
      <link href="../css/font-awesome.min.css" rel="stylesheet" />
      <link href="../js/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->



  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  
      <script>
          $(document).ready(
              function () {
                  $("#datepicker1,#datepicker2").datepicker({
                      dateFormat: "dd/mm/yy",
                      monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                      dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                  });
              }
          );
      </script>


  </head>

  <body data-spy="scroll" data-offset="0" data-target="#theMenu">
      


<asp:Panel ID="Content4" runat="server">

    <section id="login" title="login"></section>

    <form id="form2" runat="server" >

        <asp:Panel ID="pnlLogin" runat="server" CssClass="panel panel-default" Height="100%" style="background-color:#9EBDC9">
            <div id="loginwrap">
                <div class="container" style="background-color:white">
                    <div class="row mt centered">

                        <h1>Iniciar Sesión</h1>
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group ">
                        <!--usuario-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-right 4">Usuario</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_usuario" runat="server" required="true" placeholder="Nombre de usuario" CssClass="caja2"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-right 4">Contraseña</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass" runat="server" TextMode="Password" required="true" placeholder="Contraseña" CssClass="caja2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Recordarme-->
                        <div class="row centered">
                            <div>
                                <asp:CheckBox ID="chk_recordar" Text="  &nbsp Recordarme" runat="server" />
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--olvido pass-->
                        <div class="row centered">
                            <div>
                                <asp:LinkButton ID="lnk_olvido" runat="server" OnClick="lnk_olvido_Click">¿Ha olvidado la contraseña?</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Boton-->
                        <div class="row centered">
                            <asp:Button ID="btn_iniciar_sesion" CssClass="btn btn-default" runat="server" Text="Iniciar Sesión" ValidationGroup="val_inicio_sesion" OnClick="btn_iniciar_sesion_Click" />
                        </div>
                         <!--Invitado-->
                        <div class="row centered">
                            <asp:Button ID="btn_invitado" CssClass="btn btn-default btn-link" formnovalidate="true" ForeColor="Black" Font-Bold="true" runat="server" Text="Iniciar como Invitado" OnClick="btn_invitado_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- /row -->
        </asp:Panel>

        <asp:Panel ID="pnl_cambiar_pass" runat="server" CssClass="panel panel-default">
            <div id="cambiar_pass_wrap">
                <div class="container">
                    <div class="form-group ">

                        <div class="row mt centered">
                            <h1>Cambiar contraseña</h1>
                            <p>&nbsp;</p>
                        </div>

                        <!--pass anterior-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Contraseña anterior</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_anterior" runat="server" required="true" CssClass="caja2" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass nueva-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Contraseña nueva</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_nueva" TextMode="Password" MaxLength="100" required="true" runat="server" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CompareValidator ID="compararNueva" runat="server" CssClass="text-danger" ErrorMessage="La contraseña nueva debe ser distinta a la anterior" Display="Dynamic" ControlToValidate="txt_pass_nueva" ControlToCompare="txt_pass_anterior" ValidationGroup="val_cambiar_pass" Operator="NotEqual"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass anterior-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Repetir contraseña nueva</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_repetida" runat="server" required="true" MaxLength="100" CssClass="caja2" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CompareValidator ID="compararPass" runat="server" CssClass="text-danger" ErrorMessage="Las contraseñas nuevas deben ser iguales" Display="Dynamic" ControlToValidate="txt_pass_repetida" ControlToCompare="txt_pass_nueva" ValidationGroup="val_cambiar_pass"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>




                        <!--Boton-->
                        <div class="row centered">
                            <asp:Button ID="btn_cambiar" CssClass="btn btn-default" runat="server" Text="Cambiar" ValidationGroup="val_cambiar_pass" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Panel>
       <!-- CONTACT SEPARATOR -->
       

        <div id="contactwrap">
            <div class="container">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <p>Contacto</p>
                    <div class=" col-xs-6 col-sm-6 col-md-3 col-lg-3">
                        <p>
                            <small>Buchaillot, Emilio<br />
                                Moll, Luciana<br />
                                Solovey, Melina<br />
                                Tita, Romina</small>
                        </p>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-3 col-lg-3">
                        <p>
                            <small>emiliobuchaillot@gmail.com<br />
                                lucianam910@gmail.com<br />
                                melinasolovey@gmail.com<br />
                                rominaftita@gmail.com</small>
                        </p>
                    </div>
                </div>

                <div class="col-lg-2 col-md-2 hidden-xs hidden-sm ">
                    <p>&nbsp;</p>
                </div>

                <div class="col-xs-12 col-sm-12 col-lg-4 col-md-4">
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                    <p>
                        <small>Universidad Tecnológica Nacional.<br />
                            Facultad Regional Córdoba.<br />
                            Argentina.</small>
                    </p>
                </div>

                <div class=" col-sm-12 col-md-12 col-lg-12 centered">
                    <p>
                        <small>JJSS (Jiu-Jitsu Sport System)<br />
                        </small>
                    </p>
                </div>


                <%-- <div class="col-lg-6" style="left: 0px; top: 0px">
                        <form role="form">
                            <div class="form-group">
                                <label for="exampleInputName1">Your Name</label>
                                <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter name">
                                <label for="exampleInputText1">Message</label>
                                <textarea class="form-control" rows="3"></textarea>
                            </div>
                            <button type="submit" class="btn btn-default">Submit</button>
                        </form>
                    </div>--%>

                <!--/row -->
            </div>
            <!-- /container -->
        </div>



    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
      <link href="../css/MisEstilos.css" rel="stylesheet" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/classie.js"></script>
    <script src="../js/smoothscroll.js"></script>
    <script src="../js/jquery.stellar.min.js"></script>
    <script src="../js/fancybox/jquery.fancybox.js"></script>  
    <script src="../js/main.js"></script>
    <script src="../js/jquery-ui-1.11.4.custom/jquery-ui.min.js"></script>

		<script>
            $(function () {
                $.stellar({
                    horizontalScrolling: false,
                    verticalOffset: 40
                });
            });
		</script>
		
		<script type="text/javascript">
            $(function () {
                //    fancybox
                jQuery(".fancybox").fancybox();
            });
	   </script>

       <script>
           $(document).ready(
               function () {
                   $("#datepicker").datepicker({
                       dateFormat: "dd/mm/yy",
                       monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                       dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                   });
               }
           );


           function htmlbodyHeightUpdate() {
               var height3 = $(window).height()
               var height1 = $('.nav').height() + 50
               height2 = $('.main').height()
               if (height2 > height3) {
                   $('html').height(Math.max(height1, height3, height2) + 10);
                   $('body').height(Math.max(height1, height3, height2) + 10);
               }
               else {
                   $('html').height(Math.max(height1, height3, height2));
                   $('body').height(Math.max(height1, height3, height2));
               }

           }
           $(document).ready(function () {
               htmlbodyHeightUpdate()
               $(window).resize(function () {
                   htmlbodyHeightUpdate()
               });
               $(window).scroll(function () {
                   height2 = $('.main').height()
                   htmlbodyHeightUpdate()
               });
           });
       </script>


  

  </body>
</html>