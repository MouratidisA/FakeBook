using System.Web.Optimization;

namespace FakeBook
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jquery bundling section

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            #endregion


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                ));


           //JAVASCRIPT BUNDLE

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js"

                ));
            //bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
            //    "~/Scripts/bootbox.js"));


            // CSS BUNDLE

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css"
                ));


            //bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
            //        "~/Content/font-awesome.css"));


            //  Bundling and minification
            //BundleTable.EnableOptimizations = true;

        }
    }
}
