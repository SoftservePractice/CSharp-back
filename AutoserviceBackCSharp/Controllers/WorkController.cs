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

        [HttpGet("~/[controller]/{id}")]
        public Work GetWork(int id)
        {
            return _context.Works.SingleOrDefault(work => work.Id == id)!;
        }

        [HttpPost]
        public Work PostWork(int detail, float detailPrice, int order, float workPrice)
        {
            var newWork = new Work() { Detail = detail, DetailPrice = detailPrice, WorkPrice = workPrice, Order = order };
            _context.Works.Add(newWork);
            _context.SaveChanges();
            return newWork;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateWork(int id, int detail, float detailPrice, float workPrice, int order)
        {
            var updWork = _context.Works.SingleOrDefault(work => work.Id == id);
            updWork.Detail = detail;
            updWork.DetailPrice = detailPrice;
            updWork.WorkPrice = workPrice;
            updWork.Order = order;
            _context.SaveChanges();
            return true;
        }

        [HttpDelete("~/[controller]/{id}")]
        public bool DeleteWork(int id)
        {
            _context.Remove(new Work() { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}
