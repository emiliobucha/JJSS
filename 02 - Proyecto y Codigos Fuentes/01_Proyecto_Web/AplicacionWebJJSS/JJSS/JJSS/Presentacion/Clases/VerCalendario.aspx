<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerCalendario.aspx.cs" Inherits="JJSS.Presentacion.Clases.VerCalendario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form runat="server">

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

                <div class="col-auto "><a>Tipo de Clase</a></div>
                <div class="col-md-2 col-lg-2">
                    <asp:DropDownList ID="ddl_tipo_clase" class="caja2" runat="server"></asp:DropDownList>
                </div>

                <div class="col justify-content-center ">
                    <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btn_buscar_Click" />
                </div>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container">
            <h2><asp:Label ID="lbl_vacio" runat="server" Text=""></asp:Label></h2>
            <telerik:RadScheduler RenderMode="Auto" runat="server" ID="RadScheduler1"
                ReadOnly="true" AllowDelete="false" AllowEdit="false" AllowInsert="false" ShowFullTime="true" ShowAllDayRow="true"
                DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End" DataRecurrenceField="RecurrenceRule"
                DataRecurrenceParentKeyField="RecurrenceParentId" OnAppointmentDataBound="RadScheduler1_AppointmentDataBound" 
                HoursPanelTimeFormat="hh:mm tt">
                <AdvancedForm Modal="true" ></AdvancedForm>
                <TimelineView UserSelectable="false"></TimelineView>
                <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>
                <Reminders Enabled="false"></Reminders>
                <WeekView ShowHoursColumn="true" />

            </telerik:RadScheduler>
        </div>
    </form>
</asp:Content>
