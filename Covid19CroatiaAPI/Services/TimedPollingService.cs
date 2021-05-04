using Covid19CroatiaAPI.Data;
using Covid19CroatiaAPI.Entities;
using Covid19CroatiaAPI.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Services
{
    public class TimedPollingService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedPollingService> _logger;
        private readonly IServiceProvider _services;
        private readonly ICovidStatsService _covidStatsService;
        private Timer _timer;

        public TimedPollingService(ILogger<TimedPollingService> logger, IServiceProvider services, ICovidStatsService covidStatsService)
        {
            _logger = logger;
            _services = services;
            _covidStatsService = covidStatsService;
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(TryPollingNewData, null, TimeSpan.FromSeconds(15), TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private async void TryPollingNewData(object state)
        {

            await TryUpdatingDataForDateAsync(DateTime.UtcNow.Date);
            await TryUpdatingDataForDateAsync(DateTime.UtcNow.Date.AddDays(-1));

        }

        // fetches data for dayD and dayBeforeD to get daily new cases as well.
        private async Task TryUpdatingDataForDateAsync(DateTime dateUtc)
        {
            DailyCovidOverview[] dailyDataExternal = await _covidStatsService.GetPocosByDateAsync(dateUtc);

            DailyCovidOverview externalDayD = dailyDataExternal.Where(dailyData => dailyData.Date == dateUtc).FirstOrDefault();
            DailyCovidOverview externalDayBefore = dailyDataExternal.Where(dailyData => dailyData.Date == dateUtc.AddDays(-1)).FirstOrDefault();

            // if there is data available at the external server
            if (externalDayD != null)
            {
                // Scope has to be created to access any scoped service.
                using (IServiceScope scope = _services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    DailyCovidOverview internalDayD = context.DailyCovidOverviews.Where(dailyData => dailyData.Date == dateUtc).FirstOrDefault();
                    // if there is no data for this day in our db, add new database record
                    if (internalDayD == null)
                    {
                        DailyCovidOverview recordToBePersisted = DailyCovidOverviewHelpers.PrepareNewRecord(externalDayD, externalDayBefore);
                        context.DailyCovidOverviews.Add(recordToBePersisted);
                    }

                    // if there is, check if data from our db matches external data. If there are differences, update our db.
                    else if (!DailyCovidOverviewHelpers.CheckTotals(externalDayD, internalDayD))
                    {
                        internalDayD.UpdateEntityObject(externalDayD, externalDayBefore);
                        context.Update(internalDayD);
                    }

                    context.SaveChanges();
                }
            }

        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Polling Service stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

