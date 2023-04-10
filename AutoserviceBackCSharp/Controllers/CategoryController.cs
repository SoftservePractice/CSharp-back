//using AutoserviceBackCSharp.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace AutoserviceBackCSharp.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class CategoryController : ControllerBase
//    {
//        private readonly ILogger<CategoryController> _logger;
//        private readonly PracticedbContext _context;

//        public CategoryController(ILogger<CategoryController> logger, PracticedbContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpGet]
//        public IEnumerable<Category> GetCategory(string? name, int parentCategory)
//        {
//            return _context.Categories.Where(
//                category =>
//                (name == null || category.Name == name)
//                && (parentCategory == null || category.ParentCategory == parentCategory)

//            )!;

//        }

//        [HttpGet("~/[controller]/{id}")]
//        public Category GetCategory(int id)
//        {
//            return _context.Categories.SingleOrDefault(category => category.Id == id)!;
//        }

//        [HttpPost]
//        public Category PostCategory(string? name, int parentCategory)
//        {
//            var category = new Category() { Name = name, ParentCategory = parentCategory };
//            _context.Categories.Add(category);
//            _context.SaveChanges();
//            return category;
//        }

//        [HttpPatch("~/[controller]/{id}")]
//        public bool UpdateCategory(int id, string? name, int? parentCategory)
//        {
//            var updCategory = _context.Categories.SingleOrDefault(updCategory => updCategory.Id == id);

//            if (updCategory != null)
//            {
//                updCategory.Name = name ?? updCategory.Name;
//                updCategory.ParentCategory = parentCategory ?? updCategory.ParentCategory;
//                _context.SaveChanges();
//                return true;
//            }
//            return false;

//        }
//        [HttpDelete("~/[controller]/{id}")]
//        public bool DeleteCategory(int id)
//        {
//            var category = _context.Categories.SingleOrDefault(category => category.Id == id);

//            if (category != null)
//            {
//                _context.Remove(category);
//                _context.SaveChanges();
//                return true;
//            }
//            return false;
//        }

//    }
//}