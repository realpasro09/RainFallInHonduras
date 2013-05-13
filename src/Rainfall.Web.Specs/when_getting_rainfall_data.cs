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
    public class when_getting_rainfall_data
    {
        static Mock<IRepository> _mockRepository;
        static Mock<IMappingEngine> _mockMappingEngine;
        private static IAlmanacDayGridSummaryModel _almanacDayGridSummary;
        static RainfallDataController _rainfallController;
        static JsonResult _result;
        static AlmanacDayGridSummaryModel _almanacDayGridSummaryModel;
        private static List<AlmanacDayGridItemModel> _almanacDayGridItemModels; 

        Establish context =
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

                    IQueryable<AlmanacDay> almanacDays =
                        new List<AlmanacDay> {new AlmanacDay(), new AlmanacDay()}.AsQueryable();
                    DateTime dateTime = DateTime.Now;
                    SystemDateTime.Now = () => dateTime;

                    _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                        .ThatMatches(new AlmanacDay(){Date = dateTime.AddDays(-30)})
                        .ThatDoesNotMatch(new AlmanacDay(){Date = dateTime.AddDays(-31)},new AlmanacDay(){Date = dateTime.AddDays(-32)})
                        .Build()))
                        .Returns(almanacDays);
                   
                    _almanacDayGridItemModels = new List<AlmanacDayGridItemModel>()
                                                {new AlmanacDayGridItemModel(),new AlmanacDayGridItemModel()};
                    _mockMappingEngine.Setup(
                        x => x.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays))
                        .Returns(_almanacDayGridItemModels);

                    _almanacDayGridSummaryModel = new AlmanacDayGridSummaryModel() { AlmanacDays = _almanacDayGridItemModels };
                };

        Because of = () => _result = _rainfallController.Get();

        It should_return_rainfall_data_over_the_past_30_day = () => _result.Data.ShouldBeLike(_almanacDayGridSummaryModel);
        
    }
}