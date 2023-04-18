using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly PracticedbContext _context;

        public CarController(ILogger<CarController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            var cars = _context.Cars.ToArray();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(car => car.Id == id)!;
            if (car == null)
            {
                return NotFound(new { message = "���������� �� ������" });
            }
            return Ok(car);
        }

        [HttpPost]
        public ActionResult PostCar(string mark, DateTime year, string vin, string carNumber, int clientId)
        {
            if(mark.Length < 3 || mark.Length > 30)
            {
                return BadRequest("����� ������ ������������");
            }
            if (vin.Length < 3 || vin.Length > 30)
            {
                return BadRequest("Vin ��� ������ ������������");
            }
            if (carNumber.Length < 3 || carNumber.Length > 20)
            {
                return BadRequest("����� ������ ������������");
            }
            if (DateOnly.FromDateTime(year).Year < 1900 || DateOnly.FromDateTime(year).Year > DateTime.Now.Year)
            {
                return BadRequest("��� ������������ ������ ������������");
            }
            if (clientId != 0 && (_context.Clients.SingleOrDefault(client => client.Id == clientId) == null))
            {
                return NotFound(new { message = "������������ � ����� ID ���" });
            }

            var car = new Car()
            {
                Mark = mark,
                Year = DateOnly.FromDateTime(year),
                Vin = vin,
                CarNumber = carNumber,
                Client = clientId
            };

            _context.Cars.Add(car);
            _context.SaveChanges();

            return CreatedAtAction(nameof(PostCar), new { car = car, message = "���������� ������� ������" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Car> UpdateCar(int id, string? mark, DateTime? year, string? vin, string? carNumber, int? clientId)
        {

            if (mark!=null && (mark.Length < 3 || mark.Length > 30))
            {
                return BadRequest("����� ������ ������������");
            }
            if (vin != null && (vin.Length < 3 || vin.Length > 30))
            {
                return BadRequest("Vin ��� ������ ������������");
            }
            if (carNumber != null && (carNumber.Length < 3 || carNumber.Length > 20))
            {
                return BadRequest("����� ������ ������������");
            }
            if (year != null && (DateOnly.FromDateTime(year.Value).Year < 1900 || DateOnly.FromDateTime(year.Value).Year > DateTime.Now.Year))
            {
                return BadRequest("��� ������������ ������ ������������");
            }
            if (clientId != null && (_context.Clients.Where(client => client.Id == clientId).SingleOrDefault() == null))
            {
                return NotFound(new { message = "������������ � ����� ID ���" });
            }

            var car = _context.Cars.SingleOrDefault(car => car.Id == id);
            if (car != null)
            {
                car.Mark = mark ?? car.Mark;
                car.Vin = vin ?? car.Vin;
                car.CarNumber = carNumber ?? car.CarNumber;
                car.Client = clientId ?? car.Client;
                
                if (year.HasValue)
                {
                    car.Year = DateOnly.FromDateTime(year.Value);
                }
                _context.SaveChanges();
                return Ok(new { car = car, message = "���������� ������� ��������" });
            }
            return NotFound(new { message = "���������� �� ������" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(car => car.Id == id);

            if (car != null)
            {
                _context.Remove(car);
                _context.SaveChanges();
                return Ok(new { message = "���������� ������� ������" });
            }

            return NotFound(new { message = "���������� �� ������" });
        }
    }
}