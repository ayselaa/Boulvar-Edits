using BoulevardResidence.Domain.Entity.Comforts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IComfortBlogService
    {
        Task<List<ComfortBlog>> GetAllAsync();
    }
}
