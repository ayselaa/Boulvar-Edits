using BoulevardResidence.Domain.Entity.ArchitecturalElegances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IArchitecturalBlogService
    {
        Task<List<ArchitecturalBlog>> GetAllAsync();
    }
}
