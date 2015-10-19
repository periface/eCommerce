using System.Web;
using System.Web.Optimization;

namespace PymeTamFinal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/layoutEshop").Include(
                "~/Content/Temas/Eshopper/js/jquery.scrollUp.min.js",
    "~/Content/Temas/Eshopper/js/price-range.js",
    "~/Content/Temas/Eshopper/js/jquery.scrollUp.min.js",
    "~/Content/Temas/Eshopper/js/main.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/TiendaCss").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Temas/Eshopper/css/font-awesome.css",
                "~/Content/Temas/Eshopper/css/prettyPhoto.css",
                "~/Content/Temas/Eshopper/css/price-range.css",
                "~/Content/Temas/Eshopper/css/animate.css",
                "~/Content/Temas/Eshopper/css/main.css",
                "~/Content/Temas/Eshopper/css/responsive.css"
                ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
