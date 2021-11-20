using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DTO;
using Model;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service;

namespace OpenWeatherMap
{
    public enum ExitCode : int
    {
        Success = 0,
        Failure = 1
    }

    public class WeatherApp
    {
        private static string Key
        {
            get
            {
                return System.Environment.GetEnvironmentVariable(
                    "OpenWeatherMapApiToken", EnvironmentVariableTarget.Process
                );
            }
        }

        public static int Main(string[] args)
        {
            ExitCode returnedCode = ExitCode.Success;
            try
            {
                if (Key == null)
                {
                    throw new Exception("No Api Token was found please insert a valid api key token in the enviroment variable 'OpenWeatherMapApiToken'");
                }

                var requestParams = ConstructParameters(args);
                OpenWeatherMapRequest request;
                using (var service = new OpenWeatherMapServ())
                {
                    service.ApiToken = Key;
                    service.Attributes = requestParams;
                    request = service.Run();
                }

                switch (request?.Cod)
                {
                    case 200: // 200
                        Console.Write($"{request}");
                        break;

                    default:
                        Console.Write($"Unexpected response  with HTTP status code: '{request.Cod}'\r\nPlease check the request parameters and headers");
                        returnedCode = ExitCode.Failure;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Write($"Unexpected error:\r\n\t{e.Message}\r\nTrace:\r\n\t{e.StackTrace}\r\n");
                returnedCode = ExitCode.Failure;
            }

            return (int)returnedCode;
        }

        private static IList<HTTPAttribute> ConstructParameters(string[] args)
        {
            List<HTTPAttribute> attributes = new List<HTTPAttribute>();
            if (args.Length == 0)
            { // Default parameters if nothing is specified
                attributes = new List<HTTPAttribute>()
                {
                    {
                        new HTTPURLParameterAttribute("lat", "13.752053483435862")
                    },
                    {
                        new HTTPURLParameterAttribute("lon", "100.49273671213328")
                    },
                    {
                        new HTTPURLParameterAttribute("lang", "en")
                    },
                    {
                        new HTTPURLParameterAttribute("units", "metric")
                    },
                    {
                        new HTTPURLParameterAttribute("mode", "json")
                    }
                };
            }
            else if (args.Length % 2 != 0)
            {
                throw new Exception("Invalid number of arguments, one of the arguments has no associated value");
            }
            else if (args.Length < 4)
            {
                throw new Exception("Not enough arguments, at least specify two of them, i.e:\r\n\t--Lat 13.7520\r\n\t--Lon 100.4927");
            }
            else
            {
                string paramName = null;
                string paramValue = null;
                for (int i = 0; i < args.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        paramName = args[i].Replace("-", "").Trim().ToLower();
                    }
                    else
                    {
                        paramValue = args[i].Trim().ToLower();
                        attributes.Add(
                            new HTTPURLParameterAttribute(paramName, paramValue)
                            );
                    }
                }

                var mode = attributes.Where(attr => attr.Name.Equals("mode"))
                    .DefaultIfEmpty(null).First();
                if (mode == null)
                {
                    attributes.Add(new HTTPURLParameterAttribute("mode", "json"));
                }
                else
                {
                    mode.Value = "json"; // Force json mode
                }
            }

            return attributes;
        }
    }
}
