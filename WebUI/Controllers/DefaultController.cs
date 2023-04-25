using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IBlogService _blogService;
        public DefaultController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values=_blogService.TGetBlogListWithCategory();
            return View(values);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var values=_blogService.TGetBlogByIdWithCategory(id);
            ViewBag.Id = id;
            return View(values);
        }
    }
}
