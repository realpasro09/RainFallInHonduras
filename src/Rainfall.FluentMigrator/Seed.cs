using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.FluentMigrator
{
    [Migration(4)]
    class Seed : Migration

    {
        public override void Up()
        {
            // Seed.
            //Insert.IntoTable("AlmanacDay").Row(new { Date = DateTime.Now, Precipitation = 0.0, TempHigh = 0.0, TempLow = 0.0 });
        }
    }
}
