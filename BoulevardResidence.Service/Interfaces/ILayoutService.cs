using BoulevardResidence.Domain.Entity.Settings;
using BoulevardResidence.Domain.Entity.Socials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface ILayoutService
    {
        Task<List<Social>> GetAll();
        Dictionary<string,string> GetSettings();
        Task<List<Setting>> GetSettingDatas();
        Task<Setting> GetById(int? id);
    }
}
