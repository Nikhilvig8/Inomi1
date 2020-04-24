using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AssignTaskDetails
    {
        public int Id { get; set; }
        public string Counsellor { get; set; }
        public string Student { get; set; }
        public string DeadlineDate { get; set; }
        public string TaskDescription { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string TaskType { get; set; }

        public string UniversityId { get; set; }
        public string EssayId { get; set; }
        public string EssayCount { get; set; }
        public string UserType { get; set; }

    }
}
