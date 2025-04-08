using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using collegeAppBackend.Data;
using collegeAppBackend.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using studentApp.Models;


namespace collegeAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>List of students as DTOs.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            _logger.LogInformation("Fetching all students.");
            var students = await _studentRepository.GetAllAsync();
            var studentDTOs = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDTOs);
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <returns>Student DTO.</returns>
        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid student ID provided.");
                return BadRequest();
            }

            var student = await _studentRepository.GetAsync(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            var studentDTO = _mapper.Map<StudentDto>(student);
            return Ok(studentDTO);
        }

        /// <summary>
        /// Retrieves a student by name.
        /// </summary>
        /// <param name="name">Student name.</param>
        /// <returns>Student DTO.</returns>
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudentByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = await _studentRepository.GetAsync(s => s.FirstName.ToLower().Contains(name.ToLower()));
            if (student == null)
                return NotFound($"Student with name {name} not found.");

            var studentDTO = _mapper.Map<StudentDto>(student);
            return Ok(studentDTO);
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="dto">Student DTO.</param>
        /// <returns>Created student DTO.</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDto>> CreateStudentAsync([FromBody] StudentDto dto)
        {
            if (dto == null)
                return BadRequest();

            var student = _mapper.Map<Student>(dto);
            var createdStudent = await _studentRepository.CreateAsync(student);
            dto.Id = createdStudent.Id;

            return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Updates an existing student.
        /// </summary>
        /// <param name="dto">Student DTO.</param>
        /// <returns>No content.</returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDto dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest();

            var existingStudent = await _studentRepository.GetAsync(s => s.Id == dto.Id, true);
            if (existingStudent == null)
                return NotFound();

            var updatedStudent = _mapper.Map<Student>(dto);
            await _studentRepository.UpdateAsync(updatedStudent);

            return NoContent();
        }

        /// <summary>
        /// Partially updates an existing student.
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <param name="patchDocument">Patch document for student DTO.</param>
        /// <returns>No content.</returns>
        [HttpPatch("{id:int}/updatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDto> patchDocument)
        {

            // Example JSON Patch payload for updating a specific field:
            // [
            //     {
            //         "path": "/address",
            //         "op": "replace",
            //         "value": "Dubai"
            //     }
            // ]

            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = await _studentRepository.GetAsync(s => s.Id == id, true);
            if (existingStudent == null)
                return NotFound();

            var studentDTO = _mapper.Map<StudentDto>(existingStudent);
            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedStudent = _mapper.Map<Student>(studentDTO);
            await _studentRepository.UpdateAsync(updatedStudent);

            return NoContent();
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <returns>True if deletion was successful.</returns>
        [HttpDelete("Delete/{id:int}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = await _studentRepository.GetAsync(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            await _studentRepository.DeleteAsync(student);
            return Ok(true);
        }
    }
}
