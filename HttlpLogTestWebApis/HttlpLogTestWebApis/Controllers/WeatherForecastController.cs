using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttlpLogTestWebApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Specify the URL you want to make a request to
                string apiUrl = "https://reqres.in/api/users?page=2";

        
                // Make a GET request
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Check if the request was successful (status code in the range 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response
                    await response.Content.ReadAsStringAsync();
 
                }
                else
                {
                    Console.WriteLine("HTTP Request Failed with Status Code: " + response.StatusCode);
                }
            }


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [HttpPost]
        public async Task<string> Post(Data testdata)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Specify the URL you want to make a request to
                string apiUrl = "https://reqres.in/api/users";

                    Data data = new Data();

                data.Name = "Mukesh";
                data.Job = "Job1";

                string jsonContent = JsonSerializer.Serialize(data);
                HttpResponseMessage response;

                using (StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                {
                    // Make a POST request
                    response = await httpClient.PostAsync(apiUrl, content);

                    // Check if the request was successful (status code in the range 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine("HTTP Request Failed with Status Code: " + response.StatusCode);
                    }

                }

                return "testing response";
            }

        }
     
    }

    public class Data
    {
        public string Name { get; set; }
        public string Job { get; set; }
    }
}