//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Net.Mail;
//using MailKit.Net.Smtp;
//using MailKit;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;
//using MimeKit;
//using System;
//using System.Threading.Tasks;
//using MailKit.Net.Smtp;
//using MimeKit;
//using AutoMapper.Internal;
//using Microsoft.Extensions.Options;

//namespace BanHangOnl.Controllers
//{[ApiController]
//    [Route("[controller]")]
//    public class Email : Controller
//    {
//        private readonly EmailSender _emailSender;

//        public Email(EmailSender emailSender)
//        {
//            _emailSender = emailSender;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost("/email")]
//        public async Task<IActionResult> SendEmail(string toEmail)
//        {
//            string subject = "Subject of the Email";
//            string body = "<p>This is the body of the email.</p>";

//            await _emailSender.SendEmailAsync(toEmail, subject, body);

//            ViewBag.Message = "Email sent successfully";
//            return View("Index");
//        }
//    }
//    public class EmailSender
//    {
//        private const string SmtpServer = "smtp.gmail.com";
//        private const int SmtpPort = 587;
//        private const string SmtpUsername = "yourgmail@gmail.com";
//        private const string SmtpPassword = "yourgmailpassword";

//        public async Task SendEmailAsync(string toEmail, string subject, string body)
//        {
//            var email = new MimeMessage();

//            email.From.Add(new MailboxAddress("Sender Name", "sender@email.com"));
//            email.To.Add(new MailboxAddress("Receiver Name", "receiver@email.com"));

//            email.Subject = "Testing out email sending";
//            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
//            {
//                Text = "Hello all the way from the land of C#"
//            };
//            using (var smtp = new SmtpClient())
//            {
//                smtp.Connect("smtp.server.address", SmtpPort, false);

//                // Note: only needed if the SMTP server requires authentication
//                smtp.Authenticate("smtp_username", "smtp_password");

//                smtp.Send(email);
//                smtp.Disconnect(true);
//            }
//        }

//    }
//}
