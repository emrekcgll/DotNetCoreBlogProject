using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICategoryService _categoryService;

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
            var allBlogList = _blogService.TGetList();
            var pendingBlogList = _blogService.TGetBlogListWithCategoryByPendingApproval();
            ViewBag.AllBlogCount = allBlogList.Count();
            ViewBag.BlogCount = blogList.Count();
            ViewBag.Name = values.Name + " " + values.Surname;
            ViewBag.Role = await _userManager.GetRolesAsync(values);
            return View(pendingBlogList);
        }

        [HttpGet]
        public IActionResult BlogList()
        {
            var values = _blogService.TGetBlogListWithCategory();
            return View(values);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PendingApprovalBlogList()
        {
            var values = _blogService.TGetBlogListWithCategoryByPendingApproval();
            if (values.Count() == 0)
            {
                ViewBag.Message = "Onay bekleyen blog bulunamadı..";
            }
            return View(values);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult GetPendingBlogCount()
        {
            var count = _blogService.TGetBlogListWithCategoryByPendingApproval().Count();
            return Json(count);
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
            p.BlogStatus = false;
            p.AppUserId = values.Id;
            // BlogImage'i kaydetmek için dosyayı alın
            var file = HttpContext.Request.Form.Files.GetFile("BlogImage");
            // Dosya adı oluşturun
            var fileName = Path.GetFileName(file.FileName);
            // Dosya adı benzersiz olsun diye, GUID kullanarak bir dosya adı oluşturun
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            // Dosyayı kaydetmek için hedef klasörü belirleyin
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            // Dosyayı kaydedin
            var filePath = Path.Combine(uploadFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            // BlogImage'in dosya adını Blog nesnesine atayın
            p.BlogImage = uniqueFileName;
            _blogService.TAdd(p);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBlog(int id)
        {
            var cat = _categoryService.TGetList();
            var catList = cat.Select(x => new SelectListItem
            {
                Value = x.CategoryID.ToString(),
                Text = x.CategoryName
            });
            ViewBag.CategoryList = catList;

            var values = _blogService.TGetBlogByIdWithCategory(id);
            return View(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBlog(Blog p)
        {
            _blogService.TUpdate(p);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBlog(int id)
        {
            var values = _blogService.TGetById(id);

            var imagePath = "wwwroot/images/" + values.BlogImage;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _blogService.TDelete(values);
            return RedirectToAction("BlogList", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory(Category p)
        {
            p.CategoryStatus = true;
            _categoryService.TAdd(p);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryList()
        {
            var values = _categoryService.TGetList();
            return View(values);
        }
    }
}
