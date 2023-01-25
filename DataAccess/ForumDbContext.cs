using Application.InterfacesDB;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ForumDbContext : DbContext, IForumDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public ForumDbContext(DbContextOptions op): base(op)
        {
            Database.EnsureCreated();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
