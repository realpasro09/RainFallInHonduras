using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Rainfall.DatabaseMigrator.Migrations
{
    [Migration(0)]
    public class DatabaseSchemaV1 : Migration
    {
        public override void Up()
        {
            Create.Table("City")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Name").AsString().NotNullable();

            Create.Table("AlmanacDay")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Date").AsDateTime().NotNullable()
                 .WithColumn("CityId").AsInt32().NotNullable().ForeignKey("City", "Id");

            Create.Table("AlmanacHourly")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Precipitation").AsDouble().NotNullable()
                .WithColumn("TempHigh").AsDouble().NotNullable()
                .WithColumn("TempLow").AsDouble().NotNullable()
                .WithColumn("AlmanacDayId").AsInt32().NotNullable().ForeignKey("AlmanacDay", "Id");

            Insert.IntoTable("City").Row(new { Name = "San Pedro Sula" });
            Insert.IntoTable("City").Row(new { Name = "Tegucigalpa" });
        }

        public override void Down()
        {
            Delete.Table("AlmanacHourly");
            Delete.Table("AlmanacDay");
            Delete.Table("City");
        }
    }
}
