using Covid19CroatiaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Helpers
{
    public static class DailyCovidOverviewHelpers
    {
        /// <summary>
        /// Checks if TotalConfirmed, TotalRecovered, TotalDeaths and Dates are equal for two records.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool CheckTotals(DailyCovidOverview x, DailyCovidOverview y)
        {
            if (x.TotalConfirmed == y.TotalConfirmed
                && x.TotalDeaths == y.TotalDeaths
                && x.TotalRecovered == y.TotalRecovered
                && x.Date == y.Date) return true;
            else return false;
        }

        /// <summary>
        /// Creates new entity object from data for two consecutive days.
        /// </summary>
        /// <param name="dayD"></param>
        /// <param name="dayBeforeD"></param>
        /// <returns></returns>
        public static DailyCovidOverview PrepareNewRecord(DailyCovidOverview dayD, DailyCovidOverview dayBeforeD)
        {
            DailyCovidOverview recordToBePersisted = new DailyCovidOverview();

            recordToBePersisted.Date = dayD.Date;
            recordToBePersisted.TotalConfirmed = dayD.TotalConfirmed;
            recordToBePersisted.TotalDeaths = dayD.TotalDeaths;
            recordToBePersisted.TotalRecovered = dayD.TotalRecovered;

            recordToBePersisted.DailyNewConfirmed = dayD.TotalConfirmed - dayBeforeD.TotalConfirmed;
            recordToBePersisted.DailyNewDeaths = dayD.TotalDeaths - dayBeforeD.TotalDeaths;
            recordToBePersisted.DailyNewRecovered = dayD.TotalRecovered - dayBeforeD.TotalRecovered;

            return recordToBePersisted;

        }


        /// <summary>
        /// Creates new entity object for the first day of pandemic.
        /// </summary>
        /// <param name="dayD"></param>
        /// <returns></returns>
        public static DailyCovidOverview PrepareNewFirstRecord(DailyCovidOverview dayD)
        {
            DailyCovidOverview recordToBePersisted = new DailyCovidOverview();

            recordToBePersisted.Date = dayD.Date;
            recordToBePersisted.TotalConfirmed = dayD.TotalConfirmed;
            recordToBePersisted.TotalDeaths = dayD.TotalDeaths;
            recordToBePersisted.TotalRecovered = dayD.TotalRecovered;

            recordToBePersisted.DailyNewConfirmed = dayD.TotalConfirmed;
            recordToBePersisted.DailyNewDeaths = dayD.TotalDeaths;
            recordToBePersisted.DailyNewRecovered = dayD.TotalRecovered;

            return recordToBePersisted;

        }

        /// <summary>
        /// Updates current entity object from data for two consecutive days. Perserves Id.
        /// </summary>
        /// <param name="internalRecord"></param>
        /// <param name="dayD"></param>
        /// <param name="dayBeforeD"></param>
        /// <returns></returns>
        public static DailyCovidOverview UpdateEntityObject(this DailyCovidOverview internalRecord, DailyCovidOverview dayD, DailyCovidOverview dayBeforeD)
        {
            internalRecord.Date = dayD.Date;
            internalRecord.TotalDeaths = dayD.TotalDeaths;
            internalRecord.TotalConfirmed = dayD.TotalConfirmed;
            internalRecord.TotalRecovered = dayD.TotalRecovered;
            internalRecord.DailyNewConfirmed = dayD.TotalConfirmed - dayBeforeD.TotalConfirmed;
            internalRecord.DailyNewDeaths = dayD.TotalDeaths - dayBeforeD.TotalDeaths;
            internalRecord.DailyNewRecovered = dayD.TotalRecovered - dayBeforeD.TotalRecovered;

            return internalRecord;
        }
    }
}
