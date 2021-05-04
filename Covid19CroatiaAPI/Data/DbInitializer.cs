using Covid19CroatiaAPI.Entities;
using Covid19CroatiaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, ICovidStatsService covidStatsService)
        {
            context.Database.Migrate();

            if (!context.DailyCovidOverviews.Any())
            {
                DailyCovidOverview[] retrievedDailyCovidOverviews = await covidStatsService.GetInitalPocosAsync();

                for (int i = 0; i < retrievedDailyCovidOverviews.Length; i++)
                {
                    DailyCovidOverview current = retrievedDailyCovidOverviews[i];
                    if (i != 0)
                    {
                        DailyCovidOverview previous = retrievedDailyCovidOverviews[i - 1];
                        current.DailyNewConfirmed = current.TotalConfirmed - previous.TotalConfirmed;
                        current.DailyNewDeaths = current.TotalDeaths - previous.TotalDeaths;
                        current.DailyNewRecovered = current.TotalRecovered - previous.TotalRecovered;
                    }
                    else
                    {
                        current.DailyNewConfirmed = current.TotalConfirmed;
                        current.DailyNewDeaths = current.TotalDeaths;
                        current.DailyNewRecovered = current.TotalRecovered;
                    }

                    context.DailyCovidOverviews.Add(current);
                }

                context.SaveChanges();

            }
        }
    }
}
