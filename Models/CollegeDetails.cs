using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CollegeDetails
    {
        public int Id { get; set; }
        public string CollegeCode { get; set; }
        public string NameOfCollege { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Courses { get; set; }
        public string ApplicationMode { get; set; }
        public string EssayName { get; set; }
        public string Topic { get; set; }
        public string WordCount { get; set; }
        public string deadline { get; set; }



        public string TestingRequirement { get; set; }
        public string Link { get; set; }
        public string ApplicationDeadline { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }

        public string InternationalRank { get; set; }
        public string IndianRank { get; set; }
        public string USNewsRanking { get; set; }
        public string QSWorldRanking { get; set; }
        public string QSBySubject { get; set; }

        public string GradeProfile { get; set; }

        public string CampusName { get; set; }
        public string EarlyDate { get; set; }
        public string LateDate { get; set; }
        public string AboutCollege { get; set; }


    }
}
