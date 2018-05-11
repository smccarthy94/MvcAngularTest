using System.Web.Optimization;

namespace ProgrammingTest.WebClient
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true; // minify files

            bundles.Add(new ScriptBundle("~/bundles/js-libs").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/js-app").IncludeDirectory("~/js/app/", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
