using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AcklenAvenue.Testing.Moq;
using AutoMapper;
using Autofac;
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
    public class when_getting_rainfall_data_by_period_and_location
    {
        static JsonResult _result;
        static Mock<IRepository> _mockRepository;
        static Mock<IMappingEngine> _mockMappingEngine;
        static RainfallDataController _rainfallController;
        static AlmanacDayGridSummaryModel _almanacDayGridSummaryModelToday;
        static List<AlmanacDayGridItemModel> _almanacDayGridItemModels;
        static int _locationId;

        private Establish context = 
            () =>
                {
                    _mockRepository = new Mock<IRepository>();
                    _mockMappingEngine = new Mock<IMappingEngine>();
                    _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object);

                    _locationId = 2;
                    var wrongLocation = new City() {Id = 1};
                    var correctLocation = new City() { Id = 2 };

                    DateTime dateTime = DateTime.Now;
                    SystemDateTime.Now = () => dateTime;

                    IQueryable<AlmanacDay> almancDays = 
                        new List<AlmanacDay>{new AlmanacDay(), new AlmanacDay()}.AsQueryable();
                    
                    _almanacDayGridItemModels = new List<AlmanacDayGridItemModel>
                        {
                            new AlmanacDayGridItemModel(),
                            new AlmanacDayGridItemModel()
                        };

                    //LastYear
                    _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                                                           .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-365) })
                                                           .ThatDoesNotMatch(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-366) },
                                                                             new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-367) },
                                                                             new AlmanacDay { City = wrongLocation, Date = dateTime.AddDays(-366) })
                                                           .Build()))
                        .Returns(almancDays);

                    _mockMappingEngine.Setup(x => x.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almancDays))
                        .Returns(_almanacDayGridItemModels);
                    _almanacDayGridSummaryModelToday = new AlmanacDayGridSummaryModel() { aaData = _almanacDayGridItemModels };
                    

                };

        private Because of = () => _result = _rainfallController.GetRainfallData(_locationId, (int)PeriodType.LastYear,null);

        private It should_return_filtered_data_by_location_and_period = () => _result.Data.ShouldBeLike(_almanacDayGridSummaryModelToday);
        
    }
}