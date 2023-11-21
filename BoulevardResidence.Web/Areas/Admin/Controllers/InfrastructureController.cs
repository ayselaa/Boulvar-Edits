using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Infrastructures;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InfrastructureController : Controller
    {
        private readonly AppDbContext _context;
        public InfrastructureController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Infrastructure";
            ViewBag.CurrentAction = "Index";

            List<Infrastructure> infrastructures = await _context.Infrastructures
                                                                  .Include(m => m.InfrastructureTranslates
                                                                 .Where(mt => mt.LangCode == "en"))
                                                                 .OrderByDescending(m => m.Id)
                                                                 .ToListAsync();
            return View(infrastructures);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateInfrastructureVM model)
        {
            try
            {
                List<InfrastructureTranslate> infrastructureTranslates = model.Translates.Select(t => new InfrastructureTranslate
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Description = t.Description,
                }).ToList();

                Infrastructure infrastructure = new Infrastructure();
                infrastructure.InfrastructureTranslates = infrastructureTranslates;

                await _context.Infrastructures.AddAsync(infrastructure);
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
                if(id is null)
                    return BadRequest();

                var model = await _context.Infrastructures.Where(m => !m.SoftDelete)
                                                           .Include(m => m.InfrastructureTranslates)
                                                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.InfrastructureTranslates.RemoveRange(model.InfrastructureTranslates);
                _context.Infrastructures.Remove(model);
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
            var menu = await _context.Infrastructures
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.InfrastructureTranslates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            InfrastructureUpdateVM model = new InfrastructureUpdateVM
            {
                Id = menu.Id,
                Translates = menu.InfrastructureTranslates.Select(t => new InfrastructureTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Description = t.Description
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InfrastructureUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.Infrastructures
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.InfrastructureTranslates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.InfrastructureTranslates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Name = item.Name;
                        translate.Description = item.Description;
                    }
                    else
                    {
                        menu.InfrastructureTranslates.Add(new InfrastructureTranslate
                        {
                            LangCode = item.LangCode,
                            Name = item.Name
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
            var menu = await _context.Infrastructures
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.InfrastructureTranslates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();

            InfrastructureDetailVM model = new InfrastructureDetailVM
            {
                Id = menu.Id,
                Translates = menu.InfrastructureTranslates.Select(t => new InfrastructureTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                    Description = t.Description
                }).ToList()
            };
            return View(model);
        }

       

    }
}
