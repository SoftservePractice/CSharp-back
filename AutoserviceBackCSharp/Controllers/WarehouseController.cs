﻿using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("~/[controller]/{id}")]
        public Warehouse GetWarehouse(int id)
        {
            return _context.Warehouses.SingleOrDefault(car => car.Id == id)!;
        }

        [HttpPost]
        public Warehouse PostWarehouse(string adress, string name)
        {
            var newWarehouse = new Warehouse() { Address = adress, Name = name};
            _context.Warehouses.Add(newWarehouse);
            _context.SaveChanges();
            return newWarehouse;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateWarehouse(int id, string? adress, string? name)
        {
            var updWarehouse = _context.Warehouses.SingleOrDefault(Warehouse => Warehouse.Id == id);
            if(updWarehouse != null)
            {
                updWarehouse.Address = adress ?? updWarehouse.Address;
                updWarehouse.Name = name ?? updWarehouse.Name;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("~/[controller]/{id}")]
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
