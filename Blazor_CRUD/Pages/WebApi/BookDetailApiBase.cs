using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataService.Entity;
using DataService.IService;

namespace Blazor_CRUD.Pages.WebApi
{
    public class BookDetailApiBase : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }
        public Book Book { get; set; } = new Book();
        [Inject]
        public IBookServiceApi BookService { get; set; }

        [Inject]
        public ICategoryServiceApi CategoryService { get; set; }
        protected string NamaCategory = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Book = await BookService.GetById(int.Parse(BookId));
            var cat = await CategoryService.GetById(Book.CategoryID);
            NamaCategory = cat.Nama;
           
        }
    }
}
