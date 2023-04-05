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
        public IEnumerable<Car> GetCars(int? clientId)
        {
            if(clientId.HasValue)
            {
                return _context.Cars.Where(car => car.Client == clientId.Value);
            }
            return _context.Cars;
        }

        [HttpGet("~/[controller]/{id}")]
        public Car GetCar(int id)
        {
            return _context.Cars.SingleOrDefault(car => car.Id == id)!;
        }

        [HttpPost]
        public Car PostCar(string mark, DateTime year, string vin, string carNumber, int clientId)
        {
            var newCar = new Car() { Mark = mark, Year = DateOnly.FromDateTime(year), Vin = vin, CarNumber = carNumber, Client = clientId };
            _context.Cars.Add(newCar);
            _context.SaveChanges();
            return newCar;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateCar(int id, string? mark, DateTime? year, string? vin, string? carNumber, int? clientId)
        {
            var updCar = _context.Cars.SingleOrDefault(car => car.Id == id);
            if(updCar != null)
            {
                updCar.Mark = mark ?? updCar.Mark;
                updCar.Vin = vin ?? updCar.Vin;
                updCar.CarNumber = carNumber ?? updCar.CarNumber;
                updCar.Client = clientId ?? updCar.Client;
                if(year.HasValue)
                {
                    updCar.Year = DateOnly.FromDateTime(year.Value);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("~/[controller]/{id}")]
        public bool DeleteCar(int id)
        {
            _context.Remove(new Car() { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}