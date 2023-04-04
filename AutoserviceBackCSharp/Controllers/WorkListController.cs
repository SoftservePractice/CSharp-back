using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkListController : ControllerBase
    {
        private readonly ILogger<WorkListController> _logger;
        private readonly MyDbContext _context;

        public WorkListController(ILogger<WorkListController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WorkList> GetWorkLists()
        {
            return _context.WorkLists.ToList();
        }

        [HttpGet("{id}")]
        public WorkList GetWorkList(uint id)
        {
            return _context.WorkLists.SingleOrDefault(workList => workList.Id == id);
        }

        [HttpPost]
        public WorkList PostWorkList([FromBody] WorkList workList)
        {
            _context.WorkLists.Add(workList);
            _context.SaveChanges();
            return workList;
        }

        [HttpPut("{id}")]
        public bool UpdateWorkList(uint id, [FromBody] WorkList workList)
        {
            var updWorkList = _context.WorkLists.SingleOrDefault(wl => wl.Id == id);
            updWorkList.Name = workList.Name;
            updWorkList.Description = workList.Description;
            updWorkList.Price = workList.Price;
            updWorkList.Duration = workList.Duration;
            _context.SaveChanges();
            return true;
        }

        [HttpDelete("{id}")]
        public bool DeleteWorkList(uint id)
        {
            _context.WorkLists.Remove(new WorkList { Id = id });
            _context.SaveChanges();
            return true;
        }
    }
}
