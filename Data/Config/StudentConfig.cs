using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace collegeAppBackend.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.Email).IsRequired().HasMaxLength(100);

            //created separate file for the create default data
            builder.HasData(new List<Student>()
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
        }
    }
}
