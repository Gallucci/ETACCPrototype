using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class Person
    {
        // Properties
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="Last name cannot be longer than 50 characters.")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Preferred First Name")]
        public string PreferredFirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                    return string.Empty;

                if (string.IsNullOrEmpty(PreferredFirstName))
                    return string.Format("{0}, {1}", LastName, FirstName);
                else
                    return string.Format("{0}, {1}", LastName, PreferredFirstName);
            }
        }
    }
}