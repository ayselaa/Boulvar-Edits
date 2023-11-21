using BoulevardResidence.Domain.Entity.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Gallerry
{
    public class GalleryVM
    {
        public List <GalleryCategory> GalleryCategories { get; set; }
        public List <GalleryItem> GalleryItems { get; set; }

        public string LangCode { get; set; }
    }

}
