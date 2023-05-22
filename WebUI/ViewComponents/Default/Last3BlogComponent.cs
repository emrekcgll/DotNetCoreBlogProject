using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.Default
{
    public class Last3BlogComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public Last3BlogComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.TGetLastThreeBlog();
            return View(values);
        }
    }
}
