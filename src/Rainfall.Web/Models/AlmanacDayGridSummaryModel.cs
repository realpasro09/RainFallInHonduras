using System;
using System.Collections.Generic;
using System.Linq;

namespace Rainfall.Web.Models
{
    public class AlmanacDayGridSummaryModel
    {
        public IEnumerable<AlmanacDayGridItemModel> aaData { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public string sEcho { get; set; }
    }
}
