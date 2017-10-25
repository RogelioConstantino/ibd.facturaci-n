using System;

using System.Web.Optimization;
using WebMaterialize.App_Start;

namespace WebMaterialize
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}