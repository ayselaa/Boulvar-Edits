using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Service.DTOs.Aparments;
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
    public class ApartmentService : IApartmentService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApartmentService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateApartmentAsync(int id, string status)
        {

           var apartment= await GetFindApartmentById(id);

            apartment.Status = status;
            _context.Update(apartment);

            await _context.SaveChangesAsync();
        }
        public async Task<Apartment> GetFindApartmentById(int Id)
        {
            var result = await _context.Apartments.FindAsync(Id);
            return result;
        }
        public async Task CreateApartment(CreateApartmentDto createApartmentDto)
        {
            Apartment apartment = new Apartment()
            {
                Id = createApartmentDto.Id,

                HouseId = createApartmentDto.HouseId,
                HouseName = createApartmentDto.HouseName,
                ProjectName = createApartmentDto.ProjectName,
                Number = createApartmentDto.Number,
                RoomsAmount = createApartmentDto.RoomsAmount,
                Floor = createApartmentDto.Floor,
                SectionName = createApartmentDto.SectionName,
                LayoutType = createApartmentDto.LayoutType,
                WithoutLayout = createApartmentDto.WithoutLayout,
                Studio = createApartmentDto.Studio,
                FreeLayout = createApartmentDto.FreeLayout,
                EuroLayout = createApartmentDto.EuroLayout,


                TypePurpose = createApartmentDto.TypePurpose,

                AreaTotal = createApartmentDto.AreaTotal,

                Status = createApartmentDto.Status,
                CustomStatusId = createApartmentDto.CustomStatusId,
                SpecialOffersIds = createApartmentDto.SpecialOffersIds
            };

            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Apartment>> GetAllAsync()
        {
            return await _context.Apartments.Where(m=>m.Status == "AVAILABLE").
                                             Include(a=>a.ApartmentFeatures)
                                             .ThenInclude(m=>m.Feature)
                                             .ThenInclude(t=>t.FeatureTranslates
                                             .Where(mt=>mt.LangCode =="en"))
                                             .ToListAsync();
        }

        public async Task<List<Apartment>> GetAllAsyncWithoutStatus()
        {
            return await _context.Apartments.
                                             Include(a => a.ApartmentFeatures)
                                             .ThenInclude(m => m.Feature)
                                             .ThenInclude(t => t.FeatureTranslates
                                             .Where(mt => mt.LangCode == "en"))
                                             .ToListAsync();
        }

        public async Task<List<Apartment>> GetFilteredApartmentsAsync(string buildingFilter, int? roomFilter, /*stri
                                                                                                               * ng featuresFilter,*/ int? floorFilter, string floorAreaFilter)
        {
            var query = _context.Apartments.Where(apartment => apartment.Status == "AVAILABLE");

            if (!string.IsNullOrEmpty(buildingFilter))
            {
                query = query.Where(apartment => apartment.SectionName == buildingFilter);
            }

            if (roomFilter.HasValue)
            {
                query = query.Where(apartment => apartment.RoomsAmount == roomFilter);
            }

            //if (!string.IsNullOrEmpty(featuresFilter))
            //{
            //    query = query.Where(apartment => apartment.Features == featuresFilter);
            //}

            if (floorFilter.HasValue)
            {
                query = query.Where(apartment => apartment.Floor == floorFilter.Value);
            }

            if (!string.IsNullOrEmpty(floorAreaFilter))
            {
                query = query.Where(apartment => apartment.AreaTotal == floorAreaFilter);
            }

            return await query.ToListAsync();
        }

        public List<string> GetBuildings()
        {
            return _context.Apartments.Select(a => a.SectionName).Distinct().ToList();
        }

        public List<int> GetFloors()
        {
            return _context.Apartments.Select(a => a.Floor).Distinct().ToList();
        }

        public List<int> GetRooms()
        {
            return _context.Apartments.Select(a => a.RoomsAmount).Distinct().ToList();
        }

        public List<string> GetFloorAreas()
        {
            return _context.Apartments.Select(a => a.AreaTotal).Distinct().ToList();
        }

        public async Task<Apartment> GetFullDataByIdAsync(int? id) => await _context.Apartments/*.Where(m => m.Status == "AVAILABLE")*/
                                                                    .Include(m=>m.ApartmentFeatures)
                                                                     .ThenInclude(m=>m.Feature)
            .Include(m=>m.ApartmentFloors).ThenInclude(m=>m.Floor)
                                                             .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<List<Apartment>> GetPaginatedDatasAsync(int page, int take,string langcode)
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await _context.Apartments
                    .Where(m=>m.Status == "AVAILABLE")
                    .Include(m=>m.ApartmentFeatures)
                    .ThenInclude(m=>m.Feature)
                    .ThenInclude(m=>m.FeatureTranslates
                     .Where(m=>m.LangCode == lang.ToLower()))
                    .Include(p => p.ApartmentFloors)
                    .ThenInclude(p=>p.Floor)
                    .Skip((page * take) - take)
                    .Take(take)
                    .ToListAsync();
        }

        public async Task<int> GetCountAsync() => await _context.Apartments.Where(m=>m.Status == "AVAILABLE")
                                                .CountAsync();

        //public async Task<List<Apartment>> GetApartmentByBuildingIdAsync(int? id, int page= 1, int take=14)
        //{
        //    var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
        //    if (string.IsNullOrEmpty(lang))
        //    {

        //        lang = "az";
        //    }
        //    List<Apartment> model = new();
        //    var apartments = await _context.Apartments.Where(m => m.Status == "AVAILABLE")
        //            .Include(m => m.ApartmentFeatures)
        //            .ThenInclude(m => m.Feature)
        //            .ThenInclude(m => m.FeatureTranslates
        //             .Where(m => m.LangCode == lang.ToLower()))
        //            .Include(p => p.ApartmentFloors)
        //            .ThenInclude(p => p.Floor)
        //            .ToListAsync();

        //    foreach (var item in apartments)
        //    {
        //        model.Add(new Apartment
        //        {
        //            Id = item.Id,
        //            SectionName = item.SectionName,
        //            RoomsAmount = item.RoomsAmount,
        //            Floor = item.Floor,

        //        });
        //    }
        //    return model;
        //}
    }
}
