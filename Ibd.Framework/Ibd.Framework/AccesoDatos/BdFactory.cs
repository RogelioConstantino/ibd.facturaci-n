using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ibd.Framework.AccesoDatos.ConexionesConfig;

namespace Ibd.Framework.AccesoDatos
{
    /*Puede exister cualquier tipo de Factory*/
    internal interface IBDFactory
    {
        IBaseDatos ObtenerBD();
    }

    /// <summary>
    /// Clase concreta para la creacion de objetos de BD en base a DBProvider.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 19/02/2014 Creación.
    /// </history>
    public class BDFactoryProvider : IBDFactory
    {
        private readonly ConexionElement _bdElement;

        public BDFactoryProvider(ConexionElement bdElement)
        {
            _bdElement = bdElement;
        }

        public IBaseDatos ObtenerBD()
        {
            //Se obtiene
            IBaseDatos bd = new BDProvider(_bdElement.Proveedor, _bdElement.Conexion);
            return bd;
        }
    }

    /// <summary>
    /// Clase concreta para la creacion de objetos de BD en base a Reflexion.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 19/02/2014 Creación.
    /// </history>
    public class BDFactoryAssembly : IBDFactory
    {
        private readonly ConexionElement _bdElement;
        private static readonly Dictionary<string, Type> BasesDatos = new Dictionary<string, Type>();

        public BDFactoryAssembly(ConexionElement bdElement)
        {
            _bdElement = bdElement;
        }

        public IBaseDatos ObtenerBD()
        {
            /* Como Reflexion consume recursos, se almacena en diccionario Static*/
            if (!BasesDatos.Keys.Contains(_bdElement.Proveedor))
            {
                /* Los clientes vienen de este forma 'MySql.Data.MySqlClient, System.Data.SqlClient
                 * System.Data.OleDb' por eso se extrae el ultimo y se remplaza el Client 
                 * asi el nombre nos quedaria asi 'BdMySql' */
                var sBd = _bdElement.Proveedor.Split('.').Last().Replace("Client", "");

                var baseDatos =
                    Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .First(t => t.Name == string.Format("Bd{0}", sBd));

                BasesDatos.Add(_bdElement.Proveedor, baseDatos);
            }
            var bd = (IBaseDatos)Activator.CreateInstance(BasesDatos[_bdElement.Proveedor], _bdElement.Conexion);

            return bd;
        }
    }

    /// <summary>
    /// Clase que permitirá la creacion de nuevos objetos de BD.
    /// </summary>
    /// <history>
    ///    [Oscar López Osorio] 19/02/2014 Creación.
    /// </history>
    public class BDFactory
    {

        public IBaseDatos ObtenerBd(string bdConfig)
        {
            var oAppBds = Config.GetConfigSection<ConexionConfigurationSection>("ConexionesSection");
            var oAppElement = oAppBds.Elementos[bdConfig];

            //Se obtine la fabrica que creara el objeto
            var factoryName = oAppElement.Factory;
            var requiredFactory = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where string.Equals(type.Name, factoryName, StringComparison.OrdinalIgnoreCase)
                                   select type).FirstOrDefault();
            
            //Si no se encuentra el factory que creara el objeto se regresa null
            if (requiredFactory == null) throw new ArgumentException("No se puede crear el objeto de BD a partir de " + factoryName + ".");
            
            //Se crea el factory a utilizar
            var factoryInstance = Activator.CreateInstance(requiredFactory, oAppElement);

            //Se crea el metodo que sera ejecutado
            var methodInfo = factoryInstance.GetType().GetMethod("ObtenerBD");

            //Se ejecuta el metodo que crea el objeto de BD
            var bd = (IBaseDatos)methodInfo.Invoke(factoryInstance, null);
            //.Invoke(factoryInstance);//(factoryInstance, BindingFlags.InvokeMethod, null, null, null);

            return bd;
        }
    }
}