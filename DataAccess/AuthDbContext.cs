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
    public class AuthDbContext : DbContext
    {
     
        public AuthDbContext(DbContextOptions source) : base(source)
        {
            Database.EnsureCreated();
            
        }

    }
}
