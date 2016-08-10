namespace ConsoleApplication6
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentBad> StudentBads { get; set; }
        public virtual DbSet<StudentGood> StudentGoods { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentBads)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentGoods)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);
        }
    }
}
