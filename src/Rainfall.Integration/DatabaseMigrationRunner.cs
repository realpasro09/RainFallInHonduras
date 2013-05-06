using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;

namespace Rainfall.Integration
{
    public static class DataBaseMigrationRunner
    {
        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
        }

        public static void MigrateUp(string connectionString)
        {
            var runner = GetRunner(connectionString);
            runner.MigrateUp();
        }

        public static void MigrateUp(string connectionString, int version)
        {
            var runner = GetRunner(connectionString);
            runner.MigrateUp(version);
        }

        public static void MigrateDown(string connectionString, int version)
        {
            var runner = GetRunner(connectionString);
            runner.MigrateDown(version);
        }

        private static MigrationRunner GetRunner(string connectionString)
        {
            var announcer = new TextWriterAnnouncer(Console.WriteLine);
            var assembly = Assembly.GetExecutingAssembly();
            
            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = "Rainfall.Integration.Migrations"
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new SqlServer2008ProcessorFactory();
            var processor = factory.Create(connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);

            return runner;
        }
    }
}
