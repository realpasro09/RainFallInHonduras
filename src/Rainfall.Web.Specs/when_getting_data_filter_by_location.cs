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
    public class when_getting_data_filter_by_location
    {
        private static Mock<IRepository> _mockRepository;
        private static Mock<IMappingEngine> _mockMappingEngine;
        private static RainfallDataController _rainfallController;
        private static List<CityFilterLocationModel> _citiesModel;
        private static JsonResult _result;
        private Establish context = () =>
            {
                _mockRepository = new Mock<IRepository>();
                _mockMappingEngine = new Mock<IMappingEngine>();
                _rainfallController = new RainfallDataController(_mockRepository.Object,
                                                                     _mockMappingEngine.Object);

                IQueryable<City> cities = new List<City>{new City(), new City()}.AsQueryable();
                _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<City>()
                    .Build()))
                    .Returns(cities);

                _citiesModel = new List<CityFilterLocationModel> 
                 {new CityFilterLocationModel(),new CityFilterLocationModel()};

                _mockMappingEngine.Setup(x => x.Map<IEnumerable<City>,IEnumerable<CityFilterLocationModel>>(cities))
                    .Returns(_citiesModel); 

            };

        private Because of = () => _result = _rainfallController.GetLocations();

        private It should_return_data_filter_by_location = () => _result.Data.ShouldBeLike(_citiesModel);
        
    }
}