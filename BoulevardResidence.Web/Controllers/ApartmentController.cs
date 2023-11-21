using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Domain.Entity.Features;
using BoulevardResidence.Service.DTOs.Aparments;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BoulevardResidence.Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IFeatureService _featureService;
        public ApartmentController(IApartmentService apartmentService, IEmailService emailService, AppDbContext context, IFeatureService featureService)
        {
            _apartmentService = apartmentService;
            _emailService = emailService;
            _context = context;
            _featureService = featureService;
        }


        [HttpGet]
        public async Task<IActionResult> GetApartmentData(string buildingsArr, string floorArr, string roomArr, string featureArr, int page, string rangeInput1Value, string rangeInput2Value)
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            ApartpentListPagination apartpentListPagination = new ApartpentListPagination();


            List<Apartment> apartments = await _context.Apartments
                                                .Where(m => m.Status == "AVAILABLE")
                  .Select(m => new Apartment
                  {
                      Id = m.Id,
                      ApartmentFeatures = m.ApartmentFeatures
                           .Select(af => new FeatureApartment
                           {
                               Id = af.Id,
                               FeatureId = af.FeatureId,
                               Feature = af.Feature != null ? new Feature
                               {
                                   Id = af.Feature.Id,
                                   Logo = af.Feature.Logo,
                                   // Diğer Feature özellikleri

                                   // FeatureTranslates isimlerini set etme
                                   FeatureTranslates = af.Feature.FeatureTranslates
                           .Where(ft => ft.LangCode.ToLower() == lang.ToLower()) // Filter by language
                          .Select(ft => new FeatureTranslate
                          {
                              // FeatureTranslate ismi set etme
                              LangCode = lang.ToLower(),
                              Name = ft.Name
                          })
                          .ToList()
                               } : null
                           })
              .ToList(),
                      ApartmentPlan = m.ApartmentPlan,
                      SectionName = m.SectionName,
                      Floor = m.Floor,
                      Status = m.Status,
                      RoomsAmount = m.RoomsAmount,
                      AreaTotal = m.AreaTotal
                  })
      .ToListAsync();




            if (!string.IsNullOrWhiteSpace(buildingsArr))
            {
                string[] stringArr = buildingsArr.Split(",");

                apartments = apartments.Where(s => s.Status == "AVAILABLE").Where(a => stringArr.Contains(a.SectionName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(floorArr))
            {
                string[] stringArr = floorArr.Split(",");

                apartments = apartments.Where(s => s.Status == "AVAILABLE").Where(a => stringArr.Contains(a.Floor.ToString())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(roomArr))
            {
                string[] stringArr = roomArr.Split(",");

                apartments = apartments.Where(s => s.Status == "AVAILABLE").Where(a => stringArr.Contains(a.RoomsAmount.ToString())).ToList();
            }
            if ((rangeInput1Value is not "undefined") && (rangeInput2Value is not "undefined"))
            {
                decimal rangeInput1, rangeInput2;


                if (decimal.TryParse(rangeInput1Value, NumberStyles.Any, CultureInfo.InvariantCulture, out rangeInput1) &&
                    decimal.TryParse(rangeInput2Value, NumberStyles.Any, CultureInfo.InvariantCulture, out rangeInput2))
                {

                    apartments = apartments
                        .Where(x => decimal.TryParse(x.AreaTotal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal area) && area >= rangeInput1 && area <= rangeInput2)
                        .ToList();
                }

            }


            //if ((rangeInput1Value is not "undefined") && (rangeInput2Value is not "undefined"))
            //{
            //    decimal rangeInput1, rangeInput2;

            //    if (decimal.TryParse(rangeInput1Value, out rangeInput1) && decimal.TryParse(rangeInput2Value, out rangeInput2))
            //    {
            //        apartments = apartments.Where(x => decimal.TryParse(x.AreaTotal, out decimal area) && area >= rangeInput1 && area <= rangeInput2).ToList();
            //    }
            //    else
            //    {
            //        // Handle the case where parsing fails
            //        // Log an error or provide a default value for the ranges
            //    }

            //}


            if (!string.IsNullOrWhiteSpace(featureArr))
            {
                string[] stringArr = featureArr.Split(",");

                apartments = apartments
                    .Where(a => a.Status == "AVAILABLE" &&
                                a.ApartmentFeatures.Any(af => af.FeatureId != null &&
                                                              stringArr.Contains(af.FeatureId.ToString())))
                    .ToList();
            }


            apartpentListPagination.Apartments = apartments.Skip((page - 1) * 10).Take(10).ToList();
            apartpentListPagination.Total = apartments.Count;


            return Ok(apartpentListPagination);
        }

        public async Task<IActionResult> Index(int page = 1, int take = 14)
        {
            var buildings = _apartmentService.GetBuildings();


            var sortedBuildings = buildings.OrderBy(b => b).ToList();


            ViewBag.Buildings = sortedBuildings;



            var floors = _apartmentService.GetFloors();
            var sortedFloors = floors.OrderBy(b => b).ToList();
            ViewBag.Floors = sortedFloors;

            var rooms = _apartmentService.GetRooms();
            var sortedRooms = rooms.OrderBy(r => r).ToList();
            ViewBag.Rooms = sortedRooms;

            var areas = _apartmentService.GetFloorAreas();
            var sortedAreas = areas.OrderBy(r => r).ToList();
            var value = _context.Apartments.Where(x => x.Status == "AVAILABLE").Select(x => x.AreaTotal).ToList();

            ViewBag.MinFloorArea = value.Min();


            ViewBag.MaxFloorArea = sortedAreas.Max(); // Maksimum değer

            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            List<Apartment> paginateApartments = await _apartmentService.GetPaginatedDatasAsync(page, take, lang);
            int pageCount = await GetPageCountAsync(take);

            Paginate<Apartment> paginateDatas = new(paginateApartments, page, pageCount, lang.ToLower());
            paginateDatas.LangCode = lang;
            GetAllApartmentDto model = new GetAllApartmentDto
            {
                ApartmentCount = await _apartmentService.GetCountAsync(),
                PaginateApartment = paginateDatas,
                LangCode = lang.ToLower(),
                //Features = _context.Features.Include(f => f.FeatureTranslates).ToList()
                Features = await _featureService.GetFeatures()
            };
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> ApartmentDetail(int? id)
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            try
            {
                if (id is null) return BadRequest();
                var dbApartment = await _apartmentService.GetFullDataByIdAsync((int)id);
                if (dbApartment is null) return NotFound();

                ApartmentDetailDto model = new()
                {
                    Id = dbApartment.Id,
                    Building = dbApartment.SectionName,
                    Floor = dbApartment.Floor,
                    Room = dbApartment.RoomsAmount,
                    SectionName = dbApartment.SectionName,
                    FloorArea = dbApartment.AreaTotal,
                    Number = dbApartment.Number,
                    ApartmentPlan = dbApartment.ApartmentPlan,
                    LangCode = lang.ToLower()


                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var apartmentCount = await _apartmentService.GetCountAsync();

            return (int)Math.Ceiling((decimal)apartmentCount / take);
        }



        //private async Task<int> GetPageCountAsync(int take, int? buildingId, int? floorId, int? roomId)
        //{
        //    int prodCount = 0;
        //    if (buildingId is not null)
        //    {
        //        prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
        //    }
        //    if (floorId is not null)
        //    {
        //        prodCount = await _productService.GetProductsCountByColorAsync(colorId);
        //    }
        //    if (roomId is not null)
        //    {
        //        prodCount = await _productService.GetProductsCountByTagAsync(tagId);
        //    }

        //    if (buildingId == null && floorId == null && roomId == null)
        //    {
        //        prodCount = await _apartmentService.GetCountAsync();
        //    }

        //    return (int)Math.Ceiling((decimal)prodCount / take);
        //}


        [HttpPost]
        public async Task<IActionResult> SendRequestForApartment(string name, string email, string phoneNumber, string apartmentInfo, string subject)
        {
            try
            {
                string apiUrl = "https://realestate.elevenlab.ru/other_site_form/ajax.php";

                // Construct the form data
                var formData = new List<KeyValuePair<string, string>>
         {

            new KeyValuePair<string, string>("Name", name),
            new KeyValuePair<string, string>("Email", email),
            new KeyValuePair<string, string>("PhoneNumber", phoneNumber),
            new KeyValuePair<string, string>("ApartmentInfo", apartmentInfo),
            new KeyValuePair<string, string>("Subject", subject),

        };

                // Create the HTTP request content
                var content = new FormUrlEncodedContent(formData);

                using (HttpClient client = new HttpClient())
                {
                    // Post the data to the external URL
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle success if needed
                        return RedirectToAction("Index", "Apartment");
                    }
                    else
                    {
                        // Handle failure if needed
                        // You might want to log or handle errors appropriately
                        return RedirectToAction("Error", "Apartment");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                // You might want to log or handle errors appropriately
                return RedirectToAction("Error", "Apartment");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> SendRequestForApartment(string name, string email, string phoneNumber, string apartmentInfo, string subject)
        //{

        //    string to = email; // E-posta alıcısını belirleyin
        //    string subject = "Wait For Call For Apartment"; // E-posta konusunu belirleyin
        //    string html = $"Name: {name}, Phone Number: {phoneNumber}, ApartmentInfo:{apartmentInfo}"; // E-posta içeriğini oluşturun

        //    // E-postayı göndermek için EmailService'i kullanın
        //    _emailService.Send(to, subject, html);

        //    return RedirectToAction("Index", "Apartment");

        //}
        //[HttpPost]
        //public async Task<IActionResult> SendRequestForApartmentPdf(string name, string email, string phoneNumber, string apartmentInfo)
        //{
        //    string to = email;
        //    string subject = "Send PDF For Apartment";
        //    string html = $"Name: {name}, Phone Number: {phoneNumber}, ApartmentInfo:{apartmentInfo}";


        //    _emailService.Send(to, subject, html);

        //    return RedirectToAction("Index", "Apartment");
        //}

        [HttpPost]
        public async Task<IActionResult> SendRequestForApartmentPdf(string name, string email, string phoneNumber, string apartmentInfo, string subject)
        {
            try
            {
                string apiUrl = "https://realestate.elevenlab.ru/other_site_form/ajax.php";

                // Construct the form data
                var formData = new List<KeyValuePair<string, string>>
         {

            new KeyValuePair<string, string>("Name", name),
            new KeyValuePair<string, string>("Email", email),
            new KeyValuePair<string, string>("PhoneNumber", phoneNumber),
            new KeyValuePair<string, string>("ApartmentInfo", apartmentInfo),
            new KeyValuePair<string, string>("Subject", subject),

        };

                // Create the HTTP request content
                var content = new FormUrlEncodedContent(formData);

                using (HttpClient client = new HttpClient())
                {
                    // Post the data to the external URL
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle success if needed
                        return RedirectToAction("Index", "Apartment");
                    }
                    else
                    {
                        // Handle failure if needed
                        // You might want to log or handle errors appropriately
                        return RedirectToAction("Error", "Apartment");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                // You might want to log or handle errors appropriately
                return RedirectToAction("Error", "Apartment");
            }
        }

    }
    public class ApartpentListPagination
    {
        public int Total { get; set; }
        public List<Apartment> Apartments { get; set; }
    }
}
