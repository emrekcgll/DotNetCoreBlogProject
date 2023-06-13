using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.ViewComponents.Default
{
    public class PopularBlogComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using (var c = new Context())
            {
                var values = c.Blogs.Include(x => x.Comments).ToList();
                var sortedBlogs = values.OrderByDescending(x => x.Comments.Count).ToList();
                var mostCommentedBlog = sortedBlogs[0];

                return View(mostCommentedBlog);
            }
        }
    }
}
