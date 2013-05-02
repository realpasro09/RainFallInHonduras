using System.Collections.Generic;

namespace Rainfall.WeatherFetcher.JsonGeneratedClasses
{
    public class Weather
    {
        public List<Astronomy> Astronomy { get; set; }
        public string Date { get; set; }
        public List<Hourly> Hourly { get; set; }
        public string MaxtempC { get; set; }
        public string MaxtempF { get; set; }
        public string MintempC { get; set; }
        public string MintempF { get; set; }
    }
}