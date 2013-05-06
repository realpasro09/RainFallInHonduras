using AutoMapper;
using Autofac;
using FizzWare.NBuilder;
using Machine.Specifications;
using Rainfall.Domain.Entities;
using Rainfall.Web.Models;

namespace Rainfall.Web.Specs
{
    public class when_mapping_an_almanac_day_to_a_model
    {
        static AlmanacDay _originalAlmanacDay;
        static AlmanacDayGridItemModel _expectedGridItemModel;
        static IMappingEngine _mapper;
        static AlmanacDayGridItemModel _result;

        Establish context =
            () =>
                {
                    var containerBuilder = new ContainerBuilder();
                    IContainer container = new Bootstrapper(containerBuilder).Run();
                    _mapper = container.Resolve<IMappingEngine>();

                    _originalAlmanacDay = Builder<AlmanacDay>.CreateNew()
                        .With(x => x.AlmanacHourly = Builder<AlmanacHourly>.CreateListOfSize(20).Build())
                        .And(x => x.City = Builder<City>.CreateNew().Build())
                        .Build();

                    _expectedGridItemModel = new AlmanacDayGridItemModel
                                                 {
                                                     AlmanacDayId = _originalAlmanacDay.Id,
                                                     Date = _originalAlmanacDay.Date.ToShortDateString(),
                                                     Precipitation =
                                                         _originalAlmanacDay.Get24HrsPrecipitation(),
                                                     TempHigh = _originalAlmanacDay.Get24HrsTempHigh(),
                                                     TempLow = _originalAlmanacDay.Get24HrsTempLow(),
                                                     City = _originalAlmanacDay.City.Name,
                                                 };
                };

        Because of = () => _result = _mapper.Map<AlmanacDay, AlmanacDayGridItemModel>(_originalAlmanacDay);

        It should_return_the_expected_mapped_model = () => _result.ShouldBeLike(_expectedGridItemModel);
    }
}