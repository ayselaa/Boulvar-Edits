using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Domain.Entity.Features;
using BoulevardResidence.Service.Interfaces;
using BoulevardResidence.Web.Areas.Admin.ViewModels;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Apartment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApartmentController : Controller
    {

        private readonly IApartmentCreatedService _service;
        private readonly IFeatureService _featureService;
        private readonly IApartmentService _apartmentService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ApartmentController(IApartmentCreatedService service, IFeatureService featureService, IApartmentService apartmentService, AppDbContext context, IWebHostEnvironment env)
        {
            _service = service;
            _featureService = featureService;
            _apartmentService = apartmentService;
            _context = context;
            _env = env;

        }


        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Apartment";
            ViewBag.CurrentAction = "Index";

            List<Apartment> apartments = await _context.Apartments/*.Where(m=>m.Status=="AVAILABLE")*/.Include(m=>m.ApartmentFeatures)
                                           .ThenInclude(m=>m.Feature.FeatureTranslates)
                                           .ToListAsync();
           return View(apartments);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApartmentVM apartmentVM)
        {
            await _service.CreateApartmentWithRequest(apartmentVM.Id);
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                //ViewBag.Features = await GetFutureAsync();
                var features = await GetFeatures();
                ViewBag.Features = new MultiSelectList(features.Select(f => new
                {
                    Id = f.Id,
                    Name = f.FeatureTranslates.FirstOrDefault(ft => ft.LangCode == "az")?.Name
                }), "Id", "Name");


                Apartment dbApartment = await _apartmentService.GetFullDataByIdAsync((int)id);

                if (dbApartment == null) return NotFound();

                ApartmentUpdateVM model = new()
                {
                    Id = dbApartment.Id,
                    ApartmentPlan = dbApartment.ApartmentPlan,
                    GTagPlan= dbApartment.GTagPlan,
                    NotAviableGTagPlan=dbApartment.NotAviableGTagPlan,
                    FeatureIds = dbApartment.ApartmentFeatures.Select(m => m.Feature.Id).ToList(),
                    
              
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ApartmentUpdateVM updatedProduct, IFormFile NewImage)
        {
            try
            {
                //ViewBag.Features = await GetFutureAsync();
                var features = await GetFeatures();
                ViewBag.Features = new MultiSelectList(features.Select(f => new
                {
                    Id = f.Id,
                    Name = f.FeatureTranslates.FirstOrDefault(ft => ft.LangCode == "az")?.Name
                }), "Id", "Name");

                if (id == null) return BadRequest();
                Apartment dbApartment = await _apartmentService.GetFullDataByIdAsync((int)id);
                if (dbApartment == null) return NotFound();

                ApartmentUpdateVM model = new()
                {
                    Id = dbApartment.Id,
                    GTagPlan= dbApartment.GTagPlan,
                    NotAviableGTagPlan=dbApartment.NotAviableGTagPlan,
                    FeatureIds = dbApartment.ApartmentFeatures.Select(m => m.Feature.Id).ToList(),
                };


                List<FeatureApartment> featureApartments = new();
                if (updatedProduct.FeatureIds.Count > 0)
                {
                    foreach (var cateId in updatedProduct.FeatureIds)
                    {
                        FeatureApartment productCategory = new()
                        {
                           FeatureId = cateId
                        };
                        featureApartments.Add(productCategory);
                    }
                    dbApartment.ApartmentFeatures = featureApartments;
                }
                else
                {
                    ModelState.AddModelError("FeatureIds", "Don't be empty");
                    return View();
                }

                string imagePath = null;

                if (NewImage != null)
                {
                    if (!string.IsNullOrEmpty(dbApartment.ApartmentPlan))
                    {
                        imagePath = $"{_env.WebRootPath}/apartmentplangallery/{dbApartment.ApartmentPlan}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = Path.Combine(_env.WebRootPath, "apartmentplangallery", imageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }
                    dbApartment.ApartmentPlan = imageName;
                }


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        private async Task<List<Feature>> GetFeatures()
        {
            var features = await _context.Features
                .Include(f => f.FeatureTranslates)
                .Where(f => f.FeatureTranslates.Any(ft => ft.LangCode == "az"))
                .ToListAsync();

            return features;
        }

    }
}
