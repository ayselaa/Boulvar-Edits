namespace BoulevardResidence.Web.Areas.Admin.ViewModels.ComfortBlog
{
    public class ComfortBlogCreateVM
    {
        public IFormFile Photo { get; set; }
        public List<ComfortBlogTranslateVM> Translates { get; set; }
    }
}
