﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;




namespace JJSS.Presentacion

{
    public partial class CrearTorneo : System.Web.UI.Page
    {

       
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


       

        protected void btn_aceptar_Click1(object sender, EventArgs e)
        {
            //GestorTorneos gestorTorneos = new GestorTorneos();
            //gestorTorneos.GenerarNuevoTorneo(DateTime.Today, "Hola", (float)1.5, (float)2, "11:00", 1, DateTime.Today, "15:00");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }


}