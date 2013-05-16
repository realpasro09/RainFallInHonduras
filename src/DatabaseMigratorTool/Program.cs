using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator;
using Rainfall.Integration;

namespace DatabaseMigratorTool
{
    class Program
    {
        private static string _connectionStringSettings;

        static void Main(string[] args)
        {
            _connectionStringSettings = ConfigurationManager.ConnectionStrings[GetConnectionStringName()].ConnectionString;
            DataBaseMigrationRunner.MigrateUp(_connectionStringSettings);
            Thread.Sleep(2000);
        }

        static string GetConnectionStringName()
        {
            string env = ConfigurationManager.AppSettings["Env"];
            if (string.IsNullOrEmpty(env)) env = "Development";
            string connectionStringName = "Rainfall_" + env;
            return connectionStringName;
        }
    }
}