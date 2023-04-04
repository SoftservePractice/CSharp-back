using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WorkController : ControllerBase
    {

        private readonly ILogger<WorkController> _logger;
        private readonly PracticedbContext _context;
        public WorkController(ILogger<WorkController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Work> GetWorks()
        {
            return _context.Works;
        }
        //[HttpGet("~/{id}")]
        [HttpGet("~/[controller]/{id}")]
        public Work GetWork(int id)
        {
            return _context.Works.SingleOrDefault(work => work.Id == id);
        }

        [HttpPost]
        public Work PostWork(int Detail, float DetailPrice, float WorkPrice, int Order, Detail DetailNavigation,Order OrderNavigation)
        {
            var newWork = new Work() { Detail = Detail, DetailPrice = DetailPrice, WorkPrice = WorkPrice, Order = Order, DetailNavigation = DetailNavigation,OrderNavigation=OrderNavigation };
            _context.Works.Add(newWork);
            _context.SaveChanges();
            return newWork;
        }

        [HttpPatch]
        public bool UpdateWork(int id,int Detail, float DetailPrice, float WorkPrice, int Order, Detail DetailNavigation, Order OrderNavigation)
        {
            var updWork = _context.Works.SingleOrDefault(work => work.Id == id);
            updWork.Detail = Detail;
            updWork.DetailPrice = DetailPrice;
            updWork.WorkPrice = WorkPrice;
            updWork.Order = Order;
            updWork.DetailNavigation = DetailNavigation;
            updWork.OrderNavigation = OrderNavigation;
            _context.SaveChanges();
            return true;
        }

        [HttpDelete]
        public bool DeleteWork(int id)
        {
            _context.Remove(new Work() { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}
