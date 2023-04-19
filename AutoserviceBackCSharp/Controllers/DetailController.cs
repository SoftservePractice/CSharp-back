using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DetailController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public DetailController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Detail>> GetDetails(int? catId)
        {
            var details = _context.Details.Where(
                detail =>
                    (catId == null || detail.Category == catId)
            )!.ToArray();

            return Ok(details);
        }

        [HttpGet("{id}")]
        public ActionResult<Detail> GetDetail(int id)
        {
            var detail = _context.Details.SingleOrDefault(detail => detail.Id == id)!;

            if (detail == null)
            {
                return NotFound(new { message = "Detail не найден" });
            }

            return Ok(detail);
        }

        [HttpPost]
        public ActionResult PostDetail(string model, string vendorCode, string description, string compatibleVehicles, int catId)
        {
            if (model.Length < 3 || model.Length > 32)
            {
                return BadRequest("Модель некорректная");
            }

            if (vendorCode.Length < 3 || vendorCode.Length > 32)
            {
                return BadRequest("Vendor код некорректный");
            }

            if (description.Length < 3 || description.Length > 300)
            {
                return BadRequest("Описание некорректное");
            }

            if (compatibleVehicles.Length < 3 || compatibleVehicles.Length > 300)
            {
                return BadRequest("Совместимые автомобили некорректны");
            }

            var detail = new Detail() { 
                Model = model, 
                VendorCode = vendorCode, 
                Description = description, 
                CompatibleVehicles = compatibleVehicles,
                Category = catId
            };

            _context.Details.Add(detail);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostDetail), new { detail = detail, message = "Detail успешно создан" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Detail> UpdateDetail(int id, string? model, string? vendorCode, string? description, string? compatibleVehicles, int? catId)
        {
            if (model != null && (model.Length < 3 || model.Length > 32))
            {
                return BadRequest("Модель некорректная");
            }

            if (vendorCode != null && (vendorCode.Length < 3 || vendorCode.Length > 32))
            {
                return BadRequest("Vendor код некорректный");
            }

            if (description != null && (description.Length < 3 || description.Length > 300))
            {
                return BadRequest("Описание некорректное");
            }

            if (compatibleVehicles != null && (compatibleVehicles.Length < 3 || compatibleVehicles.Length > 300))
            {
                return BadRequest("Совместимые автомобили некорректны");
            }

            var detail = _context.Details.SingleOrDefault(detail => detail.Id == id);

            if(detail != null)
            {
                detail.Model = model ?? detail.Model;
                detail.VendorCode = vendorCode ?? detail.VendorCode;
                detail.Description = description ?? detail.Description;
                detail.CompatibleVehicles = compatibleVehicles ?? detail.CompatibleVehicles;
                detail.Category = catId ?? detail.Category;
                _context.SaveChanges();
                return Ok(new { message = "Detail успешно обновлены" });
            }

            return NotFound(new { message = "Detail не найдены" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDetail(int id)
        {
            var detail = _context.Details.SingleOrDefault(detail => detail.Id == id);

            if (detail != null)
            {
                detail.Works.ToList().ForEach(x => _context.Remove(x));
                detail.DetailLists.ToList().ForEach(x => _context.Remove(x));
                _context.Remove(detail);
                _context.SaveChanges();
                return Ok(new { message = "Detail успешно удален" });
            }

            return NotFound(new { message = "Detail не найден" });
        }
    }
}
