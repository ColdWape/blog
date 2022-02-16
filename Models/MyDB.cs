using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class MyDB : DbContext
    {
        public DbSet<BloggerModel> Bloggers { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public MyDB(DbContextOptions<MyDB> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
            SaveChanges();
        }
    }
}
