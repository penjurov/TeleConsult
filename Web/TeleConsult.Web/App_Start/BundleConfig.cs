namespace TeleConsult.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/moment.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/Libs/grid-0.5.6.js",
                    "~/Scripts/bootstrap-dialog.js",
                    "~/Scripts/bootstrap-datetimepicker.js",
                    "~/Scripts/Common.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/grid-0.5.6.css",
                      "~/Content/bootstrap-dialog.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));
        }
    }
}
