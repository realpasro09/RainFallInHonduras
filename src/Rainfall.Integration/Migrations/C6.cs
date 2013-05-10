using FluentMigrator;

namespace Rainfall.Integration.Migrations
{
    [Migration(0)]
    public class C6 : Migration
    {
        public override void Up()
        {
            if (Schema.Table("AlmanacHourly").Exists())
                Delete.Table("AlmanacHourly");

            if (Schema.Table("AlmanacDay").Exists())
                Delete.Table("AlmanacDay");

            if (Schema.Table("City").Exists())
                Delete.Table("City");

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
                .WithColumn("Hour").AsInt32().NotNullable()
                .WithColumn("Precipitation").AsDouble().NotNullable()
                .WithColumn("Temperature").AsDouble().NotNullable()
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
