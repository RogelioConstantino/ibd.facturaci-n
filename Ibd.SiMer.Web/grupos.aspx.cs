﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibd.SiMer.Negocio;
using System.Text;

namespace Ibd.SiMer.Web
{
    public partial class grupos : System.Web.UI.Page
    {
        StringBuilder strHTML = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Data.DataTable dtGR = new System.Data.DataTable();

            gruposNE ocls = new gruposNE();

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