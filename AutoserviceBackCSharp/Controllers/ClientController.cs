using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly PracticedbContext _context;

        public ClientController(ILogger<ClientController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClients(string? name,string? phone,string? email,string? telegramId)
        {
            var clients = _context.Clients.Where(
                client =>
                    (name == null || client.Name == name)
                    && (phone == null || client.Phone == phone)
                    && (email == null || client.Email == email)
                    && (telegramId == null || client.TelegramId == telegramId)
            )!;

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);
            if(client == null) 
            {
                return NotFound(new {message = "Пользователь не найден"});
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult PostClient(string? name,string? phone,string? email,string? telegramId)
        {
            if ((name == null || name.Length == 0) || (phone == null || phone.Length == 0))
            {
                return BadRequest(
                    new { message = "Имя или номер пользователя не могут быть пустыми" }
                );
            }

            var client = new Client()
            {
                Name = name ?? "",
                Phone = phone ?? "",
                Email = email ?? null,
                TelegramId = telegramId ?? null,
                IsConfirm = false
            };
            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(PostClient), new {client = client, message = "Пользователь успешно создан"});
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateClient(int id,string? name,string? phone,string? email,string? telegramId,bool? isConfirm)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);

            if (client != null)
            {
                client.Name = name ?? client.Name;
                client.Phone = phone ?? client.Phone;
                client.Email = email ?? client.Email;
                client.TelegramId = telegramId ?? client.TelegramId;
                client.IsConfirm = isConfirm ?? client.IsConfirm;
                _context.SaveChanges();
                return Ok(new {client = client, message = "Пользователь успешно обновлен"});
            }

            return NotFound(new {message = "Пользователь не найден"});
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);

            if (client != null)
            {
                _context.Remove(client);
                _context.SaveChanges();
                return Ok(new { message = "Пользователь успешно удален" });
            }

            return NotFound(new {message = "Пользователь не найден"});
        }
    }
}
