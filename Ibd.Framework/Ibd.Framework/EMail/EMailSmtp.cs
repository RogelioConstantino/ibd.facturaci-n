using System;
using System.Net.Mail;
using Ibd.Framework.Extensores;

namespace Ibd.Framework.EMail
{
    public class EMailSmtp : EMail
    {
        #region "Propiedades"

        public string ServidorSmtp { get; set; }

        #endregion

        #region "Constructores"

        public EMailSmtp()
        {
            ServidorSmtp = "";
        }

        public EMailSmtp(string mailDe, string para, string asunto, string cuerpo)
        {
            ServidorSmtp = "";
            AsignarDe(mailDe);
            AgregarPara(para);
            Asunto = asunto;
            Cuerpo = cuerpo;
        }

        public EMailSmtp(string mailDe, string para, string cc, string asunto, string cuerpo)
        {
            ServidorSmtp = "";
            AsignarDe(mailDe);
            AgregarPara(para);
            AgregarCc(cc);
            Asunto = asunto;
            Cuerpo = cuerpo;
        }
        #endregion

        public void Enviar()
        {
            if (De == null)
                throw new Exception("Error al enviar Email, no se estableció una cuenta de correo emisor.");

            if (Para.Count == 0 & Cc.Count == 0 & Cco.Count == 0)
                throw new Exception("Error al enviar Email, no se estableció una cuenta de correo destino.");

            if (ServidorSmtp.NoEstablecido())
                throw new Exception("Error al enviar Email, no se estableció servidor SMTP.");

            var omail = new MailMessage {From = De};
            LlenarMailAddress(omail.To, Para);
            LlenarMailAddress(omail.CC, Cc);
            LlenarMailAddress(omail.Bcc, Cco);
            omail.Body = Cuerpo;
            omail.Subject = Asunto;
            omail.Priority = Prioridad;
            omail.IsBodyHtml = EsHtml;
            LlenarAdjuntos(omail.Attachments, DocumentosAdjuntos);

            var smtp = new SmtpClient {Host = ServidorSmtp};

            smtp.Send(omail);
        }
    }
}
//