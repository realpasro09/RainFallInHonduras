using System;
using AutoMapper;
using Autofac;
using Rainfall.Domain.Entities;
using Rainfall.Web.Models;

namespace Rainfall.Web.Infrastructure
{
    public class ConfigureAutomapper : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get { return container =>
                             {
                                 container.RegisterInstance(Mapper.Engine).As<IMappingEngine>();
                                 Mapper.CreateMap<AlmanacDay, AlmanacDayGridItemModel>()
                                       .ForMember(x => x.AlmanacDayId, o => o.MapFrom(x => x.Id))
                                       .ForMember(x => x.TempHigh, o => o.MapFrom(x => x.Get24HrsTempHigh()))
                                       .ForMember(x => x.TempLow, o => o.MapFrom(x => x.Get24HrsTempLow()))
                                       .ForMember(x => x.Precipitation, o => o.MapFrom(x => x.Get24HrsPrecipitation()))
                                       .ForMember(x => x.Date, o => o.MapFrom(x => x.Date.ToShortDateString()))
                                       .ForMember(x => x.City, o => o.MapFrom(x => x.City.Name));
                                 Mapper.CreateMap<City, CityFilterLocationModel>()
                                       .ForMember(x => x.CityId, o => o.MapFrom(x => x.Id))
                                       .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
                             }; }
        }

        #endregion
    }
}