using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class ComfortService : IComfortService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ComfortService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
       
 
        public async Task<List<Comfort>> GetAllAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.Comforts.Include(c => c.ComfortTranslates
                                          .Where(ct => ct.LangCode == lang.ToLower()))
                                          .ToListAsync();

        }
    }
}
