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
        public IActionResult Index(int? page, string? searchQuery)
        {
            using (var c = new Context())
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    int pageSize = 3; // Sayfa başına görüntülenecek data sayısı
                    int pageNumber = (page ?? 1); // Sayfa numarası, varsayılan olarak 1

                    var query = c.Blogs.Include(x => x.Category).Include(y => y.Comments).Include(z => z.AppUser).Where(p => p.BlogStatus == true).OrderByDescending(o => o.BlogID);
                    var pagedResult = query.ToPagedList(pageNumber, pageSize);

                    return View(pagedResult);
                }
                else
                {
                    int pageSize = 3; // Sayfa başına görüntülenecek data sayısı
                    int pageNumber = (page ?? 1); // Sayfa numarası, varsayılan olarak 1

                    var query = c.Blogs.Include(x => x.Category).Include(y => y.Comments).Include(z => z.AppUser).Where(b => b.BlogStatus == true && b.BlogTitle.Contains(searchQuery)).OrderByDescending(o => o.BlogID);
                    var pagedResult = query.ToPagedList(pageNumber, pageSize);

                    return View(pagedResult);
                }
            }
        }

        public IActionResult FilterByCategory(int? page, int id, string? searchQuery)
        {
            using (var c = new Context())
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    int pageSize = 3; // Sayfa başına görüntülenecek data sayısı
                    int pageNumber = (page ?? 1);

                    var query = c.Blogs.Include(x => x.Category).Include(y => y.Comments).Include(z => z.AppUser).Where(x => x.CategoryID == id && x.BlogStatus == true).OrderByDescending(o => o.BlogID);
                    var pagedResult = query.ToPagedList(pageNumber, pageSize);
                    return View(pagedResult);
                }
                else
                {
                    int pageSize = 3; // Sayfa başına görüntülenecek data sayısı
                    int pageNumber = (page ?? 1); // Sayfa numarası, varsayılan olarak 1

                    var query = c.Blogs.Include(x => x.Category).Include(y => y.Comments).Include(z => z.AppUser).Where(b => b.CategoryID == id && b.BlogStatus == true && b.BlogTitle.Contains(searchQuery)).OrderByDescending(o => o.BlogID);
                    var pagedResult = query.ToPagedList(pageNumber, pageSize);

                    return View(pagedResult);
                }
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var values = _blogService.TGetBlogByIdWithCategory(id);
            ViewBag.Id = id;
            return View(values);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
