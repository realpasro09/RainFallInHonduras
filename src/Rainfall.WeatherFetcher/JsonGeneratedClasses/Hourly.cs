using System.Collections.Generic;

namespace Rainfall.WeatherFetcher.JsonGeneratedClasses
{
    public class Hourly
    {
        public string Cloudcover { get; set; }
        public string DewPointC { get; set; }
        public string DewPointF { get; set; }
        public string FeelsLikeC { get; set; }
        public string FeelsLikeF { get; set; }
        public string HeatIndexC { get; set; }
        public string HeatIndexF { get; set; }
        public string Humidity { get; set; }
        public string PrecipMm { get; set; }
        public string Pressure { get; set; }
        public string TempC { get; set; }
        public string TempF { get; set; }
        public string Time { get; set; }
        public string Visibility { get; set; }
        public string WeatherCode { get; set; }
        public List<WeatherDesc> WeatherDesc { get; set; }
        public List<WeatherIconUrl> WeatherIconUrl { get; set; }
        public string WindChillC { get; set; }
        public string WindChillF { get; set; }
        public string Winddir16Point { get; set; }
        public string WinddirDegree { get; set; }
        public string WindGustKmph { get; set; }
        public string WindGustMiles { get; set; }
        public string WindspeedKmph { get; set; }
        public string WindspeedMiles { get; set; }
    }
}