using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.FluentMigrator
{
    [Migration(0)]
    class CreateCity : Migration

    {
        public override void Up()
        {
            Create.Table("City")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("City");
        }
    }
}
