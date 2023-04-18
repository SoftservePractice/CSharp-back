﻿using AutoserviceBackCSharp.Models;
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
                && (order == null || feedbacks.Order == order))!;
            return Ok(feedbacks);
        }
        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedback(int id)
        {
            var feedback = _context.Feedbacks.SingleOrDefault(feedback => feedback.Id == id);
            if (feedback == null)
                return NotFound(new { message = "Отзывов пока что нет" });
            
            return Ok(feedback);
        }

        [HttpPost]
        public ActionResult<Feedback> PostFeedback(int client, string? content, int order)
        {
            if (client < 0)
                return BadRequest("ID клиента не может быть меньше 0");

            if (content != null && (content.Length < 3 || content.Length > 300))
                return BadRequest("Отзыв должен быть в диапазоне от 3 до 300 символов");

            if(order < 0)
                return BadRequest("Номер заказа не может быть меньше 0");

            var feedback = new Feedback() { Client = client, Content = content, Order = order };
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return Ok(feedback);
        }

        [HttpPatch("{id}")]
        public ActionResult<Feedback> UpdateFeedback(int id, int? client, string? content, int? order, bool? rating)
        {
            if (client != null && client < 0)
                return BadRequest("ID клиента не может быть меньше 0");

            if (content != null && (content.Length < 3 || content.Length > 300))
                return BadRequest("Отзыв должен быть в диапазоне от 3 до 300 символов");

            if (order != null && order < 0)
                return BadRequest("Номер заказа не может быть ниже 0");

            if (rating != null && (rating == false || rating == true))
                return BadRequest("Оценка должна быть только false или true");

            var updFeedback = _context.Feedbacks.SingleOrDefault(fb => fb.Id == id);
            if (updFeedback != null)
            {
                updFeedback.Client = client ?? updFeedback.Client;
                updFeedback.Content = content ?? updFeedback.Content;
                updFeedback.Order = order ?? updFeedback.Order;
                updFeedback.Rating = rating ?? updFeedback.Rating;
                _context.SaveChanges();
                return updFeedback;
            }
            return NotFound(new { message = "Отзыв не найден" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.SingleOrDefault(feedback => feedback.Id == id);
            if (feedback != null)
            {
                _context.Remove(feedback);
                _context.SaveChanges();
                return Ok(new { message = "Отзыв успешно удален" });
            }
           return NotFound(new { message = "Отзыв не найден" });
        }
    }
}