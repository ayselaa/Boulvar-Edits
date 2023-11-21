using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.DTOs.Contact
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
