using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

using Ibd.SiMer.Negocio;

namespace Ibd.SiMer.Web
{
    public partial class ScoreBoard : System.Web.UI.Page
    {
        StringBuilder strHTML = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Data.DataTable dtGR = new System.Data.DataTable();
            scoreBoardNe oclsRpt = new scoreBoardNe();

            dtGR = oclsRpt.ScoreBoardCargakvarh("", "");
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //Session["dtGRCarga"] = dtGR;
                strHTML = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolderCarga.Controls.Add(new Literal { Text = strHTML.ToString() });
            }

            dtGR = oclsRpt.ScoreBoardCargakwhe("","");
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //Session["dtGRCarga"] = dtGR;
                strHTML = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolderCarga.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
                        
            dtGR = oclsRpt.ScoreBoardNum("", "");
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                //Session["dtGRNum"] = dtGR;
                strHTML = oclsRpt.CreateTableHTMLIcons(dtGR, "", "");
                DBDataPlaceHolderNum.Controls.Add(new Literal { Text = strHTML.ToString() });
            }

        }
    }
}