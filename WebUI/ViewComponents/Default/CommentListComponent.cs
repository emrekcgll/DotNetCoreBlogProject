using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebUI.ViewComponents.Default
{
    public class CommentListComponent : ViewComponent
    {
        private readonly ICommentService _commentService;
        public CommentListComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values=_commentService.TGetCommentListWithBlogID(id);
            var count=values.Count();
            ViewBag.CommentCount = count;
            return View(values);
        }
    }
}
