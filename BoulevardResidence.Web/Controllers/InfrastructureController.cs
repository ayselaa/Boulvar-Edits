using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Infrastructures;
using BoulevardResidence.Service.DTOs.Infrastructures;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class InfrastructureController : Controller
    {
        private readonly IInfrastructureService _infraService;
        public InfrastructureController(IInfrastructureService infraService)
        {
            _infraService = infraService;
        }
        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            InfrastructureVM model = new()
            {
                Infrastructures = await _infraService.GetAllAsync(),
                LangCode = lang.ToLower()
            };
            return View(model);
            //List<Infrastructure> infrastructures = await _infraService.GetAllAsync();
            //return View(infrastructures);
        }
    }
}
