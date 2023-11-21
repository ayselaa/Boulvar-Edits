using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Galleries;
using BoulevardResidence.Web.Areas.Admin.ViewModels.GalleryCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public GalleryCategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "GalleryCategory";
            ViewBag.CurrentAction = "Index";

            List<GalleryCategory> categories = await _context.GalleryCategories
                                                           .Include(c => c.GalleryCategoryTranslates
                                                           .Where(ct => ct.LangCode == "en"))
                                                           .OrderByDescending(m=>m.Id)
                                                           .ToListAsync();
            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(GalleryCategoryCreateVM model)
        {
            try
            {
                List<GalleryCategoryTranslate> categoryTranslates = model.Translates.Select(t => new GalleryCategoryTranslate
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                }).ToList();

                GalleryCategory category = new GalleryCategory();

                category.GalleryCategoryTranslates = categoryTranslates;

                await _context.GalleryCategories.AddAsync(category);
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
                if (id is null)
                    return BadRequest();
                var model = await _context.GalleryCategories
                           .Where(m => !m.SoftDelete)
                            .Include(m => m.GalleryCategoryTranslates)
                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.GalleryCategoryTranslates.RemoveRange(model.GalleryCategoryTranslates);

                _context.GalleryCategories.Remove(model);

                await _context.SaveChangesAsync();
                return Ok();

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
            var menu = await _context.GalleryCategories
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.GalleryCategoryTranslates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            GalleryCategoryUpdateVM model = new GalleryCategoryUpdateVM
            {
                Id = menu.Id,
                Translates = menu.GalleryCategoryTranslates.Select(t => new GalleryCategoryTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle= t.Tittle
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GalleryCategoryUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.GalleryCategories
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.GalleryCategoryTranslates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.GalleryCategoryTranslates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Tittle = item.Tittle;
                    }
                    else
                    {
                        menu.GalleryCategoryTranslates.Add(new GalleryCategoryTranslate
                        {
                            LangCode = item.LangCode,
                            Tittle = item.Tittle
                        });
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
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
                return BadRequest(0);
            var menu = await _context.GalleryCategories
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.GalleryCategoryTranslates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();

            GalleryCategoryDetailVM model = new GalleryCategoryDetailVM
            {
                Id = menu.Id,
                Translates = menu.GalleryCategoryTranslates.Select(t => new GalleryCategoryTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                }).ToList()
            };
            return View(model);
        }

    }
}
