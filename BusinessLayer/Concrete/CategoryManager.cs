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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TAdd(Category t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Category t)
        {
            throw new NotImplementedException();
        }

        public Category TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> TGetList()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Category t)
        {
            throw new NotImplementedException();
        }
    }
}
