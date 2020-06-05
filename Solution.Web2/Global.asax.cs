using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Gnostice.StarDocsSDK;
namespace Solution.Web2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static StarDocs StarDocs;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StarDocs = new StarDocs(new ConnectionInfo(new Uri("https://api.gnostice.com/stardocs/v1"), "8b4b3b1a978c4ed7826665d984c61087"
                , "2b83347eb211400fb4688962c837f505"));
            //StarDocs.Auth.loginApp();
        }
    }
}
