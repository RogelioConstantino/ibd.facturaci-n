using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;
using System.Text;

namespace Ibd.SiMer.Web
{
    public partial class clientes : System.Web.UI.Page
    {
        StringBuilder strHTML = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Data.DataTable dtGR = new System.Data.DataTable();

            clientesNe ocls = new clientesNe();

            dtGR = ocls.Consultar();

            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                Session["dtGR"] = dtGR;
                strHTML = ocls.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }
 
    }
}