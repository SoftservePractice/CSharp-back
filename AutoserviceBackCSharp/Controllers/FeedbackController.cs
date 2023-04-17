using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Validation;
using System.Xml.Linq;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly PracticedbContext _context;

        public FeedbackController(ILogger<ClientController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Feedback>> GetFeedbacks(int? client, string? content, int? order)
        {
            var feedbacks = _context.Feedbacks.Where(
                feedbacks =>
                (client == null || feedbacks.Client == client)
                && (content == null || feedbacks.Content == content)
                && (order == null || feedbacks.Order == order)

                )!.ToArray();
            return Ok(feedbacks);
        }
        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedback(int id)
        {
            var feedback = _context.Feedbacks.SingleOrDefault(feedback => feedback.Id == id);
            if (feedback == null)
            {
                return NotFound(new { message = "Обратная связь не найден" });
            }
            return Ok(feedback);

        }
        [HttpPost]
        public ActionResult<Feedback> PostFeedback(int client, string? content, int order, bool? rating)
        {
            var feedback = new Feedback() 
            {
                Client = client,
                Content = content,
                Order = order, 
                Rating = rating 
            };
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return Ok(feedback);
        }

        [HttpPatch("{id}")]
        public ActionResult<Feedback> UpdateFeedback(int id, int? client, string? content, int? order, bool? rating)
        {
            var feedback = _context.Feedbacks.SingleOrDefault(feedback => feedback.Id == id);

            if (feedback != null)
            {
                feedback.Client = client ?? feedback.Client;
                feedback.Content = content ?? feedback.Content;
                feedback.Order = order ?? feedback.Order;
                feedback.Rating = rating ?? feedback.Rating;
                _context.SaveChanges();
                return Ok(feedback);

            }
            return NotFound(new { message = "Обратная связь не найден" });

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.SingleOrDefault(feedback => feedback.Id == id);
            if (feedback != null)
            {

                _context.Remove(feedback);
                _context.SaveChanges();
                return Ok(new { message = "Обратная связь успешно удален" });
            }


            return NotFound(new { message = "Обратная связь не найден" });
        }

    }
}