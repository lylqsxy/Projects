using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cobra.Models
{
    public class Admin
    {
        public class OrganisationViewModel
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string OrgName { get; set; }
            public string lastUpdate { get; set; }
            [Url]
            public string WebsiteUrl { get; set; }
            public bool isActive { get; set; }
        }
    }
}