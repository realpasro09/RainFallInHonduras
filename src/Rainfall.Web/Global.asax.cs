﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace Rainfall.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var containerBuilder = new ContainerBuilder();
            var bootstrapper = new Bootstrapper(containerBuilder);
            ILifetimeScope container = bootstrapper.GetConfiguredContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }        
    }
}