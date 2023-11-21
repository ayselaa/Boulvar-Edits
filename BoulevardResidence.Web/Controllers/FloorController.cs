using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class FloorController : Controller
    {
        private readonly IFloorService _floorService;
        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }


        public async Task<IActionResult> Index(string sectionName, int floornumber)
        {
            var model= await _floorService.GetAllApartmentsFloorByApartmentId( sectionName,  floornumber);
            var floors = await _floorService.GetApartmentFloorByApartmentId(sectionName);

            ViewBag.Floors = floors;
            ViewBag.SelectedFloor = floornumber;
            

            return View(model);
        }
    }
}
