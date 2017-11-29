using System.Web;
using System.Web.Optimization;

namespace Tuatara
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-3.3.7-dist/css/bootstrap.css",
                      "~/Content/angular-xeditable-0.8.0/css/xeditable.min.css",
                      "~/Content/site.css"));

            bundles.Add(new Bundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/ui-bootstrap-tpls-2.5.0.min.js",
                "~/Content/angular-xeditable-0.8.0/js/xeditable.min.js"
                ));
            
        }
    }
}
