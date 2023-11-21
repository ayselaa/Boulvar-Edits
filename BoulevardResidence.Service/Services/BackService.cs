using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.DailyBackGround;
using BoulevardResidence.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class BackService:IBackService
    {
        private readonly AppDbContext _context;

        public BackService(AppDbContext context)
        {
            _context = context;
        }

        public async void Create(DailyTaskResult dailyTaskResult)
        {
            await _context.DailyTaskResults.AddAsync(dailyTaskResult);
            _context.SaveChanges();
        }
    }
}
