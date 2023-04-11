using AutoserviceBackCSharp.Models;
using AutoserviceBackCSharp.Validation;
using Microsoft.AspNetCore.Mvc;


namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly PracticedbContext _context;

        public CategoryController(ILogger<CategoryController> logger, PracticedbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory(string? name, int? parentCategory)
        {
            return _context.Categories;
        }

        [HttpGet("~/[controller]/{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(category => category.Id == id)!;
            if (category == null)
            {
                return BadRequest("Категория не найдена");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> PostCategory(string name, int? parentCategory)
        {
            var category = new Category() { Name = name, ParentCategory = parentCategory };
            if (name == null)
            {
                return BadRequest("Имя категории не может быть пустым");
            }
            if (!name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя категории может содержать только буквы");
            }
            if (name.Length > 32 || name.Length < 3)
            {
                return BadRequest("Имя категории не может быть такой длинны");
            }
            if (parentCategory == null)
            {
                return BadRequest("Родитель категории не может быть пустым");
            }
            

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        [HttpPatch("~/[controller]/{id}")]
        public ActionResult<Category> UpdateCategory(int id, string? name, int? parentCategory)
        {
            var updCategory = _context.Categories.SingleOrDefault(updCategory => updCategory.Id == id);
            if (name == null)
            {
                return BadRequest("Имя техника не может быть пустым");
            }
            if (!name.All(x => char.IsLetter(x)))
            {
                return BadRequest("Имя техника может содержать только буквы");
            }
            if (name.Length > 32 || name.Length < 3)
            {
                return BadRequest("Имя техника не может быть такой длинны");
            }
            if (parentCategory == null)
            {
                return BadRequest("Родитель категории не может быть пустым");
            }
            


            if (updCategory != null)
            {
                    updCategory.Name = name ?? updCategory.Name;
                    updCategory.ParentCategory = parentCategory ?? updCategory.ParentCategory;
                    _context.SaveChanges();
                    return Ok(new { updCategory = updCategory, message = "Категория успешно обновлена" });
            }
                return BadRequest("Категория не найдена");

            
        }
        [HttpDelete("~/[controller]/{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(category => category.Id == id);

            if (category != null)
            {
                 _context.Remove(category);
                 _context.SaveChanges();
                 return Ok(new { message = "Категория успешна удалена" });
            }
                return NotFound(new { message = "Категория не найдена" });
        }
        

    }
}