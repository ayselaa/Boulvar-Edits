using BoulevardResidence.Domain.Entity.Apartments;
using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using BoulevardResidence.Domain.Entity.Comforts;
using BoulevardResidence.Domain.Entity.Contacts;
using BoulevardResidence.Domain.Entity.DailyBackGround;
using BoulevardResidence.Domain.Entity.Features;
using BoulevardResidence.Domain.Entity.Galleries;
using BoulevardResidence.Domain.Entity.Headers;
using BoulevardResidence.Domain.Entity.Infrastructures;
using BoulevardResidence.Domain.Entity.Locations;
using BoulevardResidence.Domain.Entity.Settings;
using BoulevardResidence.Domain.Entity.Sliders;
using BoulevardResidence.Domain.Entity.Socials;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        //Infrastructure
        public DbSet<Infrastructure> Infrastructures { get; set; }
        public DbSet<InfrastructureTranslate> InfrastructureTranslates { get; set; }

        //Architectural Elegance
        public DbSet<Architectural> Architecturals { get; set; }
        public DbSet<ArchitecturalTranslate> ArchitecturalTranslates { get; set; }
        public DbSet<ArchitecturalBlog> ArchitecturalBlogs { get; set; }
        public DbSet<ArchitecturalBlogTranslate> ArchitecturalBlogTranslates { get; set; }

        //Comfort
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<ComfortTranslate> ComfortTranslates { get; set; }
        public DbSet<ComfortBlog> ComfortBlogs { get; set; }
        public DbSet<ComfortBlogTranslate> ComfortBlogTranslates { get; set; }

        //Gallery
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<GalleryCategory> GalleryCategories { get; set; }
        public DbSet<GalleryCategoryTranslate> GalleryCategoryTranslates { get; set; }

        //Setting
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }

        //Slider Header
        public DbSet<SliderHeader> SliderHeaders { get; set; }
        public DbSet<SliderHeaderTranslate> SliderHeaderTranslates { get; set; }

        //Section Headers
        public DbSet<SectionBackgroundImage> SectionBackgroundImages { get; set; }

        //Locations
        public DbSet<Location> Locations { get; set; }

        //Apartment
        public DbSet<Apartment> Apartments { get; set; }

        //Features
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureTranslate> FeatureTranslates { get; set; }
        public DbSet<FeatureApartment> FeatureApartments { get; set; }

        //Floors
        public DbSet<Floor> Floors { get; set; }
        public DbSet<ApartmentFloor> ApartmentFloors { get; set; }

        public DbSet<DailyTaskResult> DailyTaskResults { get; set; }

    }
}
