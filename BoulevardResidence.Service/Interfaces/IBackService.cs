using BoulevardResidence.Domain.Entity.DailyBackGround;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IBackService
    {
        void Create(DailyTaskResult dailyTaskResult);
    }
}
