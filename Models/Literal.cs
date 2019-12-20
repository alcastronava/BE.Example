using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.Models
{
    public class Literal
    {
        [Key]
        public int LiteralId { get; set; }

        public int ModuleId { get; set; }

        public Module Module { get; set; }

        [StringLength(256)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public bool Plural { get; set; }

        [Url]
        public string ExampleURL { get; set; }
    }
}
