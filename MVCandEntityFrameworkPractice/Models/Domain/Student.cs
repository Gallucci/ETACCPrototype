using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class Student : Person
    {
        // Properties
        [Required]
        public string NetId { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}