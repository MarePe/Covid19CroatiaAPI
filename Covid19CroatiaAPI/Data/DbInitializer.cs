using Covid19CroatiaAPI.Entities;
using Covid19CroatiaAPI.Helpers;
using Covid19CroatiaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Data
{
    /// <summary>
    /// Performs initial seeding of the local database with the data that is fetched from the remote server.
    /// </summary>
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
                    DailyCovidOverview recordToBeAdded;
                    if (i > 0)
                    {
                        recordToBeAdded = DailyCovidOverviewHelpers.PrepareNewRecord(retrievedDailyCovidOverviews[i], retrievedDailyCovidOverviews[i - 1]);
                    }
                    else
                    {
                        recordToBeAdded = DailyCovidOverviewHelpers.PrepareNewFirstRecord(retrievedDailyCovidOverviews[i]);
                    }

                    context.DailyCovidOverviews.Add(recordToBeAdded);
                }


                context.SaveChanges();

            }
        }
    }
}
