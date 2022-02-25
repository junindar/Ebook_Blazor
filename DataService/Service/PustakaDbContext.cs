using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataService.Service
{
    public class PustakaDbContext : DbContext
    {
        public PustakaDbContext(DbContextOptions<PustakaDbContext>
            options) : base(options)
        {

        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
