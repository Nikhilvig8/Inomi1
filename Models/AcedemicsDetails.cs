using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcedemicsDetails
    {
        public int Id { get; set; }
        public string StudentID { get; set; }
        public string Class { get; set; }
        public string Year { get; set; }
        public string Board { get; set; }
        public string School { get; set; }
        public string Transcript { get; set; }
        public string OverAllGPAMark { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDateTime { get; set; }
        public string FilePath { get; set; }
        public string IsActive { get; set; }
    }
}
