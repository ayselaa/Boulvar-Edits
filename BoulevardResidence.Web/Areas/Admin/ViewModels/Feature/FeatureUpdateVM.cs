namespace BoulevardResidence.Web.Areas.Admin.ViewModels.Feature
{
    public class FeatureUpdateVM
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public List<FeatureTranslateVM> Translates { get; set; }
    }
}
