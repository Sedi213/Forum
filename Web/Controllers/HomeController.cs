﻿using AutoMapper;
using Domain.Models;
using Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using Web.Models;
using Web.ResourceModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private readonly INoteService _noteService;

        public HomeController(ILogger<HomeController> logger, 
                               INoteService noteService,
                               IMapper mapper)
        {
            _mapper=mapper;
            _logger = logger;
            _noteService = noteService;
        }

        public async Task<IActionResult> Index()
        {
            var rawlist = await _noteService.GetListNotes();

            var list = _mapper.Map<Note[], IEnumerable<NoteModel>>(rawlist.ToArray()).ToList();
            
            return View(list);
        }
        [Authorize]
        [HttpGet]
        public IActionResult NewNote()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewNote(RequestedNote note)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newid = _noteService.CreateNote(new Note
            {
                text = note.text,
                title = note.title,
                userId = Guid.Parse(userid),
                CreateDate = DateTime.Now,
            });

            _logger.LogInformation($"New note {newid.ToString()} was created by {userid} at{DateTime.Now:hh:mm:ss}");

            return Redirect("~/Home/UserNotes");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserNotes()
        {
            var list = await _noteService.GetAllNoteByUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteNote(Guid guid)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _noteService.DeleteNote(guid);

            _logger.LogInformation($"Note {guid.ToString()} was deleted by {userid} at{DateTime.Now:hh:mm:ss}");
            return Redirect("~/Home/UserNotes");
        }
    }
}