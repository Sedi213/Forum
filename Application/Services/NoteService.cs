using Application.InterfacesDB;
using Domain.Models;
using Domain.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NoteService : INoteService
    {
        private readonly IForumDbContext _dbContext;
        public NoteService(IForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateNote(Note note)
        {
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.id;
        }

        public async Task DeleteNote(Guid id)
        {
            var entity = await _dbContext.Notes.FindAsync(new object[] { id });

            if (entity == null)
            {
                throw new Exception("NotFound " + id.ToString());
            }

            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Note>> GetListNotes()
        {
            var notesQuery = await _dbContext.Notes
                 .ToListAsync();


            return notesQuery;
        }


        public async Task<List<Note>> GetAllNoteByUser(Guid userid)
        {
            var notesQuery = await _dbContext.Notes
                 .Where(x => x.userId == userid)
                 .ToListAsync();

            return notesQuery;
        }
    }
}
