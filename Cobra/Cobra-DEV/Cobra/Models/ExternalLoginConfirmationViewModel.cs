using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cobra.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        // [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        #region new fields 
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        #endregion
    }
}