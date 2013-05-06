using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator;
using Rainfall.DatabaseMigrator;
using Rainfall.DatabaseMigrator.Migrations;

namespace DatabaseMigratorTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //Actual version 2
            //Actual Schema DatabaseSchemaV1
            RunMigrate("Down", 2 , new DatabaseSchemaV2());
            Thread.Sleep(2000);
        }


        private static void RunMigrate(string migrateType, int version, IMigration schema)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["Rainfall"].ConnectionString;
            switch (migrateType)
            {
                case "Up":
                    DataBaseMigrationRunner.MigrateUp(connectionStringSettings, version, schema);
                    break;
                case "Down":
                    DataBaseMigrationRunner.MigrateDown(connectionStringSettings, version, schema);
                    break;
            }
        }
        
    }
}
