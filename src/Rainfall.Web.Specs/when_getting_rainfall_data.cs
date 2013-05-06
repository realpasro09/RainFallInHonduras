using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AcklenAvenue.Testing.Moq;
using AutoMapper;
using Machine.Specifications;
using Moq;
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
        static RainfallDataController _rainfallController;
        static JsonResult _result;
        static List<AlmanacDayGridItemModel> _almanacDayModels;

        Establish context =
            () =>
                {
                    _mockRepository = new Mock<IRepository>();
                    _mockMappingEngine = new Mock<IMappingEngine>();
                    _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object);

                    IQueryable<AlmanacDay> almanacDays =
                        new List<AlmanacDay> {new AlmanacDay(), new AlmanacDay()}.AsQueryable();
                    _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>().Build()))
                        .Returns(almanacDays);

                    _almanacDayModels = new List<AlmanacDayGridItemModel>
                                            {new AlmanacDayGridItemModel(), new AlmanacDayGridItemModel()};
                    _mockMappingEngine.Setup(
                        x => x.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays))
                        .Returns(_almanacDayModels);
                };

        Because of = () => _result = _rainfallController.Get();

        It should_return_rainfall_data = () => _result.Data.ShouldBeLike(_almanacDayModels);
    }
}