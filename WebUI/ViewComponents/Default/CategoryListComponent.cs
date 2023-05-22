using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.Default
{
    public class CategoryListComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryListComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _categoryService.TGetList();
            return View(values);
        }
    }
}
