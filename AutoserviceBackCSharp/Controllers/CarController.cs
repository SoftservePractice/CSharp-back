using AutoserviceBackCSharp.Models;
using AutoserviceBackCSharp.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly PracticedbContext _context;
        private readonly CarFieldsValidator carValidator;

        public CarController(PracticedbContext context)
        {
            _context = context;
            carValidator = new CarFieldsValidator();
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
            var car = _context.Cars.FirstOrDefault(car => car.Id == id)!;

            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult PostCar(string mark, DateTime year, string vin, string carNumber, int clientId)
        {
            if (!carValidator.ValidateMark(mark))
            {
                return BadRequest("Car mark incorrect");
            }

            if (!carValidator.ValidateVinCode(vin))
            {
                return BadRequest("Car Vin code incorrect");
            }

            if (!carValidator.ValidateCarNumber(carNumber))
            {
                return BadRequest("Car number incorrect");
            }

            if (!carValidator.ValidateYear(year))
            {
                return BadRequest("Car manufacturing year incorrect");
            }

            if (!carValidator.ValidateClientID(clientId,_context))
            {
                return BadRequest("Client ID incorrect");
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

            return CreatedAtAction(nameof(PostCar), new { car = car, message = "Car created successfully" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Car> UpdateCar(int id, string? mark, DateTime? year, string? vin, string? carNumber, int? clientId)
        {
            if (mark!=null && !carValidator.ValidateMark(mark))
            {
                return BadRequest("Car mark incorrect");
            }

            if (vin != null && !carValidator.ValidateVinCode(vin))
            {
                return BadRequest("Car Vin code incorrect");
            }

            if (carNumber != null && !carValidator.ValidateCarNumber(carNumber))
            {
                return BadRequest("Car number incorrect");
            }

            if (year != null && !carValidator.ValidateYear(year.Value))
            {
                return BadRequest("Car manufacturing year incorrect");
            }

            if (clientId != null && !carValidator.ValidateClientID(clientId.Value, _context))
            {
                return NotFound(new { message = "Client ID incorrect" });
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
                return Ok(new { car = car, message = "Car updated successfully" });
            }

            return NotFound(new { message = "Cae" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(car => car.Id == id);

            if (car != null)
            {
                _context.Remove(car);
                _context.SaveChanges();
                return Ok(new { message = "Car successfully deleted" });
            }

            return NotFound(new { message = "Car not found" });
        }
    }
}