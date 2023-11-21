using BoulevardResidence.Domain.Entity.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetFeatures();
        Task<Feature> GetFullDataById(int? id);
    }
}
