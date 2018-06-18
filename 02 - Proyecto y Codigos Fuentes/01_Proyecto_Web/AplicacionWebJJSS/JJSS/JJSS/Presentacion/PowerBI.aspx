<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PowerBI.aspx.cs" Inherits="JJSS.Presentacion.Power_BI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <div>
        &nbsp;
    </div>

    <div class="centered">
        <h1>Inteligencia de Negocios</h1>
        <p>&nbsp;</p>
    </div>
    <div>
        &nbsp;
    </div>
    <div class="container justify-content-center centered">
        <div class="row justify-content-center centered">
            <div class="col col-12 justify-content-center centered">
                <iframe width="800" height="600" src="https://app.powerbi.com/view?r=eyJrIjoiZThkNTAwMzItMmY3YS00MDJjLTg4YjMtOWUwOTZhYTY4MWYzIiwidCI6ImRjZmI2MzJhLWI4OTYtNDI4OC04NDEzLWVjOGQ5NTQxMDZlNiIsImMiOjR9" frameborder="0" allowfullscreen="true"></iframe>
            </div>
        </div>
    </div>
    <div>
        &nbsp;
    </div>
</asp:Content>
