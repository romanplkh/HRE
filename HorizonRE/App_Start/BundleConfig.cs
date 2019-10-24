using System.Web;
using System.Web.Optimization;

namespace HorizonRE
{
   public class BundleConfig
   {
      // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
      public static void RegisterBundles( BundleCollection bundles )
      {
         bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

         bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*"));

         // Use the development version of Modernizr to develop with and learn from. Then, when you're
         // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
         bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

         bundles.Add(new ScriptBundle("~/bundles/popper").Include(
            "~/Scripts/umd/popper.js"));


         bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.js"));

         bundles.Add(new ScriptBundle("~/bundles/owl").Include(
            "~/Scripts/owl.carousel.min.js"));

         bundles.Add(new ScriptBundle("~/bundles/slick").Include(
            "~/Scripts/slick.js", "~/Scripts/InputSpinner.js"));

         bundles.Add(new ScriptBundle("~/bundles/app").Include(
            "~/Scripts/app.js"));

         bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/CSS/bundle.css"));

         bundles.Add(new StyleBundle("~/Content/PagedList").Include(
             "~/Content/PagedList.css"));

            bundles.Add(new StyleBundle("~/Content/fonts").Include(
            "~/Content/Fonts/webfonts/all.css"));

//         bundles.Add(new StyleBundle("~/Content/css").Include(
//                   "~/Content/bootstrap.css",
//                   "~/Content/site.css"));
      }
   }
}
