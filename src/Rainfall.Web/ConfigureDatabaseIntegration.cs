using System;
using System.Configuration;
using AcklenAvenue.Data.NHibernate;
using Autofac;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Rainfall.Integration;

namespace Rainfall.Web
{
    public class ConfigureDatabaseIntegration : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
                    ConnectionString(x => x.FromConnectionStringWithKey(GetConnectionStringName()));

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

        static string GetConnectionStringName()
        {
            string env = ConfigurationManager.AppSettings["Environment"];
            if (string.IsNullOrEmpty(env)) env = "Development";
            string connectionStringName = "Rainfall_" + env;
            return connectionStringName;
        }
    }
}