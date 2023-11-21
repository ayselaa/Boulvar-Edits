using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Service.DTOs.EvertythingComfort;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class ComfortController : Controller
    {
        private readonly IComfortService _comfortService;
        private readonly IComfortBlogService _comfortBlogService;

        public ComfortController(IComfortService comfortService, IComfortBlogService comfortBlogService)
        {
            _comfortService = comfortService;
            _comfortBlogService = comfortBlogService;
        }

        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            EverytingComfortVM model = new EverytingComfortVM
            {
                Comforts = await _comfortService.GetAllAsync(),
                ComfortBlogs = await _comfortBlogService.GetAllAsync(),
                LangCode = lang.ToLower()
             };
           
            return View(model);
        }
    }
}
