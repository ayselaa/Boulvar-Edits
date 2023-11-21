using BoulevardResidence.Service.DTOs.Layouts;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IContactService _contactService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FooterViewComponent(ILayoutService layoutService, IContactService contactService, IHttpContextAccessor httpContextAccessor)
        {
           
            _layoutService = layoutService;
            _contactService = contactService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lang = _httpContextAccessor.HttpContext.Request.Cookies["SelectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {

                lang = "az";
            }
            return await Task.FromResult(View(new LayoutVM
            {
                Socials = await _layoutService.GetAll(),
                Settings = _layoutService.GetSettings(),
                LangCode = lang.ToLower(),
            }));
        }
    }
}
