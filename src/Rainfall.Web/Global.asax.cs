using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Rainfall.Web.Infrastructure;

namespace Rainfall.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
                                           
            var containerBuilder = new ContainerBuilder();
            new Bootstrapper(containerBuilder).Run();                               
        }        
    }
}