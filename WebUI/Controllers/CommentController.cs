using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddComment(Comment p, int id)
        {
            p.CommentDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
            p.CommentStatus = true;
            p.CommentImg = "deneme";
            _commentService.TAdd(p);
            return RedirectToAction("Details", "Default", new {id});
        }
    }
}
