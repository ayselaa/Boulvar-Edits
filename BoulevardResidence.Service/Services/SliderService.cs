using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Sliders;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }
    }
}
