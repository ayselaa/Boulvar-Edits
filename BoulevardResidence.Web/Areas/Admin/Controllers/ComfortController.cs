using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Comfort;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComfortController : Controller
    {
        private readonly AppDbContext _context;
        public ComfortController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Comfort";
            ViewBag.CurrentAction = "Index";

            List<Comfort> comforts = await _context.Comforts
                                           .Include(m => m.ComfortTranslates
                                           .Where(mt => mt.LangCode == "en"))
                                           .OrderByDescending(m => m.Id)
                                           .ToListAsync();
            return View(comforts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ComfortCreateVM model)
        {
            try
            {
                List<ComfortTranslate> comfortTranslates = model.Translates.Select(t => new ComfortTranslate
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Header = t.Header
                }).ToList();

                Comfort comfort = new Comfort();
                comfort.ComfortTranslates = comfortTranslates;

                await _context.Comforts.AddAsync(comfort);
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

                var model = await _context.Comforts.Where(m => !m.SoftDelete)
                                                           .Include(m => m.ComfortTranslates)
                                                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.ComfortTranslates.RemoveRange(model.ComfortTranslates);
                _context.Comforts.Remove(model);
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
            var menu = await _context.Comforts
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.ComfortTranslates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            ComfortUpdateVM model = new ComfortUpdateVM
            {
                Id = menu.Id,
                Translates = menu.ComfortTranslates.Select(t => new ComfortTranslateVM
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
        public async Task<IActionResult> Edit(ComfortUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.Comforts
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.ComfortTranslates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.ComfortTranslates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Name = item.Name;
                        translate.Header = item.Header;
                    }
                    else
                    {
                        menu.ComfortTranslates.Add(new ComfortTranslate
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
                return BadRequest(0);
            var menu = await _context.Comforts
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.ComfortTranslates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();

            ComfortDetailVM model = new ComfortDetailVM
            {
                Id = menu.Id,
                Translates = menu.ComfortTranslates.Select(t => new ComfortTranslateVM
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
