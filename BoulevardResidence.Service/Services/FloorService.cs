using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Services
{
    public class FloorService : IFloorService
    {
        private readonly AppDbContext _context;
        public FloorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApartmentFloor>> GetApartmentFloorByApartmentId(string sectionName)
        {
            var apartmentFloor =await _context.ApartmentFloors.Where(f => f.Apartment.SectionName == sectionName).Include(m => m.Apartment).Include(m => m.Floor).OrderByDescending(m => m.FloorId).ToListAsync();
            
            return apartmentFloor;
         
        }
    

        public async Task<List<ApartmentFloor>> GetAllApartmentsFloorByApartmentId(string sectionName,int floor)
        {
            var apartmentFloor = await _context.ApartmentFloors
                .Include(m => m.Apartment)
                .Include(m => m.Floor)
                .Where(f => f.Apartment.SectionName == sectionName && f.FloorId==floor)
                .ToListAsync();
            return apartmentFloor;

        }
        public Floor GetFloorByFloorNumber(int floorNumber)
        {
            var floor = _context.Floors.FirstOrDefault(f=>f.FloorNumber == floorNumber);
            return floor;
     
        }

    }
}
