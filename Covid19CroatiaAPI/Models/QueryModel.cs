using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19CroatiaAPI.Models
{
    public class QueryModel : IValidatableObject
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        public int? MinConfirmed { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        public int? MaxConfirmed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            
            if (From != null && To != null)
            {
                if (To < From) validationResults.Add(new ValidationResult("To date cannot be earlier than From date."));
            }

            if (MinConfirmed != null && MaxConfirmed != null)
            {
                if (MaxConfirmed < MinConfirmed) validationResults.Add(new ValidationResult("Max number of confirmed cases cannot be less " +
                    "than min number of confirmed cases."));
            }

            return validationResults;
        }
    }
}
