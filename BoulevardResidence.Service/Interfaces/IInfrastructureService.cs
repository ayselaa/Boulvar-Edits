using BoulevardResidence.Domain.Entity.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IInfrastructureService
    {
        Task<List<Infrastructure>> GetAllAsync();
    }
}
