using System.Collections.Generic;

namespace DTO
{
    public class OpenWeatherMapRequest
    {
        public Coordinate Coord { get; set; }
        public IList<Weather> Weather { get; set; }
        public string Base { get; set; }
        public MainAttributte Main { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public Cloud Clouds { get; set; }
        public uint Dt { get; set; }
        public Sys Sys { get; set; }
        public uint TimeZone { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Cod { get; set; }

        public string OpenStreetMapsLocationUrl =>
            $"https://www.openstreetmap.org/#map=14/{Coord?.Lat}/{Coord?.Lon}";

        public override string ToString()
        {
            var currentWeather = Weather?.Count > 0 ? Weather[0] : null;
            return $"Given the following Coordinates:\r\n{Coord}." +
            $"\r\nThe actual weather is:\r\n{currentWeather}" +
            $"\r\nPreasure and Temperature:\r\n{Main}" +
            $"\r\nYou can check the location in a Map by clicking in this link:\r\n\t{OpenStreetMapsLocationUrl}\r\n";
        }
    }
}