using BoulevardResidence.Service.Interfaces;
using BoulevardResidence.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IInfrastructureService, InfrastructureService>();
            services.AddScoped<IArchitecturalService, ArchitecturalService>();
            services.AddScoped<IArchitecturalBlogService, ArchitecturalBlogService>();
            services.AddScoped<IComfortService, ComfortService>();
            services.AddScoped<IComfortBlogService, ComfortBlogService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IGalleryCategoryService, GalleryCategoryService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ISliderHeaderService, SliderHeaderService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBackgroundImageService, BackgroundImageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFeatureService, FeatureService>();

            services.AddScoped<IApartmentCreatedService, ApartmentCreatedService>();
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IFloorService, FloorService>();

            services.AddScoped<IBackService,BackService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
