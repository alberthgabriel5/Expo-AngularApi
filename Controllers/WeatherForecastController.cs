using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPILab.Controllers
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

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("[action]/{date}")]
        [HttpGet]
        public WeatherForecast GetByDate(DateTime date)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }


        [Route("[action]")]
        [HttpPost]
        public WeatherForecast Post(WeatherForecast weatherForecast)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };
        }

        //TODO PUT Y PATCH

        [Route("[action]/{date}/{temperatureC}/{summary}")]
        [HttpPut]
        public WeatherForecast Put(DateTime date,int temperatureC, String summary)
        {
            var rng = new Random();
            WeatherForecast weatherForecast = Post(new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 22,
                Summary ="summary"
            });

            weatherForecast.Date = date;
            weatherForecast.TemperatureC = temperatureC;
            weatherForecast.Summary += summary;
            return weatherForecast;
           
        }


        [Route("[action]/{summary}")]
        [HttpPatch]
        public WeatherForecast Patch(String summary)
        {
            var rng = new Random();
            WeatherForecast weatherForecast = Post(new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 22,
                Summary = "prueba"
            });

            weatherForecast.Summary = summary;

            return weatherForecast;
            
        }


    }
}
