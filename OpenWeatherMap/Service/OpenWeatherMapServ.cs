using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using DTO;
using Model;
using Newtonsoft.Json;

namespace Service
{
   public class OpenWeatherMapServ : IOpenWeatherMapServ, IDisposable
   {
        private HttpClient client;
        private HttpRequestMessage request;
        private string apiToken;
        private string urlWithParams;

        private static string url = "https://community-open-weather-map.p.rapidapi.com/weather";
        public static string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string ApiToken
        {
            set
            {
                apiToken = value;
            }
        }

        public IList<HTTPAttribute> Attributes { get; set; }

        public OpenWeatherMapServ()
        {
            this.Initialize();
        }

        public OpenWeatherMapServ(string apiToken)
        {
            this.apiToken = apiToken;
        }

        public OpenWeatherMapRequest Run()
        {
            if (Attributes != null)
            {
                AddAttributtes();
            }
            else
            {
                throw new Exception("No attributes specified! Please at least indicate the atributes 'lat' and 'lon'.");
            }

            if (apiToken != null)
            {
                AddApiToken();
            }
            else
            {
                throw new Exception("No ApiToken specified! Please specify one!");
            }

            request.RequestUri = new Uri(urlWithParams);

            var responseTask = client.SendAsync(request).ContinueWith(
                t =>
                {
                    var response = t.Result;

                    var status = response.StatusCode;
                    var readContentTask = response.Content.ReadAsStringAsync();
                    readContentTask.Wait();

                    return JsonConvert.DeserializeObject<OpenWeatherMapRequest>(readContentTask.Result);
                }
            );
            responseTask.Wait();

            return responseTask.Result;
        }

        private void AddApiToken()
        {
            var apiTokenHeaderName = "x-rapidapi-key";
            var headerAdded = request.Headers
                .TryAddWithoutValidation(apiTokenHeaderName, apiToken);

            if (!headerAdded)
            {
                throw new Exception($"Error: The header {apiTokenHeaderName}, couldn't be added.");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                request?.Dispose();
                client?.Dispose();
            }
        }

        private void AddAttributtes()
        {
            urlWithParams = Url;
            uint paramsCount = 0;

            foreach (var i in Attributes)
            {
                switch (i.Type)
                {
                    case HTTPAttribute.AttributeType.HttpHeader:
                        request.Headers.Add(i.Name, i.Value);
                        break;
                    case HTTPAttribute.AttributeType.HttpURLParameter:
                        char symbol;
                        symbol = paramsCount > 0 ? '&' : '?';
                        urlWithParams += $"{symbol}{i.Name}={i.Value}";
                        ++paramsCount;
                        break;
                }
            }
        }

        private void Initialize()
        {
            client = new HttpClient();
            request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };

            // Indicate that we're accepting JSON responses
            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
   }
}