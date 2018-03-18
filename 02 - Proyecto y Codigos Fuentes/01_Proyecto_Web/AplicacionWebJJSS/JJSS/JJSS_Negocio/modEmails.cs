using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JJSS_Negocio
{
    public class modEmails
    {
        private String MAIL_SERVIDOR = "smtp.gmail.com";
        private String MAIL_USUARIO = "jjss.hinojal";
        private String MAIL_CLAVE = "jjssmariano";
        private String MAIL_REMITENTE = "jjss.hinojal@gmail.com";
        private long MAIL_PUERTO = 465;
        private long MAIL_AUT = 1;
        private bool MAIL_SSL = true;
        private String PARCIAL_MAIL = "emiliobuchaillot@gmail.com";


        public ArrayList Msg_Destinatarios = new ArrayList();
        public ArrayList Msg_CC = new ArrayList();
        public ArrayList Msg_BCC = new ArrayList();
        public String Msg_ReplyTo;
        public ArrayList Msg_CCO = new ArrayList();

        public String Msg_Asunto;
        public String Msg_Cuerpo;

        public ArrayList Msg_Adjuntos = new ArrayList();

        public void Enviar()
        {
            //Prueba();
            System.Web.Mail.MailMessage mailMsg = new System.Web.Mail.MailMessage();

            foreach (String sAux in Msg_CC)
            {
                mailMsg.Cc = (mailMsg.Cc + ";" + sAux).Replace(";", "");

            }
            foreach (String sAux in Msg_CCO)
            {
                mailMsg.Bcc = (mailMsg.Bcc + ";" + sAux).Replace(";", "");

            }
            foreach (String sAux in Msg_Destinatarios)
            {
                mailMsg.To = (mailMsg.To + ";" + sAux).Replace(";", "");

            }
            mailMsg.From = MAIL_REMITENTE;
            mailMsg.Subject = Msg_Asunto;
            mailMsg.BodyFormat = System.Web.Mail.MailFormat.Text;
            mailMsg.Body = Msg_Cuerpo;
            mailMsg.Priority = System.Web.Mail.MailPriority.High;
            System.Web.Mail.SmtpMail.SmtpServer = MAIL_SERVIDOR;
            if (Msg_ReplyTo != null)
            {
                mailMsg.Headers.Add("Reply-To", Msg_ReplyTo);
            }
            if (Msg_Adjuntos.Count > 0)
            {
                foreach (Object oArchivo in Msg_Adjuntos)
                {
                    if (oArchivo.GetType() == typeof(String))
                    {
                        String sAux = (String)oArchivo;
                        mailMsg.Attachments.Add(new System.Web.Mail.MailAttachment(sAux));

                    }
                    else if (oArchivo.GetType() == typeof(System.IO.FileInfo))
                    {
                        System.IO.FileInfo oAux = (System.IO.FileInfo)oArchivo;
                        mailMsg.Attachments.Add(new System.Web.Mail.MailAttachment(oAux.FullName));
                    }

                }
            }
            mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", MAIL_AUT);
            mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", MAIL_USUARIO);
            mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", MAIL_CLAVE);
            mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", MAIL_PUERTO);
            mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", MAIL_SSL);
            try
            {
                System.Web.Mail.SmtpMail.Send(mailMsg);
            }
            catch (Exception ex)
            {

                throw ex;
            }






        }

        public void Prueba()
        {
            Msg_Destinatarios.Add("emiliobuchaillot@gmail.com");
            Msg_Asunto= "Prueba";
            Msg_Cuerpo = "Pruebita";
        }
    }



}
