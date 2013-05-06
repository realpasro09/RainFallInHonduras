using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;
using Rainfall.DatabaseMigrator.Migrations;

namespace Rainfall.DatabaseMigrator
{
    public static class DataBaseMigrationRunner
    {
        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
        }

        public static void MigrateUp(string connectionString, int version, IMigration schema)
        {
            // var announcer = new NullAnnouncer();
            var runner = GetRunner(connectionString);
            var info = new MigrationInfo(version, TransactionBehavior.Default, schema);
            
            runner.ApplyMigrationUp(info, true);
        }

        public static void MigrateDown(string connectionString, int version, IMigration scheema)
        {
            var runner = GetRunner(connectionString);
            var info = new MigrationInfo(version, TransactionBehavior.Default, scheema);

            runner.ApplyMigrationDown(info, true);
        }

        private static MigrationRunner GetRunner(string connectionString)
        {
            var announcer = new TextWriterAnnouncer(Console.WriteLine);
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = "Rainfall.FluenMigrator"
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new SqlServer2008ProcessorFactory();
            var processor = factory.Create(connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);

            return runner;
        }
    }
}
