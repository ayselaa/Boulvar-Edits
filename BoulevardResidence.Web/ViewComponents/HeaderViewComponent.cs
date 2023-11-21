using BoulevardResidence.Service.DTOs.Layouts;
using BoulevardResidence.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoulevardResidence.Web.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HeaderViewComponent(ILayoutService layoutService, IHttpContextAccessor httpContextAccessor)
        {
            _layoutService = layoutService;
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
                Settings =  _layoutService.GetSettings(),
                LangCode = lang.ToLower()
            }));;; 
        }
    }
}
