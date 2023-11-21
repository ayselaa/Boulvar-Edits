using BoulevardResidence.Domain.Entity.Comforts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.EvertythingComfort
{
    public class EverytingComfortVM
    {
        public List<Comfort> Comforts  { get; set; }
        public List<ComfortBlog> ComfortBlogs { get; set; }

        public string LangCode { get; set; }
    }
}
