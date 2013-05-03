using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcklenAvenue.Data.NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using Rainfall.Domain;
using Rainfall.Integration;
using Rainfall.WeatherFetcher.APIClasses;

namespace Rainfall.WeatherFetcher
{
    class Program
    {
        static void Main(string[] args)
        {
            MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString(x => x.FromConnectionStringWithKey("Rainfall"));

            ISessionFactory sessionFactory = new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration).Build();

            ISession session = sessionFactory.OpenSession();
            var weatherData = new WorldWeatherOnline(session);
            
            //weatherData.SaveDataFromPast();
            weatherData.SaveDailyData();

            session.Close();
            sessionFactory.Close();

            #region Sample

            /*
                        using (var sess = sessionFactory.OpenSession())
                        {
                            using (IDbCommand cmd = sess.Connection.CreateCommand())
                            {

                                var weatherCondition = new AlmanacDay
                                    {
                                        Date = DateTime.Now,
                                        AlmanacHourly = new AlmanacHourly[]
                                            {}
                                    };
                                sess.Save(weatherCondition);
                                var almanacHourlies = new AlmanacHourly[]
                                    {
                                        new AlmanacHourly
                                            {
                                                Date = DateTime.Now,
                                                Time = 0,
                                                Precipitation = 0.1,
                                                TempHigh = 40,
                                                TempLow = 32
                                            },
                                        new AlmanacHourly
                                            {
                                                Date = DateTime.Now,
                                                Time = 0,
                                                Precipitation = 0.0,
                                                TempHigh = 42,
                                                TempLow = 35
                                            },
                                        new AlmanacHourly
                                            {
                                                Date = DateTime.Now,
                                                Time = 0,
                                                Precipitation = 3.2,
                                                TempHigh = 32,
                                                TempLow = 25
                                            }
                                    };
                    

                                foreach (var almanacHourly in almanacHourlies)
                                {
                                    almanacHourly.AlmanacDayId = weatherCondition.Id;
                                    sess.Save(almanacHourly);
                                }
                                weatherCondition.AlmanacHourly = almanacHourlies;
                    
                                var prec = weatherCondition.Get24HrsPrecipitation();
                                var maxTemp = weatherCondition.Get24HrsTempHigh();
                                var minTemp = weatherCondition.Get24HrsTempLow();

                                var getData = sess.Query<AlmanacDay>().Where(x=>x.Date.Date==DateTime.Now);
             
                            }*/

            #endregion

        }
    }
}
