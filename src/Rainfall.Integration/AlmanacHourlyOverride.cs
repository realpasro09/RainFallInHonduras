﻿using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Rainfall.Domain;
using Rainfall.Domain.Entities;

namespace Rainfall.Integration
{
    public class AlmanacHourlyOverride : IAutoMappingOverride<AlmanacHourly>
    {
        public void Override(AutoMapping<AlmanacHourly> mapping)
        {
            mapping.Id(x => x.Id);
            mapping.Map(x => x.AlmanacDayId).Column("AlmanacDayId");
        }
    }
}