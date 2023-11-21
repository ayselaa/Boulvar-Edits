using BoulevardResidence.Domain.Entity.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IFloorService
    {
        Task<List<ApartmentFloor>> GetApartmentFloorByApartmentId(string sectionName);
        Task<List<ApartmentFloor>> GetAllApartmentsFloorByApartmentId(string sectionName, int floor);
        Floor GetFloorByFloorNumber(int floorNumber);
    }
}
