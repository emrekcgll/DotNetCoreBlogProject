using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        List<Blog> TGetBlogListWithCategory();
        List<Blog> TGetBlogByIdWithCategory(int id);
        List<Blog> TGetBlogListWithCategoryByPendingApproval();
        List<Blog> TGetLastBlogListWithWriter(int id);
        List<Blog> TGetLastThreeBlog();

        List<Blog> TGetTodaysBlogs();
        List<Blog> TGetNotTodaysBlogs();

        List<Blog> TGetLast6Blog();
    }
}
