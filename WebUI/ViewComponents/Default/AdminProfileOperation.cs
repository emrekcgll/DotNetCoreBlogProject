using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.Default
{
    public class AdminProfileOperation : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminProfileOperation(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Name = values.Name + " " + values.Surname;
            ViewBag.Role = await _userManager.GetRolesAsync(values);
            return View();
        }
    }
}
