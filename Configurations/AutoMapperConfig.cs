using AutoMapper;
using collegeAppBackend.Data;
using studentApp.Models;

namespace collegeAppBackend.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Create mappings here
            CreateMap<Student, StudentDto>().ReverseMap();

            // ( For different property name ) -  Maps the Name property from the source to the StudentName property in the destination.
            //CreateMap<StudentDTO, Student>().ForMember(n => n.StudentName, opt=> opt.MapFrom(x => x.Name)).ReverseMap();

            // Config For ignore property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());

            // replacing mfield value if null
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Data Found!" : n);

            // replacing field value if null
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Data Found!" : n);

            // replacing specific field value if null
            //CreateMap<StudentDTO, Student>().ReverseMap()
            //    .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No address found" : n.Address));

        }
    }
}
