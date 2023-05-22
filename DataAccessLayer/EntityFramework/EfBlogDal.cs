using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetBlogByIdWithCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(y => y.Comments).Include(v => v.AppUser).Where(x => x.BlogID == id).ToList();
            }
        }

        public List<Blog> GetBlogListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(y => y.Comments).Where(z => z.BlogStatus == true).ToList();
            }
        }

        public List<Blog> GetBlogListWithCategoryByPendingApproval()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(y => y.Comments).Where(z => z.BlogStatus == false).ToList();
            }
        }

        public List<Blog> GetLastBlogListWithWriter(int id)
        {
            using (var c = new Context())
            {
                var query = c.Blogs.Include(x => x.Category).Include(y => y.AppUser).Where(x => x.AppUserId == id && x.BlogStatus == true).ToList();
                var last3 = query.Skip(Math.Max(0, query.Count - 3)).ToList();
                return last3;
            }
        }

        public List<Blog> GetLastThreeBlog()
        {
            using (var c = new Context())
            {
                var query = c.Blogs.Where(x => x.BlogStatus == true).ToList();
                var last3 = query.Skip(Math.Max(0, query.Count - 3)).ToList();
                return last3;
            }
        }
    }
}
