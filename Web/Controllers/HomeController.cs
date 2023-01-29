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
        private readonly ILogger<HomeController> _logger;
        private readonly INoteService _noteService;

        public HomeController(ILogger<HomeController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        public async Task<IActionResult> Index()
        {
            var list =await _noteService.GetListNotes();
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
            _noteService.CreateNote(new Note
            {
                text = note.text,
                title = note.title,
                userId =Guid.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier)),
                CreateDate = DateTime.Now,
            });
            return Redirect("~/Home/UserNotes");
        }
        [Authorize]
        [HttpGet]
        public async  Task<IActionResult> UserNotes()
        {
            var list =await _noteService.GetAllNoteByUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return View(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteNote(Guid guid)
        {
            await _noteService.DeleteNote(guid);

            return Redirect("~/Home/UserNotes");
        }
    }
}