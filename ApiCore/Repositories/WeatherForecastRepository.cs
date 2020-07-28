using ApiCore.Context;
using ApiCore.Entities;
using ApiCore.Models;
using ApiCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore.Repositories
{
    public class WeatherForecastRepository : RepositoryBase, IWeatherForecast
    {
        public WeatherForecastRepository(AppDBContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
        {
        }

        public async Task<WeatherForecast> CreateWeather(WeatherForecast weatherForecast )
        {
           // DbContext.WeatherForecasts.Add(weatherForecast);
            //await DbContext.SaveChangesAsync();
            return weatherForecast;
        } 
        
        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            var a = new List<WeatherForecast>();
            return a; //await DbContext.WeatherForecasts.ToListAsync();
        
        }
    }
}
