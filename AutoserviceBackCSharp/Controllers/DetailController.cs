using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DetailController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly PracticedbContext _context;

        public DetailController(ILogger<OrderController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Detail> GetDetails(int? catId)
        {
            return _context.Details.Where(
                detail =>
                    (catId == null || detail.Category == catId)
            )!;
        }
        [HttpGet("{id}")]
        public Detail GetDetail(int id)
        {
            return _context.Details.SingleOrDefault(order => order.Id == id)!;
        }

        [HttpPost]
        public Detail PostDetail(string model, string vendorCode, string? description, string compatibleVehicles)
        {
            var newDetail = new Detail() { 
                Model = model, 
                VendorCode = vendorCode, 
                Description = description ?? "", 
                CompatibleVehicles = compatibleVehicles
            };
            _context.Details.Add(newDetail);
            _context.SaveChanges();
            return newDetail;
        }

        [HttpPatch("{id}")]
        public bool UpdateDetail(int id, string? model, string? vendorCode, string? description, string? compatibleVehicles)
        {
            var updDetail = _context.Details.SingleOrDefault(detail => detail.Id == id);
            if(updDetail != null)
            {
                updDetail.Model = model ?? updDetail.Model;
                updDetail.VendorCode = vendorCode ?? updDetail.VendorCode;
                updDetail.Description = description ?? updDetail.Description;
                updDetail.CompatibleVehicles = compatibleVehicles ?? updDetail.CompatibleVehicles;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("{id}")]
        public bool DeleteDetail(int id)
        {
            var detail = _context.Details.SingleOrDefault(detail => detail.Id == id);

            if (detail != null)
            {
                _context.Remove(detail);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
