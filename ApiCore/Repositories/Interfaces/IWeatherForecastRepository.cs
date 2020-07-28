using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore.Repositories.Interfaces
{
    public interface IWeatherForecast : IRepositoryBase
    {
        Task<WeatherForecast> CreateWeather(WeatherForecast weatherForecast);
        Task<IEnumerable<WeatherForecast>> GetAll();

    }
}
