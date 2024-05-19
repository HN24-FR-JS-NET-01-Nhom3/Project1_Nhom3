using LotteryChecker.EmailService.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace LotteryChecker.EmailService.Services;

public class EmailSender : IEmailSender
{
	private readonly EmailConfig _emailConfig;

	public EmailSender(EmailConfig emailConfig)
	{
		_emailConfig = emailConfig;
	}

	public void SendEmail(Message message)
	{
		var emailMessage = CreateEmailMessage(message);

		Send(emailMessage);
	}
	
	public async Task SendEmailAsync(Message message)
	{
		var mailMessage = CreateEmailMessage(message);

		await SendAsync(mailMessage);
	}

	private MimeMessage CreateEmailMessage(Message message)
	{
		var emailMessage = new MimeMessage();
		emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
		emailMessage.To.AddRange(message.To);
		emailMessage.Subject = message.Subject;
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

		return emailMessage;
	}

	private void Send(MimeMessage mailMessage)
	{
		using (var client = new SmtpClient())
		{
			try
			{
				client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

				client.Send(mailMessage);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			finally
			{
				client.Disconnect(true);
			}
		}
	}
	
	private async Task SendAsync(MimeMessage mailMessage)
	{
		using (var client = new SmtpClient())
		{
			try
			{
				await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

				await client.SendAsync(mailMessage);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			finally
			{
				await client.DisconnectAsync(true);
			}
		}
	}
}