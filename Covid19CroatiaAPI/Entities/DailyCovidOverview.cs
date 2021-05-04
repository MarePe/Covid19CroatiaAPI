using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Entities
{
    public class DailyCovidOverview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [JsonPropertyName("Confirmed")]
        public int TotalConfirmed { get; set; }

        [JsonPropertyName("Recovered")]
        public int TotalRecovered { get; set; }

        [JsonPropertyName("Deaths")]
        public int TotalDeaths { get; set; }

        public int DailyNewConfirmed { get; set; }

        public int DailyNewRecovered { get; set; }

        public int DailyNewDeaths { get; set; }

    }
}
