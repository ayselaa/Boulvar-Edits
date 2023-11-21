using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Settings;
using BoulevardResidence.Domain.Entity.Socials;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Social>> GetAll()
        {
            return await _context.Socials.ToListAsync();
        }

        public Dictionary<string, string> GetSettings() => _context.Settings.AsEnumerable()
                                                                             .ToDictionary(s => s.Key, s => s.Value);
        public async Task<List<Setting>> GetSettingDatas()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return settings;
        }
        public async Task<Setting> GetById(int? id) => await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
    }
}
