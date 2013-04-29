using Autofac;
using Autofac.Integration.Mvc;

namespace Rainfall.Web
{
    public class Bootstrapper
    {
        readonly ContainerBuilder _containerBuilder;

        public Bootstrapper(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public ILifetimeScope GetConfiguredContainer()
        {
            _containerBuilder.RegisterControllers(typeof (Bootstrapper).Assembly);

            //register all dependencies here, like this:
            //containerBuilder.RegisterType<SomeConcreteClass>().As<ISomeInterface>();

            return _containerBuilder.Build();
        }
    }
}