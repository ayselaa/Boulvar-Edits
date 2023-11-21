using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
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
    public class ArchitecturalBlogService : IArchitecturalBlogService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArchitecturalBlogService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ArchitecturalBlog>> GetAllAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.ArchitecturalBlogs.Include(m => m.ArchitecturalBlogTranslates
                                                 .Where(mt => mt.LangCode == lang.ToLower()))
                                                .ToListAsync();
        }
    }
}
