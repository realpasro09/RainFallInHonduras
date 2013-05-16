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
                string connectionStringName = GetConnectionStringName();
                MsSqlConfiguration databaseConfiguration;
                try
                {
                    databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
                    ConnectionString(x => x.FromConnectionStringWithKey(connectionStringName));
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        string.Format("The connection string name '{0}' does not exist in the local.config file.",
                                      connectionStringName), ex);
                }

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
            string env = ConfigurationManager.AppSettings["Env"];
            if (string.IsNullOrEmpty(env)) env = "Development";
            string connectionStringName = "Rainfall_" + env;
            return connectionStringName;
        }
    }
}