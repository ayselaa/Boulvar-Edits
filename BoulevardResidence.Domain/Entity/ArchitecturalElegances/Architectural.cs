using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.ArchitecturalElegances
{
    public class Architectural
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }

        public List<ArchitecturalTranslate> ArchitecturalTranslates { get; set; }
    }
}
