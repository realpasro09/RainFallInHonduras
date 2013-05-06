using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Rainfall.DatabaseMigrator.Migrations
{
    [Migration(2)]
    public class DatabaseSchemaV2 : Migration
    {
        public override void Up()
        {
            Alter.Table("City").AddColumn("Name2").AsString().NotNullable().WithDefaultValue("1");
        }

        public override void Down()
        {
            Delete.Column("Name2").FromTable("City");
        }
    }
}
