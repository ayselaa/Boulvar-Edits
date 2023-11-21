using BoulevardResidence.Domain.Entity.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Infrastructures
{
    public class InfrastructureVM
    {
        public List <Infrastructure> Infrastructures { get; set; }
        public string LangCode { get; set; }
    }

}
