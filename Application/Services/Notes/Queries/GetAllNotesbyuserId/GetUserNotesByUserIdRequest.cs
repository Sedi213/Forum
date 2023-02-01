using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notes.Queries.GetAllNotesbyUserId
{
    public class GetUserNotesByUserIdRequest : IRequest<ListUserNoteVm>
    {
        public Guid userid { get; set; }
    }
}
