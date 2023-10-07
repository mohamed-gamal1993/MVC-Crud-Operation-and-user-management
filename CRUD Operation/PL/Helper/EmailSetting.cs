using DAL.Entites;
using System.Net;
using System.Net.Mail;

namespace PL.Helper
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.sendgrid.net", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("apikey", "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");
			client.Send("mgamalabdaal@fcih1.com",email.To,email.Title,email.Body);
		}
	}
}
