using System.Linq;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using NHibernate;
using Rainfall.Domain;

namespace DatabaseDeploymentTool
{
    public class SeedAlamanacDays : IDataSeeder
    {
        readonly ISession _session;

        public SeedAlamanacDays(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            var list = Builder<AlmanacDay>.CreateListOfSize(400).Build().ToList();
            list.ForEach(x => _session.Save(x));            
        }
    }
}