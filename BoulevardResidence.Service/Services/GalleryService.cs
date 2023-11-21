using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Galleries;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly AppDbContext _context;
        public GalleryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GalleryItem>> GetAllAsync()
        {
            return await _context.GalleryItems
                                         .Include(m=>m.GalleryCategory)
                                         .ToListAsync();
        }
    }
}
