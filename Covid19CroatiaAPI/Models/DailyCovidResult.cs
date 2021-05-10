using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Models
{
    /// <summary>
    /// Contains daily Covid data that is returned to the client.
    /// </summary>
    public class DailyCovidResult
    {
        public DateTime Date { get; set; }

        public int TotalConfirmed { get; set; }

        public int TotalRecovered { get; set; }

        public int TotalDeaths { get; set; }

        public int DailyNewConfirmed { get; set; }

        public int DailyNewRecovered { get; set; }

        public int DailyNewDeaths { get; set; }

        public int Active { get; set; }
    }
}
