using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    public class MessageController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MessageController
        public ActionResult Index()
        {
            return View();
        }

        

        // POST: MessageController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            string text = collection["messageText"];
            int convId = int.Parse(collection["convId"]);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || userId == null)
                return NotFound($"login/register first{userId}");

            var conv = await _context.Conversations.FindAsync(convId);
            if (conv == null)
                return NotFound("no conversation");

            var message = new Message { 
                SenderId = userId, SendDate = DateTime.Now,
                ConversationId = convId, Text = text
            };

            _context.Messages.Add(message);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Conversation", new { id = conv.Id });
        }

    }
}
