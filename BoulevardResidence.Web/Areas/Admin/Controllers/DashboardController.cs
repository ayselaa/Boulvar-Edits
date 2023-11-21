using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "Dashboard";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
