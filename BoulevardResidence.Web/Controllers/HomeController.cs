using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Contacts;
using BoulevardResidence.Service.DTOs.Contact;
using BoulevardResidence.Service.DTOs.Home;
using BoulevardResidence.Service.Interfaces;
using BoulevardResidence.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BoulevardResidence.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IInfrastructureService _infraService;
        private readonly IArchitecturalService _architecturalService;
        private readonly IArchitecturalBlogService _architecturalBlogService;
        private readonly IComfortService _comfortService;
        private readonly IComfortBlogService _comfortBlogService;
        private readonly IGalleryService _galleryService;
        private readonly IGalleryCategoryService _galleryCategoryService;
        private readonly ISliderHeaderService _sliderHeaderService;
        private readonly IBackgroundImageService _backgroundImageService;
        private readonly IEmailService _emailService;
        private readonly IApartmentService _apartmentService;
        public HomeController(AppDbContext context, ISliderService sliderService, 
                             IInfrastructureService infraService, IArchitecturalService architecturalService,
                             IArchitecturalBlogService architecturalBlogService, IComfortService comfortService,
                             IComfortBlogService comfortBlogService, IGalleryService galleryService,
                             IGalleryCategoryService galleryCategoryService, ISliderHeaderService sliderHeaderService,
                             IBackgroundImageService backgroundImageService, IEmailService emailService
                             , IApartmentService apartmentService)
        {
            _context = context;
            _sliderService = sliderService;
            _infraService = infraService;
            _architecturalService = architecturalService;
            _architecturalBlogService = architecturalBlogService;
            _comfortService = comfortService;
            _comfortBlogService = comfortBlogService;
            _galleryService = galleryService;
            _galleryCategoryService = galleryCategoryService;
            _sliderHeaderService = sliderHeaderService;
            _backgroundImageService = backgroundImageService;
            _emailService = emailService;
            _apartmentService = apartmentService;
        }
        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            HomeVM model = new HomeVM()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Infrastructures = await _infraService.GetAllAsync(),
                Architecturals = await _architecturalService.GetAllAsync(),
                ArchitecturalBlogs = await _architecturalBlogService.GetAllAsync(),
                Comforts = await _comfortService.GetAllAsync(),
                ComfortBlogs = await _comfortBlogService.GetAllAsync(),
                GalleryItems = await _galleryService.GetAllAsync(), //bunu duzelt
                GalleryCategories = await _galleryCategoryService.GetAllAsync(),
                SliderHeaders = await _sliderHeaderService.GetAllAsync(),
                SectionBackgroundImages = await _backgroundImageService.GetAllAsync(),
                Apartments = await _apartmentService.GetAllAsync(),
                ApartmentsWithoutStatus = await _apartmentService.GetAllAsyncWithoutStatus(),
                LangCode = lang.ToLower()

            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //[HttpPost]
        //public IActionResult WaitCall(string name,string phoneNumber)
        //{
        //    string to = "ibrahimliyev7@gmail.com"; // E-posta alıcısını belirleyin
        //    string subject = "Wait For Call"; // E-posta konusunu belirleyin
        //    string html = $"Name: {name}, Phone Number: {phoneNumber}"; // E-posta içeriğini oluşturun

        //    // E-postayı göndermek için EmailService'i kullanın
        //    _emailService.Send(to, subject, html);

        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //public IActionResult WaitCall(string name, string phoneNumber)
        //{
        //    try
        //    {
        //        string apiUrl = "https://realestate.elevenlab.ru/other_site_form/ajax.php";


        //        string queryString = HttpContext.Request.QueryString.Value;
        //        string subject = $"Wait For Call - QueryString: {queryString}";

        //        using (HttpClient client = new HttpClient())
        //        {

        //            var formData = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("Name", name),
        //        new KeyValuePair<string, string>("PhoneNumber", phoneNumber)
        //    };


        //            var content = new FormUrlEncodedContent(formData);


        //            HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;


        //            if (response.IsSuccessStatusCode)
        //            {

        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                // Handle failure if needed
        //                // You might want to log or handle errors appropriately
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions if any
        //        // You might want to log or handle errors appropriately
        //        return RedirectToAction("Error", "Home");
        //    }
        //}

        [HttpPost]
        public IActionResult WaitCall(string name, string phoneNumber,string subject)
        {
            try
            {
                string apiUrl = "https://realestate.elevenlab.ru/other_site_form/ajax.php";


                using (HttpClient client = new HttpClient())
                {
                    var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Name", name),
                new KeyValuePair<string, string>("PhoneNumber", phoneNumber),
                new KeyValuePair<string, string>("Subject", subject)
            };

                    var content = new FormUrlEncodedContent(formData);

                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Handle failure if needed
                        // You might want to log or handle errors appropriately
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                // You might want to log or handle errors appropriately
                return RedirectToAction("Error", "Home");
            }
        }


    }
}
