﻿
using System;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;


namespace EntryWebFormsApplication1 {
    public class Global : HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}