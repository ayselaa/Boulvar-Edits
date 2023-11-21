using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Apartments
{
    public class ApartmentFloor
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }

        public int FloorId { get; set; }
        public Floor? Floor { get; set; }

        public string? FloorPlan { get; set; }

        public string? SVGPlan { get; set; }

        //[NotMapped]
        //public string? LangCode { get; set; }
    }
}
