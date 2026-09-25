using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contact = await _context.Contacts.ToListAsync();
            var contacts = contact.Where(x => x.UserId == id);
            
            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string username)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();

            var user = await _context.Users.FindAsync(userId);

            if(user == null || userId == null)
                return NotFound($"login/register first{userId}");

            var contact = await _context.Users.FirstOrDefaultAsync(x => x.Email == username);

            // check if dupli

            if (contact == null)
                return NotFound($"User not found {username}");

            else if (user.UserName == username)
                return BadRequest("Yourself?");

            var newContact = new Contact { 
                UserId = userId, User = user,
                ContactUserId = contact.Id, ContactUser = contact
            };

            _context.Add(newContact);
            await _context.SaveChangesAsync();

            return Ok($"{username} is added");
        }
    }
}
