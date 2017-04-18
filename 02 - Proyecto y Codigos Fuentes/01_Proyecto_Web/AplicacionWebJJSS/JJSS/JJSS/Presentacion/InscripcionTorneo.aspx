<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionTorneo.aspx.cs" Inherits="JJSS.InscripcionTorneo" %>

<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
    <div id="headerwrap">
        <div class="container">
            <div class="row">
                <div class="col-md-6 col-md-offset-3">
                    <h1>Inscripcion Torneo</h1>
                </div>
            </div>
            <! --/row -->
        </div>
        <! --/container -->
    </div>
    <! --/headerwrap -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="crearTorneo" title="crearTorneo"></section>
    <asp:Panel ID="pnlFormulario" runat="server">
        <div id="crearTorneowrap">
            <div class="container">
                <div class="row mt centered">
                    <div>
                    <asp:Label ID="lbl_Inscripcion" runat="server" Text="Inscripción Torneo" Font-Size="XX-Large" CssClass=""></asp:Label>
                        </div>
                    
                    <div class="col-lg-5 mt centered">
                        <form id="form1" runat="server">
                            <div class="form-group ">

                                <p>
                                    <label class="pull-left">Elegir torneo:</label>
                                    <asp:DropDownList ID="ddl_torneos" class="form-control" runat="server"></asp:DropDownList>

                                </p>

                                <p>
                                    <label class="pull-left">Ingresar nombre:</label>
                                    <asp:TextBox ID="txt_nombre" class="form-control" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requeridoNombre" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre" CssClass="txt-danger"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <label class="pull-left">Ingresar apellido:</label>
                                    <asp:TextBox ID="txt_apellido" class="form-control" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requeridoApellido" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <label class="pull-left">Sexo:</label>
                                    <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem>Femenino</asp:ListItem>
                                        <asp:ListItem>Masculino</asp:ListItem>
                                    </asp:RadioButtonList>
                                </p>
                                <p>
                                    <label class="pull-left">Peso:</label>
                                    <asp:TextBox class="form-control" ID="txt_peso" runat="server" placeholder="Ingrese peso"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requeridoPeso" runat="server" ErrorMessage="Debe ingresar un peso" ControlToValidate="txt_peso" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <label class="pull-left">Edad</label>
                                    <asp:TextBox class="form-control" ID="txt_edad" runat="server" placeholder="Ingrese edad"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requeridoEdad" runat="server" ErrorMessage="Debe ingresar la edad" ControlToValidate="txt_edad" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <label class="pull-left">Faja:</label>
                                    <asp:DropDownList class="form-control" ID="ddl_fajas" runat="server">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" />
                                </p>
                                </div>
                        </form>
                    </div>
                    <! --/row -->		
                </div>

            </div>
        </div>
 
    </asp:Panel>

    <asp:Panel id="inscripcionCorrecta" hidden="true" runat="server">
        <div class="container">
        <p>
           <label class="pull-left" > La inscripción se ha realizado correctamente</label>
            <asp:Button ID="btn_cod_barra" class="btn btn-default" runat="server" Text="Imprimir codigo de barra" Height="32px" />
        </p>
            </div>

    </asp:Panel>
</asp:Content>
