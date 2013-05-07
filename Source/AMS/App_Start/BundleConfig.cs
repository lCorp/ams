using System.Web;
using System.Web.Optimization;

namespace AMS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/metro/metro.css",
                "~/Content/ams.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui/base/css").Include(
            "~/Content/jqueryui/base/jquery.ui.core.css",
            "~/Content/jqueryui/base/jquery.ui.resizable.css",
            "~/Content/jqueryui/base/jquery.ui.selectable.css",
            "~/Content/jqueryui/base/jquery.ui.accordion.css",
            "~/Content/jqueryui/base/jquery.ui.autocomplete.css",
            "~/Content/jqueryui/base/jquery.ui.button.css",
            "~/Content/jqueryui/base/jquery.ui.dialog.css",
            "~/Content/jqueryui/base/jquery.ui.slider.css",
            "~/Content/jqueryui/base/jquery.ui.tabs.css",
            "~/Content/jqueryui/base/jquery.ui.datepicker.css",
            "~/Content/jqueryui/base/jquery.ui.progressbar.css",
            "~/Content/jqueryui/base/jquery.ui.theme.css"));
        }
    }
}