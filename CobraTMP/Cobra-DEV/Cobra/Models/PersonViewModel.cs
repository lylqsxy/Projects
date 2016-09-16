using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cobra.Models
{
    public class PersonViewModel
    {
        [Key]
        public int Id { get; set; }
        //public int OrgId { get; set; }
        //public int RoleId { get; set; }
        //public int ContactId { get; set; }
        [Required]
        [Display(Name="First name")]
        // [Display(Name = "First name", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle name")]
        // [Display(Name = "middle name", ResourceType = typeof(Resources.Resource))]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        // [Display(Name = "Last name", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }
        [Required]
        // [ ResourceType = typeof(Resources.Resource))]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        // [Display(Name = "Date of birth", ResourceType = typeof(Resources.Resource))]
        public DateTime DoB { get; set; }
    }
}