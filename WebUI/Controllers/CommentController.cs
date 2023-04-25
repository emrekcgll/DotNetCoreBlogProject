using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CommentController : Controller
    {



        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddComment(Comment p) 
        {
            return RedirectToAction("Index", "Destination");
        }
    }
}
