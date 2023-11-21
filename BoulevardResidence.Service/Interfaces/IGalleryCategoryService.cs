using BoulevardResidence.Domain.Entity.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IGalleryCategoryService
    {
        Task<List<GalleryCategory>> GetAllAsync();
    }
}
