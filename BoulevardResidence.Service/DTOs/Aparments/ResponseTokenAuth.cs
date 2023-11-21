using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Aparments
{
    public class ResponseTokenAuth
    {
        public string access_token { get; set; }
        public int remaining_time { get; set; }
        public string lang { get; set; }
    }
}
