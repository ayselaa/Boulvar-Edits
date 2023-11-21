namespace BoulevardResidence.Web.Areas.Admin.ViewModels.ArchitecturalBlog
{
    public class ArchitecturalBlogDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<ArchitecturalBlogTranslateVM> Translates { get; set; }
    }
}
