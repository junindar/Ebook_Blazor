using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor_CRUD.Components
{
    public class AddEditBookDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        
        [Inject]
        public IFileUpload fileUpload { get; set; }
        [Inject]
        public IBookService BookService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }


        public Book Book { get; set; } = new Book();
        public List<Category> Categories { get; set; } = new List<Category>();

        protected string CategoryId = string.Empty;
        IBrowserFile file;
        protected MemoryStream fs;
        protected string ImageData = string.Empty;
        private async Task<MemoryStream> CopyToMemoryStream()
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

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        

        public async void Show(int bookid)
        {
            CategoryId = "";
            Categories = await CategoryService.GetAll();

            ImageData = string.Empty;
            fs = null;
            file = null;
            if (bookid == 0)
            {
                Book = new Book();
            }
            else
            {

                Book = await BookService.GetById(bookid);
                CategoryId = Book.CategoryID.ToString();
                ImageData = $"images/{Book.Gambar}";
            }

            ShowDialog = true;
            StateHasChanged();
        }

       
        public void Close()
        {
            ShowDialog = false;
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

            if (file != null)
            {
                await fileUpload.UploadAsync(fs, Book.Gambar);
            }

            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
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
        protected async Task DeleteBook()
        {
            await BookService.Delete(Book.ID);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
      

        }

    }
}
