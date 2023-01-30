using AutoMapper;
using Domain.Models;

namespace Web.ResourceModels.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Note, NoteModel>();
        }
    }
}
