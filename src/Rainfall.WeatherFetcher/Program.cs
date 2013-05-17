using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcklenAvenue.Data.NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using Rainfall.Domain;
using Rainfall.Domain.Entities;
using Rainfall.Integration;
using Rainfall.WeatherFetcher.APIClasses;

namespace Rainfall.WeatherFetcher
{
    class Program
    {
        static void Main(string[] args)
        {
            MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString(x => x.FromConnectionStringWithKey(GetConnectionStringName()));

            ISessionFactory sessionFactory = new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration).Build();

            ISession session = sessionFactory.OpenSession();
            
            var weatherData = new WorldWeatherOnline(session);
            
            weatherData.SaveDataFromPast();
            weatherData.SaveDailyData();

            session.Close();
            sessionFactory.Close();
        }

        static string GetConnectionStringName()
        {
            string env = ConfigurationManager.AppSettings["Env"];
            if (string.IsNullOrEmpty(env)) env = "Development";
            string connectionStringName = "Rainfall_" + env;
            return connectionStringName;
        }
    }
}
