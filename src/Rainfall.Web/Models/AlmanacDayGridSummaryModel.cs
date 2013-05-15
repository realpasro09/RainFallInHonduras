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

        //public double MaxTemperature
        //{
        //    get
        //    {
        //        return !AlmanacDays.Any() ? 0 : AlmanacDays.Max(x => x.TempHigh);
        //    }
        //}

        //public double MinTemperature
        //{
        //    get
        //    {
        //        return !AlmanacDays.Any() ? 0 : AlmanacDays.Min(x => x.TempLow);
        //    }
        //}
        //public double AvgTemperature
        //{
        //    get
        //    {
        //        if (!AlmanacDays.Any())
        //            return 0;

        //        var highList = AlmanacDays.Select(x=>x.TempHigh).ToList();
        //        var lowList = AlmanacDays.Select(x=>x.TempLow).ToList();
        //        var generalList = highList.Concat(lowList);
        //        return generalList.Average();
                
        //    }
        //}
        //public double AvgPrecipitation
        //{
        //    get
        //    {
        //        return !AlmanacDays.Any() ? 0 : AlmanacDays.Average(x => x.Precipitation);
        //    }
        //}
    }
}
