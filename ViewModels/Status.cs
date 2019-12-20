using BE.Example.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Example.ViewModels
{
    public class StatusVM
    {
        public Language Language { get; set; }

        public Country Country { get; set; }

        public int Translated { get; set; }

        public int InReview { get; set; }
    }
}
