using System;
using Autofac;
using Rainfall.Domain.Services;
using Rainfall.Integration;

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

                               //container.RegisterType<AccountInputValidator>().As<IAccountInputValidator>();
                               //container.RegisterInstance(Mapper.Engine).As<IMappingEngine>();
                               //container.RegisterType<WishlistValidator>().As<IWishlistValidator>();
                               //container.RegisterType<WishlistItemValidator>().As<IWishlistItemValidator>();
                           };
            }
        }

        #endregion
    }
}