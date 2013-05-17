using AutoMapper;
using Autofac;
using FizzWare.NBuilder;
using Machine.Specifications;
using Rainfall.Domain.Entities;
using Rainfall.Web.Infrastructure;
using Rainfall.Web.Models;

namespace Rainfall.Web.Specs
{
    public class when_mapping_a_city_to_a_model
    {
        static City _originalCity;
        static CityFilterLocationModel _expectedCityFilterLocationModel;
        static IMappingEngine _mapper;
        static CityFilterLocationModel _result;

        private Establish context = () =>
            {
                var containerBuilder = new ContainerBuilder();
                IContainer container = new Bootstrapper(containerBuilder).Run();
                _mapper = container.Resolve<IMappingEngine>();

                _originalCity = Builder<City>.
                    CreateNew()
                    .Build();

                _expectedCityFilterLocationModel = new CityFilterLocationModel
                    {
                        CityId = _originalCity.Id,
                        Name = _originalCity.Name
                    };
            };

        private Because of = () => _result = _mapper.Map<City, CityFilterLocationModel>(_originalCity); 

        private It should_return_the_expected_mapped_model  = () => _result.ShouldBeLike(_expectedCityFilterLocationModel);
    }
}