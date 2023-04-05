using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly PracticedbContext _context;
        public OrderController(ILogger<OrderController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders;
        }

        [HttpGet("~/[controller]/{id}")]
        public Order GetOrder(int id)
        {
            return _context.Orders.SingleOrDefault(order => order.Id == id)!;
        }

        [HttpPost]
        public Order PostOrder(int client, int technician, DateTime start, DateTime end, int finalPrice, int car, int carMieleage, DateTime appointmentTime)
        {
            var newOrder = new Order() { Client = client, Technician = technician, Start = DateOnly.FromDateTime(start), End = DateOnly.FromDateTime(end), FinalPrice = finalPrice, Car = car,CarMileage= carMieleage, AppointmentTime= DateOnly.FromDateTime(appointmentTime) };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateOrder(int id, int? client, int? technician, DateTime? start, DateTime? end, int? finalPrice, int? car, int? carMieleage, DateTime? appointmentTime)
        {
            var updOrder = _context.Orders.SingleOrDefault(order => order.Id == id);
            if(updOrder != null)
            {
                updOrder.Client = client ?? updOrder.Client;
                updOrder.Technician = technician ?? updOrder.Technician;
                updOrder.FinalPrice = finalPrice ?? updOrder.FinalPrice;
                updOrder.Car = car ?? updOrder.Car;
                updOrder.CarMileage = carMieleage ?? updOrder.CarMileage;
                if (start.HasValue)
                {
                    updOrder.Start = DateOnly.FromDateTime(start.Value);
                }
                if (end.HasValue)
                {
                    updOrder.End = DateOnly.FromDateTime(end.Value);
                }
                if(appointmentTime.HasValue)
                {
                    updOrder.AppointmentTime = DateOnly.FromDateTime(appointmentTime.Value);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("~/[controller]/{id}")]
        public bool DeleteOrder(int id)
        {
            _context.Remove(new Order() { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}
