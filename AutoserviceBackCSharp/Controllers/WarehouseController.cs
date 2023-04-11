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
        public ActionResult<Warehouse> GetWarehouse(int id)
        {
            var warehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id)!;
            if (warehouse == null)
            {
                return NotFound(new { message = "Склад не найден" });
            }
            return Ok(warehouse);
        }

        [HttpPost]
        public ActionResult<Warehouse> PostWarehouse(string adress, string name)
        {
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if (name == null)
            {
                return BadRequest("Имя склада не может быть пустым");
            }
            if (adress == null)
            {
                return BadRequest("Адресс склада не может быть пустым");
            }
            if (name.Length > 32 || name.Length < 3)
            {
                return BadRequest("Имя склада не может быть такой длинны");
            }
            if (adress.Length > 32 || adress.Length < 3)
            {
                return BadRequest("Адресс склада не может быть такой длинны");
            }
            if (validator.IsValid(adress) == false)
            {
                return BadRequest("Склад не может содержать такие символы");
            }
            
            var newWarehouse = new Warehouse() { Address = adress, Name = name};
            _context.Warehouses.Add(newWarehouse);
            _context.SaveChanges();
            return newWarehouse;
        }

        [HttpPatch("{id}")]
        public ActionResult<Warehouse> UpdateWarehouse(int id, string adress, string name)
        {
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if (name == null)
            {
                return BadRequest("Имя склада не может быть пустым");
            }
            if (adress == null)
            {
                return BadRequest("Адресс склада не может быть пустым");
            }
            if (name.Length > 32 || name.Length < 3)
            {
                return BadRequest("Имя склада не может быть такой длинны");
            }
            if (adress.Length > 32 || adress.Length < 3)
            {
                return BadRequest("Адресс склада не может быть такой длинны");
            }
            if (validator.IsValid(adress) == false)
            {
                return BadRequest("Склад не может содержать такие символы");
            }

            var updWarehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);
            if(updWarehouse != null)
            {
                updWarehouse.Address = adress ?? updWarehouse.Address;
                updWarehouse.Name = name ?? updWarehouse.Name;
                _context.SaveChanges();
                return Ok(new { updWarehouse = updWarehouse, message = "Склад успешно обновлен" }); ;
            }
            return BadRequest("Склад не найден");
        }

        [HttpDelete("{id}")]
        public ActionResult<Warehouse> DeleteWarehouse(int id)
        {
            var warehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);

            if (warehouse != null)
            {
                _context.Remove(warehouse);
                _context.SaveChanges();
                return Ok(new { message = "Склад успешно удален" });
            }

            return NotFound(new { message = "Склад не найден" });
        }
    }
}
