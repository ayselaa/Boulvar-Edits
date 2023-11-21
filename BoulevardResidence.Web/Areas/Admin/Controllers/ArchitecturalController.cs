using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Architectural;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchitecturalController : Controller
    {
        private readonly AppDbContext _context;
        public ArchitecturalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Architectural";
            ViewBag.CurrentAction = "Index";

            List<Architectural> architecturals = await _context.Architecturals
                                                                 .Include(m => m.ArchitecturalTranslates
                                                                .Where(mt => mt.LangCode == "en"))
                                                                .OrderByDescending(m => m.Id)
                                                                .ToListAsync();
            return View(architecturals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ArchitecturalCreateVM model)
        {
            try
            {
                List<ArchitecturalTranslate> architecturalTranslates = model.Translates.Select(t => new ArchitecturalTranslate
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Header = t.Header
                }).ToList();

                Architectural architectural = new Architectural();
                architectural.ArchitecturalTranslates = architecturalTranslates;

                await _context.Architecturals.AddAsync(architectural);
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

                var model = await _context.Architecturals.Where(m => !m.SoftDelete)
                                                           .Include(m => m.ArchitecturalTranslates)
                                                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.ArchitecturalTranslates.RemoveRange(model.ArchitecturalTranslates);
                _context.Architecturals.Remove(model);
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
            var menu = await _context.Architecturals
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.ArchitecturalTranslates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            ArchitecturalUpdateVM model = new ArchitecturalUpdateVM
            {
                Id = menu.Id,
                Translates = menu.ArchitecturalTranslates.Select(t => new ArchitecturalTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Header = t.Header
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArchitecturalUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.Architecturals
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.ArchitecturalTranslates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.ArchitecturalTranslates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Name = item.Name;
                        translate.Header = item.Header;
                    }
                    else
                    {
                        menu.ArchitecturalTranslates.Add(new ArchitecturalTranslate
                        {
                            LangCode = item.LangCode,
                            Name = item.Name,
                            Header = item.Header
                            
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
                return BadRequest();
            var menu = await _context.Architecturals
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.ArchitecturalTranslates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();

            ArchitecturalDetailVM model = new ArchitecturalDetailVM
            {
                Id = menu.Id,
                Translates = menu.ArchitecturalTranslates.Select(t => new ArchitecturalTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Header = t.Header
                }).ToList()
            };
            return View(model);
        }
    }
}
