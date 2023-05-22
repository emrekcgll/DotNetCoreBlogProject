using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.Default
{
    public class WriterLastBlogListComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public WriterLastBlogListComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = _blogService.TGetLastBlogListWithWriter(id);
            return View(values);
        }
    }
}
