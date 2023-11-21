using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Sliders;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Web.Areas.Admin.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public SliderController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Slider";
            ViewBag.CurrentAction = "Index";

            List<Slider> sliders = await _context.Sliders.OrderByDescending(s => s.Id).ToListAsync();

            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "slidergallery", fileName);
                await FileHelper.SaveFileAsync(path, model.Photo);

                Slider newSlider = new Slider
                {
                    Image = fileName
                };

                await _context.Sliders.AddAsync(newSlider);
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

                var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
                if (slider is null)
                    return NotFound();

                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "slidergallery", slider.Image);
                FileHelper.DeleteFile(path);

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
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null)
                return NotFound();

            SliderUpdateVM model = new SliderUpdateVM()
            {
                Id = slider.Id,
                Image = slider.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Edit(SliderUpdateVM model,IFormFile NewImage)
        {
            try
            {
                var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == model.Id);
                if (slider is null)
                    return NotFound();

                string imagePath = null;
                
                if(NewImage != null)
                {
                    if (!string.IsNullOrEmpty(slider.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/slidergallery/{slider.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/slidergallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }

                    slider.Image = imageName;
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
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if(slider is null) return NotFound();

            SliderDetailVM model = new SliderDetailVM
            {
                Id = slider.Id,
                Image = slider.Image
            };
            
            return View(model);
        }
    }
}
