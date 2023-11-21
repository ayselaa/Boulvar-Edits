using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Features;
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
    public class FeatureService: IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeatureService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<List<Feature>> GetFeatures() => await _context.Features.Include(m=>m.FeatureTranslates
        //                                                         .Where(m=>m.LangCode=="az"))
        //                                                 .Include(m => m.FeatureApartments).ToListAsync();

        public async Task<List<Feature>> GetFeatures() 
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }

            return await _context.Features.Include(m => m.FeatureTranslates
                                                          .Where(m=>m.LangCode== lang.ToLower()))
                                                        .Include(m => m.FeatureApartments).ToListAsync();
        }



        //public async Task<Feature> GetFullDataById(int? id) => await _context.Features.Include(m => m.FeatureTranslates
        //                                                         .Where(m => m.LangCode == "az"))
        //                                                          .Include(m => m.FeatureApartments)
        //                                                           .ThenInclude(m => m.Feature)
        //                                                           .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Feature> GetFullDataById(int? id)
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.Features.Include(m => m.FeatureTranslates
                                                                .Where(m => m.LangCode == lang.ToLower()))
                                                                 .Include(m => m.FeatureApartments)
                                                                  .ThenInclude(m => m.Feature)
                                                                  .FirstOrDefaultAsync(m => m.Id == id);
        } 
    }
}
