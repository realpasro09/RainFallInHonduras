using System.Collections.Generic;
using System.Linq;

namespace Rainfall.Web.Models
{
    public class AlmanacDayGridSummaryModel
    {
        public IEnumerable<AlmanacDayGridItemModel> AlmanacDays { get; set; }
        public double MaxTemperature
        {
            get
            {
                return AlmanacDays.Max(x => x.TempHigh);
            }
        }

        public double MinTemperature
        {
            get
            {
                return AlmanacDays.Min(x => x.TempLow);
            }
        }
        public double AvgTemperature
        {
            get
            {
                var highList = AlmanacDays.Select(x=>x.TempHigh).ToList();
                var lowList = AlmanacDays.Select(x=>x.TempLow).ToList();
                var generalList = highList.Concat(lowList);
                return generalList.Average();
            }
        }
        public double AvgPrecipitation
        {
            get
            {
                return AlmanacDays.Average(x => x.Precipitation);
            }
        }
    }
}
