using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;

namespace DataService.IService
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int bookId);
        Task<Book> Insert(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
        Task<IEnumerable<Book>> GetRandomBooks(string cat);
    }

    public interface IBookServiceApi
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int bookId);
        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
        
    }
}
