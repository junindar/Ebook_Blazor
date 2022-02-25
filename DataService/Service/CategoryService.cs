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
   public class CategoryService: ICategoryService
    {
        private readonly PustakaDbContext _dbContext;
        public CategoryService(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

      
        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Category> Insert(Category obj)
        {
            _dbContext.Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task<Category> Update(Category obj)
        {
            _dbContext.Update(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(int id)
        {
            var book = await _dbContext.Categories.FindAsync(id);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }

   public class CategoryServiceApi : ICategoryServiceApi
    {
        public HttpClient _httpClient { get; }
        public AppSettings _appSettings { get; }
        public CategoryServiceApi(HttpClient httpClient
            , IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;


            httpClient.BaseAddress = new Uri(_appSettings.PustakaBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }
        public async Task<List<Category>> GetAll()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "categories");
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<Category>>(responseBody));
            }

            return null;
        }

        public async  Task<Category> GetById(int id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"categories/{id}");
            var response = await _httpClient.SendAsync(requestMessage);


            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<Category>(responseBody));
            }

            return null;
        }
    }

}
