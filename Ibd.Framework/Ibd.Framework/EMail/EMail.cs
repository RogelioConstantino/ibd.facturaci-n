using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Ibd.Framework.EMail
{
    /// <summary>
    ///    Clase base para el manejo de Mails.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 01/10/2012 Creación.
    ///    [Oscar López Osorio] 01/10/2012 Mejoras DispalyName y Arreglo de correos.
    /// </history>
    public abstract class EMail : IDisposable
    {

        #region "Constructor"

        protected EMail()
        {
            DocumentosAdjuntos = new List<Attachment>();
            Cco = new List<MailAddress>();
            Para = new List<MailAddress>();
            Cc = new List<MailAddress>();
            Prioridad = MailPriority.Normal;
            Asunto = "";
            Cuerpo = "";
        }

        #endregion

        #region "Propiedades"

        public MailAddress De { get; private set; }

        public List<MailAddress> Para { get; private set; }

        public List<MailAddress> Cc { get; private set; }

        public List<MailAddress> Cco { get; private set; }

        public string Cuerpo { get; set; }

        public string Asunto { get; set; }

        public MailPriority Prioridad { get; set; }

        public bool EsHtml { get; set; }

        public List<Attachment> DocumentosAdjuntos { get; private set; }

        #endregion

        #region "Métodos Públicos"

        public void AsignarDe(string correo)
        {
            De = CrearMail(correo);
        }

        public void AgregarPara(string correo)
        {
            var sCorreos = correo.Split(";,".ToCharArray());

            foreach (var sMailTemp in sCorreos)
            {
                Para.Add(CrearMail(sMailTemp));
            }
        }

        public void AgregarCc(string correo)
        {
            var sCorreos = correo.Split(";,".ToCharArray());

            foreach (var sMailTemp in sCorreos)
            {
                Cc.Add(CrearMail(sMailTemp));
            }
        }

        public void AagregarCco(string correo)
        {
            var sCorreos = correo.Split(";,".ToCharArray());

            foreach (var sMailTemp in sCorreos)
            {
                Cco.Add(CrearMail(sMailTemp));
            }
        }

        public void AgregarDocumentoAdjunto(string rutaArchivo)
        {
            try
            {
                DocumentosAdjuntos.Add(new Attachment(ObtenerArchivoStream(rutaArchivo), Path.GetFileName(rutaArchivo)));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No se pudo adjuntar el archivo {0}.", rutaArchivo), ex);
            }
        }

        public void AgregarDocumentoAdjunto(Stream oStream, string sNombreArchivo)
        {
            try
            {
                DocumentosAdjuntos.Add(new Attachment(oStream, sNombreArchivo));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("No se pudo adjuntar el archivo {0}.", sNombreArchivo), ex);
            }
        }

        #endregion

        #region "Métodos Privados"

        private MailAddress CrearMail(string sMail)
        {

            MailAddress oMailAddress;
            var sMailTemp = sMail.Split("<>()[]{}".ToCharArray());

            if ((sMailTemp.Length == 1 || sMailTemp.Length == 3))
            {
                var sDireccion = sMailTemp[0];
                var sNombre = "";

                //En caso que traiga nombre se asigna al mail.
                if (sMailTemp.Length == 3)
                {
                    sDireccion = sMailTemp[1];
                    sNombre = sMailTemp[0];
                }

                if (ValidarMail(sDireccion))
                {
                    oMailAddress = new MailAddress(sDireccion, sNombre);
                }
                else
                {
                    throw new Exception(string.Format("No se reconoció \"{0}\" como correo valido.", sMail));
                }
            }
            else
            {
                throw new Exception(string.Format("No se reconoció \"{0}\" como correo valido.", sMail));
            }

            return oMailAddress;
        }

        private bool ValidarMail(string sEmail)
        {
            if (string.IsNullOrEmpty(sEmail))
            {
                throw new FormatException("Correo electrónico inválido.");
            }

            if (sEmail.Contains("@") == false)
            {
                throw new FormatException("El correo electrónico debe contener el carácter @.");
            }

            var oReg = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",
                                 RegexOptions.Singleline + (int) RegexOptions.IgnoreCase);

            return oReg.IsMatch(sEmail);
        }

        protected Stream ObtenerArchivoStream(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, Convert.ToInt32(fileStream.Length));

                return memStream;
            }
        }

        //No se permite la asignacion directa, asi que se deben agregar uno por uno. 
        protected void LlenarMailAddress(MailAddressCollection oDirecciones, List<MailAddress> destinatarios)
        {
            foreach (var item in destinatarios.Where(item => item != null))
                oDirecciones.Add(item);
        }

        //No se permite la asignacion directa, asi que se deben agregar uno por uno. 
        protected void LlenarAdjuntos(AttachmentCollection oAdjuntos, List<Attachment> adjuntos)
        {
            foreach (var item in adjuntos)
                oAdjuntos.Add(item);
        }

        #endregion

        #region "Dispose"

        // Indica si ya se llamo al método Dispose. (default = false)
        private bool _isDisposed;

        /// <summary>
        /// Implementación de IDisposable. No se sobreescribe.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // GC.SupressFinalize quita de la cola de finalización al objeto.
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Limpia los recursos manejados y no manejados.
        /// </summary>
        /// <param name="disposing">
        /// Si es true, el método es llamado directamente o indirectamente
        /// desde el código del usuario.
        /// Si es false, el método es llamado por el finalizador
        /// y sólo los recursos no manejados son finalizados.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Preguntamos si Dispose ya fue llamado.
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Llamamos al Dispose de todos los RECURSOS MANEJADOS.
                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ... 
            }
            _isDisposed = true;
        }

        ~EMail()
        {
            // Destructor
            Dispose(false);
        }

        #endregion
    }
}
// Prueba