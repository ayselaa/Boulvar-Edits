using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Galleries
{
    public class GalleryItem
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public GalleryCategory GalleryCategory { get; set; }
        public int GalleryCategoryId { get; set; }

    }
}
