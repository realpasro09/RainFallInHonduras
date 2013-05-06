namespace Rainfall.Web.Models
{
    public class AlmanacDayGridItemModel
    {
        public string City { get; set; }
        public double TempLow { get; set; }
        public double TempHigh { get; set; }
        public double Precipitation { get; set; }
        public string Date { get; set; }
        public int AlmanacDayId { get; set; }
    }
}