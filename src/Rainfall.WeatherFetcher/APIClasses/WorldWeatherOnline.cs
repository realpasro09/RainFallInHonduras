﻿using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Rainfall.Domain;
using Rainfall.WeatherFetcher.JsonGeneratedClasses;
using RestSharp;

namespace Rainfall.WeatherFetcher.APIClasses
{
    public class WorldWeatherOnline
    {
        private readonly ISession _session;
        private readonly DateTime _startLoggingDate;
        private readonly string _baseFreeUrl;
        private readonly string _basePremiumUrl;
        private readonly RestClient _client = new RestClient();

        public WorldWeatherOnline(ISession session)
        {
            _session = session;
            _startLoggingDate = new DateTime(2012,01,01);
            _baseFreeUrl = "http://api.worldweatheronline.com/free/v1/weather.ashx?";
            _basePremiumUrl = "http://api.worldweatheronline.com/premium/v1/past-weather.ashx?";
        }

        public void SaveDataFromPast()
        {
            var cities = _session.Query<City>();
            foreach (var city in cities)
            {
                var dateReview = _startLoggingDate;
                while (dateReview.Date != DateTime.Now.Date)
                {
                    if (!_session.Query<AlmanacDay>().Where(x => x.Date.Date == dateReview.Date && x.City.Id == city.Id).Any())
                    {
                        var weatherData = ApiCall(_basePremiumUrl, "Honduras", city.Name, "2e2rdxvat4nk5vvawt293pur", dateReview.ToString("yyyy-MM-dd"), dateReview.ToString("yyyy-MM-dd"));
                        SaveDailyCondition(weatherData, city);
                    }
                    dateReview = dateReview.AddDays(1);
                }
            }
        }

        private void SaveDailyCondition(IRestResponse<RootObject> weatherData, City city)
        {
            if (weatherData.Data.Data.Weather == null || weatherData.Data == null)
                return;

            foreach (var weatherDataCondition in weatherData.Data.Data.Weather)
            {
                using (var tx = _session.BeginTransaction())
                {
                    var weatherCondition = new AlmanacDay
                    {   
                        CityId = city.Id,
                        Date = Convert.ToDateTime(weatherDataCondition.Date),
                        AlmanacHourly = new AlmanacHourly[] { }
                    };
                    _session.Save(weatherCondition);

                    foreach (var hourlyCondition in weatherDataCondition.Hourly.Select(weatherHourlyDataCondition => new AlmanacHourly
                        {
                            AlmanacDayId = weatherCondition.Id,
                            Date = weatherCondition.Date,
                            Hour = Convert.ToInt32(weatherHourlyDataCondition.Time),
                            Precipitation = Convert.ToDouble(weatherHourlyDataCondition.PrecipMm),
                            Temp = Convert.ToDouble(weatherHourlyDataCondition.TempC)
                        }))
                    {
                        _session.Save(hourlyCondition);
                    }
                    tx.Commit();
                }
            }
        }

        private void SaveCurrentCondition(IRestResponse<RootObject> weatherData, City city)
        {
            if (weatherData.Data.Data.Current_Condition == null || weatherData.Data == null)
                return;

            foreach (var weatherDataCondition in weatherData.Data.Data.Current_Condition)
            {
                using (var tx = _session.BeginTransaction())
                {
                    var weatherCondition = new AlmanacDay
                    {
                        CityId = city.Id,
                        Date = DateTime.Now,
                        AlmanacHourly = new AlmanacHourly[] { }
                    };
                    _session.Save(weatherCondition);

                    var hourlyCondition = new AlmanacHourly
                        {
                            AlmanacDayId = weatherCondition.Id,
                            Date = weatherCondition.Date,
                            Hour = 0,
                            Precipitation = Convert.ToDouble(weatherDataCondition.precipMM),
                            Temp = Convert.ToDouble(weatherDataCondition.temp_C)
                        };
                    _session.Save(hourlyCondition);
                    tx.Commit();
                }
            }
        }
        private IRestResponse<RootObject> ApiCall(string baseUrl, string country, string city, string key ,string date, string enddate )
        {
            var dateParameter = "date=" + date;
            if (!String.IsNullOrEmpty(enddate))
                dateParameter += "&enddate=" + enddate;
                
            var locationParameter = "q=" + city.Replace(' ', '+') + "%2C" + country + "&";
            var url = baseUrl + locationParameter + "format=json&" + dateParameter + "&key=" + key;

            return  _client.Execute<RootObject>(new RestRequest(url));
        }

        public void SaveDailyData()
        {
            var cities = _session.Query<City>();
            foreach (var city in cities)
            {
                if (!_session.Query<AlmanacDay>().Any(x => x.Date.Date == DateTime.Now.Date && x.City.Id == city.Id))
                {
                    var weatherData = ApiCall(_baseFreeUrl, "Honduras", city.Name, "evmw22suz8ep8rauv9qh5xcd", DateTime.Now.ToString("yyyy-MM-dd"), null);
                    SaveCurrentCondition(weatherData, city);
                }
            }
        }

    }
}