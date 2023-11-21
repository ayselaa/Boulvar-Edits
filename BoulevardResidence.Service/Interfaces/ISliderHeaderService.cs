using BoulevardResidence.Domain.Entity.Sliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface ISliderHeaderService
    {
        Task<List<SliderHeader>> GetAllAsync();
    }
}
