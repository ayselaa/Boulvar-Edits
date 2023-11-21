using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Infrastructures;
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
    public class InfrastructureService : IInfrastructureService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InfrastructureService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Infrastructure>> GetAllAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.Infrastructures.Include(m => m.InfrastructureTranslates
                                                  .Where(mt => mt.LangCode == lang.ToLower()))
                                                  .ToListAsync();
            
        }
    }
}
