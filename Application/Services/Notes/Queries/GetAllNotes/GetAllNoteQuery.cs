using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notes.Queries.GetAllNotes
{
    public class GetAllNoteQuery : IRequest<NotelistVm>
    {
        public Guid id { get; set; } 

    }
}
