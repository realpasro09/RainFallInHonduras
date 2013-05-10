using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace Rainfall.Web
{
    public class ConfigureAspNetMvc : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                           {
                               builder.RegisterControllers(typeof(MvcApplication).Assembly);
                    
                               RegisterGlobalMvcFilters(GlobalFilters.Filters);
                               RegisterWebRoutes(RouteTable.Routes);                    
                           };
            }
        }

        #endregion

        public static void RegisterWebRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Clear();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }

        public static void RegisterGlobalMvcFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}