using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibd.SiMer.Entidades
{
    public class EntidadFiltrable<T>
    {
        public DateTime fecha { get; set; }

        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        public string rmu { get; set; }
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosFiltrados { get; set; }
        public string Filtro { get; set; }
        public int? OrdenarPor { get; set; }
        public int? Direccion { get; set; }
        public List<T> Datos { get; set; }
    }
}