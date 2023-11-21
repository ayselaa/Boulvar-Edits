using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Infrastructures
{
    public class InfrastructureTranslate
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
