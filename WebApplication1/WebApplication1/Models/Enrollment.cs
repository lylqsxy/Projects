namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrollment")]
    public partial class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int? ClassID { get; set; }

        public int? StudentID { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }
    }
}
