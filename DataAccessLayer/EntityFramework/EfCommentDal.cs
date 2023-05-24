using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetCommentListNotToday()
        {
            using (var c = new Context())
            {
                return c.Comments.Include(x => x.Blog).Where(x => x.CommentDate != DateTime.Today).ToList();
            }
        }

        public List<Comment> GetCommentListToday()
        {
            using (var c = new Context())
            {
                return c.Comments.Include(x => x.Blog).Where(x => x.CommentDate == DateTime.Today).ToList();
            }
        }

        public List<Comment> GetCommentListWithBlog()
        {
            using (var c = new Context())
            {
                return c.Comments.Include(x => x.Blog).ToList();
            }
        }
    }
}
