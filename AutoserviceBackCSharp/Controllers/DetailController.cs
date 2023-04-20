using AutoserviceBackCSharp.Models;
using AutoserviceBackCSharp.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DetailController : ControllerBase
    {
        private readonly PracticedbContext _context;
        private readonly DetailFieldsValidator detailValidator;

        public DetailController(PracticedbContext context)
        {
            _context = context;
            detailValidator = new DetailFieldsValidator();
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
                return NotFound(new { message = "Detail not found" });
            }

            return Ok(detail);
        }

        [HttpPost]
        public ActionResult PostDetail(string model, string vendorCode, string description, string compatibleVehicles, int catId)
        {
            if (!detailValidator.ValidateModel(model))
            {
                return BadRequest("Model incorrect");
            }

            if (!detailValidator.ValidateVendorCode(vendorCode))
            {
                return BadRequest("Vendor code incorrect");
            }

            if (!detailValidator.ValidateDescription(description))
            {
                return BadRequest("Description incorrect");
            }

            if (!detailValidator.ValidateCompatibleVehicles(compatibleVehicles))
            {
                return BadRequest("Compatible Vehicles incorrect");
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
            return CreatedAtAction(nameof(PostDetail), new { detail = detail, message = "Detail created successfully" });
        }

        [HttpPatch("{id}")]
        public ActionResult<Detail> UpdateDetail(int id, string? model, string? vendorCode, string? description, string? compatibleVehicles, int? catId)
        {
            if (model != null && !detailValidator.ValidateModel(model))
            {
                return BadRequest("Model incorrect");
            }

            if (vendorCode != null && !detailValidator.ValidateVendorCode(vendorCode))
            {
                return BadRequest("Vendor code incorrect");
            }

            if (description != null && !detailValidator.ValidateDescription(description))
            {
                return BadRequest("Description incorrect");
            }

            if (compatibleVehicles != null && !detailValidator.ValidateCompatibleVehicles(compatibleVehicles))
            {
                return BadRequest("Compatible Vehicles incorrect");
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
                return Ok(new { message = "Detail updated successfully" });
            }

            return NotFound(new { message = "Detail not found" });
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
                return Ok(new { message = "Detail deleted successfully" });
            }

            return NotFound(new { message = "Detail not found" });
        }
    }
}
