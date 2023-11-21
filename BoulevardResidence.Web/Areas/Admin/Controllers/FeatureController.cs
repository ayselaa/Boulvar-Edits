using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Features;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Feature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public FeatureController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Feature";
            ViewBag.CurrentAction = "Index";

            List<Feature> features = await _context.Features.Include(ft=>ft.FeatureTranslates
                                                   .Where(ft=> ft.LangCode == "en"))
                                                   .OrderByDescending(fm=>fm.Id)
                                                   .ToListAsync();
            return View(features);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(FeatureCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Logo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "featurelogo", fileName);
                await FileHelper.SaveFileAsync(path, model.Logo);

                Feature newFuture = new()
                {
                    Logo = fileName
                };

                List<FeatureTranslate> featureTranslates = model.Translates.Select(t => new FeatureTranslate
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                }).ToList();

                newFuture.FeatureTranslates = featureTranslates;

                await _context.Features.AddAsync(newFuture);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null)
                return BadRequest();

            var feature = await _context.Features
                .Include(s => s.FeatureTranslates)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (feature is null)
                return NotFound();

            FeatureUpdateVM model = new FeatureUpdateVM
            {
                Id = feature.Id,
                Logo = feature.Logo,
                Translates = feature.FeatureTranslates.Select(t => new FeatureTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name

                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FeatureUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var feture = await _context.Features
                 .Include(s => s.FeatureTranslates)
                 .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (feture is null)
                    return NotFound();

                string imagePath = null;

                if (NewImage != null)
                {

                    if (!string.IsNullOrEmpty(feture.Logo))
                    {
                        imagePath = $"{_env.WebRootPath}/featurelogo/{feture.Logo}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Save the new image
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/featurelogo/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }

                    feture.Logo = imageName;
                }

                foreach (var translate in model.Translates)
                {
                    var existingTranslate = feture.FeatureTranslates
                                                   .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Name = translate.Name;
                    }
                    else
                    {
                       FeatureTranslate newTranslate = new FeatureTranslate()
                        {
                            LangCode = translate.LangCode,
                            Name = translate.Name
                        };
                        feture.FeatureTranslates.Add(newTranslate);
                    }
                }
                foreach (var existingTranslate in feture.FeatureTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.FeatureTranslates.Remove(existingTranslate);
                }



                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var feature = await _context.Features
                    .Include(b => b.FeatureTranslates)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (feature is null)
                    return NotFound();


                _context.FeatureTranslates.RemoveRange(feature.FeatureTranslates);

                await _context.SaveChangesAsync();


                _context.Features.Remove(feature);

                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "featurelogo", feature.Logo);
                FileHelper.DeleteFile(path);


                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest();

            var feature = await _context.Features
                .Include(s => s.FeatureTranslates)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (feature is null)
                return NotFound();

            FeatureDetailVM model = new FeatureDetailVM
            {
                Id = feature.Id,
                Logo = feature.Logo,
                Translates = feature.FeatureTranslates.Select(t => new FeatureTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name
                }).ToList()
            };

            return View(model);
        }
    }
}
