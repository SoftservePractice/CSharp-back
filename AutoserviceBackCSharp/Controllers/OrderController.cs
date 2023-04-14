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
        public ActionResult<IEnumerable<Order>> GetOrders(int? clientId)
        {
            var orders = _context.Orders.Where(
                order =>
                    (clientId == null || order.Client == clientId)
            )!;

            return Ok(orders);
        }
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order =  _context.Orders.SingleOrDefault(order => order.Id == id);
            if (order == null)
            {
                return NotFound(new { message = "Заказ не найден" });
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> PostOrder(int? clientId, int? technician, DateTime? start, DateTime? end, int? finalPrice, int? car, int? carMieleage, DateTime? appointmentTime)
        {
            var client =_context.Clients.FirstOrDefault(client => client.Id == clientId) ?? null;
            if(client == null)
            {
                return BadRequest(new { message = "Пользователь с таким id не найден" });
            }
            else
            {
                if(finalPrice != null && finalPrice > 10000000){
                    return BadRequest(new { message = "Некорректное значение цены" });
                }
                if(carMieleage != null && carMieleage > 1000000000000){
                    return BadRequest(new { message = "Некорректное значение километража машины" });
                }

                var newOrder = new Order() { 
                    Client = clientId, 
                    Technician = technician ?? null,
                    FinalPrice = finalPrice ?? null, 
                    Car = car ?? null,
                    CarMileage = carMieleage ?? null
                };
                if(start.HasValue)
                {
                    newOrder.Start = DateOnly.FromDateTime(start.Value);
                }
                if(end.HasValue)
                {
                    newOrder.End = DateOnly.FromDateTime(end.Value);
                }
                if(appointmentTime.HasValue)
                {
                    newOrder.AppointmentTime = DateOnly.FromDateTime(appointmentTime.Value);
                }
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return Ok(newOrder);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult<Order> UpdateOrder(int id, int? clientId, int? technician, DateTime? start, DateTime? end, int? finalPrice, int? car, int? carMieleage, DateTime? appointmentTime)
        {
            var client =_context.Clients.FirstOrDefault(client => client.Id == clientId) ?? null;
            if(client == null)
            {
                return BadRequest(new { message = "Пользователь с таким id не найден" });
            }
            else
            {
                if(finalPrice != null && finalPrice > 10000000){
                    return BadRequest(new { message = "Ну слишком чет большая сумма)" });
                }
                if(carMieleage != null && carMieleage > 1000000000000){
                    return BadRequest(new { message = "Такую машину можно на свалку" });
                }

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
                    else
                    {
                        updOrder.AppointmentTime = DateOnly.MinValue;
                    }
                    _context.SaveChanges();
                    return Ok(new { order = updOrder, message = "Заказ успешно обновлен" });
                }
                return NotFound(new { message = "Заказ не найден" });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(order => order.Id == id);

            if (order != null)
            {
                _context.Feedbacks.Where(fd => fd.Order == id).ToList().ForEach(fd => _context.Remove(fd));
                _context.Works.Where(wr => wr.Order == id).ToList().ForEach(wr => _context.Remove(wr));
                _context.Remove(order);
                _context.SaveChanges();
                return Ok(new { message = "Заказ успешно удален" });
            }

            return NotFound(new { message = "Заказ не найден" });
        }
    }
}
