using BoulevardResidence.Domain.Entity.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IBackgroundImageService
    {
        Task<List<SectionBackgroundImage>> GetAllAsync();

        Dictionary<string, string> GetSectionBackgroundImages();
        Task<List<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync();
    }
}
    