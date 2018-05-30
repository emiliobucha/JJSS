using Telerik;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using JJSS_Entidad;
using JJSS_Negocio.Administracion;
using Telerik.Web.UI;

namespace JJSS.Presentacion.Clases
{
    public partial class VerCalendario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarComboAcademias();
                cargarComboProfesores();
                cargarComboTipoClase();

                cargarScheduler();
            }
        }

        private void cargarComboProfesores()
        {
            GestorProfesores gp = new GestorProfesores();
            List<profesor> profesores = gp.ObtenerProfesores();
            foreach (profesor p in profesores)
            {
                p.nombre = p.nombre + " " + p.apellido;
            }
            profesor itemTodos = new profesor()
            {
                id_profesor = 0,
                nombre = "Todos"
            };
            profesores.Insert(0, itemTodos);
            ddl_profesores.DataSource = profesores;
            ddl_profesores.DataValueField = "id_profesor";
            ddl_profesores.DataTextField = "nombre";
            ddl_profesores.DataBind();
        }

        private void cargarComboAcademias()
        {
            GestorAcademias ga = new GestorAcademias();
            List<academia> academias = ga.ObtenerAcademias();
            academia itemTodos = new academia()
            {
                id_academia = 0,
                nombre = "Todas"
            };
            academias.Insert(0, itemTodos);
            ddl_academias.DataSource = academias;
            ddl_academias.DataValueField = "id_academia";
            ddl_academias.DataTextField = "nombre";
            ddl_academias.DataBind();
        }

        private void cargarComboTipoClase()
        {
            GestorTipoClase gtc = new GestorTipoClase();
            List<tipo_clase> academias = gtc.ObtenerTipoClase();
            tipo_clase itemTodos = new tipo_clase()
            {
                id_tipo_clase = 0,
                nombre = "Todas"
            };
            academias.Insert(0, itemTodos);
            ddl_tipo_clase.DataSource = academias;
            ddl_tipo_clase.DataValueField = "id_tipo_clase";
            ddl_tipo_clase.DataTextField = "nombre";
            ddl_tipo_clase.DataBind();
        }

        private void cargarScheduler()
        {
            GestorClases gc = new GestorClases();
            string filtroNombre = txt_filtro_nombre.Text;
            int filtroProfesor = Convert.ToInt32(ddl_profesores.SelectedValue);
            string filtroAcademia = ddl_academias.SelectedValue != "0" ? ddl_academias.SelectedItem.Text : "";
            string filtroTipoClase = ddl_tipo_clase.SelectedValue != "0" ? ddl_tipo_clase.SelectedItem.Text : "";

            List<ClaseHorario> horarios = gc.ObtenerTodosLosHorarios(filtroNombre, filtroTipoClase, filtroProfesor, filtroAcademia);
            List<Appointment> app = new List<Appointment>();
            if (horarios.Count == 0)
            {
                RadScheduler1.Visible = false;
                lbl_vacio.Text = "No hay datos para mostrar con los filtros seleccionados";
            }
            else
            {
                RadScheduler1.Visible = true;
                lbl_vacio.Text = "";
                foreach (ClaseHorario h in horarios)
                {
                    Appointment newAppointment = new Appointment();
                    newAppointment.ID = h.id;
                    newAppointment.Subject = h.nombreClase;

                    int daysToAdd = buscarDia(Convert.ToInt32(h.dia));
                    newAppointment.Start = DateTime.Parse(h.desde).AddDays(daysToAdd);
                    newAppointment.End = DateTime.Parse(h.hasta).AddDays(daysToAdd);
                    newAppointment.Description = h.idClase.ToString();
                    newAppointment.ToolTip = h.tipoClase;

                    RecurrenceRange range = new RecurrenceRange();
                    range.Start = newAppointment.Start;
                    range.EventDuration = newAppointment.End - newAppointment.Start;
                    //range.RecursUntil = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
                    range.MaxOccurrences = 100;

                    RecurrenceDay recurrenceDay = calcularRecurrenceDay(Convert.ToInt32(h.dia));

                    RecurrenceRule newWeekly = new WeeklyRecurrenceRule(1, recurrenceDay, range);

                    foreach (DateTime o in newWeekly.Occurrences)
                    {
                        int a = o.Day;
                    }

                    newAppointment.RecurrenceRule = newWeekly.ToString();
                    app.Add(newAppointment);
                    //RadScheduler1.InsertAppointment(newAppointment);
                    //RadScheduler1.Rebind();
                }
            }

            RadScheduler1.DataSource = app;
            RadScheduler1.DataBind();

        }

        private RecurrenceDay calcularRecurrenceDay(int diaSemana)
        {
            switch (diaSemana)
            {
                case 0: return RecurrenceDay.Sunday;
                case 1: return RecurrenceDay.Monday;
                case 2: return RecurrenceDay.Tuesday;
                case 3: return RecurrenceDay.Wednesday;
                case 4: return RecurrenceDay.Thursday;
                case 5: return RecurrenceDay.Friday;
                case 6: return RecurrenceDay.Saturday;
                default: return RecurrenceDay.None;
            }
        }

        private int buscarDia(int classDay)
        {
            DayOfWeek todayDayOfWeek = DateTime.Today.DayOfWeek;
            int[][] matriz = new int[7][];
            matriz[0] = new int[] { 0, 1, 2, 3, 4, 5, 6 };
            matriz[1] = new int[] { -1, 0, 1, 2, 3, 4, 5 };
            matriz[2] = new int[] { -2, -1, 0, 1, 2, 3, 4 };
            matriz[3] = new int[] { -3, -2, -1, 0, 1, 2, 3 };
            matriz[4] = new int[] { -4, -3, -2, -1, 0, 1, 2 };
            matriz[5] = new int[] { -5, -4, -3, -2, -1, 0, 1 };
            matriz[6] = new int[] { -6, -5, -4, -3, -2, -1, 0 };

            int numberTodayDayOfWeek = (int)todayDayOfWeek;

            return matriz[numberTodayDayOfWeek][classDay];
        }

        protected void RadScheduler1_AppointmentClick(object sender, SchedulerEventArgs e)
        {
            //e.Appointment.BackColor= System.Drawing.Color.
        }

        protected void RadScheduler1_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            string valorHexa = "#" + string.Format("{0:X}", Convert.ToInt32(e.Appointment.ID) * 1000);

            Color color = ColorTranslator.FromHtml(valorHexa);
            e.Appointment.BackColor = Color.FromArgb(color.ToArgb());
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarScheduler();
        }
    }
}