using Application.InterfacesDB;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notes.Queries.GetAllNotes
{
    public class GetAllNoteQueryHandler :
        IRequestHandler<GetAllNoteQuery,NotelistVm>
    {
        private readonly IForumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllNoteQueryHandler(IForumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<NotelistVm> Handle(GetAllNoteQuery request, CancellationToken cancellationToken)
        {
            var rawlist = await _dbContext.Notes.ToArrayAsync();
            
            
            var notesQuery = new NotelistVm { AllNotes = _mapper.Map<Note[], IEnumerable<AllNoteVm>>(rawlist).ToList() };

            return notesQuery;
        }
    }
}
