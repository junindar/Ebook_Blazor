using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;

namespace DataService.IService
{
  public  interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> Insert(Category obj);
        Task<Category> Update(Category obj);
        Task Delete(int id);
    }

  public interface ICategoryServiceApi
  {
      Task<List<Category>> GetAll();
      Task<Category> GetById(int id);
  }
}
