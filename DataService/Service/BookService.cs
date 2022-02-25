using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DataService.Service
{
   public class BookService: IBookService
    {
        private readonly PustakaDbContext _dbContext;
        public BookService(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Book>> GetAll()
        {
            return _dbContext.Books;
        }

        public async Task<Book> GetById(int id)
        {
            var book = await _dbContext.Books.Include(b => b.Category).
                Where(c => c.ID == id).FirstOrDefaultAsync();

            return book;
        }

        public async Task<Book> Insert(Book book)
        {
            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task Update(Book book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Book>> GetRandomBooks(string cat)
        {
            return await _dbContext.Books.Where(c => c.Category.Nama == cat)
                .OrderBy(r => Guid.NewGuid()).Include(b => b.Category).ToListAsync();
        }
    }

   public class BookServiceApi : IBookServiceApi
   {
       public HttpClient _httpClient { get; }
       public AppSettings _appSettings { get; }
       public BookServiceApi(HttpClient httpClient
           , IOptions<AppSettings> appSettings)
       {
           _appSettings = appSettings.Value;


           httpClient.BaseAddress = new Uri(_appSettings.PustakaBaseAddress);
           httpClient.DefaultRequestHeaders.Accept.Clear();
           httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));


           _httpClient = httpClient;
       }
        public async Task<IEnumerable<Book>> GetAll()
       {
           var requestMessage = new HttpRequestMessage(HttpMethod.Get, "books");
           var response = await _httpClient.SendAsync(requestMessage);


           var responseStatusCode = response.StatusCode;

           if (responseStatusCode.ToString() == "OK")
           {
               var responseBody = await response.Content.ReadAsStringAsync();
               return await Task.FromResult(JsonConvert.DeserializeObject<List<Book>>(responseBody));
           }

           return null;
        }

       public async Task<Book> GetById(int id)
       {
           var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"books/{id}");
           var response = await _httpClient.SendAsync(requestMessage);


           var responseStatusCode = response.StatusCode;

           if (responseStatusCode.ToString() == "OK")
           {
               var responseBody = await response.Content.ReadAsStringAsync();
               return await Task.FromResult(JsonConvert.DeserializeObject<Book>(responseBody));
           }

           return null;
        }

        public async Task Insert(Book book)
        {
            string serializedUser = JsonConvert.SerializeObject(book);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "books");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }
        }

        public async Task Update(Book book)
        {
            string serializedUser = JsonConvert.SerializeObject(book);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "books");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }
        }

        public async Task Delete(int bookId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"books/{bookId}");

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Sorry, something went wrong");

            }
        }

    }
}
