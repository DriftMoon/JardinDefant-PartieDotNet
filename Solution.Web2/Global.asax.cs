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

            StarDocs = new StarDocs(new ConnectionInfo(new Uri("https://api.gnostice.com/stardocs/v1"), "01499657d42b46e380e5e1273fcef212"
                , "f1986a6f9f734f188ef018f956c00d36"));
            StarDocs.Auth.loginApp();
        }
    }
}
