using BoulevardResidence.Domain.Entity.Socials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Layouts
{
    public class LayoutVM
    {
        public List<Social> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        public string LangCode { get; set; }
    }
}
