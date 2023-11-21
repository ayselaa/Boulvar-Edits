namespace BoulevardResidence.Web.Areas.Admin.ViewModels.Apartment
{
    public class ApartmentUpdateVM
    {
        public int Id { get; set; }
        public string ApartmentPlan { get; set; }
        public string GTagPlan { get; set; }
        public string NotAviableGTagPlan { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}
