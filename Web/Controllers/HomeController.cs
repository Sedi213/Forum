using Application.Services.Notes.Commands.CreateNote;
using Application.Services.Notes.Commands.DeleteNote;
using Application.Services.Notes.Queries.GetAllNotes;
using Application.Services.Notes.Queries.GetAllNotesbyUserId;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models;


namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private IMediator _mediator;
        private IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private Guid userId
        {
            get
            {
                return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetAllNoteQuery
            {
                id = userId
            };

            var list = await Mediator.Send(query);

            return View(list.AllNotes);
        }
        [Authorize]
        [HttpGet]
        public IActionResult NewNote()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewNote(RequestedNewUser note)
        {
            var command = new CreateNoteCommand
            {
                text = note.text,
                title = note.title,
                userId = userId,
            };

            var newid = await Mediator.Send(command);

            _logger.LogInformation($"New note {newid.ToString()} was created by {userId.ToString()} at{DateTime.Now:hh:mm:ss}");

            return Redirect("~/Home/UserNotes");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserNotes()
        {

            var query = new GetUserNotesByUserIdRequest
            {
                userid = userId
            };

            var list = await Mediator.Send(query);

            return View(list.Notes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteNote(Guid guid)
        {
            var command = new DeleteNoteCommand
            {
                id = guid,
                userId = userId,
            };

            var newid = await Mediator.Send(command);

            _logger.LogInformation($"Note {guid.ToString()} was deleted by {userId.ToString()} at{DateTime.Now:hh:mm:ss}");
            return Redirect("~/Home/UserNotes");
        }
    }
}