using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;
using Microsoft.EntityFrameworkCore;

namespace DataService.Service
{
   public class UserService: IUserService
    {
        private readonly PustakaDbContext _dbContext;

        public UserService(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CheckLoginAsync(User obj)
        {
            var user = await _dbContext.Users.Where(c => c.Username.ToLower() == obj.Username.ToLower() &&
                                                         c.Password == obj.Password).FirstOrDefaultAsync();

            return user;
        }
    }
}
