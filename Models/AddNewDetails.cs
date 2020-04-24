using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AddNewDetails
    {
        public List<StudentDetails> Students { get; set; }
        public List<CollegeDetails> Colleges { get; set; }
        public List<CounsellorDetails> CounsellorDetails { get; set; }
        public List<CountryDetails> Countrys { get; set; }
        public List<CourseDetails> Courses { get; set; }
        public List<FormatDetails> Formats { get; set; }
        public List<ProductDetails> Products { get; set; }
        public List<TaskDetails> Tasks { get; set; }
        public List<TestDetails> Tests { get; set; }

    }
}
