using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Interfaces
{
    public interface IApartmentCreatedService
    {
        Task UpdatedApartmentsWithBackgroundService();
        Task CreateApartmentWithRequest(int Id);
    }
}
