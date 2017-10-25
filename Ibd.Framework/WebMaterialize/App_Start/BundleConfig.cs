using System.Web.Optimization;

namespace WebMaterialize.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/materialize/materialize.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/materialize/css/materialize.css", "~/Content/iberdrola.css"));
            bundles.Add(new ScriptBundle("~/bundles/jsAux").Include("~/Scripts/menu.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
