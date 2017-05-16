<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionTorneo.aspx.cs" Inherits="JJSS.InscripcionTorneo" %>



<asp:Content ID="crearTorneoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>			
    <a href="#inscripcionTorneo" class="smoothScroll">Inscripcion</a>
</asp:Content>


<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="inscripcionTorneo" title="inscripcionTorneo"></section>
    <asp:Panel ID="pnlFormulario" runat="server">
        <div id="crearTorneowrap">
            <div class="container">
                <div class="row mt centered">
                    <div>
                        <asp:Label ID="lbl_Inscripcion" runat="server" Text="Inscripción Torneo" Font-Size="XX-Large" CssClass=""></asp:Label>
                    </div>

                    <form id="form1" runat="server">
                        <div class="form-group ">
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Elegir torneo-->
                             <asp:Panel ID="pnl_elegirTorneo" CssClass="panel panel-default" runat="server">
                            <div class="row centered">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    <label class="pull-left">Elegir torneo:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddl_torneos" class="caja2" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnAceptarTorneo" runat="server" Text="Aceptar" CssClass="btn btn-default" OnClick="btnAceptarTorneo_Click" />
                                    <asp:Button ID="btnGenerarListado" runat="server" Text="GenerarListado" CssClass="btn btn-default" OnClick="btnGenerarListado_Click" />
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>
                                 </asp:Panel>
                            <!--PANEL DE INFORMACION DEL TORNEO-->

                            <asp:Panel ID="pnl_InfoTorneo" CssClass="panel panel-default" runat="server">
                               
                                <!--Nombre-->
                                <div class="row centered"><p>&nbsp;</p></div>

                                <div class="row centered">
                                    <div class="col centered">
                                        
                                        <asp:Label ID="Label5" runat="server" Text="Informacion del Evento " Font-Bold="true" Font-Size="Large"></asp:Label> 
                                        <asp:Label ID="lbl_NombreTorneo" CssClass="centered" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    </div>
                                </div>  

                                <!--Direccion-->
                               <!--  <div class="row centered"><p>&nbsp;</p></div>

                                    <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Direccion del evento:&nbsp; </label>
                                        <asp:Label ID="lbl_Direccion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                -->
                                <!--Fecha-->
                                 <div class="row centered"><p>&nbsp;</p></div>

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Fecha:&nbsp; </label>
                                        <asp:Label ID="lbl_FechaDeTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="Label1" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                        <asp:Label ID="lbl_HoraTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="Label2" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                                    </div>
                                </div>
                            
                                <!--Cierre Inscripciones-->
                                 <div class="row centered"><p>&nbsp;</p></div>

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-6">
                                        <label class="pull-left">Cierre de Inscripciones:&nbsp;</label>  
                                        <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>                                  
                                    </div>
                                </div>
                                
                                <!--Precio-->
                                 <div class="row centered"><p>&nbsp;</p></div>

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-5">
                                        <label class="pull-left">El costo de inscripcion:&nbsp; </label>                                        
                                        <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>  
                                        <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        <label class="pull-left">&nbsp; Precio Absoluto:&nbsp;</label>                                        
                                        <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text="$"></asp:Label>  
                                        <asp:Label ID="lbl_CostoInscripcionAbsoluto" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>


                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                    </div>
                                </div>



                            </asp:Panel>

                             <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                            <!-- PANEL DNI-->
                            <asp:Panel ID="pnl_dni" CssClass="panel panel-default" runat="server" Visible="false">
                                <div class="row centered"><p>&nbsp;</p></div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Ingresar DNI-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label class="pull-left">Ingresar DNI del participante:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtDni" class="caja2" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" ValidationGroup="grupoDni" OnClick="btnBuscarDni_Click" OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ValidationGroup="grupoDni" ControlToValidate="txtDni" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                            </asp:Panel>

                            <!--PANEL DE INSCRIPCION-->

                            <asp:Panel ID="pnl_Inscripcion" CssClass="panel panel-default" runat="server" Visible="false">

                                <div class="row centered"><p>&nbsp;</p></div>

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
                                    <div class="col-md-2">
                                        <label class="pull-left">Ingresar nombre:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txt_nombre" class="caja2" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="requeridoNombre" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Ingresar apellido-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label class="pull-left">Ingresar apellido:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txt_apellido" class="caja2" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="requeridoApellido" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Sexo-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label class="pull-left">Sexo:</label>
                                    </div>
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
                                        <label class="pull-left">Peso:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox class="caja2" ID="txt_peso" runat="server" placeholder="Ingrese peso"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="requeridoPeso" runat="server" ErrorMessage="Debe ingresar un peso" ControlToValidate="txt_peso" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        <%--<asp:CompareValidator ID="tipoPeso" runat="server" ErrorMessage="El peso debe ser un valor numérico" ControlToValidate="txt_peso" CssClass="text-danger" Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                        <asp:CompareValidator ID="positivoPeso" runat="server" ErrorMessage="El peso debe ser un valor mayor a 0" ControlToValidate="txt_peso" CssClass="text-danger" Type="Double"  ValueToCompare="0" Operator="GreaterThan" Display="Dynamic"></asp:CompareValidator>
                                    
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Edad-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label class="pull-left">Edad</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox class="caja2" ID="txt_edad" runat="server" placeholder="Ingrese edad"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="requeridoEdad" runat="server" ErrorMessage="Debe ingresar la edad" ControlToValidate="txt_edad" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        <%--<asp:CompareValidator ID="tipoEdad" runat="server" ErrorMessage="La edad debe ser un valor numérico" ControlToValidate="txt_edad" CssClass="text-danger" Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                        <asp:CompareValidator ID="positivoEdad" runat="server" ErrorMessage="La edad debe ser un valor mayor a 0" ControlToValidate="txt_edad" CssClass="text-danger" Type="Double" ValueToCompare="0" Operator="GreaterThan" Display="Dynamic"></asp:CompareValidator>
                                    
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Faja-->

                                <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <label class="pull-left">Faja:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Boton Aceptar-->
                                <div class="row centered">
                                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click"  OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                  
                                </div>
                            </asp:Panel>
                            
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="inscripcionCorrecta" hidden="true" runat="server">
        <div class="container">
            <p>
                <label class="pull-left">La inscripción se ha realizado correctamente</label>
                <asp:Button ID="btn_cod_barra" class="btn btn-default" runat="server" Text="Imprimir codigo de barra" Height="32px" />
            </p>
        </div>

    </asp:Panel>
</asp:Content>
