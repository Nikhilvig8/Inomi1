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
    
    public partial class tblStudentInstallmentDetail
    {
        public int Id { get; set; }
        public Nullable<int> SID { get; set; }
        public Nullable<System.DateTime> InsDate { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string InstallmentAmount { get; set; }
        public string Payment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
    }
}
