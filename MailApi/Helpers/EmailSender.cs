using System.Net.Mail;
using System.Net;
using MailApi.Models;

namespace MailApi.Helpers
{
    public class EmailSender
    {
        public void Email(EmailModel emailMessage) {
            MailAddress fromAddress = new MailAddress("sckarch@hotmail.com", "Medical Service Premium");
            MailAddress toAddress = new MailAddress(emailMessage.Email, $"To {emailMessage.Name}");
            const string fromPassword = "Marley2487";
            
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = emailMessage.Subject,
                Body = emailMessage.Body
            })

        {
                smtp.Send(message);
            }
        }
        
    }
}
