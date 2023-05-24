using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> TGetCommentListWithBlogID(int id);

        List<Comment> TGetCommentListToday();
        List<Comment> TGetCommentListNotToday();
    }
}
