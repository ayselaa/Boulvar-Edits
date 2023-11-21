using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Headers;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class BackgroundImageService : IBackgroundImageService
    {
        private readonly AppDbContext _context;
        public BackgroundImageService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SectionBackgroundImage>> GetAllAsync()
        {
            return await _context.SectionBackgroundImages.ToListAsync();
        }

        public async Task<List<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync()
        {
            return await _context.SectionBackgroundImages.ToListAsync();
        }

        public Dictionary<string, string> GetSectionBackgroundImages()
        {
            return _context.SectionBackgroundImages.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
