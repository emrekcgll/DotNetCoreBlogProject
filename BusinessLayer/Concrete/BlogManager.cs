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
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;
        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void TAdd(Blog t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Blog t)
        {
            throw new NotImplementedException();
        }

        public List<Blog> TGetBlogByIdWithCategory(int id)
        {
            return _blogDal.GetBlogByIdWithCategory(id);
        }

        public List<Blog> TGetBlogListWithCategory()
        {
            return _blogDal.GetBlogListWithCategory();
        }

        public Blog TGetById(int id)
        {
            return _blogDal.GetListById(id);
        }

        public List<Blog> TGetList()
        {
            return _blogDal.GetList();
        }

        public void TUpdate(Blog t)
        {
            throw new NotImplementedException();
        }
    }
}
