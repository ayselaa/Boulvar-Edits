using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Galleries;
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
    public class GalleryCategoryService : IGalleryCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GalleryCategoryService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GalleryCategory>> GetAllAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.GalleryCategories.Include(m=> m.GalleryCategoryTranslates
                                                   .Where(mt => mt.LangCode == lang.ToLower()))
                                                   .ToListAsync();
        }
    }
}
