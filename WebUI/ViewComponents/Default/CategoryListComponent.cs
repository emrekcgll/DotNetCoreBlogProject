using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.Default
{
    public class CategoryListComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryListComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            using (var c = new Context())
            {
                var values = c.Categories.Where(x => x.CategoryStatus == true).ToList();
                return View(values);
            }
        }
    }
}
