using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Features
{
    public class Feature
    {
        public int Id { get; set; }
        public string Logo { get; set; }

        public List<FeatureTranslate> FeatureTranslates { get; set; }
        public List<FeatureApartment> FeatureApartments { get; set; }
    }
}
