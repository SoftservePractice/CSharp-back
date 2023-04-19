using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;


namespace AutoserviceBackCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly PracticedbContext _context;

        public CategoryController(PracticedbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            return _context.Categories.ToArray();
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

            if (name != null && (name.Length > 32 || name.Length < 3))
            {
                return BadRequest("Имя категории не может быть такой длинны");
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        [HttpPatch("~/[controller]/{id}")]
        public ActionResult<Category> UpdateCategory(int id, string? name, int? parentCategory)
        {
            var updCategory = _context.Categories.SingleOrDefault(updCategory => updCategory.Id == id);

            if (name != null && (name.Length > 32 || name.Length < 3))
            {
                return BadRequest("Имя категории не может быть такой длинны");
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
                category.Details.ToList().ForEach(x => _context.Remove(x));
                category.InverseParentCategoryNavigation.ToList().ForEach(x => _context.Remove(x));
                _context.Remove(category);
                _context.SaveChanges();
                return Ok(new { message = "Категория успешна удалена" });
            }

            return NotFound(new { message = "Категория не найдена" });
        }
    }
}