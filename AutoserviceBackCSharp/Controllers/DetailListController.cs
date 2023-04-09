using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using AutoserviceBackCSharp.Validation;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DisableCors]

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
        public DetailList PostDetailList(int warehouseId, int detailId, int? count)
        {

            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((warehouseId == null || warehouseId.Length == 0) || (detailId == null || detailId.Length == 0)||(count==null||count.Length==0))
            {
                return BadRequest(
                    new { message = "ID склада, ID детали или кол-во деталей не могут быть пустыми" }
                );
            }

            if (warehouseId.All(x => char.IsLetter(x)))
            {
                return BadRequest("ID склада не может содержать буквы");
            }
            if (validator.IsValid(warehouseId)==false)
            {
                return BadRequest("ID склада не может содержать такие символы");
            }


            if (detailId.All(x => char.IsLetter(x)))
            {
                return BadRequest("ID склада не может содержать буквы");
            }
            if (validator.IsValid(detailId)==false)
            {
                return BadRequest("ID детали не может содержать такие символы");
            }


            if (count.All(x => char.IsLetter(x)))
            {
                return BadRequest("Кол-во деталей не может содержать буквы");
            }
            if (validator.IsValid(count)==false)
            {
                return BadRequest("Кол-во нне может содержать такие символы");
            }


            var newDetailList = new DetailList() { Warehouse = warehouseId, Detail = detailId, Count = count ?? 0 };
            _context.DetailLists.Add(newDetailList);
            _context.SaveChanges();
            return newDetailList;
        }

        [HttpPatch("{id}")]
        public bool UpdateDetailList(int id, int? warehouseId, int? detailId, int? count)
        {
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' });
            if ((warehouseId == null || warehouseId.Length == 0) || (detailId == null || detailId.Length == 0)||(count==null||count.Length==0))
            {
                return BadRequest(
                    new { message = "ID склада, ID детали или кол-во деталей не могут быть пустыми" }
                );
            }

            if (warehouseId.All(x => char.IsLetter(x)))
            {
                return BadRequest("ID склада не может содержать буквы");
            }
            if (validator.IsValid(warehouseId)==false)
            {
                return BadRequest("ID склада не может содержать такие символы");
            }


            if (detailId.All(x => char.IsLetter(x)))
            {
                return BadRequest("ID склада не может содержать буквы");
            }
            if (validator.IsValid(detailId)==false)
            {
                return BadRequest("ID детали не может содержать такие символы");
            }


            if (count.All(x => char.IsLetter(x)))
            {
                return BadRequest("Кол-во деталей не может содержать буквы");
            }
            if (validator.IsValid(count)==false)
            {
                return BadRequest("Кол-во нне может содержать такие символы");
            }


            var updDetailList = _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id);
            if (updDetailList != null)
            {
                updDetailList.Warehouse = warehouseId ?? updDetailList.Warehouse;
                updDetailList.Detail = detailId ?? updDetailList.Detail;
                updDetailList.Count = count ?? updDetailList.Count;
                _context.SaveChanges();
                return true;
            }
            return false;
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
