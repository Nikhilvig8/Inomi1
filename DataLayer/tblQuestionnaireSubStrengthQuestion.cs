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
    
    public partial class tblQuestionnaireSubStrengthQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public Nullable<int> SubStrengthId { get; set; }
        public string IsActive { get; set; }
    
        public virtual tblQuestionnaireSubStrength tblQuestionnaireSubStrength { get; set; }
    }
}
