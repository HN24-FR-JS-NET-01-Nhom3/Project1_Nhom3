using LotteryChecker.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace LotteryChecker.Common.Models.ViewModels
{
    public class MultipleLotteryNumbersVm
    {
        [Required(ErrorMessage = "Lottery numbers are required")]
        [LotteryNumberValidation(ErrorMessage = "Each lottery number must be from 1 to 6 digits or multiple numbers separated by commas.")]
        public string LotteryNumbers { get; set; }
        public DateTime DrawDate { get; set; }
    }
}
