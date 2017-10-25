using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ibd.SiMer.Web
{
    public partial class Bajarresumengral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string strNameFile = (string)Request.QueryString["n"];
                if (!string.IsNullOrEmpty(strNameFile))
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + strNameFile + "\"");
                    Response.TransmitFile(Server.MapPath(GetPathUploadReports()) + strNameFile);
                    Response.End();
                }
                else
                {
                    Response.Redirect("resumengeneral.aspx");
                }

            }
        }
        public String GetPathUploadReports()
        {
            return ConfigurationManager.AppSettings["GuardarReporteGeneral"].ToString();
        }

    }
}