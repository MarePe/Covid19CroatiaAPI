using Covid19CroatiaAPI.Entities;
using Covid19CroatiaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Helpers
{
    public class DailyCovidDataMapper
    {
        public static DailyCovidResult Map(DailyCovidOverview entity)
        {
            DailyCovidResult result = new DailyCovidResult();

            result.Date = entity.Date;
            result.TotalConfirmed = entity.TotalConfirmed;
            result.TotalDeaths = entity.TotalDeaths;
            result.TotalRecovered = entity.TotalRecovered;
            result.DailyNewConfirmed = entity.DailyNewConfirmed;
            result.DailyNewDeaths = entity.DailyNewDeaths;
            result.DailyNewRecovered = entity.DailyNewRecovered;
            result.Active = result.TotalConfirmed - result.TotalDeaths - result.TotalRecovered;

            return result;

        }

        public static List<DailyCovidResult> Map(IEnumerable<DailyCovidOverview> entities)
        {
            List<DailyCovidResult> results = new List<DailyCovidResult>();

            foreach (var entity in entities)
            {
                results.Add(Map(entity));
            }

            return results;
        }


    }
}
