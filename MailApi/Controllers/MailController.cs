using MailApi.Helpers;
using MailApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        [HttpPost(Name = "EmailSender")]
        public void Email(EmailModel emailMessage)
        {
            EmailSender sender = new EmailSender();
            sender.Email(emailMessage);
        }
    }
}