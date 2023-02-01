using Application.InterfacesDB;
using Application.Services.Notes.Queries.GetAllNotes;
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

namespace Application.Services.Notes.Queries.GetAllNotesbyUserId
{
    public class GetUserNotesByUserIdRequestHandler :
        IRequestHandler<GetUserNotesByUserIdRequest, ListUserNoteVm>
    {
        private readonly IForumDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserNotesByUserIdRequestHandler(IForumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ListUserNoteVm> Handle(GetUserNotesByUserIdRequest request, CancellationToken cancellationToken)
        {
            var rawlist = await _dbContext.Notes
            .Where(x => x.userId == request.userid).ToListAsync();

            var notesQuery = new ListUserNoteVm { Notes = _mapper.Map<Note[], IEnumerable<UserNoteVm>>(rawlist.ToArray()).ToList() };
            return notesQuery;
        }
    }
}
