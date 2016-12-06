using System.Web;
using System.Web.Optimization;

namespace ProyectoMoya
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/jquery.prettyPhoto.js",
                      "~/Scripts/jquery.scrollUp.min.js",
                      //"~/Scripts/price-range.js",
                      //"~/Scripts/gmaps.js",
                      //"~/Scripts/contact.js",
                      "~/Scripts/main.js",
                      "~/Scripts/bootstrap-slider.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/main.css",
                      "~/Content/prettyPhoto.css",
                      //"~/Content/price-range.css",
                      "~/Content/bootstrap-slider.css",
                      "~/Content/responsive.css"
                      ));
        }
    }
}
