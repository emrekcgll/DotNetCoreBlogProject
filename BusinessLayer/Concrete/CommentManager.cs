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
            _commentDal.Insert(t);
        }

        public void TDelete(Comment t)
        {
            throw new NotImplementedException();
        }

        public Comment TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetCommentListNotToday()
        {
            return _commentDal.GetCommentListNotToday();
        }

        public List<Comment> TGetCommentListToday()
        {
            return _commentDal.GetCommentListToday();
        }

        public List<Comment> TGetCommentListWithBlogID(int id)
        {
            return _commentDal.GetListByFilter(x => x.BlogID == id);
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
