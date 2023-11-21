using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Galleries;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Web.Areas.Admin.ViewModels.GalleryItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryItemController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public GalleryItemController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "GalleryItem";
            ViewBag.CurrentAction = "Index";

            List<GalleryItem> blogs = await _context.GalleryItems
                                    .Include(c=>c.GalleryCategory.GalleryCategoryTranslates)
                                     .OrderByDescending(b => b.Id)
                                     .ToListAsync();
            List<GalleryItemVM> galleries = blogs.Select(item =>
            {
                var translation = item.GalleryCategory?.GalleryCategoryTranslates?.FirstOrDefault(t => t.LangCode == "en");
                return new GalleryItemVM
                {
                    Id = item.Id,
                    Image = item.Image,
                    GalleryCategoryName = translation?.Tittle

                };
            }).ToList();
            return View(galleries);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await _context.GalleryCategories.Select(gc => new GalleryDto
            {
                Id = gc.Id,
                Tittle = gc.GalleryCategoryTranslates.Where(t => t.LangCode == "en").FirstOrDefault().Tittle
            }).ToListAsync();
            ViewBag.Menus = new SelectList(data, "Id", "Tittle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalleryItemCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var data = await _context.GalleryCategories.Select(gc => new GalleryDto
                    {
                        Id = gc.Id,
                        Tittle = gc.GalleryCategoryTranslates.Where(t => t.LangCode == "en").FirstOrDefault().Tittle
                    }).ToListAsync();
                    ViewBag.Menus = new SelectList(data, "Id", "Tittle");
                    return View();
                }
                string filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "boulevardgallery", filename);
                await FileHelper.SaveFileAsync(path, model.Photo);

                GalleryItem newItem = new GalleryItem
                {
                    Image = filename,
                    GalleryCategoryId = model.GalleryCategoryId
                };

                await _context.GalleryItems.AddAsync(newItem);
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

                var model = await _context.GalleryItems
                                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.GalleryItems.Remove(model);
                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "boulevardgallery", model.Image);
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

            var blog = await _context.GalleryItems
                           .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null) return NotFound();

            var data = await _context.GalleryCategories.Select(fm => new GalleryDto
            {
                Id = fm.Id,
                Tittle = fm.GalleryCategoryTranslates.Where(t => t.LangCode == "en").FirstOrDefault().Tittle
            }).ToListAsync();

            var viewModel = new GalleryUpdateVM
            {
                Id = blog.Id,
                Image = blog.Image,
                GalleryCategoryId = blog.GalleryCategoryId
            };

            ViewBag.Menus = new SelectList(data, "Id", "Tittle");


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id,GalleryUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var blog = await _context.GalleryItems
                                 .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (blog is null)
                    return NotFound();
                var data = await _context.GalleryCategories.Select(fm => new GalleryDto
                {
                    Id = fm.Id,
                    Tittle = fm.GalleryCategoryTranslates.Where(t => t.LangCode == "en").FirstOrDefault().Tittle
                }).ToListAsync();
                ViewBag.Menus = new SelectList(data, "Id", "Tittle");

                blog.GalleryCategoryId = model.GalleryCategoryId;


                string imagePath = null;

                if (NewImage != null)
                {
                    if (!string.IsNullOrEmpty(blog.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/boulevardgallery/{blog.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/boulevardgallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }
                    blog.Image = imageName;

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

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest(0);

            var blog = await _context.GalleryItems
                             .FirstOrDefaultAsync(b => b.Id == id);

            if (blog is null)
                return NotFound();

            var data = await _context.GalleryCategories
                .Where(gc => gc.Id == blog.GalleryCategoryId) // Assuming there is a CategoryId in GalleryItems
                .Select(fm => new GalleryDto
                {
                    Id = fm.Id,
                    Tittle = fm.GalleryCategoryTranslates
                        .FirstOrDefault(t => t.LangCode == "en")
                        .Tittle
                })
                .ToListAsync();

            if (data is null)
                return NotFound();

            var viewModel = new GalleryItemDetailVM
            {
                Id = blog.Id,
                Image = blog.Image,
                CategoryName = data.FirstOrDefault()?.Tittle // Assuming CategoryName is a string in GalleryItemDetailVM
            };

            return View(viewModel);
        }

        //public async Task<IActionResult> Detail(int? id)
        //{
        //    if (id is null)
        //        return BadRequest(0);

        //    var blog = await _context.GalleryItems
        //                 .FirstOrDefaultAsync(b => b.Id == id);
        //    if (blog is null) return NotFound();

        //    var data = await _context.GalleryCategories.Select(fm => new GalleryDto
        //    {
        //        Id = fm.Id,
        //        Tittle = fm.GalleryCategoryTranslates.Where(t => t.LangCode == "en").FirstOrDefault().Tittle
        //    }).ToListAsync();

        //    var viewModel = new GalleryItemDetailVM
        //    {
        //        Id = blog.Id,
        //        Image = blog.Image,
        //        CategoryName  = 

        //    }


        //}

    }

    public class GalleryDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
    }
}
