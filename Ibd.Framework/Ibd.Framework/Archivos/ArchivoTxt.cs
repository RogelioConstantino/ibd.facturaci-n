using System.IO;

namespace Ibd.Framework.Archivos
{
    /// <summary>
    /// Clase que permite realizar las tareas básicas de los archivos de texto.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 11/09/2012 Creación.
    /// </history>
    public class ArchivoTxt : Archivo
    {
        //Implements IArchivoDataAccess

        #region "Campos"

        private TextReader _lector;

        #endregion

        private TextWriter _escritor;

        #region "Constructores"

        //Este constructor tendrá todos los parámetros
        public ArchivoTxt(string nombre)
            : base(nombre)
        {
        }

        //Este constructor tendrá todos los parámetros
        public ArchivoTxt(string nombre, string ubicacion)
            : base(nombre, ubicacion)
        {
        }

        #endregion

        #region "Metodos publicos"

        public void AbrirLector()
        {
            if (Existe())
            {
                _lector = new StreamReader(ObtenerRuta());
            }
            else
            {
                throw new IOException(string.Format("El archivo {0} no existe.", ObtenerRuta()));
            }
        }

        public bool LectorActivo()
        {
            return _lector?.Peek() >= 0;
        }

        public string LeerLinea()
        {
            try
            {
                return _lector.ReadLine();
            }
            catch (IOException ex)
            {
                throw new IOException("Error al leer linea del archivo {ObtenerRuta()}.", ex);
            }
        }

        /// <summary>
        /// Inicializa un TextWriter para escribir sobre el Archivo.
        /// </summary>
        public void AbrirEscritor()
        {
            if (!Existe())
            {
                throw new IOException("El archivo {ObtenerRuta()} no existe.");
            }
            if (_escritor == null)
            {
                _escritor = File.AppendText(ObtenerRuta());
            }
        }

        /// <summary>
        /// Crea el archivo.
        /// </summary>
        public override void Crear()
        {
            if (Existe()) return;
            try
            {
                var writer = File.CreateText(ObtenerRuta());
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
            catch (IOException ex)
            {
                throw new IOException("Error al crear el archivo {Nombre}.", ex);
            }
        }

        /// <summary>
        /// Escribe el mensaje y agrega un salto de linea.
        /// </summary>
        /// <param name="linea">Mensaje a escribir en el archivo.</param>
        public void EscribirLinea(string linea)
        {
            try
            {
                _escritor.WriteLine(linea);
            }
            catch (IOException ex)
            {
                throw new IOException("Error al escribir {linea} en el archivo {ObtenerRuta()}.",                    ex);
            }
        }

        /// <summary>
        /// Escribe el mensaje en el archivo deseado.
        /// </summary>
        /// <param name="linea">Mensaje a escribir en el archivo.</param>
        /// <remarks>Este medoto no agrega salto de linea.</remarks>
        public void Escribir(string linea)
        {
            try
            {
                _escritor.Write(linea);
            }
            catch (IOException ex)
            {
                throw new IOException("Error al escribir {linea} en el archivo {ObtenerRuta()}.",
                    ex);
            }
        }

        /// <summary>
        /// Cierra el TextWriter para escribir sobre el Archivo.
        /// </summary>>
        public void CerrarLector()
        {
            if (_lector == null) return;
            _lector.Close();
            _lector.Dispose();
            _lector = null;
        }

        /// <summary>
        /// Cierra el TextWriter para escribir sobre el Archivo.
        /// </summary>>
        public void CerrarEscritor()
        {
            if (_escritor == null) return;
            _escritor.Close();
            _escritor.Dispose();
            _escritor = null;
        }

        #endregion
    }
}