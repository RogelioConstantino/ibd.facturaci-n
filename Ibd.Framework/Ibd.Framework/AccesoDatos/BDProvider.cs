using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Ibd.Framework.AccesoDatos

{
    /// <summary>
    ///     Representa las principales operaciones que se realizaran para realizar acciones
    ///     DML de la base de datos.
    ///     Tiene la ventaja que se puede utilizar en cualquier tipo de base de datos.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    23/01/2013      Creación.
    /// </history>
    public class BDProvider : IBaseDatos
    {
        private DbConnection _conexion;
        private DbCommand _comando;
        private DbTransaction _transaccion;
        private readonly string _cadenaConexion;
        private static DbProviderFactory _proveedor;

        /// <summary>
        /// Crea una instancia del acceso a la base de datos.
        /// </summary>
        public BDProvider(string proveedor, string cadenaConexion)
        {
            try
            {
                _cadenaConexion = cadenaConexion;
                _proveedor = DbProviderFactories.GetFactory(proveedor);
            }
            catch (ConfigurationException ex)
            {
                throw new BaseDatosException("Error al cargar la configuración del acceso a datos.", ex);
            }
        }

        /// <summary> Se concecta con la base de datos. </summary>
        /// <exception cref="BaseDatosException">Si existe un error al conectarse.</exception>
        public void Conectar()
        {
            if (_conexion != null && !_conexion.State.Equals(ConnectionState.Closed))
            {
                throw new BaseDatosException("La conexión ya se encuentra abierta.");
            }
            try
            {
                if (_conexion == null)
                {
                    _conexion = _proveedor.CreateConnection();
                    if (_conexion != null) _conexion.ConnectionString = _cadenaConexion;
                }
                if (_conexion != null) _conexion.Open();
            }
            catch (DataException ex)
            {
                throw new BaseDatosException("Error al conectarse a la base de datos.", ex);
            }
        }

        /// <summary>
        /// Permite desconectarse de la base de datos.
        /// </summary>
        public void Desconectar()
        {
            if (_conexion.State.Equals(ConnectionState.Open)) { _conexion.Close(); }
        }

        /// <summary>
        /// Ejecuta el comando creado y retorna un escalar.
        /// </summary>
        /// <returns>El escalar que es el resultado del comando.</returns>
        /// <exception cref="BaseDatosException">Si ocurre un error al ejecutar el comando.</exception>
        public T EjecutarEscalar<T>()
        {
            T escalar;
            try
            {
                escalar = (T)Convert.ChangeType(_comando.ExecuteScalar(), typeof(T));
            }
            catch (Exception ex)
            {
                throw new BaseDatosException("Error al ejecutar escalar.", ex);
            }

            return escalar;
        }

        /// <summary>
        /// Ejecuta el comando creado.
        /// </summary>
        public void EjecutarComando()
        {
            try
            {
                _comando.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new BaseDatosException("Error al ejecutar comando.", ex); }
        }

        /// <summary>
        /// Crea un comando en base a una sentencia SQL o un Stored Procedure.
        /// Ejemplo:
        /// <code>SELECT Campo1, Campo2 FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code>
        /// <code>usp_ObtenerCampos</code>
        /// Guarda el comando para el seteo de parámetros y la posterior ejecución.
        /// </summary>
        /// <param name="sCommandText">La sentencia SQL o Stored Procedure.</param>
        /// <param name="ctTipo">Tipo de comando: sentencia SQL o un Stored Procedure.</param>
        public void CrearComando(string sCommandText, CommandType ctTipo)
        {
            try
            {
                _comando = _proveedor.CreateCommand();
                if (_comando != null)
                {
                    _comando.Connection = _conexion;
                    _comando.CommandType = ctTipo;
                    _comando.CommandText = sCommandText;
                    if (_transaccion != null) { _comando.Transaction = _transaccion; }
                }
            }
            catch (DataException ex)
            {
                throw new BaseDatosException("Error al crear comando.", ex);
            }

        }

        public void EstablecerTimeout(int segundos)
        {
            _comando.CommandTimeout = segundos;
        }

        DbDataAdapter IBaseDatos.CrearDataAdapter()
        {
            return CrearDataAdapter();
        }

        protected DbDataAdapter CrearDataAdapter()
        {
            DbDataAdapter adaptador = _proveedor.CreateDataAdapter();
            if (adaptador != null)
            {
                adaptador.SelectCommand = _comando;
            }
            return adaptador;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        /// </summary>
        public DataSet TraerDataSet()
        {
            var dataSet = new DataSet();
            CrearDataAdapter().Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// Obtiene un DataTable de la ejucución del DbCommand.
        /// </summary>
        public DataTable TraerDataTable() { return TraerDataSet().Tables[0].Copy(); }

        /// <summary>  Ejecuta el comando creado y retorna el resultado de la consulta.  </summary>
        /// <returns>El resultado de la consulta.</returns>
        /// <exception cref="BaseDatosException">Si ocurre un error al ejecutar el comando.</exception>
        public DbDataReader TraerDataReader() { return _comando.ExecuteReader(); }

        /// <summary> Asigna un parámetro con valor al DbCommand. </summary>
        /// <param name="nombre">Nombre del parámetro.</param>
        /// <param name="valor">Valor del parámetro.</param>
        public void AsignarParametroWhitValue(string nombre, object valor)
        {
            DbParameter parametro = _proveedor.CreateParameter();
            if (parametro != null)
            {
                parametro.ParameterName = nombre;
                parametro.Direction = ParameterDirection.Input;
                parametro.Value = valor;
                _comando.Parameters.Add(parametro);
            }
        }

        /// <summary> Asigna un parámetro al DbCommand. </summary>
        /// <param name="nombre">Nombre del parámetro.</param>
        /// <param name="valor">Valor del parámetro.</param>
        /// <param name="tipo">Tipo de dato.</param>
        /// <param name="direccion">Dirección del parámetro.</param>
        /// <param name="tamano">Tamaño del campo.</param>
        public void AsignarParametro(string nombre, object valor, DbType tipo, ParameterDirection direccion, int tamano)
        {
            DbParameter parametro = _proveedor.CreateParameter();
            if (parametro != null)
            {
                parametro.ParameterName = nombre;
                parametro.Direction = direccion;
                parametro.Value = valor;
                parametro.DbType = tipo;
                if (tamano > 0) { parametro.Size = tamano; }
                _comando.Parameters.Add(parametro);
            }
        }

        /// <summary>
        /// Comienza una transacción en base a la conexion abierta.
        /// _Todo lo que se ejecute luego de esta ionvocación estará 
        /// dentro de una tranasacción.
        /// </summary>
        public void ComenzarTransaccion()
        {
            if (_transaccion == null)
            {
                _transaccion = _conexion.BeginTransaction();
            }
        }

        /// <summary>
        /// Cancela la ejecución de una transacción.
        /// _Todo lo ejecutado entre ésta invocación y su 
        /// correspondiente <c>ComenzarTransaccion</c> será perdido.
        /// </summary>
        public void CancelarTransaccion()
        {
            if (_transaccion != null)
            {
                _transaccion.Rollback();
            }
        }

        /// <summary>
        /// Confirma _todo los comandos ejecutados entre el <c>ComanzarTransaccion</c>
        /// y ésta invocación.
        /// </summary>
        public void ConfirmarTransaccion()
        {
            if (_transaccion != null)
            {
                _transaccion.Commit();
            }
        }

        public IBaseDatos Clonar()
        {
            // Shallow copy 
            return (IBaseDatos)MemberwiseClone();
        }
    }
}