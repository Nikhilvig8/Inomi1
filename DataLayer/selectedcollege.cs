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
    
    public partial class selectedcollege
    {
        public int id { get; set; }
        public int StuId { get; set; }
        public int CID { get; set; }
        public string Status { get; set; }
        public Nullable<int> counsellorid { get; set; }
        public string counsellorstatus { get; set; }
        public Nullable<int> adminid { get; set; }
        public string adminstatus { get; set; }
    }
}