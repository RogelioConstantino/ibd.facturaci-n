using System.Data;
using System.Data.Common;

namespace Ibd.Framework.AccesoDatos
{
    /// <summary>
    ///     Interface base para representar las acciones
    ///     DML de la base de datos.
    /// </summary>
    /// <history>
    ///     [Oscar López Osorio]    19/02/2014      Creación.
    /// </history>
    public interface IBaseDatos
    {
        /// <summary> Se concecta con la base de datos. </summary>
        void Conectar();

        /// <summary>
        ///  Permite desconectarse de la base de datos.
        ///  </summary>
        void Desconectar();

        /// <summary>
        ///  Ejecuta el comando creado y retorna un escalar.
        ///  </summary><returns>El escalar que es el resultado del comando.</returns>
        T EjecutarEscalar<T>();

        /// <summary>
        ///  Ejecuta el comando creado.
        ///  </summary>
        void EjecutarComando();

        /// <summary>
        ///  Crea un comando en base a una sentencia SQL o un Stored Procedure.
        ///  Ejemplo:
        ///  <code>SELECT Campo1, Campo2 FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code><code>usp_ObtenerCampos</code>
        ///  Guarda el comando para el seteo de parámetros y la posterior ejecución.
        ///  </summary><param name="sCommandText">La sentencia SQL o Stored Procedure.</param><param name="ctTipo">Tipo de comando: sentencia SQL o un Stored Procedure.</param>
        void CrearComando(string sCommandText, CommandType ctTipo);

        /// <summary>
        ///  Establece el tiempo de ejecucion del comendo, antes de producir error.
        ///  Usar 0 para sin limite.
        ///  </summary><param name="segundos">Tiempo de espera de la consulta.</param>
        void EstablecerTimeout(int segundos);

        DbDataAdapter CrearDataAdapter();

        /// <summary>
        ///  Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros. 
        ///  </summary>
        DataSet TraerDataSet();

        /// <summary>
        ///  Obtiene un DataTable de la ejucución del DbCommand.
        ///  </summary>
        DataTable TraerDataTable();

        /// <summary>  Ejecuta el comando creado y retorna el resultado de la consulta.  </summary><returns>El resultado de la consulta.</returns>
        DbDataReader TraerDataReader();

        /// <summary> Asigna un parámetro con valor al DbCommand. </summary><param name="nombre">Nombre del parámetro.</param><param name="valor">Valor del parámetro.</param>
        void AsignarParametroWhitValue(string nombre, object valor);

        /// <summary> Asigna un parámetro al DbCommand. </summary><param name="nombre">Nombre del parámetro.</param><param name="valor">Valor del parámetro.</param><param name="tipo">Tipo de dato.</param><param name="direccion">Dirección del parámetro.</param><param name="tamano">Tamaño del campo.</param>
        void AsignarParametro(string nombre, object valor, DbType tipo, ParameterDirection direccion, int tamano);

        /// <summary>
        ///  Comienza una transacción en base a la conexion abierta.
        ///  _Todo lo que se ejecute luego de esta ionvocación estará 
        ///  dentro de una tranasacción.
        ///  </summary>
        void ComenzarTransaccion();

        /// <summary>
        ///  Cancela la ejecución de una transacción.
        ///  _Todo lo ejecutado entre ésta invocación y su 
        ///  correspondiente <c>ComenzarTransaccion</c> será perdido.
        ///  </summary>
        void CancelarTransaccion();

        /// <summary>
        ///  Confirma _todo los comandos ejecutados entre el <c>ComanzarTransaccion</c>
        ///  y ésta invocación.
        ///  </summary>
        void ConfirmarTransaccion();


        /// <summary>
        ///  Permite crear una copia del objeto actual
        ///  </summary>
        IBaseDatos Clonar();
    }
}