using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Galleries
{
    public class GalleryCategory
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }

        public List<GalleryCategoryTranslate> GalleryCategoryTranslates { get; set; }
        public List<GalleryItem> GalleryItems { get; set; }

    }
}
