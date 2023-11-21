using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.ArchitecturalElegance
{
    public class ArchitecturalEleganceVM
    {
        public List<Architectural> Architecturals { get; set; }
        public List<ArchitecturalBlog> ArchitecturalBlogs { get; set; }

        public string LangCode { get; set; }
    }
}
