using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void TAdd(Comment t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Comment t)
        {
            throw new NotImplementedException();
        }

        public Comment TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetCommentListWithBlogID(int id)
        {
            return _commentDal.GetListByFilter(x=>x.BlogID == id);
        }

        public List<Comment> TGetList()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
