using System;
using AutoMapper;
using Autofac;

namespace Rainfall.Web
{
    public class ConfigureAutomapper : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get { return container =>
                             {
                                 container.RegisterInstance(Mapper.Engine).As<IMappingEngine>();


                             }; }
        }

        #endregion
    }
}