using Covid19CroatiaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Services
{
    public interface ICovidStatsService
    {
        Task<string> GetInitialAsync();

        Task<DailyCovidOverview[]> GetInitalPocosAsync();

        Task<string> GetStringByDateAsync(DateTime dateUTC);

        Task<DailyCovidOverview[]> GetPocosByDateAsync(DateTime dateUTC);
    }
}
