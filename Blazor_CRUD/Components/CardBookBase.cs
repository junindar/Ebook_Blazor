using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Components;

namespace Blazor_CRUD.Components
{
    public class CardBookBase : ComponentBase
    {
        public string PanelText { get; set; }
        [Parameter]
        public string CategoryName { get; set; }

        [Inject]
        public IBookService BookService { get; set; }
        public IEnumerable<Book> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PanelText = "DAFTAR BUKU : TEKNOLOGI";
            Books = (await BookService.GetRandomBooks(CategoryName)).ToList();
        }

        public async void ShowBookCategoryAsync(string cat)
        {
            PanelText = $"DAFTAR BUKU : {cat.ToUpper()}";
            Books = (await BookService.GetRandomBooks(cat)).ToList();
            StateHasChanged();
        }

    }
}
