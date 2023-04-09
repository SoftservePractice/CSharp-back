using AutoserviceBackCSharp.Models; 
using Microsoft.AspNetCore.Cors; 
using Microsoft.AspNetCore.Mvc; 
using System.Xml.Linq; 
using AutoserviceBackCSharp.Validation; 
using System.Text.RegularExpressions; 
 
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
        public ActionResult<DetailList> PostDetailList(int warehouseId, int detailId, int? count) 
        { 
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' }); 
 
            if (warehouseId <= 0  detailId <= 0  count == null) 
            { 
                return BadRequest(new { message = "ID склада, ID детали или кол-во деталей не могут быть пустыми" }); 
            } 
 
            if (!Regex.IsMatch(warehouseId.ToString(), "^[0-9]+$")) 
            { 
                return BadRequest(new { message = "ID склада не может содержать буквы" }); 
            } 
 
            if (!validator.IsValid(detailId.ToString())) 
            { 
                return BadRequest(new { message = "ID детали не может содержать такие символы" }); 
            } 
 
            if (count.Value <= 0  !Regex.IsMatch(count.Value.ToString(), "^[0-9]+$")  !validator.IsValid(count.Value.ToString())) 
            { 
                return BadRequest(new { message = "Кол-во деталей должно быть больше 0 и не содержать буквы или запрещенные символы" }); 
            } 
 
            var newDetailList = new DetailList { Warehouse = warehouseId, Detail = detailId, Count = count.Value }; 
            _context.DetailLists.Add(newDetailList); 
            _context.SaveChanges(); 
            return newDetailList; 
        } 
 
        [HttpPatch("{id}")] 
        public ActionResult<DetailList> UpdateDetailList(int id, int? warehouseId, int? detailId, int? count) 
        { 
            var validator = new SymbolValidator(new char[] { '%', '$', '@', '!', '%', '^', '`' }); 
            if (warehouseId == 0  detailId == 0  count == null) 
            { 
                return BadRequest(new { message = "ID склада, ID детали или кол-во деталей не могут быть пустыми" }); 
            } 
 
            if (!Regex.IsMatch(warehouseId.ToString(), "^[0-9]+$")) 
            { 
                return BadRequest("ID склада не может содержать буквы"); 
            } 
            if (!validator.IsValid(warehouseId.ToString()) || !validator.IsValid(detailId.ToString())) 
            { 
                return BadRequest("ID детали не может содержать такие символы"); 
            } 
 
            if (!Regex.IsMatch(count.ToString(), "^[0-9]+$")) 
            { 
                return BadRequest("Кол-во деталей не может содержать буквы"); 
            } 
            if (!validator.IsValid(count.Value.ToString())) 
            { 
                return BadRequest("Кол-во деталей не может содержать такие символы"); 
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