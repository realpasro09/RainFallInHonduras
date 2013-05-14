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
        static IAlmanacDayGridSummaryModel _almanacDayGridSummary;
        static RainfallDataController _rainfallController;
        static AlmanacDayGridSummaryModel _almanacDayGridSummaryModelToday;
        static List<AlmanacDayGridItemModel> _almanacDayGridItemModels;
        static int _locationId;

        private Establish context = 
            () =>
                {
                    var containerBuilder = new ContainerBuilder();
                    IContainer container = new Bootstrapper(containerBuilder).Run();
                    _almanacDayGridSummary = container.Resolve<IAlmanacDayGridSummaryModel>();

                    _mockRepository = new Mock<IRepository>();
                    _mockMappingEngine = new Mock<IMappingEngine>();
                    _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object,
                                                                     _almanacDayGridSummary);

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

                    ////Today
                    //_mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                    //                                       .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime })
                    //                                       .ThatDoesNotMatch(new AlmanacDay { City = wrongLocation, Date = dateTime })
                    //                                       .Build()))
                    //    .Returns(almancDays);

                    ////Yesterday
                    //_mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                    //                                       .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-1) })
                    //                                       .ThatDoesNotMatch(new AlmanacDay { City = wrongLocation, Date = dateTime.AddDays(-1) })
                    //                                       .Build()))
                    //    .Returns(almancDays);

                    ////LastWeek
                    //_mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                    //                                       .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-7) })
                    //                                       .ThatDoesNotMatch(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-8) },
                    //                                                         new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-9) },
                    //                                                         new AlmanacDay { City = wrongLocation, Date = dateTime.AddDays(-8) })
                    //                                       .Build()))
                    //    .Returns(almancDays);

                    ////LastMonth
                    //_mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                    //                                       .ThatMatches(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-30) })
                    //                                       .ThatDoesNotMatch(new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-31) },
                    //                                                         new AlmanacDay { City = correctLocation, Date = dateTime.AddDays(-32) },
                    //                                                         new AlmanacDay { City = wrongLocation, Date = dateTime.AddDays(-31) })
                    //                                       .Build()))
                    //    .Returns(almancDays);

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
                    _almanacDayGridSummaryModelToday = new AlmanacDayGridSummaryModel() { AlmanacDays = _almanacDayGridItemModels };
                    

                };

        private Because of = () => _result = _rainfallController.GetRainfallData(_locationId, (int)PeriodType.LastYear);

        private It should_return_filtered_data_by_location_and_period = () => _result.Data.ShouldBeLike(_almanacDayGridSummaryModelToday);
        
    }
}