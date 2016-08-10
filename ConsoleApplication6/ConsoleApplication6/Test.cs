namespace ConsoleApplication6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Test")]
    public partial class Test
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Name { get; set; }
    }
}
