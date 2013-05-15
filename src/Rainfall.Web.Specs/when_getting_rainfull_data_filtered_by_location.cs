using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AcklenAvenue.Testing.Moq;
using AutoMapper;
using Autofac;
using Machine.Specifications;
using Moq;
using Rainfall.Domain.Entities;
using Rainfall.Domain.Services;
using Rainfall.Web.Controllers;
using Rainfall.Web.Models;
using It = Machine.Specifications.It;

namespace Rainfall.Web.Specs
{
    public class when_getting_rainfull_data_filtered_by_location
    {
        static Mock<IRepository> _mockRepository;
        static Mock<IMappingEngine> _mockMappingEngine;
        static RainfallDataController _rainfallController;
        static JsonResult _result;
        static List<AlmanacDayGridItemModel> _almanacDayModels;
        static AlmanacDayGridSummaryModel _almanacDayGridSummaryModel;
        static int _locationId;

        Establish context =
            () =>
                {
                    _mockRepository = new Mock<IRepository>();
                    _mockMappingEngine = new Mock<IMappingEngine>();
                    _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object);

                    _locationId = 2;
                    var wrongLocation = new City() {Id = 1};
                    var correctLocation = new City() { Id = 2 };

                    IQueryable<AlmanacDay> almanacDays =
                        new List<AlmanacDay> {new AlmanacDay(), new AlmanacDay()}.AsQueryable();

                    _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>()
                                                           .ThatMatches(new AlmanacDay { City = correctLocation })
                                                           .ThatDoesNotMatch(new AlmanacDay { City = wrongLocation })
                                                           .Build()))
                        .Returns(almanacDays);

                    _almanacDayModels = new List<AlmanacDayGridItemModel>
                                            {
                                                new AlmanacDayGridItemModel(),
                                                new AlmanacDayGridItemModel()
                                            };
                    _mockMappingEngine.Setup(
                        x =>
                        x.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays))
                        .Returns(_almanacDayModels);

                    _almanacDayGridSummaryModel = new AlmanacDayGridSummaryModel()
                        {
                            aaData = _almanacDayModels
                        };
                };


        Because of = () => _result = _rainfallController.GetRainfallDataByLocation(_locationId);

        It should_do_return_filtered_data_by_location = () => _result.Data.ShouldBeLike(_almanacDayGridSummaryModel);
    }
}