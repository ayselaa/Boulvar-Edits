namespace BoulevardResidence.Web.Areas.Admin.ViewModels.ArchitecturalBlog
{
    public class ArchitecturalBlogCreateVM
    {
        public IFormFile Photo { get; set; }
        public List<ArchitecturalBlogTranslateVM> Translates { get; set; }
    }
}
