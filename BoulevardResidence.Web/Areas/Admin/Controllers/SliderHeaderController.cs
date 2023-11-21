using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Sliders;
using BoulevardResidence.Service.Interfaces;
using BoulevardResidence.Web.Areas.Admin.ViewModels.SliderHeader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderHeaderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderHeaderService _sliderHeader;
        public SliderHeaderController(AppDbContext context, ISliderHeaderService sliderHeader)
        {
            _context = context;
            _sliderHeader = sliderHeader;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "SliderHeader";
            ViewBag.CurrentAction = "Index";

            List<SliderHeader> headers = await _sliderHeader.GetAllAsync();
            return View(headers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderHeaderCreateVM model)
        {
            try
            {
                List<SliderHeaderTranslate> sliderHeaderTranslates = model.Translates.Select(t => new SliderHeaderTranslate
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description = t.Description
                }).ToList();

                SliderHeader header = new();
                header.SliderHeaderTranslates = sliderHeaderTranslates;

                await _context.SliderHeaders.AddAsync(header);
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

                var model = await _context.SliderHeaders.Where(m => !m.SoftDelete)
                                                           .Include(m => m.SliderHeaderTranslates)
                                                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.SliderHeaderTranslates.RemoveRange(model.SliderHeaderTranslates);
                _context.SliderHeaders.Remove(model);
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
            var menu = await _context.SliderHeaders
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.SliderHeaderTranslates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            SliderHeaderUpdateVM model = new()
            {
                Id = menu.Id,
                Translates = menu.SliderHeaderTranslates.Select(t => new SliderHeaderTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description= t.Description
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderHeaderUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.SliderHeaders
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.SliderHeaderTranslates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.SliderHeaderTranslates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Tittle = item.Tittle;
                        translate.Description = item.Description;
                    }
                    else
                    {
                        menu.SliderHeaderTranslates.Add(new SliderHeaderTranslate
                        {
                            LangCode = item.LangCode,
                            Tittle = item.Tittle,
                            Description = item.Description

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
            var menu = await _context.SliderHeaders
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.SliderHeaderTranslates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();

            SliderHeaderDetailVM model = new SliderHeaderDetailVM
            {
                Id = menu.Id,
                Translates = menu.SliderHeaderTranslates.Select(t => new SliderHeaderTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description = t.Description
                }).ToList()
            };
            return View(model);
        }
    }
}
