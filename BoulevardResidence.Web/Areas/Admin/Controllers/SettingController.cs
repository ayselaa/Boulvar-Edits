using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.Settings;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Service.Interfaces;
using BoulevardResidence.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context,ILayoutService layoutService, IWebHostEnvironment env)
        {
            _context = context;
            _layoutService = layoutService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Setting";
            ViewBag.CurrentAction = "Index";

            List<Setting> settings = await _layoutService.GetSettingDatas();
            return View(settings);
        }

        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            Setting dbSetting = await _layoutService.GetById(id);
            Setting model = new()
            {
                Value = dbSetting.Value,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Setting updatedSetting)
        {
            try
            {
                if (id == null) return BadRequest();
                Setting dbSetting = await _layoutService.GetById(id);
                if (dbSetting is null) return NotFound();

                if (dbSetting.Value.Contains(".png") || dbSetting.Value.Contains(".jpg") || dbSetting.Value.Contains(".jpeg"))
                {
                    if (updatedSetting.LogoPhoto is not null)
                    {
                        string oldPath = FileHelper.GetFilePath(_env.WebRootPath, "logogallery", dbSetting.Value);
                        FileHelper.DeleteFile(oldPath);
                        dbSetting.Value = updatedSetting.LogoPhoto.CreateFile(_env, "logogallery");
                    }
                    else
                    {
                        Setting newSetting = new()
                        {
                            Value = dbSetting.Value
                        };
                    }
                }
                else
                {
                    if (dbSetting.Value.Trim().ToLower() == updatedSetting.Value.Trim().ToLower())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    dbSetting.Value = updatedSetting.Value;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //@ViewBag.error = ex.Message;
                //return View();
                throw;
            }
        }

    }
}
