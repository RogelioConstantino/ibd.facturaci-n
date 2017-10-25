using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

using System.Data;
using System.Text;

using Ibd.SiMer.Negocio;

using AjaxControlToolkit;

namespace Ibd.SiMer.Web.WebService
{
    /// <summary>
    /// Descripción breve de wsClientes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsClientes : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] get(string knownCategoryValues)
        {
            clientesNe oCls = new clientesNe();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.Consultar();
            if (dt == null)
            {
                return values.ToArray();
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        values.Add(new CascadingDropDownNameValue
                        {
                            name = row[3].ToString(),
                            value = row[1].ToString()
                        });
                    }
                    return values.ToArray();
                }
                else
                {
                    return values.ToArray();
                }
            }

        }

    }
}
