using System.Collections.Generic;

namespace Rainfall.Web.Models
{
    public interface IAlmanacDayGridSummaryModel
    {
        IEnumerable<AlmanacDayGridItemModel> AlmanacDays { get; set; }
    }
}