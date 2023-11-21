using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Domain.Entity.Features;
using BoulevardResidence.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Aparments
{
    public class GetAllApartmentDto
    {
        //public List<Apartment> Apartments { get; set; }
        public int ApartmentCount { get; set; }
        public Paginate<Apartment> PaginateApartment { get; set; }
        public List<Feature> Features { get; set; }
        public string LangCode { get; set; }
    }
}
