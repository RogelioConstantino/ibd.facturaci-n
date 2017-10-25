using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Medicion.Class.LogError;
using System.Data;
using Ibd.Framework.Crypt;

using Ibd.SiMer.Negocio;

namespace Ibd.SiMer.Web
{
    public partial class _Default : System.Web.UI.Page
    {
  //      LogErrorMedicion clsError = new LogErrorMedicion();
        DataTable Usr;
        protected void Page_Load(object sender, EventArgs e)
        {
            email.Focus();           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strEmailusr = string.Empty;
            string strIdUsuario = string.Empty;
            try 
            {
                string FullName = string.Empty;
                if (IsPostBack)
                {
                    Negocio.Login Exists = new Negocio.Login();
                    Encrypt clsEncrypt = new Encrypt();
                    clsEncrypt.strData = pass.Value;

                    Exists.UserName = email.Value.ToString();
                    Exists.Password = clsEncrypt.EncryptData();

                    DataTable Usr = Exists.GetUser();

                    if (Usr.Rows.Count > 0)
                    {
                        foreach (DataRow row in Usr.Rows)
                        {
                            FullName = Convert.ToString(row["FirstName"]) + " " + Convert.ToString(row["LastName"]);
                            strEmailusr = Convert.ToString(row["Email"]);
                            strIdUsuario = Convert.ToString(row["IdUsuario"]);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(FullName))
                    {
                        clsEncrypt.strData = FullName;
                        Session["Fullname"] = clsEncrypt.EncryptData(); 
                        clsEncrypt.strData = email.Value;
                        Session["email"] = strEmailusr;
                        Session["IdUsuario"] = strIdUsuario;
                        if (strIdUsuario =="8")
                            Response.Redirect("scoreCardCostos.aspx");
                        else
                            Response.Redirect("scoreCard.aspx");
                    }
                    else
                    {

                        ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Error:</strong> Usuario o contraseña inválido</div>";
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Error:</strong> Usuario o contraseña inválido</div>";
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "Button1_Click";
                //clsError.LogWrite();
            }
            
        }

        
    }
}
