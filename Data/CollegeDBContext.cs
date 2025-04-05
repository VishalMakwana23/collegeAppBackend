using Microsoft.EntityFrameworkCore;

namespace collegeAppBackend.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region - Create default Data

            //created separate file for the create default data
            modelBuilder.Entity<Student>().HasData(new List<Student>()
                {
                    new Student
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Email = "john.doe@example.com"
                    },
                    new Student
                    {
                        Id = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        DateOfBirth = new DateTime(1999, 5, 15),
                        Email = "jane.smith@example.com"
                    },
                    new Student
                    {
                        Id = 3,
                        FirstName = "Michael",
                        LastName = "Johnson",
                        DateOfBirth = new DateTime(2001, 8, 22),
                        Email = "michael.johnson@example.com"
                    }
                });

            #endregion

            // Configure entity filters, relationships, etc.
        }
    }
}
