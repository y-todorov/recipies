using System.Web.Optimization;

namespace RecipiesWebFormApp
{
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;

          
            // this way no other request is made for the map file
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/*.js"));


            bundles.Add(new StyleBundle("~/Content/bundles/css")
                .Include("~/Content/Bundles/*.css")
                .Include("~/Content/Bundles/BlueOpal/*.png")
                .Include("~/Content/Bundles/BlueOpal/*.gif"))
                ;


               
        }
    }
}