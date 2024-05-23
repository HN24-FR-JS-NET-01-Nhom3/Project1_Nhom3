using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Validation;
public class LotteryNumberValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IEnumerable<string> lotteryNumber)
        {
            foreach (var number in lotteryNumber)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(number, @"^\d{6}$"))
                {
                    return new ValidationResult("Each lottery number must contains exact 6 digits.");
                }
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid lottery number format.");
    }
}
