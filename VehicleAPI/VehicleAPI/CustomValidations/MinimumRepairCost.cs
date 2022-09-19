using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.CustomValidations
{
    public class MinimumRepairAmount : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int repairCost = (int)value;
            IConfiguration _configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            int minimumRepairCost = int.Parse(_configuration["MinimumCost"]);

            if (repairCost >= minimumRepairCost)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Minimum Cost must be greater than {minimumRepairCost}");
        }
    }
}