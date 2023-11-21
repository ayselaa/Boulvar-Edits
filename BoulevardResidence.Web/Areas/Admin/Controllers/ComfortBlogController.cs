using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Web.Areas.Admin.ViewModels.ComfortBlog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComfortBlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ComfortBlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "ComfortBlog";
            ViewBag.CurrentAction = "Index";

            List<ComfortBlog> blogs = await _context.ComfortBlogs
                                     .Include(b => b.ComfortBlogTranslates
                                     .Where(bt => bt.LangCode == "az"))
                                     .OrderByDescending(b => b.Id)
                                     .ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComfortBlogCreateVM model)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "comfortbloggallery", filename);
                await FileHelper.SaveFileAsync(path, model.Photo);

                ComfortBlog newBlog = new ComfortBlog()
                {
                    Image = filename
                };

                List<ComfortBlogTranslate> blogTranslates = model.Translates.Select(translate => new ComfortBlogTranslate
                {
                    LangCode = translate.LangCode,
                    Description = translate.Description

                }).ToList();

                newBlog.ComfortBlogTranslates = blogTranslates;

                await _context.ComfortBlogs.AddAsync(newBlog);
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
                var blog = await _context.ComfortBlogs.Include(b => b.ComfortBlogTranslates).FirstOrDefaultAsync(b => b.Id == id);

                if (blog is null)
                    return NotFound();

                _context.ComfortBlogTranslates.RemoveRange(blog.ComfortBlogTranslates);
                _context.ComfortBlogs.Remove(blog);
                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "comfortbloggallery", blog.Image);
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

            var blog = await _context.ComfortBlogs
                           .Include(m => m.ComfortBlogTranslates)
                           .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null) return NotFound();

            ComfortBlogUpdateVM model = new ComfortBlogUpdateVM()
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.ComfortBlogTranslates.Select(t => new ComfortBlogTranslateVM
                {
                    LangCode = t.LangCode,
                    Description = t.Description
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ComfortBlogUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var blog = await _context.ComfortBlogs
                                 .Include(b => b.ComfortBlogTranslates)
                                 .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (blog is null)
                    return NotFound();

                string imagePath = null;

                if (NewImage != null)
                {
                    if (!string.IsNullOrEmpty(blog.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/comfortbloggallery/{blog.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/comfortbloggallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }
                    blog.Image = imageName;
                }
                foreach (var translate in model.Translates)
                {
                    var existingTranslate = blog.ComfortBlogTranslates
                                                  .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Description = translate.Description;

                    }
                    else
                    {
                        ComfortBlogTranslate newTranslate = new ComfortBlogTranslate()
                        {
                            LangCode = translate.LangCode,
                            Description = translate.Description
                        };
                        blog.ComfortBlogTranslates.Add(newTranslate);

                    }
                }
                foreach (var existingTranslate in blog.ComfortBlogTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.ComfortBlogTranslates.Remove(existingTranslate);
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

            var blog = await _context.ComfortBlogs
                             .Include(m => m.ComfortBlogTranslates)
                             .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null)
                return NotFound();

            ComfortBlogDetailVM model = new ComfortBlogDetailVM
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.ComfortBlogTranslates.Select(b => new ComfortBlogTranslateVM
                {
                    LangCode = b.LangCode,
                    Description = b.Description
                }).ToList()
            };

            return View(model);
        }
    }
}
