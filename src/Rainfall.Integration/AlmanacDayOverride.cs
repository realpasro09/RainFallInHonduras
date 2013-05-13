using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Rainfall.Domain;
using Rainfall.Domain.Entities;

namespace Rainfall.Integration
{
    public class AlmanacDayOverride:IAutoMappingOverride<AlmanacDay>
    {
        public void Override(AutoMapping<AlmanacDay> mapping)
        {
            mapping.Id(x => x.Id);
            mapping.Map(x => x.Date).Not.Nullable();
            mapping.References(x => x.City, "CityId").Cascade.None();
            mapping.HasMany(x => x.AlmanacHourly).KeyColumn("AlmanacDayId").Cascade.All();
        }
    }
}