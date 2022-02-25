using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_CRUD.Components;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Components;

namespace Blazor_CRUD.Pages
{
    public class BookListBase : ComponentBase
    {
        [Inject]
        public IBookService BookService { get; set; }
        public IEnumerable<Book> Books { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Books = (await BookService.GetAll()).ToList();
        }
        protected AddEditBookDialog AddEditBookDialog { get; set; }
        public async void AddEditBookDialog_OnDialogClose()
        {
            Books = (await BookService.GetAll()).ToList();
            StateHasChanged();
        }

        public void AddEditBookShow(int bookid)
        {
            AddEditBookDialog.Show(bookid);
        }
    }
}
