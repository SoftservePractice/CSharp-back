using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WarehouseController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public WarehouseController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Warehouse> GetWarehouses()
        {
            return _context.Warehouses.ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<Warehouse> GetWarehouse(int id)
        {
            var warehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id)!;

            if (warehouse == null)
            {
                return NotFound(new { message = "Warhouse not found" });
            }

            return Ok(warehouse);
        }

        [HttpPost]
        public ActionResult<Warehouse> PostWarehouse(string adress, string name)
        {
            if (name != null && (name.Length > 32 || name.Length < 3))
            {
                return BadRequest("Incorrect name length");
            }

            if (adress != null && (adress.Length > 32 || adress.Length < 3))
            {
                return BadRequest("Incorrect adress length");
            }

            var newWarehouse = new Warehouse() { Address = adress, Name = name};
            _context.Warehouses.Add(newWarehouse);
            _context.SaveChanges();

            return Ok(new { warehouse = newWarehouse, message = "Warehouse updated successfully" }); ;
        }

        [HttpPatch("{id}")]
        public ActionResult<Warehouse> UpdateWarehouse(int id, string adress, string name)
        {
            if (name != null && (name.Length > 32 || name.Length < 3))
            {
                return BadRequest("Incorrect name length");
            }

            if (adress != null && (adress.Length > 32 || adress.Length < 3))
            {
                return BadRequest("Incorrect adress length");
            }

            var updWarehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);

            if(updWarehouse != null)
            {
                updWarehouse.Address = adress ?? updWarehouse.Address;
                updWarehouse.Name = name ?? updWarehouse.Name;
                _context.SaveChanges();
                return Ok(new { warehouse = updWarehouse, message = "Warehouse updated successfully" }); ;
            }

            return BadRequest("Warehouse not found");
        }

        [HttpDelete("{id}")]
        public ActionResult<Warehouse> DeleteWarehouse(int id)
        {
            var warehouse = _context.Warehouses.SingleOrDefault(warehouse => warehouse.Id == id);

            if (warehouse != null)
            {
                warehouse.DetailLists.ToList().ForEach(x => _context.Remove(x));
                _context.Remove(warehouse);
                _context.SaveChanges();
                return Ok(new { message = "Warehouse deleted successfully" });
            }

            return NotFound(new { message = "Warehouse not found" });
        }
    }
}
