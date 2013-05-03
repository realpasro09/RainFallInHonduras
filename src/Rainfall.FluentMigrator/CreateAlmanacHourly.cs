using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.FluentMigrator
{
    [Migration(2)]
    class CreateAlmanacHourly : Migration
    {
        public override void Up()
        {
            Create.Table("City")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Date").AsDateTime().NotNullable()
                 .WithColumn("Precipitation").AsDouble().NotNullable()
                 .WithColumn("TempHigh").AsDouble().NotNullable()
                 .WithColumn("TempLow").AsDouble().NotNullable()
                 .WithColumn("AlmanacDayId").AsInt32().NotNullable().ForeignKey("AlmanacDay", "Id");
        }

        public override void Down()
        {
            Delete.Table("City");
        }
    }
}
