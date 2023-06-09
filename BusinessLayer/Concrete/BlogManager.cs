﻿using BusinessLayer.Abstract;
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

        public List<Blog> TGetBlogListWithCategoryByPendingApproval()
        {
            return _blogDal.GetBlogListWithCategoryByPendingApproval();
        }

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
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
            _blogDal.Update(t);
        }

        public List<Blog> TGetLastBlogListWithWriter(int id)
        {
            return _blogDal.GetLastBlogListWithWriter(id);
        }

        public List<Blog> TGetLastThreeBlog()
        {
            return _blogDal.GetLastThreeBlog();
        }

        public List<Blog> TGetTodaysBlogs()
        {
            return _blogDal.GetTodaysBlogs();
        }

        public List<Blog> TGetNotTodaysBlogs()
        {
            return _blogDal.GetNotTodaysBlogs();
        }

        public List<Blog> TGetLast6Blog()
        {
            return _blogDal.GetLast6Blog();
        }
    }
}
