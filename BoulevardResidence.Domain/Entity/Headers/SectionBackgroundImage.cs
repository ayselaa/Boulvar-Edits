﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Domain.Entity.Headers
{
    public class SectionBackgroundImage
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}