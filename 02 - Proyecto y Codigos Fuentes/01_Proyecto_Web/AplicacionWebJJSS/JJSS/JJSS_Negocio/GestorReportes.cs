using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JJSS_Entidad;
using System.Configuration;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{ /*
    *   Clase que se encarga de la generacion de los reportes y su escritura en un archivo
    */
    class GestorReportes
    {


        /*
       * Método que sirve para generar un Reporte en PDF con el listado de asistentes a una clase un dia
       * Parámetros: Listado de Participantes
       * Retorno: String ruta y nombre completo del archivo generado en PDF
       */
      
        public string GenerarReporteListadoAsistentes(List<ListadoAsistencia> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ListadoAsistencias", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptListadoAsistencia"));
            return sFile;

        }



        /*
      * Método que sirve para generar un Reporte en PDF con el comprobante de evento
      * Parámetros: Listado de un objeto participante
      * Retorno: String ruta y nombre completo del archivo generado en PDF
      */
        //public string GenerarReporteListadoParticipantes(List<Object> pListado, String pTorneoNombre, String pSede, String pDireccion, String pFecha , String pHora)
        public string GenerarReporteComprInscripcionEvento(List<CompInscripcionEvento> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ComprobanteInscripcionEvento", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptCompInscripcionEvento"));
            return sFile;

        }

        /*
        * Método que sirve para generar un Reporte en PDF con el comprobante de evento
        * Parámetros: Listado de un objeto participante
        * Retorno: String ruta y nombre completo del archivo generado en PDF
        */
        public string GenerarReporteComprInscripcionEventoPago(List<CompInscripcionEvento> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ComprobanteInscripcionEventoPago", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptCompInscripcionEventoPago"));
            return sFile;

        }

        /*
        * Método que sirve para generar un Reporte en PDF con el comprobante de evento
        * Parámetros: Listado de un objeto participante
        * Retorno: String ruta y nombre completo del archivo generado en PDF
        */
        public string GenerarReporteComprInscripcionClasePago(List<CompInscripcionClasePago> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ComprobanteInscripcionClasePago", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptCompInscripcionClasePago"));
            return sFile;

        }


        /*
           * Método que sirve para generar un Reporte en PDF con el comprobante de torneo
           * Parámetros: Listado de un objeto participante
           * Retorno: String ruta y nombre completo del archivo generado en PDF
           */
        public string GenerarReporteComprInscripcionTorneo(List<CompInscripcionTorneo> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ComprobanteInscripcionTorneo", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptCompInscripcionTorneo"));
            return sFile;

        }



        /*
        * Método que sirve para generar un Reporte en PDF con el comprobante de torneo
        * Parámetros: Listado de un objeto participante
        * Retorno: String ruta y nombre completo del archivo generado en PDF
        */
        //public string GenerarReporteListadoParticipantes(List<Object> pListado, String pTorneoNombre, String pSede, String pDireccion, String pFecha , String pHora)
        public string GenerarReporteComprInscripcionTorneoPago(List<CompInscripcionTorneo> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ComprobanteInscripcionTorneoPago", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptCompInscripcionTorneoPago"));
            return sFile;

        }


        /*
   * Método que sirve para generar un Reporte en PDF con el comprobante de torneo
   * Parámetros: Listado de un objeto participante
   * Retorno: String ruta y nombre completo del archivo generado en PDF
   */
        public string GenerarReporteListadoResultadosTorneo(List<ReporteResultadosTorneo> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ResultadosTorneo", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptListadoResultados"));
            return sFile;

        }


        /*
         * Método que sirve para generar un Reporte en PDF con el listado de participantes a un torneo
         * Parámetros: Listado de Participantes
         * Retorno: String ruta y nombre completo del archivo generado en PDF
         */
        //public string GenerarReporteListadoParticipantes(List<Object> pListado, String pTorneoNombre, String pSede, String pDireccion, String pFecha , String pHora)
        public string GenerarReporteListadoParticipantes(List<ParticipantesTorneoResultado> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ListadoTorneo", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptListadoTorneo"));
            return sFile;

        }


        /*
       * Método que sirve para generar un Reporte en PDF con el listado de participantes a un torneo
       * Parámetros: Listado de Participantes
       * Retorno: String ruta y nombre completo del archivo generado en PDF
       */
        //public string GenerarReporteListadoParticipantes(List<Object> pListado, String pTorneoNombre, String pSede, String pDireccion, String pFecha , String pHora)
        public string GenerarReporteListadoParticipantesEvento(List<ParticipantesEventoResultado> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "ListadoEvento", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptListadoEvento"));
            return sFile;

        }


        /*Método para generar el archivo PDF final listo para ser escrito. También se encarga de eliminar de la memoria con el Garbage Collector los restos que queden
         * Parametros:
         *              pDatos: List de objetos que se quieren mostrar en el reporte, coleccion de datos para ser agregados al datasource del reporte
         *              pPar : Arreglo de ReportParameters son parametros del reporte mismo, no forman habitualmente parte de la coleccion de datos 
         *              pReporte : nombre del reporte en el cual se basa el PDF
         * Retornos: Stream de Bytes el cual se transforma en un pdf cuando se llama a que se escriban 
         *            
         */
        public byte[] GenerarPDF<TEntrada>(List<TEntrada> pDatos, Microsoft.Reporting.WinForms.ReportParameter[] pPar, String pReporte)
        {
            Microsoft.Reporting.WinForms.ReportViewer oReportViewer = null;
            try
            {

                oReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
                oReportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                String encoding = String.Empty;
                String mimeType = String.Empty;
                String extension = String.Empty;
                Microsoft.Reporting.WinForms.Warning[] warnings = null;
                String[] streamids = null;

                List<TEntrada> dtDatos = pDatos;
                Microsoft.Reporting.WinForms.ReportDataSource dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DATOS", dtDatos);

                oReportViewer.LocalReport.LoadReportDefinition(ModReportes.ObtenerReporte("Reportes." + pReporte));

                oReportViewer.LocalReport.DataSources.Add(dataSource);
                //oReportViewer.LocalReport.SetParameters(pPar);
                String deviceInfo = "<DeviceInfo>" +
                 "<OutputFormat>PDF</OutputFormat>" +
                 "<HumanReadablePDF>True</HumanReadablePDF>" +
                  "</DeviceInfo>";

                Byte[] memoryBuffer = oReportViewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

                return memoryBuffer;
            }
            finally
            {
                if (oReportViewer != null)
                    oReportViewer.Dispose();
                GC.Collect();
            }


        }
    }






}

