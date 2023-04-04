using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly PracticedbContext _context;

        public TestController(ILogger<TestController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetTest")]
        public IEnumerable<Car> Get()
        {
            return _context.Cars;
        }
    }
}