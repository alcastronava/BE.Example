using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; } // GUID

        [StringLength(256)]
        public string Name { get; set; }
    }
}
