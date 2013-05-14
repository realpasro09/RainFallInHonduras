using System.ComponentModel;

namespace Rainfall.Web.Models
{
    public enum PeriodType
    {
        Today,
        Yesterday,
        [Description("Last Week")]
        LastWeek,
        [Description("Last Month")]
        LastMonth,
        [Description("Last Year")]
        LastYear
    };
}