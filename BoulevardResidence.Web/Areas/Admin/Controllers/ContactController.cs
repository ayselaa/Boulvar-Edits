using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Contact";
            ViewBag.CurrentAction = "Index";

            List<Contact> contacts = await _context.Contacts.ToListAsync();
            return View(contacts);
        }
    }
}
