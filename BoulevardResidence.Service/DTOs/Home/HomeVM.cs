using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Domain.Entity.Galleries;
using BoulevardResidence.Domain.Entity.Headers;
using BoulevardResidence.Domain.Entity.Infrastructures;
using BoulevardResidence.Domain.Entity.Sliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Home
{
    public class HomeVM
    {
        public List <Slider> Sliders { get; set; }
        public List<Infrastructure> Infrastructures { get; set; }
        public List<Architectural> Architecturals { get; set; }
        public List<ArchitecturalBlog> ArchitecturalBlogs { get; set; }
        public List<Comfort> Comforts { get; set; }
        public List<ComfortBlog> ComfortBlogs { get; set; }
        public List<ComfortBlogTranslate> ComfortBlogTranslates { get; set; }
        public List<GalleryItem> GalleryItems { get; set; } //buna bax deisilecek
        public List<GalleryCategory> GalleryCategories { get; set; }
        public List<SliderHeader> SliderHeaders { get; set; }
        public List<SectionBackgroundImage> SectionBackgroundImages { get; set; }

        public List<Apartment> Apartments { get; set; }
        public List<Apartment> ApartmentsWithoutStatus { get; set; }

        public string LangCode { get; set; }
    }
}
