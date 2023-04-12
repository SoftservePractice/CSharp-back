using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailListController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly PracticedbContext _context;

        public DetailListController(ILogger<CarController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IEnumerable<DetailList> GetDetailLists(int? warId)
        {
            return _context.DetailLists.Where(
                detailList =>
                (warId == null || detailList.Warehouse == warId))!;
        }

        [HttpGet("{id}")]
        public DetailList GetDetailList(int id)
        {
            return _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id)!;
        }

        [HttpPost]
        public ActionResult<DetailList> PostDetailList(int warehouseId, int detailId, int? count)
        {
            if (warehouseId < 0)
                return BadRequest("ID склада не может быть меньше 0");

            if (detailId < 0)
                return BadRequest("ID детали не может быть меньше 0");

            if (count != null && count < 0)
                return BadRequest("Кол-во деталей не может быть меньше 0");

            var newDetailList = new DetailList { Warehouse = warehouseId, Detail = detailId };
            if (count.HasValue)
            {
                newDetailList.Count = count.Value;
            }
            _context.DetailLists.Add(newDetailList);
            _context.SaveChanges();
            return newDetailList;
        }

        [HttpPatch("{id}")]
        public ActionResult<DetailList> UpdateDetailList(int id, int? warehouseId, int? detailId, int? count)
        {
            if (warehouseId != null && warehouseId < 0)
                return BadRequest("ID склада не может быть меньше 0");

            if (detailId != null && detailId < 0)
                return BadRequest("ID детали не может быть меньше 0");

            if (count != null && count < 0)
                return BadRequest("Кол-во деталей не может быть меньше 0");

            var updDetailList = _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id);
            if (updDetailList != null)
            {
                updDetailList.Warehouse = warehouseId ?? updDetailList.Warehouse;
                updDetailList.Detail = detailId ?? updDetailList.Detail;
                updDetailList.Count = count ?? updDetailList.Count;
                _context.SaveChanges();
                return updDetailList;
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public bool DeleteDetailList(int id)
        {
            var detailList = _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id);

            if (detailList != null)
            {
                _context.Remove(detailList);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}