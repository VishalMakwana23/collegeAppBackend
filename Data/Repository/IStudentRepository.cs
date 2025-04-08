namespace collegeAppBackend.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        // Additional methods specific to Student can be added here
        // For example, GetStudentsByCourse, GetStudentsByYear, etc.
    }
}
