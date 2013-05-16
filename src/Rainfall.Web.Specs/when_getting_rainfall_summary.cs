using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AcklenAvenue.Testing.Moq;
using AutoMapper;
using Machine.Specifications;
using Moq;
using Rainfall.Domain;
using Rainfall.Domain.Entities;
using Rainfall.Domain.Services;
using Rainfall.Web.Controllers;
using Rainfall.Web.Models;
using It = Machine.Specifications.It;

namespace Rainfall.Web.Specs
{
    public class when_getting_rainfall_summary
    {
        static JsonResult _result;
        static Mock<IRepository> _mockRepository;
        static Mock<IMappingEngine> _mockMappingEngine;
        static RainfallDataController _rainfallController;
        static RainfallSummaryModel _rainfallSummary;
        static int _location;
        static RainfallSummaryModel _expectedRainfallSummaryModel;

        Establish context = () =>
            {
                _mockRepository = new Mock<IRepository>();
                _mockMappingEngine = new Mock<IMappingEngine>();
                _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                 _mockMappingEngine.Object);

                _location = 2;

                var wrongLocation = new City() { Id = 1 };
                var correctLocation = new City() { Id = 2 };

                DateTime dateTime = DateTime.Now;
                SystemDateTime.Now = () => dateTime;

                IQueryable<AlmanacDay> almancDays =
                        new List<AlmanacDay>
                            {
                                new AlmanacDay(){AlmanacHourly = new List<AlmanacHourly>(){new AlmanacHourly(){Temperature = 40, Precipitation = 3}}},
                                new AlmanacDay(){AlmanacHourly = new List<AlmanacHourly>(){new AlmanacHourly(){Temperature = 30, Precipitation = 1}}},
                                new AlmanacDay(){AlmanacHourly = new List<AlmanacHourly>(){new AlmanacHourly(){Temperature = 35, Precipitation = 2}}}
                            }.AsQueryable();

                _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                                                       .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-365) })
                                                       .ThatDoesNotMatch(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-366) },
                                                                         new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-367) },
                                                                         new AlmanacDay { City = wrongLocation, Date = dateTime.AddDays(-366) })
                                                       .Build()))
                    .Returns(almancDays);

                _expectedRainfallSummaryModel = new RainfallSummaryModel()
                    {
                        MaxTemp = 40, MinTemp = 30, AvgTemp = 35, AvgPrecipitation = 2, TotalPrecipitation = 6
                    };

            };

        Because of = () => _result = _rainfallController.GetRainfallSummary(_location, (int)PeriodType.LastYear);

        It should_return_rainfall_summary_data = () => _result.Data.ShouldBeLike(_expectedRainfallSummaryModel);
        
    }
}