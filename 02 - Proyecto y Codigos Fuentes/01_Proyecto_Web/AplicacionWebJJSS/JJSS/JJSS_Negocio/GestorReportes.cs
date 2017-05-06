using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JJSS_Entidad;
using System.Configuration;

namespace JJSS_Negocio
{
    class GestorReportes
    {


        public string GenerarReporteListadoParticipantes(List<Object> pListado)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] oPar = new Microsoft.Reporting.WinForms.ReportParameter[0];

            String sFile = ModReportes.GetTempFileName(ConfigurationManager.AppSettings["temp"], "Listado", "pdf");
            System.IO.File.WriteAllBytes(sFile, GenerarPDF(pListado, oPar, "rptListadoTorneo"));
            return sFile;

        }


        public byte[] GenerarPDF(List<Object> pDatos, Microsoft.Reporting.WinForms.ReportParameter[] pPar, String pReporte)
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

                List<Object> dtDatos = pDatos;
                Microsoft.Reporting.WinForms.ReportDataSource dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DATOS", dtDatos);

                oReportViewer.LocalReport.LoadReportDefinition(ModReportes.ObtenerReporte(pReporte));

                oReportViewer.LocalReport.DataSources.Add(dataSource);
                //oReportViewer.LocalReport.SetParameters(pPar);
                Byte[] memoryBuffer = oReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

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

