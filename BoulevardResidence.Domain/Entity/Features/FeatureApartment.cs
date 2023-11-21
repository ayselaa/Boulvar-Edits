using BoulevardResidence.Domain.Entity.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Features
{
    public class FeatureApartment
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
