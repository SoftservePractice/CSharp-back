using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkListController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public WorkListController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WorkList> GetWorkLists(string? name, string? description, float? price, float? duration)
        {
            return _context.WorkLists.Where(
                workList =>
                    (name == null || workList.Name == name)
                    && (description == null || workList.Description == description)
                    && (price == null || workList.Price == price)
                    && (duration == null || workList.Duration == duration)
            )!.ToArray();
        }

        [HttpGet("{id}")]
        public WorkList GetWorkList(int id)
        {
            return _context.WorkLists.SingleOrDefault(workList => workList.Id == id)!;
        }

        [HttpPost]
        public ActionResult<WorkList> PostWorkList(string name, string description, float price, float duration)
        {
            if (name.Length < 3 || name.Length > 32)
            {
                return BadRequest("Имя должно содержать от 3 до 32 букв");
            }

            if (description.Length < 3 || description.Length > 300)
            {
                return BadRequest("Описание от 3 до 300 символов");
            }

            if (price < 0)
            {
                return BadRequest("Цена не может быть меньше 0");
            }

            if (duration < 0)
            {
                return BadRequest("Длительность процесса не может быть меньше 0");
            }

            var workList = new WorkList() { Name = name, Description = description, Price = price, Duration = duration };
            _context.WorkLists.Add(workList);
            _context.SaveChanges();

            return workList;
        }

        [HttpPatch("{id}")]
        public ActionResult<WorkList> UpdateWorkList(int id, string? name, string? description, float? price, float? duration)
        {
            if (name != null && (name.Length < 3 || name.Length > 32))
            {
                return BadRequest("Имя должно содержать от 3 до 32 букв");
            }

            if (description != null && (description.Length < 3 || description.Length > 300))
            {
                return BadRequest("Описание от 3 до 300 символов");
            }

            if (price != null && price < 0)
            {
                return BadRequest("Цена не может быть меньше 0");
            }

            if (duration != null && duration < 0)
            {
                return BadRequest("Длительность процесса не может быть меньше 0");
            }

            var updWorkList = _context.WorkLists.SingleOrDefault(wl => wl.Id == id);

            if (updWorkList != null)
            {
                updWorkList.Name = name ?? updWorkList.Name;
                updWorkList.Description = description ?? updWorkList.Description;
                updWorkList.Price = price ?? updWorkList.Price;
                updWorkList.Duration = duration ?? updWorkList.Duration;
                _context.SaveChanges();
                return updWorkList;
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWorkList(int id)
        {
            var worklist = _context.WorkLists.SingleOrDefault(worklist => worklist.Id == id);

            if (worklist != null)
            {
                worklist.Works.ToList().ForEach(x => _context.Remove(x));
                _context.Remove(worklist);
                _context.SaveChanges();

                return Ok(new { message = "WorkList успешно ликвидирован" });
            }

            return NotFound(new { message = "WorkList не найден" }); ;
        }
    }
}
