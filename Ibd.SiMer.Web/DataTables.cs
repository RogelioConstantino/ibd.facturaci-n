using Ibd.SiMer.Entidades;
using System.Collections.Generic;

namespace Ibd.SiMer.Web
{
    public class DataTables<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        //public List<CentralEn> data { get; set; }
        
        public List<T> data { get; set; }        
    }
}