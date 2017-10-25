using System;
using System.Diagnostics;

namespace Ibd.Framework
{
    /// <summary>
    ///    Clase para el manejo la información de los errores.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 15/02/2013 Creación.
    /// </history>
    public class ErrorHandler
    {
        /// <summary>
        /// Obtiene información del error.
        /// </summary>
        /// <param name="ex">Error a formatear para escribirlo como mensaje.</param>
        /// <returns><c>El detalle como String.</c></returns>
        public static string ObtenerDetalle(Exception ex)
        {
            string mensaje;

            if ((ex == null))
            {
                return "";
            }

            if ((ex.InnerException != null))
            {
                var trace = new StackTrace(ex.InnerException, true);
                if (ex.InnerException.TargetSite != null && ex.InnerException.TargetSite.DeclaringType != null)
                    // .DeclaringType != null)
                {
                    mensaje = string.Format("{0} - {1}, Clase: {2}, Metodo: {3}, Linea: {4}.", ex.Message,
                                            ex.InnerException.Message,
                                            ex.InnerException.TargetSite.DeclaringType.FullName,
                                            trace.GetFrame(0).GetMethod().Name, trace.GetFrame(0).GetFileLineNumber());
                }
                else
                {
                    mensaje = string.Format("{0} - {1}.", ex.Message,
                                            ex.InnerException.Message);
                }
            }
            else
            {
                var trace = new StackTrace(ex, true);
                if (ex.TargetSite.DeclaringType != null && ex.TargetSite.DeclaringType != null)
                {
                    mensaje = string.Format("{0}, Clase: {1}, Metodo: {2}, Linea: {3}.", ex.Message,
                                            ex.TargetSite.DeclaringType.FullName, trace.GetFrame(0).GetMethod().Name,
                                            trace.GetFrame(0).GetFileLineNumber());
                }
                else
                {
                    mensaje = ex.Message;
                }
            }

            return mensaje;
        }
    }
}