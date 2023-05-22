using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetBlogByIdWithCategory(int id);
        List<Blog> GetBlogListWithCategoryByPendingApproval();
        List<Blog> GetLastBlogListWithWriter(int id);
        List<Blog> GetLastThreeBlog();
    }
}
