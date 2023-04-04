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
            return _context.Orders.SingleOrDefault(order => order.Id == id);
        }

        [HttpPost]
        public Order PostOrder(int client, int technician, DateOnly start, DateOnly end, int finalPrice, int car, int carMieleage, DateOnly appointmentTime)
        {
            var newOrder = new Order() { Client = client, Technician = technician, Start = start, End = end, FinalPrice = finalPrice, Car = car,CarMileage= carMieleage, AppointmentTime=appointmentTime};
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        [HttpPatch]
        public bool UpdateOrder(int id, int client, int technician, DateOnly start, DateOnly end, int finalPrice, int car, int carMieleage, DateOnly appointmentTime)
        {
            var updOrder = _context.Orders.SingleOrDefault(order => order.Id == id);
            updOrder.Client = client;
            updOrder.Technician = technician;
            updOrder.Start = start;
            updOrder.End = end;
            updOrder.FinalPrice = finalPrice;
            updOrder.Car = car;
            updOrder.CarMileage = carMieleage;
            updOrder.AppointmentTime = appointmentTime;
            _context.SaveChanges();
            return true;
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
