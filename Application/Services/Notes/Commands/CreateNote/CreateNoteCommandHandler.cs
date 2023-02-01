using MediatR;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.InterfacesDB;

namespace Application.Services.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler 
        : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly IForumDbContext _dbContext;

        public CreateNoteCommandHandler(IForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> Handle(CreateNoteCommand request,
            CancellationToken cancellationToken)
        {
            var note = new Note
            {
                userId = request.userId,
                text=request.text,
                title=request.title,
                CreateDate= DateTime.Now,
                id= new Guid()
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.id;
        }
    }
}
