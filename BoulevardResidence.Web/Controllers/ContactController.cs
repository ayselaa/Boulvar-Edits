using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Contacts;
using BoulevardResidence.Service.DTOs.Contact;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IContactService _contactService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactController(AppDbContext context, IContactService contactService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contactService = contactService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PostComment(ContactVM model)
        //{
        //    if (!ModelState.IsValid) return RedirectToAction("Index", model);
        //    Contact contact = new()
        //    {
        //        Name = model.Name,
        //        Surname = model.Surname,
        //        Email = model.Email,
        //        Phone = model.Phone,
        //        Message = model.Message,
        //    };

        //    await _context.Contacts.AddAsync(contact);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", model);

             if (await _contactService.AddContactAsync(model))
            {
                ViewBag.SuccessMessage = "Mesajınız başarıyla alındı. İlgili kişi en kısa sürede sizinle iletişime geçecektir.";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", model);
            }
        }

    }
}
