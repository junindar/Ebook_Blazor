using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;


namespace Blazor_CRUD.Pages.WebApi
{
    public class BookEditApiBase : ComponentBase
    {
        [Inject]
        public IFileUpload fileUpload { get; set; }
        [Inject]
        public IBookServiceApi BookService { get; set; }

        [Inject]
        public ICategoryServiceApi CategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string BookId { get; set; }

        public Book Book { get; set; } = new Book();
        public List<Category> Categories { get; set; } = new List<Category>();
        IBrowserFile file;
        protected MemoryStream fs;
        protected string ImageData = string.Empty;
        protected string CategoryId = string.Empty;
        private async Task<MemoryStream>  CopyToMemoryStream()
        {
            var ms = new MemoryStream();
            var stream = file.OpenReadStream();

            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms;
        }
        protected async Task FileUpload(InputFileChangeEventArgs e)
        {
            file = e.GetMultipleFiles().FirstOrDefault();
            if (file != null)
            {
                fs = await CopyToMemoryStream(); ;
                ImageData = $"data:image/jpg;base64,{Convert.ToBase64String(fs.ToArray())}";
                Book.Gambar = file.Name;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = (await CategoryService.GetAll()).ToList();
            int.TryParse(BookId, out var bookId);
            if (bookId == 0)
            {
                Book = new Book();
            }
            else
            {
                Book = await BookService.GetById(int.Parse(BookId));
                ImageData = $"images/{Book.Gambar}";
            }
            CategoryId = Book.CategoryID.ToString();
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/BookListApi");
        }

        protected async Task HandleValidSubmit()
        {
            Book.CategoryID = int.Parse(CategoryId);
            if (Book.ID == 0) 
            {
                await BookService.Insert(Book);
            }
            else
            {
                await BookService.Update(Book);
            }
            if (file != null )
            {
                await fileUpload.UploadAsync(fs, Book.Gambar);
            }

            NavigationManager.NavigateTo("/BookListApi");
        }
 

        protected async Task DeleteBook()
        {
            await BookService.Delete(Book.ID);
            NavigationManager.NavigateTo("/BookListApi");

        }
    }
}
