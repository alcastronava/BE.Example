using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.Models
{
    public class LiteralTranslation
    {
        [Key]
        public int LiteralTranslationId { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public int LiteralId { get; set; }

        public Literal Literal { get; set; }

        public string ValueZero { get; set; }

        public string ValueOne { get; set; }

        public string ValueMany { get; set; }

        public bool InReview { get; set; }
    }
}
