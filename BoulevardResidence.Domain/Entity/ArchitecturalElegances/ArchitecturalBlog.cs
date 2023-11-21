using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.ArchitecturalElegances
{
    public class ArchitecturalBlog
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<ArchitecturalBlogTranslate> ArchitecturalBlogTranslates { get; set; }

    }
}
