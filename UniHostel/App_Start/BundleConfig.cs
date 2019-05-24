using System.Web;
using System.Web.Optimization;

namespace UniHostel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bill.js",
                      "~/Scripts/DatePickerJQuery.js",
                      "~/Scripts/userpage.js",
                      "~/Scripts/alert/sweetalert2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bill.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-datepicker3.min.css",
                      "~/Content/bootstrap-datepicker.standalone.min.css",
                      "~/Content/bootstrap-datepicker3.standalone.min.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Scripts/alert/sweetalert2.css",
                      "~/url/https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/css").Include(
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/css/animate.css",
                "~/Content/bootstrap-datepicker3.min.css",
                "~/Content/bootstrap-datepicker.min.css",
                "~/Scripts/alert/sweetalert2.css",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/css/style.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar").Include(
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/../plugins/bower_components/jquery/dist/jquery.min.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/bootstrap/dist/js/bootstrap.min.js",
                "~/Scripts/DatePickerJQuery.js",
                "~/Scripts/alert/sweetalert2.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/../plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/js/jquery.slimscroll.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/js/waves.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/js/custom.min.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/js/reset-password.js",
                "~/Content/ampleadmin/ampleadmin-html/ampleadmin-sidebar/../plugins/bower_components/styleswitcher/jQuery.style.switcher.js"
                ));
        }
    }
}
