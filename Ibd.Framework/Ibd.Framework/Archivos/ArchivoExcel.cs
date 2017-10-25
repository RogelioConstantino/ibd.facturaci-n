using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ClosedXML.Excel;

namespace Ibd.Framework.Archivos
{
    public class ArchivoExcel : Archivo
    {
        public ArchivoExcel(string nombre) : base(nombre)
        {
        }

        public ArchivoExcel(string nombre, string ubicacion)
            : base(nombre, ubicacion)
        {
        }

        /// <summary>
        /// Crea el archivo.
        /// </summary>
        public override void Crear()
        {
            if (Existe()) return;
            try
            {
                var workbook = new XLWorkbook();
                workbook.Worksheets.Add("Hoja1");
                workbook.Worksheets.Add("Hoja2");
                workbook.Worksheets.Add("Hoja3");
                workbook.SaveAs(ObtenerRuta());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al crear el archivo {0}.", Nombre), ex);
            }
        }


                public DataTable HojaToDataTable(string hoja, string[] columnas)
        {
            var dtData = new DataTable();

            var workbook = new XLWorkbook(ObtenerRuta());
            var worksheet = workbook.Worksheet(hoja);

            var range = worksheet.RangeUsed();
            var colCount = range.ColumnCount();

            foreach (var columna in columnas)
            {
                dtData.Columns.Add(columna);
            }

            foreach (var row in range.RowsUsed(null))
            {
                var rowData = new object[colCount];
                var i = 0;
                row.Cells().ForEach(c => rowData[i++] = c.Value);
                dtData.Rows.Add(rowData);
            }

            return dtData;
        }

        public List<T> HojaToList<T>(string hoja, bool propiedadesTitulo) where T : class
        {
            var oResultado = new List<T>();

            var workbook = new XLWorkbook(ObtenerRuta());
            var worksheet = workbook.Worksheet(hoja);

            var range = worksheet.RangeUsed();

            if (range==null)
                throw new ArgumentException(string.Format("La hoja de trabajo '{0}' no contiene información.",hoja));

            var titulos = range.FirstRowUsed(null);

            Dictionary<int, string> propiedades;
            var c = 1; //La primer columna comienza en 1
            var rowsInicio = 1;
            if (propiedadesTitulo)
            {
                // La descipcion de las propiedades se hace en los titulos
                propiedades = titulos.Cells().ToDictionary(cel => c++, cel => cel.Value.ToString());
                rowsInicio = 2;
            }
            else
            {
                // Si no trae titulo se buscar la propiedad en orden como se declaran
                var oClase = Activator.CreateInstance<T>();
                propiedades = oClase.GetType().GetProperties().ToDictionary(cel => c++, cel => cel.Name);
            }

            var noRow = 1;
            foreach (var row in range.RowsUsed(null))
            {
                if (rowsInicio > noRow)
                {
                    noRow++; 
                    continue;
                }
                
                var oClase = Activator.CreateInstance<T>();
                var tipo = oClase.GetType();

                foreach (var propiedad in propiedades)
                {
                    var property = tipo.GetProperty(propiedad.Value); // Se buscar la propiedad
                    var val = Convert.ChangeType(row.Cell(propiedad.Key).Value, property.PropertyType);
                    property.SetValue(oClase, val, null);
                }

                oResultado.Add(oClase);
                noRow++;
            }
            return oResultado;
        }


        /// <summary>
        /// Convierte una hoja de excel en una lista de objetos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hoja">Nombre de la hoja a transformar</param>
        /// <param name="propiedadesTitulo">Nombre de los campos que llenarán las propiedades de la clase</param>
        /// <returns></returns>
        public List<T> HojaToList<T>(string hoja, string[] propiedadesTitulo) where T : class
        {
            var oResultado = new List<T>();

            var workbook = new XLWorkbook(ObtenerRuta());
            IXLWorksheet worksheet;

            try
            {
                 worksheet = workbook.Worksheet(hoja);
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format("No se encontro la hoja de trabajo '{0}'.", hoja));
            }
            
            var range = worksheet.RangeUsed();
            if (range == null)
                throw new ArgumentException(string.Format("La hoja de trabajo '{0}' no contiene información.", hoja));

            var c = 1; //La primer columna comienza en 1
            const int rowsInicio = 2;
            var propiedades = propiedadesTitulo.ToDictionary(tit => c++, tit => tit);

            /*Se valida que vengan todos la pripiedades en los titulos*/
            var titulos = range.FirstRowUsed(null);
            c = 1;
            var propExcel = titulos.Cells().ToDictionary(cel => c++, cel => cel.Value.ToString());
            var colFaltantes = (from col in propiedades
                                select col.Value
                                into val let key = (from k in propExcel
                                                    where String.Compare(k.Value, val, StringComparison.OrdinalIgnoreCase) == 0
                                                    select k.Key).FirstOrDefault() where key == 0 select val).ToList();
            if (colFaltantes.Count>0)
                throw new ArgumentException(string.Format("No se han especificado los campos: {0}", string.Join(", ", colFaltantes.ToArray())));

            /*Ahora se organizan la propiedades en base al orden quet trae el Excel*/
            var propiedadesOrden = new Dictionary<int, string>();
            foreach (var prop in propiedades)
            {
                var key = propExcel.FirstOrDefault(x => x.Value == prop.Value).Key;
                propiedadesOrden.Add(key,prop.Value);
            }

            var noRow = 1;
            foreach (var row in range.RowsUsed(null))
            {
                if (rowsInicio > noRow)
                {
                    noRow++;
                    continue;
                }

                var oClase = Activator.CreateInstance<T>();
                var tipo = oClase.GetType();

                foreach (var propiedad in propiedadesOrden)
                {
                    var property = tipo.GetProperty(propiedad.Value); // Se buscar la propiedad
                    var val = Convert.ChangeType(row.Cell(propiedad.Key).Value, property.PropertyType);
                    property.SetValue(oClase, val, null);
                }

                oResultado.Add(oClase);
                noRow++;
            }
            return oResultado;
        }


        //public DataTable HojaToDataTable(string hoja,bool conTitulos)
        //{
        //    var dtData = new DataTable();

        //    var workbook = new XLWorkbook(ObtenerRuta());
        //    var worksheet = workbook.Worksheet(hoja);

        //    var range = worksheet.RangeUsed();
        //    var colCount = range.ColumnCount();

        //    if (conTitulos)
        //    {
        //        // Look for the first row used
        //    var firstRowUsed = worksheet.FirstRowUsed();


        //    while (!firstRowUsed.Cell(coCategoryId).IsEmpty())
        //    {
        //        String categoryName = categoryRow.Cell(coCategoryName).GetString();
        //        categories.Add(categoryName);

        //        categoryRow = categoryRow.RowBelow();
        //    }


        //        foreach (var col in range.ColumnsUsed(null))
        //        {
                    
        //            var rowData = new object[colCount];
        //            var i = 0;
        //            row.Cells().ForEach(c => rowData[i++] = c.Value);
        //            dtData.Rows.Add(rowData);
        //        }


        //        dtData.Columns.Add(columna);
        //    }
        //    else
        //    {
        //    }

        //    foreach (var columna in columnas)
        //    {
        //        dtData.Columns.Add(columna);
        //    }

        //    foreach (var row in range.RowsUsed(null))
        //    {
        //        var rowData = new object[colCount];
        //        var i = 0;
        //        row.Cells().ForEach(c => rowData[i++] = c.Value);
        //        dtData.Rows.Add(rowData);
        //    }

        //    return dtData;
        //}
    }
}
