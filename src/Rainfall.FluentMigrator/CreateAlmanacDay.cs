using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.FluentMigrator
{
    [Migration(1)]
    public class CreateAlmanacDay : Migration
    {
        public override void Up()
        {
            Create.Table("AlmanacDay")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Date").AsDateTime().NotNullable()
                 .WithColumn("CityId").AsInt32().NotNullable().ForeignKey("City", "Id");
        }

        public override void Down()
        {
            Delete.Table("AlmanacDay");
        }
    }
}
