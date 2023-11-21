using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Controllers
{
    public class LanguageController : Controller
    {
        [HttpGet]
        [Route("/lang/{lang}")]
        public IActionResult OnPostLang(string lang)
        {
            HttpContext.Response.Cookies.Append("userLanguage", lang); //buna bax


            return Redirect($"/?culture={lang}");
        }
    }
}
