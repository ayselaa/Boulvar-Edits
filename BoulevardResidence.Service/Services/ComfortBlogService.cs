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
    public class ComfortBlogService : IComfortBlogService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ComfortBlogService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<ComfortBlog>> GetAllAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.ComfortBlogs.Include(cb=>cb.ComfortBlogTranslates
                                                .Where(cbt => cbt.LangCode == lang.ToLower()))
                                               .ToListAsync();
        }
    }
}
