using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Name { get; set; }
    }
}
