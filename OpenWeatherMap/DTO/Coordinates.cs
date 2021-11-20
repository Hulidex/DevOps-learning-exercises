namespace DTO
{
    public class Coordinate
    {
        public double Lon { get; set; }
        public double Lat { get; set; }

        public override string ToString()
        {
            return $"Longitude: {Lon}, Latitude {Lat}";
        }
    }
}