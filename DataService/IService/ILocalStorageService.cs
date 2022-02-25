using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.IService
{
    public interface ILocalStorageService
    {
        Task SetItemAsync<T>(string key, T item);

        Task<T> GetItemAsync<T>(string key);
    }
}
