using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core;
using Blog.IServices;
using Blog.Services.Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Core.Controllers
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
        private readonly IAdvertisementServices _advertisementServices;
        public WeatherForecastController(IAdvertisementServices advertisementServices,ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _advertisementServices = advertisementServices;
        }
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string[] Get()
        {
            var ads = _advertisementServices.Test();
            return Summaries;
        }
        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = rng.Next(-20, 55),
        //            Summary = Summaries[rng.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //}
    }
}