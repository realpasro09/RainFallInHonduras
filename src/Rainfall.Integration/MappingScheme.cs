using System;
using AcklenAvenue.Data;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Rainfall.Domain;

namespace Rainfall.Integration
{
    public class MappingScheme : IDatabaseMappingScheme<MappingConfiguration>
    {
        #region IDatabaseMappingScheme<MappingConfiguration> Members

        public Action<MappingConfiguration> Mappings
        {
            get
            {
                AutoPersistenceModel autoPersistenceModel = AutoMap.Assemblies(typeof (IEntity).Assembly)
                    .Where(t => typeof (IEntity).IsAssignableFrom(t))
                    .UseOverridesFromAssemblyOf<AlmanacDayOverride>()
                    .UseOverridesFromAssemblyOf<AlmanacHourlyOverride>()
                    //.IncludeBase(typeof(ProjectEventBase))
                    .Conventions.Add(DefaultCascade.All());

                //var types = new TypeScanner.TypeScanner().GetTypesOf<IEntity>().ToList();f
                //if (types.Any())
                //{
                //    autoPersistenceModel.OverrideInterface<IEntity, EntityInterfaceMap>(types);
                //    autoPersistenceModel.Add(new IsDeletedFilter());
                //}

                return x =>
                           {
                               x.AutoMappings.Add(autoPersistenceModel);
                               //x.HbmMappings.AddFromAssemblyOf<MappingScheme>();
                           };
            }
        }

        #endregion
    }
}