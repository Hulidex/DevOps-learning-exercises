using System.Collections.Generic;
using DTO;
using Model;

namespace Service
{
   public interface IOpenWeatherMapServ
   {
      string ApiToken { set; }
      IList<HTTPAttribute> Attributes { get; set; }
      OpenWeatherMapRequest Run();
   }
}