using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Sliders
{
    public class SliderHeader
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }

        public List<SliderHeaderTranslate> SliderHeaderTranslates { get; set; }
    }
}
