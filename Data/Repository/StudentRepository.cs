namespace collegeAppBackend.Data.Repository
{
    // Repository class for handling Student entity operations
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        // Database context for accessing the database
        private readonly CollegeDBContext _context;

        // Constructor to initialize the context and base repository
        public StudentRepository(CollegeDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
