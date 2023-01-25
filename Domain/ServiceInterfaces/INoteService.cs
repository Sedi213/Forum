using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface INoteService
    {
        public Task<Guid> CreateNote(Note note);
        public Task DeleteNote(Guid id);
        public Task<List<Note>> GetListNotes();
        public Task<List<Note>> GetAllNoteByUser(Guid userid);
    }
}
