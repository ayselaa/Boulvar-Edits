using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Comforts
{
    public class Comfort
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }

        public List<ComfortTranslate> ComfortTranslates { get; set; }
    }
}
