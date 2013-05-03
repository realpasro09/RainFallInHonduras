using System.Collections.Generic;
using DomainDrivenDatabaseDeployer;
using NHibernate;
using Rainfall.Domain;

namespace DatabaseDeploymentTool
{
    public class SeedCity : IDataSeeder
    {
        readonly ISession _session;

        public SeedCity(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            var list = new List<City>()
                {
                    new City(){Name = "San Pedro Sula"},
                    new City(){Name = "Tegucigalpa"},
                    new City(){Name = "Yuscaran"},
                    new City(){Name = "Trujillo"},
                    new City(){Name = "Choluteca"},
                    new City(){Name = "La Ceiba"},
                    new City(){Name = "Comayagua"},
                    new City(){Name = "Santa Rosa De Copan"},
                    new City(){Name = "Comayaguela"},
                    new City(){Name = "Puerto Lempira"},
                    new City(){Name = "La Esperanza"},
                    new City(){Name = "Roatan"},
                    new City(){Name = "La Paz"},
                    new City(){Name = "Gracias"},
                    new City(){Name = "Nueva Octopeque"},
                    new City(){Name = "Juticalpa"},
                    new City(){Name = "Santa Barbara"},
                    new City(){Name = "Nacaome"},
                    new City(){Name = "Yoro"},
                    new City(){Name = "Santa Ana"},
                    new City(){Name = "Ojojona"},
                    new City(){Name = "Catacamas"},
                    new City(){Name = "Choloma"},
                    new City(){Name = "Siguatepeque"},
                    new City(){Name = "La Lima"},
                    new City(){Name = "Tela"},
                    new City(){Name = "Cofradia"},
                    new City(){Name = "Olanchito"},
                    new City(){Name = "Puerto Cortes"},
                    new City(){Name = "Tocoa"},
                    new City(){Name = "San Lorenzo"},
                    new City(){Name = "Villanueva"},
                    new City(){Name = "El Progreso"},
                    new City(){Name = "Tocoa"},
                    new City(){Name = "Danli"},
                    new City(){Name = "Utila"},
                    new City(){Name = "Guanaja"}
                };
            list.ForEach(x => _session.Save(x));
        }
    }
}