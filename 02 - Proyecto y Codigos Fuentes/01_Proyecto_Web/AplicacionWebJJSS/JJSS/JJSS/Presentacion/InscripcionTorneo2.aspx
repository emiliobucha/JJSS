<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionTorneo2.aspx.cs" Inherits="JJSS.InscripcionTorneo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <p>
        <asp:Label ID="lbl_Inscripcion" runat="server" Text="Inscripción" Font-Size="XX-Large" CssClass=""></asp:Label>
    </p>

    <asp:Panel ID="pnl_ingreso_datos" runat="server">
        <p>
        Elegir torneo:
       <asp:DropDownList ID="ddl_torneos" runat="server"></asp:DropDownList>
    </p>

   <p>
       Ingresar nombre:
       <asp:TextBox ID="txt_nombre" runat="server"></asp:TextBox>
   </p>
    <p > 
        Ingresar apellido:
        <asp:TextBox ID="txt_apellido" runat="server"></asp:TextBox>
    </p>
        <p>
            Sexo:
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                <asp:ListItem>Femenino</asp:ListItem>
                <asp:ListItem>Masculino</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p>
            Peso:
            <asp:TextBox ID="txt_peso" runat="server"></asp:TextBox>
        </p>
        <p>
            Edad
            <asp:TextBox ID="txt_edad" runat="server"></asp:TextBox>
        </p>
        <p>
            Faja:
            <asp:DropDownList ID="ddl_fajas" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" Height="32px" Width="80px"/>
            &nbsp;
            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" Height="32px" Width="80px" />
        </p>

    </asp:Panel>

    <asp:Panel ID="pnl_inscripcion_correcta" runat="server">
        <p>
            La inscripción se ha realizado correctamente

        </p>

        <p>
            <asp:Button ID="btn_cod_barra" runat="server" Text="Imprimir codigo de barra" Height="32px" />
        </p>

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
