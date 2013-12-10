using System.Web;
using System.Web.Optimization;

namespace pinit
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

            bundles.Add(new ScriptBundle("~/bundles/textext").Include(
                      "~//textext/js/textext.*"));

            /*
             * <script src="/textext/js/textext.core.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.tags.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.autocomplete.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.suggestions.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.filter.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.focus.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.prompt.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.ajax.js" type="text/javascript" charset="utf-8"></script>
                *   <script src="/textext/js/textext.plugin.arrow.js" type="text/javascript" charset="utf-8"></script>
             * */
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/base.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css"));



            /*
             * <link rel="stylesheet" href="/textext/css/textext.core.css" type="text/css">
                *    <link rel="stylesheet" href="/textext/css/textext.plugin.tags.css" type="text/css">
                *    <link rel="stylesheet" href="/textext/css/textext.plugin.autocomplete.css" type="text/css">
                *    <link rel="stylesheet" href="/textext/css/textext.plugin.focus.css" type="text/css">
                *    <link rel="stylesheet" href="/textext/css/textext.plugin.prompt.css" type="text/css">
                *    <link rel="stylesheet" href="/textext/css/textext.plugin.arrow.css" type="text/css">
             */

            bundles.Add(new StyleBundle("~/Content/textext").Include(
                      "~/textext/css/textext.*"));
        }
    }
}
