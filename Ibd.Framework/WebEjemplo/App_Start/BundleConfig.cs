using System.Web.Optimization;

namespace WebEjemplo.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Scripts
            /*bundles.Add(new StyleBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.1.1.js"
                , "~/Scripts/jquery-3.1.1.intellisense.js"
                ,"~/Scripts/bootstrap.js"));*/

            ScriptBundle thirdPartyScripts = new ScriptBundle("~/bundles/js");
            thirdPartyScripts.Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/jquery-3.1.1.intellisense.js",
                                "~/Scripts/tether/tether.js","~/Scripts/bootstrap.js");

            bundles.Add(thirdPartyScripts);

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css", "~/Content/tether/tether.css", "~/Content/iberdrola.css"
            ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
