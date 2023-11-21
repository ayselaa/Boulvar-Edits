using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Infrastructures
{
    public class Infrastructure
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }
        public List<InfrastructureTranslate> InfrastructureTranslates { get; set; }
    }
}
