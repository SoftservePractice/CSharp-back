using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailListController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public DetailListController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<DetailList> GetDetailLists(int? warId)
        {
            return Ok(_context.DetailLists.Where(
                detailList =>
                warId == null || detailList.Warehouse == warId)!.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<DetailList> GetDetailList(int id)
        {
            return Ok(_context.DetailLists.SingleOrDefault(detailList => detailList.Id == id)!);
        }

        [HttpPost]
        public ActionResult<DetailList> PostDetailList(int warehouseId, int detailId, int? count)
        {
            if (warehouseId < 0)
            {
                return BadRequest("Warehouse ID can't be less than 0");
            }

            if (detailId < 0)
            {
                return BadRequest("Detail ID can't be less than 0");
            }

            if (count != null && count < 0)
            {
                return BadRequest("Detail count can't be less than 0");
            }

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
            {
                return BadRequest("Warehouse ID can't be less than 0");
            }

            if (detailId != null && detailId < 0)
            {
                return BadRequest("Detail ID can't be less than 0");
            }

            if (count != null && count < 0)
            {
                return BadRequest("Detail count can't be less than 0");
            }

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
        public ActionResult DeleteDetailList(int id)
        {
            var detailList = _context.DetailLists.SingleOrDefault(detailList => detailList.Id == id);

            if (detailList != null)
            {
                _context.Remove(detailList);
                _context.SaveChanges();
                return Ok(new { message = "DetailList successfully deleted" });
            }

            return NotFound(new { message = "DetailList not found" });
        }
    }
}