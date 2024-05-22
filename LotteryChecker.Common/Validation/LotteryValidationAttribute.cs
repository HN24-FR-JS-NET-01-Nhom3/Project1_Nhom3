using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Validation;
public class LotteryNumberValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string lotteryNumber)
        {
            var numbers = lotteryNumber.Split(',').Select(n => n.Trim());

            foreach (var number in numbers)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(number, @"^\d{1,6}$"))
                {
                    return new ValidationResult("Each lottery number must be from 1 to 6 digits.");
                }
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid lottery number format.");
    }
}
