using System;
using AcklenAvenue.Data.NHibernate;
using Autofac;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Rainfall.Integration;

namespace Rainfall.Web
{
    public class ConfigureDatabaseIntegration : IBootstrapperTask<Autofac.ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<Autofac.ContainerBuilder> Task
        {
            get
            {
                MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
                    ConnectionString(x => x.FromConnectionStringWithKey("Rainfall"));

                return container =>
                           {
                               container.Register(c => c.Resolve<ISessionFactory>().OpenSession()).As
                                   <ISession>()
                                   .InstancePerLifetimeScope()
                                   .OnActivating(c =>
                                                     {
                                                         if (!c.Instance.Transaction.IsActive)
                                                             c.Instance.BeginTransaction();
                                                     }
                                   )
                                   .OnRelease(c =>
                                                  {
                                                      if (c.Transaction.IsActive)
                                                      {
                                                          c.Transaction.Commit();
                                                      }
                                                      c.Dispose();
                                                  });

                               container.Register(c =>
                                                  new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration).
                                                      Build())
                                   .SingleInstance()
                                   .As<ISessionFactory>();
                           };
            }
        }

        #endregion
    }
}