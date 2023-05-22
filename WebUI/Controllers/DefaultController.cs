using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

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
        public IActionResult Index(int? page)
        {
            using (var c = new Context())
            {
                int pageSize = 3; // Sayfa başına görüntülenecek data sayısı
                int pageNumber = (page ?? 1); // Sayfa numarası, varsayılan olarak 1

                var query = c.Blogs.Include(x => x.Category).Include(y => y.Comments).Where(z => z.BlogStatus == true);
                var pagedResult = query.ToPagedList(pageNumber, pageSize);

                return View(pagedResult);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var values = _blogService.TGetBlogByIdWithCategory(id);
            ViewBag.Id = id;
            return View(values);
        }
    }
}
