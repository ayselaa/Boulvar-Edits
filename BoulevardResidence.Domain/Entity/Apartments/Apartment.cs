using BoulevardResidence.Domain.Entity.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Apartments
{
    public class Apartment
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        //public bool IsHouseArchive { get; set; }
        public string ProjectName { get; set; }
        public string Number { get; set; }
        public int RoomsAmount { get; set; }
        public int Floor { get; set; }
        public string SectionName { get; set; }
        public string LayoutType { get; set; }
        public bool WithoutLayout { get; set; }
        public bool Studio { get; set; }
        public bool FreeLayout { get; set; }
        public bool EuroLayout { get; set; }
        //public string PropertyType { get; set; }
        public string TypePurpose { get; set; }
        //public bool HasRelatedPresetWithLayout { get; set; }
        //public string Facing { get; set; }
        //public string ExternalId { get; set; }

        public string AreaTotal { get; set; }

        public string Status { get; set; }
        public int? CustomStatusId { get; set; }
        public int? SpecialOffersIds { get; set; }

        //public List<Feature>? Features { get; set; }
        public List<FeatureApartment>? ApartmentFeatures { get; set; }
        public string? ApartmentPlan { get; set; }

        public string? GTagPlan { get; set; }
        public string? NotAviableGTagPlan { get; set; }

        public List<ApartmentFloor>? ApartmentFloors { get; set; }
    }
}
