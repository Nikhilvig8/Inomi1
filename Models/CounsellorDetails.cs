﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CounsellorDetails
    {
        public int Id { get; set; }
        public string CounsellorCode { get; set; }
        public string Name { get; set; }
        public string Education { get; set; }
        public string Link { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Experience { get; set; }
        public string Picture { get; set; }
        public string Achievements { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }

    }
}
