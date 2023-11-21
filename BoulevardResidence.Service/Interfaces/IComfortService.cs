using BoulevardResidence.Domain.Entity.Comforts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IComfortService
    {
        //Task<List<Comfort>> GetAllAsync(string langcode);
        Task<List<Comfort>> GetAllAsync();
    }
}
