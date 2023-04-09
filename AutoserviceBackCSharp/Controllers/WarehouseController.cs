using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using AutoserviceBackCSharp.Validation;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DisableCors]

    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly PracticedbContext _context;

        public WarehouseController(ILogger<CarController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Warehouse> GetWarehouses()
        {
            return _context.Warehouses;
        }

        [HttpGet("{id}")]
        public Warehouse GetWarehouse(int id)
        {
            return _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id)!;
        }

        [HttpPost]
        public ActionResult<Warehouse> PostWarehouse(string adress, string name)
        {
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((name == null || name.Length == 0) || (adress == null || adress.Length == 0))
            {
                return BadRequest(
                    new { message = "Имя и адресс склада не могут быть пустыми" }
                    );
            }
            if (validator.IsValid(adress) == false)
            {
                return BadRequest("Склад техника не может содержать такие символы");
            }
            if (!validator.IsValid(adress))
            {
                return BadRequest("Address can only contain letters, numbers, spaces and commas");
            }
            var newWarehouse = new Warehouse() { Address = adress, Name = name};
            _context.Warehouses.Add(newWarehouse);
            _context.SaveChanges();
            return newWarehouse;
        }

        [HttpPatch("{id}")]
        public ActionResult<Warehouse> UpdateWarehouse(int id, string? adress, string? name)
        {
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((name == null || name.Length == 0) || (adress == null || adress.Length == 0))
            {
                return BadRequest(
                    new { message = "Имя и адресс склада не могут быть пустыми"}
                    );
            }
            if (validator.IsValid(adress) == false)
            {
                return BadRequest("Склад техника не может содержать такие символы");
            }
            if (!validator.IsValid(adress))
            {
                return BadRequest("Address can only contain letters, numbers, spaces and commas");
            }
            var updWarehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);
            if(updWarehouse != null)
            {
                updWarehouse.Address = adress ?? updWarehouse.Address;
                updWarehouse.Name = name ?? updWarehouse.Name;
                _context.SaveChanges();
                return Ok(new { updWarehouse = updWarehouse, message = "Склад успешно обновлен" }); ;
            }
            return NotFound(new { message = "Склад не найден" });
        }

        [HttpDelete("{id}")]
        public bool DeleteWarehouse(int id)
        {
            var warehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);

            if (warehouse != null)
            {
                _context.Remove(warehouse);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
