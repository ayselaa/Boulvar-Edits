using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Apartments
{
    public class Floor
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }

        public List<ApartmentFloor>? ApartmentFloors { get; set; }

    }
}
