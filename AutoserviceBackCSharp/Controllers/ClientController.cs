using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;
using AutoserviceBackCSharp.Validation;
using AutoserviceBackCSharp.Validation.ClientView;
using AutoserviceBackCSharp.Validation.CustomError;

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
            var clientModelValidator = new ClientModelValidator();
            var result = clientModelValidator.Validate(new ClientViewModel(name, phone, email));

            if (!result.IsValid)
            {
                var OutputErrors = new List<ErrorDefault>();
                result.Errors.ToList().ForEach(error => OutputErrors.Add(new ErrorDefault(error.PropertyName, error.ErrorMessage)));

                return BadRequest(new { errors = OutputErrors });
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
            
            var clientModelValidator = new ClientModelValidator();
            var result = clientModelValidator.Validate(new ClientViewModel(name, phone, email));

            if (!result.IsValid)
            {
                var OutputErrors = new List<ErrorDefault>();
                result.Errors.ToList().ForEach(error => OutputErrors.Add(new ErrorDefault(error.PropertyName, error.ErrorMessage)));

                return BadRequest(new { errors = OutputErrors });
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
