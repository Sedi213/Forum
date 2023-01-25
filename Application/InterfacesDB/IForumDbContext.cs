using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesDB
{
    public interface IForumDbContext
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync();
    }
}
