using System.Web.Optimization;

namespace Ibd.SiMer.Web.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/jquery-3.1.1.js", 
                "~/Scripts/materialize/materialize.js", 
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.fixedColumns.min.js",
                "~/Scripts/DataTables/dataTables.material.js",
                "~/Scripts/jquery.blockUI.js", "~/Scripts/jquery.validate.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/materialize/css/materialize.css", "~/Content/iberdrola.css"));
            bundles.Add(new StyleBundle("~/bundles/material").Include("~/Content/material.min.css", 
                "~/Content/dataTables.material.min.css"));
            //BundleTable.EnableOptimizations = true;
            

        }
    }
}