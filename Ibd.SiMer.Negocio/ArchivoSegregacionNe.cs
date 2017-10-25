using Ibd.Framework;
using Ibd.Framework.Extensores;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Negocio.Managers;
using Ibd.SiMer.Datos;
using System;

using System.Data;

namespace Ibd.SiMer.Negocio
{
    public class ArchivoSegregacionNe
    {

        public Int64 insertarHeaderArchivo(ArchivoSegregacionEn archivo)
        {
            ArchivoSegregacionDa oDa = new ArchivoSegregacionDa(Singleton<ConexionMng>.Single.Default());

            Int64 key = oDa.insertaRegistroHeader(archivo);

            return key;
        }

        public Int64 actualizarHeaderArchivo(ArchivoSegregacionEn archivo)
        {
            ArchivoSegregacionDa oDa = new ArchivoSegregacionDa(Singleton<ConexionMng>.Single.Default());

            Int64 key = oDa.actulaizaRegistroHeader(archivo);

            return key;
        }

    }
}
