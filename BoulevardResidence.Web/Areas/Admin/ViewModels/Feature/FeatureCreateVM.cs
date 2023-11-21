namespace BoulevardResidence.Web.Areas.Admin.ViewModels.Feature
{
    public class FeatureCreateVM
    {
        public int Id { get; set; }
        public IFormFile Logo { get; set; }
        public List<FeatureTranslateVM> Translates { get; set; }
    }
}
