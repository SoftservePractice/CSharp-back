using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public DetailList PostDetailList(int warehouseId, int detailId, int count)
        {
            var newDetailList = new DetailList() { Warehouse = warehouseId, Detail = detailId, Count = count };
            _context.DetailLists.Add(newDetailList);
            _context.SaveChanges();
            return newDetailList;
        }

        [HttpPatch("{id}")]
        public bool UpdateDetailList(int id, int warehouseId, int detailId, int count)
        {
            var updDetailList = _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id);
            if (updDetailList != null)
            {
                updDetailList.Warehouse = warehouseId;
                updDetailList.Detail = detailId;
                updDetailList.Count = count;
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
