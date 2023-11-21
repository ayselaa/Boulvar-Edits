namespace BoulevardResidence.Web.Areas.Admin.ViewModels.GalleryItem
{
    public class GalleryItemDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }

        public List<GalleryItemTranslateVM> Translates { get; set; }
    }
}
