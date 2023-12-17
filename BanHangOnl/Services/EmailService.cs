using System.Net.Mail;
using System.Net;

namespace BanHangOnl.Services
{
    public interface IEmailSender
    {
        void sendEmail(string to, string subject, string content);
    }

    public class EmailSender : IEmailSender
    {
		private readonly string _from = "tranletienhoa@gmail.com";

		public void sendEmail(string to, string subject, string content)
        {
			MailMessage mm = new(_from, to);
			mm.Subject = subject;
			mm.Body = content;
			mm.IsBodyHtml = true;

			GetSmtpClient().Send(mm);
		}

        private SmtpClient GetSmtpClient()
        {
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;
			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential(_from, "bizv duyt wvmb vbix");

			return smtp;
		}
    }
}
