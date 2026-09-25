using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    public class ConversationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConversationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Convs = _context.UserConvs.Where(m => m.MemberId == userId).Select(x => x.Conversation);
            
            if(Convs == null) 
                return NotFound("Empty");
            
            return View(Convs);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            var conv = await _context.Conversations
                    .Include(x => x.messages)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (conv == null)
                return NotFound("No Conversation");

            return View(conv);
        }

        // Private
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string contactId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || userId == null)
                return NotFound($"login/register first{userId}");

            var allConversationIds = from c in _context.UserConvs
                                     where (c.MemberId == userId && !c.Conversation.IsGroup)
                                     select c.ConversationId;

            var conversation = from c in _context.UserConvs
                               where (c.MemberId == contactId && allConversationIds.Contains(c.ConversationId))
                               select c.Conversation;

            Conversation? conv = await conversation.FirstOrDefaultAsync();

            if (conv != null)
            {
                return RedirectToAction("Details", new {id =  conv.Id});
            }

            var contact = await _context.Users.FindAsync(contactId);

            if (contact == null)
                return NotFound($"no contact{contactId}");

            conv = new Conversation { IsGroup = false, Title = contact.UserName };

            var uc = new UserConversation
            {
                MemberId = userId,
                Member = user,
                Conversation = conv,
                ConversationId = conv.Id
            };

            var uc2 = new UserConversation
            {
                MemberId = contact.Id,
                Member = contact,
                Conversation = conv,
                ConversationId = conv.Id
            };

            _context.Conversations.Add(conv);
            _context.UserConvs.Add(uc);
            _context.UserConvs.Add(uc2);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = conv.Id });
        }

    }
}
