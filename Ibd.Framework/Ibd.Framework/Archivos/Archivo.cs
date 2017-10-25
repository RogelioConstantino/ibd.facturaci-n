using System;
using Ibd.Framework.Extensores;
using System.IO;

namespace Ibd.Framework.Archivos
{
     /// <summary>
    /// Clase base que permite realizar las tareas básicas de los archivos.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 29/08/2011 Creación.
    /// </history>
    public class Archivo
    {
        #region "Propiedades"

        public string Ubicacion { get; set; }
        public string Nombre { get; set; }

        #endregion

        #region "Constructores"

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Archivo"/>.
        /// </summary>
        /// <param name="nombre">El nombre del archivo.</param>
        public Archivo(string nombre)
        {
            // Se valida que el nombre no este vacio
            if (nombre.NoEstablecido())
            {
                throw new ArgumentException("El nombre del archivo es inválido.");
            }

            Nombre = Path.GetFileName(nombre);
            Ubicacion = Path.GetDirectoryName(nombre);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Archivo"/>.
        /// </summary>
        /// <param name="nombre">El nombre del archivo.</param>
        /// <param name="ubicacion">La ruta del archivo.</param>
        public Archivo(string nombre, string ubicacion)
        {
            // Se valida que el nombre no este vacio
            if (nombre.NoEstablecido())
            {
                throw new ArgumentException("El nombre del archivo es inválido.");
            }
            Ubicacion = ubicacion;
            Nombre = nombre;
        }

        #endregion

        #region "Metodos"

        public virtual void Crear()
        {
            // Si el archivo existe lo mueve
            if (Existe())
            {
                throw new ArgumentException("El archivo que desea crear ya existe.");
            }
            try
            {
                File.Create(ObtenerRuta());
            }
            catch (IOException ex)
            {
                throw new IOException(string.Format("Error al crear el archivo {0} .", Nombre) + ex.Message);
            }
        }

        //Como esta es igual para todas las clases derivadas, la declaramos virtual.
        public virtual bool Existe()
        {
            return File.Exists(ObtenerRuta());
        }

        public virtual bool Existe(string rutaArchivo)
        {
            return File.Exists(rutaArchivo);
        }

        //Como esta es igual para todas las clases derivadas, la declaramos virtual.
        public virtual string ObtenerRuta()
        {
            var path = Path.Combine(Ubicacion, Nombre);
            return path;
        }

        public string ObtenerRutaWeb()
        {
            return ObtenerRuta().Replace("\\", "/");
        }

        public string ObtenerNombreCorto()
        {
            return Path.GetFileNameWithoutExtension(ObtenerRuta());
        }

        public string ObtenerExtencion()
        {
            return Path.GetExtension(ObtenerRuta());
        }

        /// <summary>
        /// Copia un archivo.
        /// </summary>
        /// <param name="rutaDestino">Ruta a la que se desea copiar el archivo.</param>
        /// <param name="sobreEscribir"></param>
        public virtual void CopiarA(string rutaDestino, bool sobreEscribir)
        {
            // Si el archivo existe y no se quiere sobre escribir
            if (Existe() && !sobreEscribir)
            {
                try
                {
                    File.Copy(ObtenerRuta(), rutaDestino);
                }
                catch (IOException ex)
                {
                    throw new IOException("Error al copiar el archivo.", ex);
                }

                // Si el archivo existe y se quiere sobre escribir
            }
            else if (Existe() && sobreEscribir)
            {
                try
                {
                    if (Existe(rutaDestino))
                    {
                        File.Delete(rutaDestino);
                    }

                    File.Copy(ObtenerRuta(), rutaDestino);
                }
                catch (IOException ex)
                {
                    throw new IOException("Error al copiar el archivo.", ex);
                }
            }
            else
            {
                throw new ArgumentException("El archivo que desea copiar no existe.");
            }
        }

        /// <summary>
        /// Mueve un archivo de un lugar a otro.
        /// </summary>
        /// <param name="rutaDestino">Ruta a la que se desea mover el archivo.</param>
        public virtual void MoverA(string rutaDestino)
        {
            // Si el archivo existe lo mueve
            if (!Existe())
            {
                throw new ArgumentException("El archivo que desea mover no existe.");
            }
            try
            {
                File.Move(ObtenerRuta(), rutaDestino);
            }
            catch (IOException ex)
            {
                throw new IOException("Error al mover el archivo.", ex);
            }

        }

        /// <summary>
        /// Mueve un archivo de un lugar a otro.
        /// </summary>
        /// <param name="rutaDestino">Ruta a la que se desea mover el archivo.</param>
        /// <param name="sobreEscribir">Indica se el archivo debe ser sobre escrito en caso que exista.</param>
        public virtual void MoverA(string rutaDestino, bool sobreEscribir)
        {
            // Si el archivo existe lo mueve
            if (!Existe())
            {
                throw new ArgumentException("El archivo que desea mover no existe.");
            }

            // Si el archivo existe y no se quiere sobre escribir
            if (!sobreEscribir)
            {
                try
                {
                    File.Move(ObtenerRuta(), rutaDestino);
                }
                catch (IOException ex)
                {
                    throw new IOException("Error al copiar el archivo.", ex);
                }
            }
            else // Si el archivo existe y se quiere sobre escribir
            {
                try
                {
                    if (Existe(rutaDestino))
                    {
                        File.Delete(rutaDestino);
                    }

                    File.Move(ObtenerRuta(), rutaDestino);
                }
                catch (IOException ex)
                {
                    throw new IOException("Error al copiar el archivo.", ex);
                }
            }
        }

        /// <summary>
        /// Elimina el archivo.
        /// </summary>
        public virtual void Eliminar()
        {
            // Si el archivo existe lo elimina
            if (!Existe())
            {
                throw new ArgumentException("El archivo que desea eliminar no existe.");
            }
            try
            {
                File.Delete(ObtenerRuta());
            }
            catch (IOException ex)
            {
                throw new IOException("Error al eliminar el archivo. " + ex.Message);
            }
        }

        #endregion
    }
}
