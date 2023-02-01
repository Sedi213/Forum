﻿using Application.Mapping;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notes.Queries.GetAllNotesbyUserId
{
    public class UserNoteVm : IMapWith<Note>
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public DateTime CreateDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, UserNoteVm>()
                .ForMember(noteVm => noteVm.title,
                    opt => opt.MapFrom(note => note.title))  
                .ForMember(noteVm => noteVm.id,
                    opt => opt.MapFrom(note => note.id))
                .ForMember(noteVm => noteVm.CreateDate,
                    opt => opt.MapFrom(note => note.CreateDate))
                .ForMember(noteVm => noteVm.text,
                    opt => opt.MapFrom(note => note.text));
        }
    }
}
