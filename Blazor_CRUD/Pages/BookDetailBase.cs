using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataService.Entity;
using DataService.IService;

namespace Blazor_CRUD.Pages
{
    public class BookDetailBase : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }
        public Book Book { get; set; } = new Book();
        [Inject]
        public IBookService BookService { get; set; }
        protected string NamaCategory = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Book = await BookService.GetById(int.Parse(BookId));
            NamaCategory = Book.Category.Nama;
        }
    }
}
