using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkListController : ControllerBase
    {
        private readonly ILogger<WorkListController> _logger;
        private readonly PracticedbContext _context;

        public WorkListController(ILogger<WorkListController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WorkList> GetWorkLists(string? description, float? price, float? duration)
        {
            return _context.WorkLists.Where(
                workList =>
                    (description == null || workList.Description == description)
                    && (price == null || workList.Price == price)
                    && (duration == null || workList.Duration == duration)
            )!;
        }

        [HttpGet("~/[controller]/{id}")]
        public WorkList GetWorkList(int id)
        {
            return _context.WorkLists.SingleOrDefault(workList => workList.Id == id);
        }

        [HttpPost]
        public WorkList PostWorkList(string name, string description, float price, float duration)
        {
	    var workList = new WorkList() { Name = name, Description = description, Price = price, Duration = duration };
            _context.WorkLists.Add(workList);
            _context.SaveChanges();
            return workList;
        }

        [HttpPatch("~/[controller]/{id}")]
        public bool UpdateWorkList(int id, string? name, string? description, float? price, float? duration)
        {
            var updWorkList = _context.WorkLists.SingleOrDefault(wl => wl.Id == id);
	    ifupdWorkList != null)
            {
            	updWorkList.Name = name ?? workList.Name;
            	updWorkList.Description = description ?? workList.Description;
            	updWorkList.Price = price ?? workList.Price;
            	updWorkList.Duration = duration ?? workList.Duration;
            	_context.SaveChanges();

            	return true;
	      }
	     return false;
        }

        [HttpDelete("~/[controller]/{id}")]
        public bool DeleteWorkList(int id)
        {
            _context.Remove(new WorkList { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}
