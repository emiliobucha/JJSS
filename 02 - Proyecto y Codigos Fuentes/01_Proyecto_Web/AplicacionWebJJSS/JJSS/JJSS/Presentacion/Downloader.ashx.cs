using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJSS.Presentacion
{
    /// <summary>
    /// Descripción breve de Downloader
    /// </summary>
    public class Downloader : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            response.ClearContent();
            response.Clear();
            response.AddHeader("Content-Type", "Application/octet-stream");
            response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(request["sFile"]) + "\"");
            response.WriteFile(request["sFile"]);
            response.Flush();
            response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


       
    }
}