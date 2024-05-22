using LotteryChecker.EmailService.Entities;

namespace LotteryChecker.EmailService.Services;

public interface IEmailSender
{
	void SendEmail(Message message);
	Task SendEmailAsync(Message message);
}