using System.Web;
using System.Web.Optimization;

namespace LmsTool
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new StyleBundle("~/Content/modals").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/bootstrap - theme.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/modals").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new StyleBundle("~/Content/jQueryUI-css").Include(
                        "~/Content/jquery-ui.css",
                        "~/Content/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jQueryUI").Include(
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", 
                      "~/Content/index.js",
                      "~/Scripts/jquery-1.10.2.*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
