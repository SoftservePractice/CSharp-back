using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Validation;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public ClientController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients(string? name, string? phone, string? email, string? telegramId)
        {
            var clients = _context.Clients.Where(
                client =>
                    (telegramId == null || client.TelegramId == telegramId)
                    && (name == null || client.Name == name)
                    && (phone == null || client.Phone == phone)
                    && (email == null || client.Email == email)
            )!.ToArray();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);

            if (client == null)
            {
                return NotFound(new { message = "Client has not found" });
            }

            return Ok(client);
        }

        [HttpPost]
        public ActionResult PostClient(string? name, string? phone, string? email, string? telegramId)
        {
            UserFieldsValidator uservalidator = new UserFieldsValidator();

            if (!uservalidator.ValidatePhone(phone))
            {
                return BadRequest("Invalid phone number field");
            }

            if (!uservalidator.ValidateEmail(email))
            {
                return BadRequest("Invalid email field");
            }

            var client = new Client()
            {
                Name = name ?? null,
                Phone = phone ?? null,
                Email = email ?? null,
                TelegramId = telegramId ?? null,
                IsConfirm = false
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostClient), new { client = client, message = "Client has been successfully created" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Client> UpdateClient(int id, string? name, string? phone, string? email, string? telegramId, bool? isConfirm)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);
            UserFieldsValidator uservalidator = new UserFieldsValidator();

            if(!uservalidator.ValidatePhone(phone)){
                return BadRequest("Invalid phone number field");
            }

            if (!uservalidator.ValidateEmail(email))
            {
                return BadRequest("Invalid email field");
            }

            if (client != null)
            {
                client.Name = name ?? client.Name;
                client.Phone = phone ?? client.Phone;
                client.Email = email ?? client.Email;
                client.TelegramId = telegramId ?? client.TelegramId;
                client.IsConfirm = isConfirm ?? client.IsConfirm;
                _context.SaveChanges();
                return Ok(new { client = client, message = "Client has been successfully updated" });
            }

            return NotFound(new { message = "Client has not found" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);

            if (client != null)
            {
                client.Orders.ToList().ForEach(x => _context.Remove(x));
                client.Cars.ToList().ForEach(x => _context.Remove(x));
                client.Feedbacks.ToList().ForEach(x => _context.Remove(x));
                _context.Remove(client);
                _context.SaveChanges();
                return Ok(new { message = "Client has been successfully deleted" });
            }

            return NotFound(new { message = "Client has not found" });
        }
    }
}
