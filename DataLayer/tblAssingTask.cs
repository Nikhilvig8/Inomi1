//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAssingTask
    {
        public int Id { get; set; }
        public string Counsellor { get; set; }
        public string Student { get; set; }
        public Nullable<System.DateTime> DeadlineDate { get; set; }
        public string TaskDescription { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string TaskType { get; set; }
        public string EventId { get; set; }
        public string UniversityId { get; set; }
        public string EssayId { get; set; }
        public string EssayCount { get; set; }
        public string UserType { get; set; }
        public string IsActive { get; set; }
        public string Status { get; set; }
    }
}
