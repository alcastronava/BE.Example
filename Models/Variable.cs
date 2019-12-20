using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.Models
{
    public class Variable
    {
        [Key]
        public int VariableId { get; set; }

        public int LiteralId { get; set; }

        public Literal Literal { get; set; }

        [StringLength(256)]
        public string Name { get; set; }
    }
}
