using System.Collections.Generic;

namespace Rainfall.Web.Models
{
    public class AlmanacDayGridSummary
    {
        public IEnumerable<AlmanacDayGridItemModel> AlmanacDays { get; set; }
        public double MaxTemperature { get; set;}
        public double MinTemperature { get; set; }
        public double AvgMaxTemperature { get; set; }
        public double AvgMinTemperature { get; set; }
        public double MaxPrecipitation { get; set; }
        public double MinPrecipitacion { get; set; }
        public double AvgPrecipitation { get; set; }

        public AlmanacDayGridSummary(IEnumerable<AlmanacDayGridItemModel> almanacDays, double[] summary)
        {
            AlmanacDays = almanacDays;
            MaxTemperature = summary[0];
            MinTemperature = summary[1];
            AvgMaxTemperature = summary[2];
            AvgMinTemperature = summary[3];
            MaxPrecipitation = summary[4];
            MinPrecipitacion = summary[5];
            AvgPrecipitation = summary[6];
        }
    }
}
