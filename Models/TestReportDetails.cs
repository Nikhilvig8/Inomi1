using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TestReportDetails
    {
        public int Id { get; set; }
        public string StudentID { get; set; }
        public string TestName { get; set; }
        public string Score { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
