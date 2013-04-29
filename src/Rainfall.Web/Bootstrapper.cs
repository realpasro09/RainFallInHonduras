using Autofac;

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
            //register all dependencies here, like this:
            //containerBuilder.RegisterType<SomeConcreteClass>().As<ISomeInterface>();

            return _containerBuilder.Build();
        }
    }
}