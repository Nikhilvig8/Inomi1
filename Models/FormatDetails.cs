﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FormatDetails
    {
        public int Id { get; set; }
        public string FormatCode { get; set; }
        public string Format { get; set; }
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
