using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using Ibd.SiMer.Negocio;

namespace Ibd.SiMer.Web
{
 
    public partial class scoreCard : System.Web.UI.Page
    {

        #region Variables Generales

        StringBuilder strHTML = new StringBuilder();

        #endregion

        #region Eventos de pagina

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {                
                AñosNe clsNe = new AñosNe();
                DataTable dtG;
                dtG = clsNe.obtieneAñosCargados();
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ddl_year.DataSource = dtG;
                ddl_year.DataTextField = "año";
                ddl_year.DataValueField = "año";
                ddl_year.DataBind();

                   
             
                MesesNe clsMesesNe = new MesesNe();
                dtG = clsMesesNe.obtieneMesesCargados(int.Parse(ddl_year.SelectedItem.Value));
                ds = new DataSet();
                ds.Tables.Add(dtG.Copy());

                ddl_month.DataSource = dtG;
                ddl_month.DataTextField = "nombreMes";
                ddl_month.DataValueField = "numMes";
                ddl_month.DataBind();
                ddl_month.SelectedIndex = ddl_month.Items.Count;

               

                scoreCard1();
                scoreCard2();
                scoreCard3();
            }
        }

        #endregion

        #region Metodos de controles
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla1();", true);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla2();", true);
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla3();", true);
        }
        
        #endregion

        #region Metodos
        private void scoreCard1 ()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            scoreBoardNe oclsRpt = new scoreBoardNe();

            dtGR = oclsRpt.ScoreBoardNum(ddl_year.SelectedItem.Value, ddl_month.SelectedItem.Value);
            DBDataPlaceHolderIcons.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsRpt.CreateTableHTMLIcons(dtGR, ddl_year.SelectedItem.Value, ddl_month.SelectedItem.Value);
                DBDataPlaceHolderIcons.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }
        private void scoreCard2()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            scoreBoardNe oclsRpt = new scoreBoardNe();

            dtGR = oclsRpt.ScoreBoardCargakwhe(ddl_year.SelectedItem.Value, ddl_month.SelectedItem.Value);
            DBDataPlaceHolderCargakwhe.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolderCargakwhe.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }
        private void scoreCard3()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            scoreBoardNe oclsRpt = new scoreBoardNe();

            dtGR = oclsRpt.ScoreBoardCargakvarh(ddl_year.SelectedItem.Value, ddl_month.SelectedItem.Value);
            DBDataPlaceHolderCargakvarh.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsRpt.CreateTableHTML(dtGR);
                DBDataPlaceHolderCargakvarh.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        #endregion

        protected void lnkActualizar_Click(object sender, EventArgs e)
        {

            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla1();", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla2();", true);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            scoreCard1();
            scoreCard2();
            scoreCard3();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "tabla3();", true);
        }
    }
}