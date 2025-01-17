﻿using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Domain;
using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using Unity;

namespace WebAppUsingUnity
{
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            ConfigureIoc();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureIoc()
        {
            var container = this.AddUnity();
            container.RegisterSingleton<IDummyService, DummyService>();
        }
    }
}