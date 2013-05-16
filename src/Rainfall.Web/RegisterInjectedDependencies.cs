using System;
using Autofac;
using Rainfall.Domain.Services;
using Rainfall.Integration;
using Rainfall.Web.Models;

namespace Rainfall.Web
{
    public class RegisterInjectedDependencies : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               container.RegisterType<Repository>().As<IRepository>();
                           };
            }
        }

        #endregion
    }
}