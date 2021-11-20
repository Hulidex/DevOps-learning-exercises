namespace DTO
{
   public class MainAttributte
   {
       public double Temp { get; set; }
       public double Feels_like { get; set; }
       public double Temp_min { get; set; }
       public double Temp_max { get; set; }
       public double Pressure { get; set; }
       public double Humidity { get; set; }
       public double Sea_level { get; set; }
       public double Grnd_level { get; set; }

       public override string ToString()
       {
           return $"\tThe temperature is {Temp} celsius degrees, However it feels like {Feels_like}." +
            $"\r\n\tThe pressure is {Pressure} Pa." +
            $"\r\n\tThe humidity is {Humidity} %";
       }
   }
}