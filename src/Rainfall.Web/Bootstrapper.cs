using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Autofac;
using Autofac.Integration.Mvc;

namespace Rainfall.Web
{
    public class Bootstrapper
    {
        readonly ContainerBuilder builder;

        public Bootstrapper(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public IContainer Run()
        {
            new List<IBootstrapperTask<ContainerBuilder>>
                {
                    new RegisterInjectedDependencies(),
                    new ConfigureDatabaseIntegration(),
                    new ConfigureAspNetMvc(),
                    new ConfigureAutomapper(),
                }.ForEach(bootstrapper => bootstrapper.Task(builder));

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}