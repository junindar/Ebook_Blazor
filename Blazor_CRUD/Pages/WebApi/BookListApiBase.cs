using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_CRUD.Components;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Components;

namespace Blazor_CRUD.Pages.WebApi
{
    public class BookListApiBase : ComponentBase
    {
        [Inject]
        public IBookServiceApi BookService { get; set; }
        public IEnumerable<Book> Books { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Books = (await BookService.GetAll()).ToList();
        }
       
    }
}
