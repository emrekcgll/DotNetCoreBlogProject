using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService  _categoryService;

        public DashboardController(UserManager<AppUser> userManager, IBlogService blogService, IAppUserService appUserService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _blogService = blogService;
            _appUserService = appUserService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var blogList = _blogService.TGetBlogListWithCategory();
            var userList = _appUserService.TGetList();
            ViewBag.UserCount = userList.Count();
            ViewBag.BlogCount = blogList.Count();
            ViewBag.Name = values.Name + " " + values.Surname;
            return View(blogList);
        }

        [HttpGet]
        public IActionResult BlogList()
        {
            var values = _blogService.TGetBlogListWithCategory();
            return View(values);
        }

        [HttpGet]
        public IActionResult UpdateBlog(int id)
        {
            var values = _blogService.TGetBlogByIdWithCategory(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateBlog(Blog p)
        {
            _blogService.TUpdate(p);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

        public IActionResult DeleteBlog(int id)
        {
            var values = _blogService.TGetById(id);
            _blogService.TDelete(values);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            var cat = _categoryService.TGetList();
            var catList = cat.Select(x => new SelectListItem
            {
                Value = x.CategoryID.ToString(),
                Text = x.CategoryName
            });

            ViewBag.CategoryList = catList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            p.BlogCreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            p.AppUserId = values.Id;

            _blogService.TAdd(p);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

    }
}
