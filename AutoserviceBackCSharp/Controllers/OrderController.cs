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
        [HttpGet("{id}")]
        public Order GetOrder(int id)
        {
            return _context.Orders.SingleOrDefault(order => order.Id == id)!;
        }

        [HttpPost]
        public Order PostOrder(int clientId, int? technician, DateTime start, DateTime? end, int? finalPrice, int car, int carMieleage, DateTime appointmentTime)
        {
            var newOrder = new Order() { 
                Client = clientId, 
                Technician = technician, 
                Start = DateOnly.FromDateTime(start), 
                FinalPrice = finalPrice, 
                Car = car,
                CarMileage= carMieleage, 
                AppointmentTime= DateOnly.FromDateTime(appointmentTime) 
            };
            if(end.HasValue)
            {
                newOrder.End = DateOnly.FromDateTime(end.Value);
            }
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        [HttpPatch("{id}")]
        public bool UpdateOrder(int id, int? clientId, int? technician, DateTime? start, DateTime? end, int? finalPrice, int? car, int? carMieleage, DateTime? appointmentTime)
        {
            var updOrder = _context.Orders.SingleOrDefault(order => order.Id == id);
            if(updOrder != null)
            {
                updOrder.Client = clientId ?? updOrder.Client;
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

        [HttpDelete("{id}")]
        public bool DeleteOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(order => order.Id == id);

            if (order != null)
            {
                _context.Feedbacks.Where(fd => fd.Order == id).ToList().ForEach(fd => _context.Remove(fd));
                _context.Works.Where(wr => wr.Order == id).ToList().ForEach(wr => _context.Remove(wr));
                _context.Remove(order);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
