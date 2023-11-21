using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Web.Areas.Admin.ViewModels.ArchitecturalBlog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchitecturalBlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ArchitecturalBlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "ArchitecturalBlog";
            ViewBag.CurrentAction = "Index";

            List<ArchitecturalBlog> blogs = await _context.ArchitecturalBlogs
                                     .Include(b => b.ArchitecturalBlogTranslates
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
        public async Task<IActionResult> Create(ArchitecturalBlogCreateVM model)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "architecturalbloggallery", filename);
                await FileHelper.SaveFileAsync(path, model.Photo);

                ArchitecturalBlog newBlog = new ArchitecturalBlog()
                {
                    Image = filename
                };

                List<ArchitecturalBlogTranslate> blogTranslates = model.Translates.Select(translate => new ArchitecturalBlogTranslate
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                    Description = translate.Description
                   
                }).ToList();

                newBlog.ArchitecturalBlogTranslates = blogTranslates;

                await _context.ArchitecturalBlogs.AddAsync(newBlog);
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
                var blog = await _context.ArchitecturalBlogs.Include(b => b.ArchitecturalBlogTranslates).FirstOrDefaultAsync(b => b.Id == id);

                if (blog is null)
                    return NotFound();

                _context.ArchitecturalBlogTranslates.RemoveRange(blog.ArchitecturalBlogTranslates);
                _context.ArchitecturalBlogs.Remove(blog);
                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "architecturalbloggallery", blog.Image);
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

            var blog = await _context.ArchitecturalBlogs
                           .Include(m => m.ArchitecturalBlogTranslates)
                           .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null) return NotFound();

            ArchitecturalBlogUpdateVM model = new ArchitecturalBlogUpdateVM()
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.ArchitecturalBlogTranslates.Select(t => new ArchitecturalBlogTranslateVM
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

        public async Task<IActionResult> Edit(ArchitecturalBlogUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var blog = await _context.ArchitecturalBlogs
                                 .Include(b => b.ArchitecturalBlogTranslates)
                                 .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (blog is null)
                    return NotFound();

                string imagePath = null;

                if (NewImage != null)
                {
                    if (!string.IsNullOrEmpty(blog.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/architecturalbloggallery/{blog.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/architecturalbloggallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }
                    blog.Image = imageName;
                }
                foreach (var translate in model.Translates)
                {
                    var existingTranslate = blog.ArchitecturalBlogTranslates
                                                  .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Name = translate.Name;
                        existingTranslate.Description = translate.Description;
                       
                    }
                    else
                    {
                        ArchitecturalBlogTranslate newTranslate = new ArchitecturalBlogTranslate()
                        {
                            LangCode = translate.LangCode,
                            Name = translate.Name,
                            Description = translate.Description
                        };
                        blog.ArchitecturalBlogTranslates.Add(newTranslate);

                    }
                }
                foreach (var existingTranslate in blog.ArchitecturalBlogTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.ArchitecturalBlogTranslates.Remove(existingTranslate);
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

            var blog = await _context.ArchitecturalBlogs
                             .Include(m => m.ArchitecturalBlogTranslates)
                             .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null)
                return NotFound();

            ArchitecturalBlogDetailVM model = new ArchitecturalBlogDetailVM
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.ArchitecturalBlogTranslates.Select(b => new ArchitecturalBlogTranslateVM
                {
                    LangCode = b.LangCode,
                    Name = b.Name,
                    Description = b.Description
                }).ToList()
            };

            return View(model);
        }

    }
}
