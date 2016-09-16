using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cobra.Models
{
    public class ContactViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        // [Display(Name =  "First name", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }
        [Display(Name = "Middle name")]
        // [Display(Name = "Middle name", ResourceType = typeof(Resources.Resource))]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        // [Display(Name = "Last name", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }
    }
}