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

        [HttpGet("{id}")]
        public ActionResult<Work> GetWork(int id)
        {
            var work = _context.Works.SingleOrDefault(work => work.Id == id)!;
            if (work == null)
            {
                return NotFound(new { message = "Work не найден" });
            }
            return Ok(work);
        }

        [HttpPost]
        public ActionResult PostWork(int detail, float detailPrice, int order, float workPrice)
        {

            if (detail < 0)
            {
                return BadRequest("Detail не может быть меньше 0");
            }

            if (detailPrice < 0)
            {
                return BadRequest("DetailPrice не может быть меньше 0");
            }

            if (order < 0)
            {
                return BadRequest("order не может быть меньше 0");
            }

            if (workPrice < 0)
            {
                return BadRequest("WorkPrice не может быть меньше 0");
            }

            var newWork = new Work() { Detail = detail, DetailPrice = detailPrice, WorkPrice = workPrice, Order = order };
            _context.Works.Add(newWork);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostWork), new { newWork = newWork, message = "Work успешно созданa" }); ;
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateWork(int id, int? detail, float? detailPrice, float? workPrice, int? order)
        {

            if (detail!= null && detail < 0)
            {
                return BadRequest("Detail не может быть меньше 0");
            }

            if (detailPrice != null && detailPrice < 0)
            {
                return BadRequest("DetailPrice не может быть меньше 0");
            }

            if (order != null && order < 0)
            {
                return BadRequest("order не может быть меньше 0");
            }

            if (workPrice != null && workPrice < 0)
            {
                return BadRequest("WorkPrice не может быть меньше 0");
            }
            var updWork = _context.Works.SingleOrDefault(work => work.Id == id);
            if(updWork != null)
            {
                updWork.Detail = detail ?? updWork.Detail;
                updWork.DetailPrice = detailPrice ?? updWork.DetailPrice;
                updWork.WorkPrice = workPrice ?? updWork.WorkPrice;
                updWork.Order = order ?? updWork.Order;
                _context.SaveChanges();
                return Ok(new { updWork = updWork, message = "Техник успешно обновлен" }); 
            }
            return NotFound(new { message = "Техник не найден" }); 
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWork(int id)
        {
            var work = _context.Works.SingleOrDefault(work => work.Id == id);

            if (work != null)
            {
                _context.Remove(work);
                _context.SaveChanges();
                return Ok(new { message = "WOrk успешно ликвидирован" }); 
            }

            return NotFound(new { message = "Work не найден" }); ;
        }
    }
}
