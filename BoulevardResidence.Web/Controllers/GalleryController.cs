using BoulevardResidence.Service.DTOs.Gallerry;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly IGalleryCategoryService _galleryCategoryService;

        public GalleryController(IGalleryService galleryService, IGalleryCategoryService galleryCategoryService)
        {
            _galleryService = galleryService;
            _galleryCategoryService = galleryCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            GalleryVM model = new GalleryVM
            {
                GalleryCategories = await _galleryCategoryService.GetAllAsync(),
                GalleryItems = await _galleryService.GetAllAsync(),
                LangCode = lang.ToLower()
            };
            return View(model);
        }
    }
}
