using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangfireService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireTestControllercs : ControllerBase
    {
        MailObj mailObj = new MailObj();

        [HttpPost]
        [Route("Welcome")]
        public IActionResult Welcome(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => mailObj.SendEMail(userName));
            return Ok($"Job id : {jobId} is completed");

        }

        [HttpPost]
        [Route("Delayed")]
        public IActionResult Delayed(string userName)
        {
            var jobId = BackgroundJob.Schedule(() => mailObj.SendDelayedEMail(userName),TimeSpan.FromMinutes(1));
            return Ok($"Job id : {jobId} is completed");

        }

        [HttpPost]
        [Route("Invoice")]
        public IActionResult Invoice(string userName)
        {
            RecurringJob.AddOrUpdate(() => mailObj.SendInvoice(userName), Cron.Monthly);
            return Ok("Recurring job on monthly");

        }

        [HttpPost]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => mailObj.Unsubscribe(userName));
            _ = BackgroundJob.ContinueJobWith(jobId, () =>
                Console.WriteLine($"confirmation mail sent with job id {jobId} "));
            return Ok($"Unsubscried");

        }
    }


    public class MailObj
    {
        public string Email { get; set; }
        public string SendEMail(string message)
        {
            return "Mail Sent successfully";
        }

        public string SendDelayedEMail(string message)
        {
            return "Scheduled Mail Sent successfully after 1 min";
        }
        public string SendInvoice(string message)
        {
            return "Invoice generated and sent mail";
        }
        public string Unsubscribe(string message)
        {
            return "unsubscribed successfully";
        }
    }
}
