using Application.InterfacesDB;
using Application.Services.Notes.Commands.CreateNote;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly IForumDbContext _dbContext;

        public DeleteNoteCommandHandler(IForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request,
           CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes.FindAsync(new object[] { request.id });

            if (entity == null || entity.userId!=request.userId)
            {
                throw new Exception("NotFound " + request.id.ToString());
            }

            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
