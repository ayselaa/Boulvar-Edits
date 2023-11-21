using BoulevardResidence.Service.DTOs.ArchitecturalElegance;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class ArchitecturalEleganceController : Controller
    {
        private readonly IArchitecturalService _architecturalService;
        private readonly IArchitecturalBlogService _architecturalBlogService;
        public ArchitecturalEleganceController(IArchitecturalService architecturalService, IArchitecturalBlogService architecturalBlogService)
        {
            _architecturalService = architecturalService;
            _architecturalBlogService = architecturalBlogService;
        }

        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            ArchitecturalEleganceVM model = new ArchitecturalEleganceVM
            {
                Architecturals = await _architecturalService.GetAllAsync(),
                ArchitecturalBlogs = await _architecturalBlogService.GetAllAsync(),
                LangCode = lang.ToLower()
            };
            return View(model);
        }
    }
}
