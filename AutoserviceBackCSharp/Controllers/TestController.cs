using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]// /Test/GetWorkLists
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly PracticedbContext _context;

        public TestController(ILogger<TestController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWorkLists")]
        public IEnumerable<WorkList> GetWorkLists()
        {
            return _context.WorkLists;
        }

        [HttpGet(Name = "GetCars")]
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars;
        }
    }
}