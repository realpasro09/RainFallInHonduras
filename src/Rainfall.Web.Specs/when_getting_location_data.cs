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
    public class when_getting_location_data
    {
        static Mock<IRepository> _mockRepository;
        static Mock<IMappingEngine> _mockMappingEngine;
        static LocationDataController _locationController;
        static List<CityFilterLocationModel> _citiesModel;
        static JsonResult _result;

        Establish context =
            () =>
                {
                    _mockRepository = new Mock<IRepository>();
                    _mockMappingEngine = new Mock<IMappingEngine>();
                    _locationController = new LocationDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object);

                    IQueryable<City> cities = new List<City> {new City(), new City()}.AsQueryable();
                    _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<City>()
                                                           .Build()))
                        .Returns(cities);

                    _citiesModel = new List<CityFilterLocationModel>
                                       {new CityFilterLocationModel(), new CityFilterLocationModel()};

                    _mockMappingEngine.Setup(
                        x => x.Map<IEnumerable<City>, IEnumerable<CityFilterLocationModel>>(cities))
                        .Returns(_citiesModel);

                    _expectedList = new List<CityFilterLocationModel>
                                        {
                                            new CityFilterLocationModel(),
                                            new CityFilterLocationModel(),
                                            new CityFilterLocationModel {CityId = 0, Name = "All"}
                                        };
                };

        Because of = () => _result = _locationController.Get();

        It should_return_location_data = () =>
                                             {
                                                 _result.Data.ShouldBeLike(_expectedList);
                                             };

        static List<CityFilterLocationModel> _expectedList;
    }
}