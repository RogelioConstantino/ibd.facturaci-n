using System;

namespace Ibd.SiMer.Entidades
{
    public class ArchivoCincominutalesEn
    {
        public int IdArchivoCincominutales { get; set; }
        public string Archivo { get; set; }
        public int? IdPuntoCarga { get; set; }
        public string RMU { get; set; }
        public DateTime? FechaArchivo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int IdCargaArchivoEstatus { get; set; }
        public int? NoRegistros { get; set; }
        public string Mensaje { get; set; }
        public string sFechaArchivo { get; set; }
    }

    public enum CargaArchivoEstatus
    {
        EnCurso = 1,
        Exitoso = 2,
        Fallido = 3,
        Incompleto = 4,
        Cancelado = 5
    }
}