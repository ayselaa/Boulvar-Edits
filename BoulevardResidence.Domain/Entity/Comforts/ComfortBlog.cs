using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Comforts
{
    public class ComfortBlog
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<ComfortBlogTranslate> ComfortBlogTranslates { get; set; }
    }
}
