using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Ibd.Framework.AccesoDatos
{
    public class BdMySql: IBaseDatos
    {
        private MySqlConnection _conexion;
        private MySqlCommand _comando;
        private MySqlTransaction _transaccion;
        private readonly string _cadenaConexion;


        /// <summary>
        /// Crea una instancia del acceso a la base de datos.
        /// </summary>
        public BdMySql(string cadenaConexion)
        {
                _cadenaConexion = cadenaConexion;
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
                    _conexion = new MySqlConnection { ConnectionString = _cadenaConexion };
                }
                _conexion.Open();
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
        public T EjecutarEscalar<T>()
        {
            var escalar = (T)Convert.ChangeType(_comando.ExecuteScalar(), typeof(T));

            return escalar;
        }

        /// <summary>
        /// Ejecuta el comando creado.
        /// </summary>
        public void EjecutarComando()
        {
            _comando.ExecuteNonQuery();
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
            _comando = _conexion.CreateCommand();
            _comando.Connection = _conexion;
            _comando.CommandType = ctTipo;
            _comando.CommandText = sCommandText;
            _comando.CommandTimeout = 0;
            if (_transaccion != null) { _comando.Transaction = _transaccion; }
        }

        public void EstablecerTimeout(int segundos)
        {
            _comando.CommandTimeout = segundos;
        }

        public DbDataAdapter CrearDataAdapter()
        {
            var adaptador = new MySqlDataAdapter { SelectCommand = _comando };

            return adaptador;
        }

        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        public DataSet TraerDataSet()
        {
            var dataSet = new DataSet();
            CrearDataAdapter().Fill(dataSet);
            return dataSet;
        } // end TraerDataset

        // Obtiene un DataTable de la ejucución del DbCommand.
        public DataTable TraerDataTable() { return TraerDataSet().Tables[0].Copy(); }

        /// <summary>  Ejecuta el comando creado y retorna el resultado de la consulta.  </summary>
        /// <returns>El resultado de la consulta.</returns>
        public DbDataReader TraerDataReader() { return _comando.ExecuteReader(); }

        /// <summary> Asigna un parámetro con valor al DbCommand. </summary>
        /// <param name="nombre">Nombre del parámetro.</param>
        /// <param name="valor">Valor del parámetro.</param>
        public void AsignarParametroWhitValue(string nombre, object valor)
        {
            var parametro = new MySqlParameter
            {
                ParameterName = nombre,
                Direction = ParameterDirection.Input,
                Value = valor
            };
            _comando.Parameters.Add(parametro);
        }

        /// <summary> Asigna un parámetro al DbCommand. </summary>
        /// <param name="nombre">Nombre del parámetro.</param>
        /// <param name="valor">Valor del parámetro.</param>
        /// <param name="tipo">Tipo de dato.</param>
        /// <param name="direccion">Dirección del parámetro.</param>
        /// <param name="tamano">Tamaño del campo.</param>
        public void AsignarParametro(string nombre, object valor, DbType tipo, ParameterDirection direccion, int tamano)
        {
            var parametro = new MySqlParameter
            {
                ParameterName = nombre,
                Direction = direccion,
                Value = valor,
                DbType = tipo
            };
            if (tamano > 0) { parametro.Size = tamano; }
            _comando.Parameters.Add(parametro);
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
            return (IBaseDatos)MemberwiseClone(); 
        }
    }
}