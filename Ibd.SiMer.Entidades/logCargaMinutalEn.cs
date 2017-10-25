using System;

namespace Ibd.SiMer.Entidades
{
    public  class logCargaMinutalEn
    {
        public int Id { get; set; }
        public string rmu { get; set; }
        public string archivo { get; set; }
        public string fechaCarga { get; set; }
        //public DateTime fechaInicio { get; set; }
        //public DateTime fechaFin { get; set; }
        public int? numRegisttros { get; set; }        
        public string observaciones { get; set; }
                
    }
}
