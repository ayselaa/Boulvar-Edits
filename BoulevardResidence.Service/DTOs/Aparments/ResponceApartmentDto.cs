using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Aparments
{
    public class ResponceApartmentDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("house_id")]
        public int HouseId { get; set; }

        [JsonProperty("houseName")]
        public string HouseName { get; set; }

        [JsonProperty("isHouseArchive")]
        public bool IsHouseArchive { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("rooms_amount")]
        public int RoomsAmount { get; set; }

        [JsonProperty("floor")]
        public int Floor { get; set; }

        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        [JsonProperty("layout_type")]
        public string LayoutType { get; set; }

        [JsonProperty("without_layout")]
        public bool WithoutLayout { get; set; }

        [JsonProperty("studio")]
        public bool Studio { get; set; }

        [JsonProperty("free_layout")]
        public bool FreeLayout { get; set; }

        [JsonProperty("euro_layout")]
        public bool EuroLayout { get; set; }

        [JsonProperty("propertyType")]
        public string PropertyType { get; set; }

        [JsonProperty("typePurpose")]
        public string TypePurpose { get; set; }

        [JsonProperty("has_related_preset_with_layout")]
        public bool HasRelatedPresetWithLayout { get; set; }

        [JsonProperty("facing")]
        public string Facing { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("area")]
        public AreaDto Area { get; set; }

        [JsonProperty("price")]
        public PriceDto Price { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customStatusId")]
        public int CustomStatusId { get; set; }

        [JsonProperty("specialOffersIds")]
        public int? SpecialOffersIds { get; set; }
    }


    public class AreaDto
    {
        [JsonProperty("area_total")]
        public string AreaTotal { get; set; }

        [JsonProperty("area_estimated")]
        public string AreaEstimated { get; set; }

        [JsonProperty("area_living")]
        public string AreaLiving { get; set; }

        [JsonProperty("area_kitchen")]
        public string AreaKitchen { get; set; }

        [JsonProperty("area_balcony")]
        public string AreaBalcony { get; set; }

        [JsonProperty("area_without_balcony")]
        public string AreaWithoutBalcony { get; set; }
    }

    public class PriceDto
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("pricePerMeter")]
        public int PricePerMeter { get; set; }
    }


    public class MyResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<ResponceApartmentDto> Data { get; set; }
    }


}
