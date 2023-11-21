using BoulevardResidence.Domain.Entity.Contacts;
using BoulevardResidence.Service.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);

        Task<bool> AddContactAsync(ContactVM model);
    }
}
