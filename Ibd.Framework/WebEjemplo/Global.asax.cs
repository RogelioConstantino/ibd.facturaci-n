using System;
using System.Web.Optimization;
using WebEjemplo.App_Start;

namespace WebEjemplo
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}