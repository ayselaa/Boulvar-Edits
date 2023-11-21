using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Contacts;
using BoulevardResidence.Service.DTOs.Contact;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<bool> AddContactAsync(ContactVM model)
        {
            Contact contact = new()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
