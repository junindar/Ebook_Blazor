using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;

namespace DataService.IService
{
   public interface IUserService
    {
        Task<User> CheckLoginAsync(User obj);
    }
}
