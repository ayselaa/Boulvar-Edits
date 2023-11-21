using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Aparments
{
    public class ApartmentDetailDto
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public int Room { get; set; }
        public int Floor { get; set; }
        public string FloorArea { get; set; }
        public string Number { get; set; }
        public string ApartmentPlan { get; set; }
        public string SectionName { get; set; }

        public string LangCode { get; set; }
    }
}
