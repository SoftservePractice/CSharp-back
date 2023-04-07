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
        public IEnumerable<Client> GetClients(string? name, string? phone, string? email, string? telegramId)
        {
            return _context.Clients.Where(
                client =>
                    (name == null || client.Name == name)
                    && (phone == null || client.Phone == phone)
                    && (email == null || client.Email == email)
                    && (telegramId == null || client.TelegramId == telegramId)
            )!;
        }

        [HttpGet("{id}")]
        public Client GetClient(int id)
        {
            return _context.Clients.SingleOrDefault(client => client.Id == id)!;
        }

        [HttpPost]
        public Client PostClient(string? name, string? phone, string? email, string? telegramId)
        {
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
            return client;
        }

        [HttpPatch("{id}")]
        public bool UpdateClient(int id, string? name, string? phone, string? email, string? telegramId, bool? isConfirm)
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
                return true;
            }

            return false;
        }

        [HttpDelete("{id}")]
        public bool DeleteClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(client => client.Id == id);

            if (client != null)
            {
                _context.Remove(client);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
