using System;
using System.Collections.Generic;
using System.Linq;
using Angular2Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict;

namespace Angular2Core.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private DataDbContext dataDbContext;
        private ApplicationDbContext appDbContext;

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public SampleDataController(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpPost]
        [Route("CreateSampleData")]
        public void CreateSampleData()
        {
            this.dataDbContext.Samples.Add(new Sample() { Name = "Hello World-" + new Random().Next() });
            this.dataDbContext.SaveChanges();
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(this.TemperatureC / 0.5556);
                }
            }
        }
    }
}
