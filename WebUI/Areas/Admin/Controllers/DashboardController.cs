﻿using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        public DashboardController(UserManager<AppUser> userManager, IBlogService blogService, IAppUserService appUserService, ICategoryService categoryService, ICommentService commentService = null)
        {
            _userManager = userManager;
            _blogService = blogService;
            _appUserService = appUserService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var blogList = _blogService.TGetBlogListWithCategory();
            var allBlogList = _blogService.TGetList();
            var pendingBlogList = _blogService.TGetBlogListWithCategoryByPendingApproval();

            var todaysBlog = _blogService.TGetTodaysBlogs();
            var notTodaysBlog = _blogService.TGetNotTodaysBlogs();
            int todaysBlogCount = todaysBlog.Count();
            int notTodaysBlogCount = notTodaysBlog.Count();
            double oran = (double)todaysBlogCount / notTodaysBlogCount * 100;
            ViewBag.Oran = (int)oran;

            var todaysComment = _commentService.TGetCommentListToday().Count();
            var notTodaysComment = _commentService.TGetCommentListNotToday().Count();
            double commentOran = (double)todaysComment / notTodaysComment * 100;
            ViewBag.CommentOran = (int)commentOran;

            using (var c = new Context())
            {
                var userCount = c.Users.ToList().Count();
                ViewBag.UserCount = userCount;

                var passiveuserCount = c.Users.Where(x => x.LockoutEnd != null).ToList().Count();
                ViewBag.PassiveUserCount = passiveuserCount;
            }


            ViewBag.PendingBlogCount = pendingBlogList.Count();
            ViewBag.AllBlogCount = allBlogList.Count();
            ViewBag.BlogCount = blogList.Count();
            ViewBag.Name = values.Name + " " + values.Surname;
            ViewBag.Role = await _userManager.GetRolesAsync(values);
            return View(pendingBlogList);
        }

        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> MessagePage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            if (User.IsInRole("Admin"))
            {
                var values = _blogService.TGetBlogListWithCategory();
                if (values.Count() == 0)
                    ViewBag.Message = "Henüz hiç blog yayınlanmamış...";
                return View(values);
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                using (var c = new Context())
                {
                    var values = c.Blogs.Include(x => x.Category).Include(y => y.AppUser).Include(z => z.Comments).Where(y => y.AppUserId == user.Id).ToList();
                    if (values.Count() == 0)
                        ViewBag.Message = "Henüz hiç blog yayınlanmamış...";
                    return View(values);
                }
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PendingApprovalBlogList()
        {
            var values = _blogService.TGetBlogListWithCategoryByPendingApproval();
            if (values.Count() == 0)
                ViewBag.Message = "Onay bekleyen blog bulunamadı..";
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
            if (values.Count() == 0)
                ViewBag.Message = "Henüz hiç kategori oluşturulmamış...";
            return View(values);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int id)
        {
            var values = _categoryService.TGetById(id);
            _categoryService.TDelete(values);
            return RedirectToAction("CategoryList", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(int id)
        {
            var values = _categoryService.TGetById(id);
            return View(values);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(Category p)
        {
            _categoryService.TUpdate(p);
            return RedirectToAction("CategoryList", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LockedUser()
        {
            using (var c = new Context())
            {
                var values = c.Users.Where(x => x.LockoutEnd != null).ToList().Count();
                if (values > 0)
                {
                    var value = c.Users.Where(x => x.LockoutEnd != null).ToList();
                    return View(value);
                }
                else
                {
                    ViewBag.Message = "Bloke olmuş üye bulunamadı.";
                    return View();
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeActive(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                // Kullanıcının blokajını kaldırın
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("LockedUser", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            using (var c = new Context())
            {
                var values = c.Users.ToList();

                if (TempData.ContainsKey("ErrorMessage"))
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                }

                return View(values);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Contains("Admin"))
                {
                    TempData["ErrorMessage"] = "Admin rolü silinemez.";
                    return RedirectToAction("UserList", "Dashboard", new { area = "Admin" });
                }
                else
                {
                    TempData["ErrorMessage"] = "Başarı ile silindi.";
                    await _userManager.DeleteAsync(user);
                }
            }
            return RedirectToAction("UserList", "Dashboard", new { area = "Admin" });
        }
    }
}
